use proc_macro::TokenStream;
use quote::{quote, format_ident};
use syn::{parse_macro_input, DeriveInput, Data, Fields};

#[proc_macro_derive(StructOps)]
pub fn derive_math_struct_ops(input: TokenStream) -> TokenStream {
    let input = parse_macro_input!(input as DeriveInput);
    let ident = input.ident;
    let generics = input.generics;
    let (impl_generics, ty_generics, where_clause) = generics.split_for_impl();
    
    let fields = match input.data {
        Data::Struct(data) => match data.fields {
            Fields::Named(named_fields) => named_fields.named,
            _ => panic!("Struct can only be derived for structs with named fields"),
        },
        _ => panic!("Struct can only be derived for structs"),
    };
    let field_names = fields.iter().map(|field| &field.ident).map(|ident| quote! { #ident }).collect::<Vec<_>>();
    let field_types = fields.iter().map(|field| &field.ty).map(|ty| quote! { #ty }).collect::<Vec<_>>();

    let field_setters = fields.iter().map(|field| {
        let field_name = &field.ident;
        let setter_name = format_ident!("set_{}", field_name.as_ref().unwrap());
        let unit_name = format_ident!("unit_{}", field_name.as_ref().unwrap());
        let field_type = &field.ty;
        let other_fields = fields.iter().filter(|f| f.ident != *field_name).map(|f| &f.ident).collect::<Vec<_>>();
        let other_types = fields.iter().filter(|f| f.ident != *field_name).map(|f| &f.ty );

        quote! {
            pub fn #setter_name(self, value: #field_type) -> Self {
                Self {
                    #field_name: value,
                    #(#other_fields: self.#other_fields ),*
                }
            }
            pub fn #unit_name() -> Self {
                Self {
                    #field_name: #field_type::one(),
                    #(#other_fields: #other_types::zero() ),*
                }
            }
        }
    });

    let field_tuple_constructors = fields.iter().map(|field| {
        let index = syn::Index::from(fields.iter().position(|f| f.ident == field.ident).unwrap());
        quote! { tuple.#index }
    });

    let field_hashes = fields.iter().map(|field| {
        let field_name = &field.ident;
        let field_type = &field.ty;
    
        match field_type {
            syn::Type::Path(type_path) if type_path.qself.is_none() => {
                let last_segment = &type_path.path.segments.last().unwrap();
                let type_ident = &last_segment.ident;

                if type_ident == "T" || type_ident == "Float" || type_ident == "f32" || type_ident == "f64" {
                    quote! {
                        let (mantissa, exponent, sign) = self.#field_name.integer_decode();
                        mantissa.hash(state);
                        exponent.hash(state);
                        sign.hash(state);
                    }
                } else {
                    quote! {
                        self.#field_name.hash(state);
                    }
                }
            }
            _ => quote! {
                self.#field_name.hash(state);
            },
        }
    });

    let field_almost_zero = fields.iter().map(|field| {
        let field_name = &field.ident;
        let field_type = &field.ty;
    
        match field_type {
            syn::Type::Path(type_path) if type_path.qself.is_none() => {
                let last_segment = &type_path.path.segments.last().unwrap();
                let type_ident = &last_segment.ident;

                if type_ident == "T" || type_ident == "Float" {
                    quote! { self.#field_name.to_f64().unwrap().abs() < tolerance } 
                } else if type_ident == "f32" || type_ident == "i8" || type_ident == "i16" || type_ident == "i32" || type_ident == "u8" || type_ident == "u16" || type_ident == "u32" {
                    quote! { f64::from(self.#field_name).abs() < tolerance } 
                } else if type_ident == "f64" {
                    quote! { self.#field_name.abs() < tolerance } 
                } else {
                    quote! { self.#field_name.almost_zero(tolerance) } 
                }
            }
            _ => quote! {
                quote! { self.#field_name.almost_zero(tolerance) } 
            },
        }
    });

    let field_almost_equals = fields.iter().map(|field| {
        let field_name = &field.ident;
        let field_type = &field.ty;
    
        match field_type {
            syn::Type::Path(type_path) if type_path.qself.is_none() => {
                let last_segment = &type_path.path.segments.last().unwrap();
                let type_ident = &last_segment.ident;

                if type_ident == "T" || type_ident == "Float" {
                    quote! { (self.#field_name - other.#field_name).to_f64().unwrap().abs() < tolerance } 
                } else if type_ident == "f32" || type_ident == "i8" || type_ident == "i16" || type_ident == "i32" || type_ident == "u8" || type_ident == "u16" || type_ident == "u32" {
                    quote! { f64::from(self.#field_name - other.#field_name).abs() < tolerance } 
                } else if type_ident == "f64" {
                    quote! { (self.#field_name - other.#field_name).abs() < tolerance } 
                } else {
                    quote! { self.#field_name.almost_equals(&other.#field_name, tolerance) } 
                }
            }
            _ => quote! {
                quote! { self.#field_name.almost_equals(&other.#field_name, tolerance) } 
            },
        }
    });

    let field_is_nan = fields.iter().map(|field| {
        let field_name = &field.ident;
        let field_type = &field.ty;
    
        match field_type {
            syn::Type::Path(type_path) if type_path.qself.is_none() => {
                let last_segment = &type_path.path.segments.last().unwrap();
                let type_ident = &last_segment.ident;

                if type_ident == "i8" || type_ident == "i16" || type_ident == "i32" || type_ident == "u8" || type_ident == "u16" || type_ident == "u32" {
                    quote! { false } 
                } else {
                    quote! { self.#field_name.is_nan() } 
                }
            }
            _ => quote! {
                quote! { self.#field_name.is_nan() } 
            },
        }
    });

    let field_is_infinite = fields.iter().map(|field| {
        let field_name = &field.ident;
        let field_type = &field.ty;
    
        match field_type {
            syn::Type::Path(type_path) if type_path.qself.is_none() => {
                let last_segment = &type_path.path.segments.last().unwrap();
                let type_ident = &last_segment.ident;

                if type_ident == "i8" || type_ident == "i16" || type_ident == "i32" || type_ident == "u8" || type_ident == "u16" || type_ident == "u32" {
                    quote! { false } 
                } else {
                    quote! { self.#field_name.is_infinite() } 
                }
            }
            _ => quote! {
                quote! { self.#field_name.is_infinite() } 
            },
        }
    });

    let expanded = quote! {
        impl #impl_generics #ident #ty_generics #where_clause {
            pub fn new(#(#field_names: #field_types),*) -> Self { Self { #(#field_names),* } }

            pub fn zero() -> Self { Self { #(#field_names: #field_types::zero()),* } }
            pub fn one() -> Self { Self { #(#field_names: #field_types::one()),* } }
            pub fn min_value() -> Self { Self { #(#field_names: #field_types::min_value()),* } }
            pub fn max_value() -> Self { Self { #(#field_names: #field_types::max_value()),* } }

            #(#field_setters)*

            pub fn almost_zero(&self, tolerance: f64) -> bool { #((#field_almost_zero))&&* }
            pub fn almost_equals(&self, other: &Self, tolerance: f64) -> bool { #((#field_almost_equals))&&* }
            pub fn is_nan(&self) -> bool { #(#field_is_nan)||* } 
            pub fn is_infinite(&self) -> bool { #(#field_is_infinite)||* } 
        }

        impl #impl_generics From<(#(#field_types),*)> for #ident #ty_generics #where_clause {
            fn from(tuple: ( #(#field_types),* )) -> Self {
                Self::new( #(#field_tuple_constructors, )* )
            }
        }

        impl #impl_generics Into<( #(#field_types),* )> for #ident #ty_generics #where_clause {
            fn into(self) -> ( #(#field_types),* ) {
                ( #(self.#field_names),* )
            }
        }

        impl #impl_generics std::hash::Hash for #ident #ty_generics #where_clause {
            fn hash<H: std::hash::Hasher>(&self, state: &mut H) { #(#field_hashes)* }
        }
    };

    TokenStream::from(expanded)
}

#[proc_macro_derive(VectorComponentOps)]
pub fn derive_math_vector_components_ops(input: TokenStream) -> TokenStream {
    let input = parse_macro_input!(input as DeriveInput);
    let ident = input.ident;
    let generics = input.generics;
    let (impl_generics, ty_generics, where_clause) = generics.split_for_impl();
    
    let fields = match input.data {
        Data::Struct(data) => match data.fields {
            Fields::Named(named_fields) => named_fields.named,
            _ => panic!("UnaryOps can only be derived for structs with named fields"),
        },
        _ => panic!("UnaryOps can only be derived for structs"),
    };
    let field_names = fields.iter().map(|field| &field.ident).map(|ident| quote! { #ident }).collect::<Vec<_>>();

    let count = fields.len();
    let (first_field, other_fields) = field_names.split_first().expect("Expected at least one field");

    let generics_params = generics
        .type_params()
        .next();
    let type_param = match generics_params {
        Some(gp) => { 
            let t = &gp.ident;
            quote! { #t }
        }
        None => {
            let t =  &fields[0].ty;
            quote! { #t }
        }
    };

    let has_float_bound =  match generics_params {
        None => false,
        Some(gp) => {
            gp.bounds.iter().any(|bound| {
                if let syn::TypeParamBound::Trait(trait_bound) = bound { 
                    trait_bound.path.segments.last().map_or(false, |segment| segment.ident == "Float") 
                } else { false }
            })
        }
    };

    let magnitude_squared = if has_float_bound {
        quote! { pub fn magnitude_squared(&self) -> f64 { (self.sum_sqr_components()).to_f64().unwrap() } }
    } else {
        quote! { pub fn magnitude_squared(&self) -> f64 { f64::from(self.sum_sqr_components()) } }
    };

    let expanded = quote! { 
        impl #impl_generics #ident #ty_generics #where_clause {
            pub const NUM_COMPONENTS: usize = #count;

            pub fn create_from_value(value: #type_param) -> Self { Self { #(#field_names : value),* } }

            pub fn any_component_negative(&self) -> bool { self.min_component() < #type_param::zero() }
            pub fn min_component(&self) -> #type_param { self.#first_field #(.min(self.#other_fields))* }
            pub fn max_component(&self) -> #type_param { self.#first_field #(.max(self.#other_fields))* }
            pub fn sum_components(&self) -> #type_param { #(self.#field_names)+* }
            pub fn sum_sqr_components(&self) -> #type_param { #(self.#field_names * self.#field_names)+* }
            pub fn product_components(&self) -> #type_param { self.#first_field #(*self.#other_fields)* }
            pub fn get_component(&self, index: usize) -> Option<#type_param> { if index < 0 || index > #count { return None } else { Some([#(self.#field_names),*][index]) } }
            pub fn magnitude(&self) -> f64 { self.magnitude_squared().sqrt() }
            
            #magnitude_squared
            pub fn dot(&self, other: Self) -> #type_param { #(self.#field_names * other.#field_names)+* }
        }
    };

    TokenStream::from(expanded)
}

#[proc_macro_derive(VectorOperators)]
pub fn derive_math_vector_operators(input: TokenStream) -> TokenStream {
    let input = parse_macro_input!(input as DeriveInput);
    let ident = input.ident;
    let generics = input.generics;
    let (impl_generics, ty_generics, where_clause) = generics.split_for_impl();
    
    let fields = match input.data {
        Data::Struct(data) => match data.fields {
            Fields::Named(named_fields) => named_fields.named,
            _ => panic!("UnaryOps can only be derived for structs with named fields"),
        },
        _ => panic!("UnaryOps can only be derived for structs"),
    };
    let field_names = fields.iter().map(|field| &field.ident).map(|ident| quote! { #ident }).collect::<Vec<_>>();

    let generics_params = generics
        .type_params()
        .next();
    let type_param = match generics_params {
        Some(gp) => { 
            let t = &gp.ident;
            quote! { #t }
        }
        None => {
            let t =  &fields[0].ty;
            quote! { #t }
        }
    };

    let expanded = quote! { 
        impl #impl_generics Neg for #ident #ty_generics #where_clause {
            type Output = Self;
            fn neg(self) -> Self::Output { Self { #(#field_names: -self.#field_names),* } }
        }

        impl #impl_generics PartialOrd for #ident #ty_generics #where_clause {
            fn partial_cmp(&self, other: &Self) -> Option<Ordering> { self.magnitude_squared().partial_cmp(&other.magnitude_squared()) }
        }

        impl #impl_generics Add for #ident #ty_generics #where_clause {
            type Output = Self;
            fn add(self, other: Self) -> Self::Output { Self { #(#field_names: self.#field_names + other.#field_names),* } }
        }
        impl #impl_generics Add<#type_param> for #ident #ty_generics #where_clause {
            type Output = Self;
            fn add(self, other: #type_param) -> Self::Output { Self { #(#field_names: self.#field_names + other),* } }
        }

        impl #impl_generics Sub for #ident #ty_generics #where_clause {
            type Output = Self;
            fn sub(self, other: Self) -> Self::Output { Self { #(#field_names: self.#field_names - other.#field_names),* } }
        }
        impl #impl_generics Sub<#type_param> for #ident #ty_generics #where_clause {
            type Output = Self;
            fn sub(self, other: #type_param) -> Self::Output { Self { #(#field_names: self.#field_names - other),* } }
        }

        impl #impl_generics Mul for #ident #ty_generics #where_clause {
            type Output = Self;
            fn mul(self, other: Self) -> Self::Output { Self { #(#field_names: self.#field_names * other.#field_names),* } }
        }
        impl #impl_generics Mul<#type_param> for #ident #ty_generics #where_clause {
            type Output = Self;
            fn mul(self, other: #type_param) -> Self::Output { Self { #(#field_names: self.#field_names * other),* } }
        }

        impl #impl_generics Div for #ident #ty_generics #where_clause {
            type Output = Self;
            fn div(self, other: Self) -> Self::Output { Self { #(#field_names: self.#field_names / other.#field_names),* } }
        }
        impl #impl_generics Div<#type_param> for #ident #ty_generics #where_clause {
            type Output = Self;
            fn div(self, other: #type_param) -> Self::Output { Self { #(#field_names: self.#field_names / other),* } }
        }
    };

    TokenStream::from(expanded)
}

#[proc_macro_derive(VectorOps)]
pub fn derive_math_vector_ops(input: TokenStream) -> TokenStream {
    let input = parse_macro_input!(input as DeriveInput);
    let ident = input.ident;
    let generics = input.generics;
    let (impl_generics, ty_generics, where_clause) = generics.split_for_impl();
    
    let fields = match input.data {
        Data::Struct(data) => match data.fields {
            Fields::Named(named_fields) => named_fields.named,
            _ => panic!("UnaryOps can only be derived for structs with named fields"),
        },
        _ => panic!("UnaryOps can only be derived for structs"),
    };
    let field_names = fields.iter()
        .map(|field| &field.ident)
        .map(|ident| quote! { #ident })
        .collect::<Vec<_>>();
    let field_type = &fields[0].ty;

    let expanded = quote! {
        impl #impl_generics #ident #ty_generics #where_clause {
            pub fn lerp(self, other: Self, t: #field_type) -> Self { Self { #(#field_names: self.#field_names + (other.#field_names - self.#field_names) * t),* } }
            pub fn inverse_lerp(self, a: Self, b: Self) -> Self { Self { #(#field_names: (self.#field_names - a.#field_names) / (b.#field_names - a.#field_names)),* } }
            pub fn lerp_precise(self, other: Self, t: #field_type) -> Self { Self { #(#field_names: ((#field_type::one() - t) * self.#field_names) + (other.#field_names * t) ),* } }
            pub fn clamp_lower(self, min: Self) -> Self { self.max(min) }
            pub fn clamp_upper(self, max: Self) -> Self { self.min(max) }
            pub fn clamp(self, min: Self, max: Self) -> Self { self.min(max).max(min) }
            pub fn average(self, other: Self) -> Self { self.lerp(other, #field_type::from(0.5).unwrap()) }          
            pub fn barycentric(self, v2: Self, v3: Self, u: #field_type, v: #field_type) -> Self {
                let v2_sub_self = Self { #(#field_names: v2.#field_names - self.#field_names),* };
                let v3_sub_self = Self { #(#field_names: v3.#field_names - self.#field_names),* };
                self + v2_sub_self * u + v3_sub_self * v
            }

            pub fn abs(self) -> Self { Self { #(#field_names: self.#field_names.abs()),* } }
            pub fn acos(self) -> Self { Self { #(#field_names: self.#field_names.acos()),* } }
            pub fn asin(self) -> Self { Self { #(#field_names: self.#field_names.asin()),* } }
            pub fn atan(self) -> Self { Self { #(#field_names: self.#field_names.atan()),* } }
            pub fn cos(self) -> Self { Self { #(#field_names: self.#field_names.cos()),* } }
            pub fn cosh(self) -> Self { Self { #(#field_names: self.#field_names.cosh()),* } }
            pub fn exp(self) -> Self { Self { #(#field_names: self.#field_names.exp()),* } }
            pub fn ln(self) -> Self { Self { #(#field_names: self.#field_names.ln()),* } }
            pub fn log10(self) -> Self { Self { #(#field_names: self.#field_names.log10()),* } }
            pub fn sin(self) -> Self { Self { #(#field_names: self.#field_names.sin()),* } }
            pub fn sinh(self) -> Self { Self { #(#field_names: self.#field_names.sinh()),* } }
            pub fn sqrt(self) -> Self { Self { #(#field_names: self.#field_names.sqrt()),* } }
            pub fn tan(self) -> Self { Self { #(#field_names: self.#field_names.tan() ),* } }
            pub fn tanh(self) -> Self { Self { #(#field_names: self.#field_names.tanh()),* } }

            pub fn inverse(self) -> Self { Self { #(#field_names: #field_type::one() / self.#field_names),* } }
            pub fn ceil(self) -> Self { Self { #(#field_names: self.#field_names.ceil()),* } }
            pub fn floor(self) -> Self { Self { #(#field_names: self.#field_names.floor()),* } }
            pub fn round(self) -> Self { Self { #(#field_names: self.#field_names.round()),* } }
            pub fn trunc(self) -> Self { Self { #(#field_names: self.#field_names.trunc()),* } }
            pub fn sqr(self) -> Self { Self { #(#field_names: self.#field_names * self.#field_names),* } }
            pub fn cube(self) -> Self { Self { #(#field_names: self.#field_names * self.#field_names * self.#field_names),* } }
            pub fn to_radians(self) -> Self { Self { #(#field_names: self.#field_names.to_radians()),* } }
            pub fn to_degrees(self) -> Self { Self { #(#field_names: self.#field_names.to_degrees()),* } }

            pub fn distance_squared(self, other: Self) -> #field_type { (self - other).length_squared() }
            pub fn distance(self, other: Self) -> #field_type { (self - other).length() } 
            pub fn length_squared(&self) -> #field_type { self.sum_sqr_components() }
            pub fn length(&self) -> #field_type { self.length_squared().sqrt() }
            pub fn normalize(self) -> Self { 
                let len = self.length();
                self / len
            }
            pub fn safe_normalize(self) -> Self { 
                let len = self.length();
                if len != #field_type::zero() { self / len } else { self }
            }
            pub fn min(self, other: Self) -> Self { Self { #(#field_names: self.#field_names.min(other.#field_names)),* } }
            pub fn max(self, other: Self) -> Self { Self { #(#field_names: self.#field_names.max(other.#field_names)),* } }
            pub fn square_root(self) -> Self { self.sqrt() }
        }

        impl<T: Float + std::cmp::Eq> Ord for #ident #ty_generics #where_clause {
            fn cmp(&self, other: &Self) -> Ordering { self.partial_cmp(other).unwrap() }
        }
        
        impl #impl_generics Stats<#ident #ty_generics> #where_clause {
            pub fn average(&self) -> #ident #ty_generics { self.sum / T::from(self.count).unwrap() }
            pub fn extents(&self) -> #ident #ty_generics { self.max - self.min }
            pub fn middle(&self) -> #ident #ty_generics { self.extents() / (T::one() + T::one()) + self.min }
        }

        impl<T: Float + std::default::Default + std::cmp::Ord> Stats<#ident<T>>  {
            pub fn stats<I: Iterator<Item = #ident<T>>>(iter: I) -> Self {
                iter.fold(Self::default(), |a, b| {
                    Self {
                        count: a.count + 1,
                        min: a.min.min(b),
                        max: a.max.max(b),
                        sum: a.sum + b,
                    }
                })
            }
        }
        
        impl<T: Float + Default + Ord> #ident<T> {
            pub fn sum_iter<I: Iterator<Item = Self>>(iter: I) -> Self { iter.fold(Self::zero(), |acc, vec| acc + vec) }
            pub fn average_iter<I: Iterator<Item = Self>>(iter: I) -> Self { Stats::<Self>::stats(iter).average() }
            pub fn min_iter<I: Iterator<Item = Self>>(iter: I) -> Self { Stats::<Self>::stats(iter).min }
            pub fn max_iter<I: Iterator<Item = Self>>(iter: I) -> Self { Stats::<Self>::stats(iter).max }
            pub fn extents_iter<I: Iterator<Item = Self>>(iter: I) -> Self { Stats::<Self>::stats(iter).extents() }
            pub fn middle_iter<I: Iterator<Item = Self>>(iter: I) -> Self { Stats::<Self>::stats(iter).middle() }
        }
    };

    //eprintln!("expanded code:\n{}", expanded);
    TokenStream::from(expanded)
}

#[proc_macro_derive(IntervalOps)]
pub fn derive_math_interval_ops(input: TokenStream) -> TokenStream {
    let input = parse_macro_input!(input as DeriveInput);
    let ident = input.ident;
    let generics = input.generics;
    let (impl_generics, ty_generics, where_clause) = generics.split_for_impl();
    
    let fields = match input.data {
        Data::Struct(data) => match data.fields {
            Fields::Named(named_fields) => named_fields.named,
            _ => panic!("UnaryOps can only be derived for structs with named fields"),
        },
        _ => panic!("UnaryOps can only be derived for structs"),
    };
    let count = fields.len();
    let field_names = fields.iter()
        .map(|field| &field.ident)
        .map(|ident| quote! { #ident })
        .collect::<Vec<_>>();
    let (first_field, other_fields) = field_names.split_first().expect("Expected at least one field");
    let field_type = &fields[0].ty;
    let generics_params = generics.type_params().next();
    let type_param = match generics_params {
        Some(gp) => { 
            let t = &gp.ident;
            quote! { #t }
        }
        None => {
            let t =  &fields[0].ty;
            quote! { #t }
        }
    };

    let has_float_bound =  match generics_params {
        None => false,
        Some(gp) => {
            gp.bounds.iter().any(|bound| {
                if let syn::TypeParamBound::Trait(trait_bound) = bound { 
                    trait_bound.path.segments.last().map_or(false, |segment| segment.ident == "Float") 
                } else { false }
            })
        }
    };

    let magnitude_squared = match field_type {
        syn::Type::Path(type_path) if type_path.qself.is_none() => {
            let last_segment = &type_path.path.segments.last().unwrap();
            let type_ident = &last_segment.ident;

            if type_ident == "T" || type_ident == "Float" {
                quote! { 
                    pub fn magnitude_squared(&self) -> f64 { 
                        let ext = self.extent().to_f64().unwrap();
                        ext * ext
                    } 
                }
            } else if type_ident == "f32" || type_ident == "i8" || type_ident == "i16" || type_ident == "i32" || type_ident == "u8" || type_ident == "u16" || type_ident == "u32" {
                quote! { 
                    pub fn magnitude_squared(&self) -> f64 { 
                        let ext = f64::from(self.extent());
                        ext * ext
                    } 
                }
            } else if type_ident == "f64" {
                quote! { 
                    pub fn magnitude_squared(&self) -> f64 { 
                        let ext = self.extent();
                        ext * ext
                    } 
                }
            } else {
                quote! { pub fn magnitude_squared(&self) -> f64 { self.extent().magnitude_squared() } }
            }
        }
        _ => quote! {
            quote! { pub fn magnitude_squared(&self) -> f64 { self.extent().magnitude_squared() } }
        },
    };

    let center = if has_float_bound {
        quote! { pub fn center(&self) -> #field_type { (#(self.#field_names)+*) / #type_param::from(#count).unwrap() } }
    } else {
        quote! { pub fn center(&self) -> #field_type { (#(self.#field_names)+*) / #type_param::from(#count) } }
    };

    let expanded = quote! {
        impl #impl_generics #ident #ty_generics #where_clause {
            pub fn empty() -> Self { Self { #first_field: #field_type::max_value(), #(#other_fields: #field_type::min_value()),* } }
            
            pub fn extent(&self) -> #field_type { #(self.#field_names)-* } 
            #center
            #magnitude_squared
            pub fn magnitude(&self) -> f64 { self.magnitude_squared().sqrt() }
            pub fn merge(self, other: Self) -> Self { Self { #(#field_names: self.#field_names.min( (other.#field_names) )),* } }
            pub fn merge_value(self, other: #field_type) -> Self { Self { #(#field_names: self.#field_names.min(other)),* } }
            pub fn intersection(self, other: Self) -> Self { Self { #(#field_names: self.#field_names.max(other.#field_names)),* } }

            pub fn get_component(&self, index: usize) -> Option<#field_type> { if index < 0 || index > #count { return None } else { Some([#(self.#field_names),*][index]) } }
        }

        impl #impl_generics PartialOrd for #ident #ty_generics #where_clause {
            fn partial_cmp(&self, other: &Self) -> Option<Ordering> {
                self.magnitude_squared().partial_cmp(&other.magnitude_squared())
            }
        }

        impl #impl_generics Sub for #ident #ty_generics #where_clause {
            type Output = Self;
            fn sub(self, other: Self) -> Self::Output { self.intersection(other) }
        }
        impl #impl_generics Add for #ident #ty_generics #where_clause {
            type Output = Self;
            fn add(self, other: Self) -> Self::Output { self.merge(other) }
        }
        impl #impl_generics Add<#field_type> for #ident #ty_generics #where_clause {
            type Output = Self;
            fn add(self, other: #field_type) -> Self::Output { self.merge_value(other) }
        }
    };

    TokenStream::from(expanded)
}
