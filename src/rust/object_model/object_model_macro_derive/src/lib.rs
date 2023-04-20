use proc_macro::TokenStream;
use quote::{quote, format_ident};
use syn::{parse_macro_input, DeriveInput, Data, Fields};

#[proc_macro_derive(VimTable)]
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

    eprintln!("expanded code:\n{}", expanded);
    TokenStream::from(expanded)
}
