use std::collections::HashMap;
use proc_macro::TokenStream;
use quote::{quote, format_ident};
use syn::{parse_macro_input, DeriveInput, LitStr};

fn camel_to_snake(s: &str, upper_case: bool) -> String {
    let mut snake = String::new();
    for (i, c) in s.chars().enumerate() {
        if c.is_ascii_uppercase() {
            if i > 0 { snake.push('_'); }
            snake.push(c.to_ascii_lowercase());
        } else { snake.push(c); }
    }
    if upper_case { snake.to_uppercase() } else { snake.to_lowercase() }
}

fn to_ident(snake: &str) -> syn::Ident {
    if snake == "type" { format_ident!("r#{}", snake) } else { format_ident!("{}", snake) }
}

#[proc_macro_attribute]
pub fn vim_schema(args: TokenStream, input: TokenStream) -> TokenStream {
    let args = parse_macro_input!(args as LitStr);
    let input = &parse_macro_input!(input as DeriveInput);
    //let (impl_generics, ty_generics, where_clause) = input.generics.split_for_impl();

    //let table_name = args.value();
    //let struct_name = &input.ident;
    let schema_str = args.value();
    let columns: Vec<&str> = schema_str.split(',').collect();
    let pairs: Vec<(&str, &str)> = columns.iter().map(|c| { 
        let kv: Vec<&str> = c.split_terminator("__").collect();
        (kv[0], kv[1])
    }).collect();
    
    let mut tables_hashes = HashMap::<&str, Vec<&str>>::new();
    for (name, value)  in pairs {
       if tables_hashes.contains_key(name) { tables_hashes.get_mut(name).unwrap().push(value); } 
       else { tables_hashes.insert(name, vec![value]); }
    }

    let structs = tables_hashes.iter().map(|(table, columns)| {
        let struct_name = format_ident!("{}", table.split_terminator('.').last().unwrap());
        let table_struct_name = format_ident!("{}Table", struct_name);
        let mut mapped_columns = Vec::new();
        let mut column_names = Vec::new();
        let mut functions = HashMap::new();
        let mut field_seter_pair = Vec::new();

        let mut has_lfmt = quote! {};
        for c in columns {
            if let Some(idx) = c.find(':') {
                let type_name: &str = &c[0..idx];
                let filed_name: &str = &c[idx + 1..c.len()];
                
                let quote = match type_name {
                    "double" => {
                        let flat_name = &filed_name.replace(".", "");
                        let field_ident = to_ident(&camel_to_snake(flat_name, false));

                        let column_name = to_ident(&camel_to_snake(&format!("{}_column_name", flat_name), true));
                        column_names.push(quote! { pub const #column_name: &'static str = #c; });
                        
                        if !functions.contains_key("count()") { functions.insert("count", quote! { pub fn count(&self) -> usize { self.entity_table.columns.get(#c).map_or(0, |column| column.size() / std::mem::size_of::<f64>()) } }); }
                        let get_all_function = format_ident!("get_{}_all", camel_to_snake(flat_name, false));
                        let get_function = format_ident!("get_{}", camel_to_snake(flat_name, false));
                        functions.insert(c, quote! { 
                            pub fn #get_all_function(&self) -> Option<Vec<f64>> {
                                if let Some(range) = self.entity_table.columns.get(#c) {
                                    if let Some(b) = self.entity_table.data.get(range.begin..range.end) {
                                        return Some(b.chunks_exact(8).map(|bytes| f64::from_ne_bytes([bytes[0], bytes[1], bytes[2], bytes[3], bytes[4], bytes[5], bytes[6], bytes[7]])).collect())
                                    }
                                }
                                None
                            }
                            pub fn #get_function(&self, index: usize) -> Option<f64> {
                                if let Some(range) = self.entity_table.columns.get(#c) {
                                    let at = range.begin + 8 * index;
                                    if let Some(bytes) = self.entity_table.data.get(at..at + 8) {
                                        return Some(f64::from_ne_bytes([bytes[0], bytes[1], bytes[2], bytes[3], bytes[4], bytes[5], bytes[6], bytes[7]]))
                                    }
                                } 
                                None
                            }
                        });
                        field_seter_pair.push(quote! { #field_ident: self.#get_function(index), });
                        
                        quote! { pub #field_ident: Option<f64>, }
                    },
                    "float" => {
                        let flat_name = &filed_name.replace(".", "");
                        let field_ident = to_ident(&camel_to_snake(flat_name, false));

                        let column_name = to_ident(&camel_to_snake(&format!("{}_column_name", flat_name), true));
                        column_names.push(quote! { pub const #column_name: &'static str = #c; });
                        
                        if !functions.contains_key("count()") { functions.insert("count", quote! { pub fn count(&self) -> usize { self.entity_table.columns.get(#c).map_or(0, |column| column.size() / std::mem::size_of::<f32>()) } }); }
                        let get_all_function = format_ident!("get_{}_all", camel_to_snake(flat_name, false));
                        let get_function = format_ident!("get_{}", camel_to_snake(flat_name, false));
                        functions.insert(c, quote! { 
                            pub fn #get_all_function(&self) -> Option<Vec<f32>> {
                                if let Some(range) = self.entity_table.columns.get(#c) {
                                    if let Some(b) = self.entity_table.data.get(range.begin..range.end) {
                                        return Some(b.chunks_exact(4).map(|bytes| f32::from_ne_bytes([bytes[0], bytes[1], bytes[2], bytes[3]])).collect())
                                    }
                                }
                                None
                            }
                            pub fn #get_function(&self, index: usize) -> Option<f32> {
                                if let Some(range) = self.entity_table.columns.get(#c) {
                                    let at =  range.begin + 4 * index;
                                    if let Some(bytes) = self.entity_table.data.get(at..at + 4) {
                                        return Some(f32::from_ne_bytes([bytes[0], bytes[1], bytes[2], bytes[3]]));
                                    }
                                } 
                                None
                            }
                        });
                        field_seter_pair.push(quote! { #field_ident: self.#get_function(index), });
                        
                        quote! { pub #field_ident: Option<f32>, }
                    },
                    "int" => {
                        let flat_name = &filed_name.replace(".", "");
                        let field_ident = to_ident(&camel_to_snake(flat_name, false));

                        let column_name = to_ident(&camel_to_snake(&format!("{}_column_name", flat_name), true));
                        column_names.push(quote! { pub const #column_name: &'static str = #c; });
                        
                        if !functions.contains_key("count()") { functions.insert("count",quote! { pub fn count(&self) -> usize { self.entity_table.columns.get(#c).map_or(0, |column| column.size() / std::mem::size_of::<i32>()) } }); }
                        let get_all_function = format_ident!("get_{}_all", camel_to_snake(flat_name, false));
                        let get_function = format_ident!("get_{}", camel_to_snake(flat_name, false));
                        functions.insert(c, quote! { 
                            pub fn #get_all_function(&self) -> Option<Vec<i32>> {
                                if let Some(range) = self.entity_table.columns.get(#c) {
                                    if let Some(b) = self.entity_table.data.get(range.begin..range.end) {
                                        return Some(b.chunks_exact(4).map(|bytes| i32::from_ne_bytes([bytes[0], bytes[1], bytes[2], bytes[3]])).collect())
                                    }
                                }
                                None
                            }
                            pub fn #get_function(&self, index: usize) -> Option<i32> {
                                if let Some(range) = self.entity_table.columns.get(#c) {
                                    let at =  range.begin + 4 * index;
                                    if let Some(bytes) = self.entity_table.data.get(at..at + 4) {
                                        return Some(i32::from_ne_bytes([bytes[0], bytes[1], bytes[2], bytes[3]]));
                                    }
                                } 
                                None
                            }
                        });
                        field_seter_pair.push(quote! { #field_ident: self.#get_function(index), });
                        
                        quote! { pub #field_ident: Option<i32>, }
                    },
                    "byte" => {
                        let flat_name = &filed_name.replace(".", "");
                        let field_ident = to_ident(&camel_to_snake(flat_name, false));
                        
                        let column_name = to_ident(&camel_to_snake(&format!("{}_column_name", flat_name), true));
                        column_names.push(quote! { pub const #column_name: &'static str = #c; });
                        
                        if !functions.contains_key("count()") { functions.insert("count", quote! { pub fn count(&self) -> usize { self.entity_table.columns.get(#c).map_or(0, |column| column.size()) } }); }
                        let get_all_function = format_ident!("get_{}_all", camel_to_snake(flat_name, false));
                        let get_function = format_ident!("get_{}", camel_to_snake(flat_name, false));
                        functions.insert(c, quote! { 
                            pub fn #get_all_function(&self) -> Option<&[u8]> {
                                if let Some(range) = self.entity_table.columns.get(#c) {
                                    return self.entity_table.data.get(range.begin..range.end);
                                }
                                None
                            }
                            pub fn #get_function(&self, index: usize) -> Option<u8> {
                                if let Some(range) = self.entity_table.columns.get(#c) {
                                    return self.entity_table.data.get(range.begin + index).copied();
                                } 
                                None
                            }
                        });
                        field_seter_pair.push(quote! { #field_ident: self.#get_function(index), });
                        
                        quote! { pub #field_ident: Option<u8>, }
                    }, 
                    "string" => {
                        let flat_name = &filed_name.replace(".", "");
                        let field_ident = to_ident(&camel_to_snake(flat_name, false));

                        let column_name = to_ident(&camel_to_snake(&format!("{}_column_name", flat_name), true));
                        column_names.push(quote! { pub const #column_name: &'static str = #c; });
                        //if !functions.contains_key("count()") { functions.insert("count", quote! { pub fn count(&self) -> usize { self.entity_table.string_columns.get(#c).map_or(0, |column| column.len()) } }); }
                        
                        if !functions.contains_key("count()") { functions.insert("count",quote! { pub fn count(&self) -> usize { self.entity_table.columns.get(#c).map_or(0, |column| column.size() / std::mem::size_of::<i32>()) } }); }
                        let get_all_function = format_ident!("get_{}_all", camel_to_snake(flat_name, false));
                        let get_function = format_ident!("get_{}", camel_to_snake(flat_name, false));
                        functions.insert(c, quote! { 
                            pub fn #get_all_function(&self) -> Option<Vec<&str>> {
                                if let Some(range) = self.entity_table.columns.get(#c) {
                                    if let Some(b) = self.entity_table.data.get(range.begin..range.end) {
                                        return Some(b.chunks_exact(4).map(|bytes| i32::from_ne_bytes([bytes[0], bytes[1], bytes[2], bytes[3]])).map(|i| self.strings[i as usize]).collect());
                                    }
                                }
                                None
                            }
                            pub fn #get_function(&self, index: usize) -> Option<&str> {
                                if let Some(range) = self.entity_table.columns.get(#c) {
                                    let at =  range.begin + 4 * index;
                                    if let Some(bytes) = self.entity_table.data.get(at..at + 4) {
                                        let idx = i32::from_ne_bytes([bytes[0], bytes[1], bytes[2], bytes[3]]) as usize;
                                        return self.strings.get(idx).copied();
                                    }
                                }
                                None
                            }
                        });
                        field_seter_pair.push(quote! { #field_ident: self.#get_function(index), });
                        has_lfmt = quote! { <'a> };

                        quote! { pub #field_ident: Option<&'a str>, }
                    },
                    "index" => { 
                        let index_parts: Vec<&str> = filed_name.split_terminator(":").collect();
                        if index_parts.len() != 2 { return quote! { #type_name } }
                        let index_type = format_ident!("{}", index_parts[0].split_terminator('.').last().unwrap());
                        let flat_name = &index_parts[1].replace(".", "");
                        let index_name = to_ident(&camel_to_snake(flat_name, false));
                        let index_id_name = format_ident!("{}_index", index_name);
                        
                        let column_name = to_ident(&camel_to_snake(&format!("{}_column_name", index_id_name), true));
                        column_names.push(quote! { pub const #column_name: &'static str = #c; });
                        //if !functions.contains_key("count()") { functions.insert("count", quote! { pub fn count(&self) -> usize { self.entity_table.index_columns.get(#c).map_or(0, |column| column.len()) } }); }
                        
                        if !functions.contains_key("count()") { functions.insert("count",quote! { pub fn count(&self) -> usize { self.entity_table.columns.get(#c).map_or(0, |column| column.size() / std::mem::size_of::<i32>()) } }); }
                        let get_all_function = format_ident!("get_{}_indices_all", camel_to_snake(flat_name, false));
                        let get_function = format_ident!("get_{}_idex", camel_to_snake(flat_name, false));
                        functions.insert(c, quote! { 
                            pub fn #get_all_function(&self) -> Option<Vec<i32>> {
                                if let Some(range) = self.entity_table.columns.get(#c) {
                                    if let Some(b) = self.entity_table.data.get(range.begin..range.end) {
                                        return Some(b.chunks_exact(4).map(|bytes| i32::from_ne_bytes([bytes[0], bytes[1], bytes[2], bytes[3]])).collect());
                                    }
                                }
                                None
                            }
                            pub fn #get_function(&self, index: usize) -> Option<i32> {
                                if let Some(range) = self.entity_table.columns.get(#c) {
                                    let at =  range.begin + 4 * index;
                                    if let Some(bytes) = self.entity_table.data.get(at..at + 4) {
                                        return Some(i32::from_ne_bytes([bytes[0], bytes[1], bytes[2], bytes[3]]));
                                    }
                                } 
                                None
                            }
                        });
                        field_seter_pair.push(quote! { #index_id_name: self.#get_function(index), });
                        //field_seter_pair.push(quote! { #index_name: None, });
                       // has_lfmt = quote! { <'a> };

                        quote! {
                            pub #index_id_name: Option<i32>,
                           // pub #index_name: Option<&'a #index_type<'a>>,
                        }
                    },
                    _ => quote! { #type_name },
                };
                mapped_columns.push(quote);
            } else { mapped_columns.push(quote! { #c }) }
        }
        let fns = functions.values();
       
        quote! {
            pub struct #struct_name #has_lfmt {
                pub index: usize,
                #(#mapped_columns)*
            }

            pub struct #table_struct_name<'a> {
                pub entity_table: vim::EntityTable<'a>,
                pub strings: &'a Vec<&'a str>,
            }

            impl<'a> #table_struct_name<'a> {
                pub const NAME: &'static str = #table;
                #(#column_names)*
                
                pub fn new(entity_table: vim::EntityTable<'a>, strings: &'a Vec<&'a str>) -> Self {
                    Self { entity_table, strings }
                }

                #(#fns)*

                pub fn get(&self, index: usize) -> Option<#struct_name> {
                    Some(#struct_name {
                        index: index, 
                        #(#field_seter_pair)*
                    })
                }
            }
        }
    });

    let gen = quote! {
        #input

        #(#structs)*
    };
    eprintln!("expanded code:\n{}", gen);
    gen.into()
}

// #[proc_macro_attribute]
// pub fn vim_table(args: TokenStream, input: TokenStream) -> TokenStream {
//     let args = parse_macro_input!(args as LitStr);
//     let input = &parse_macro_input!(input as DeriveInput);
//     let (impl_generics, ty_generics, where_clause) = input.generics.split_for_impl();

//     let table_name = args.value();
//     let struct_name = &input.ident;
//     let struct_name_table = format_ident!("{}Table", struct_name);

//     let gen = quote! {
//         #input

//         pub struct #struct_name_table<'a> {
//             pub entity_table: vim::EntityTable<'a>,
//             pub strings: &'a Vec<&'a str>,
//         }

//         impl #impl_generics #struct_name #ty_generics #where_clause {
//             pub fn print_table_name(&self) {
//                 println!("{}", #struct_name_table);
//             }
//         }
//     };
//     //eprintln!("expanded code:\n{}", gen);
//     gen.into()
// }
 

// #[proc_macro_derive(VimTable)]
// pub fn derive_object_model_vim_table(input: TokenStream) -> TokenStream {
//     let input = parse_macro_input!(input as DeriveInput);
//     let ident = input.ident;
//     let generics = input.generics;
//     let (impl_generics, ty_generics, where_clause) = generics.split_for_impl();
    
//     let fields = match input.data {
//         Data::Struct(data) => match data.fields {
//             Fields::Named(named_fields) => named_fields.named,
//             _ => panic!("Struct can only be derived for structs with named fields"),
//         },
//         _ => panic!("Struct can only be derived for structs"),
//     };
//     // let field_names = fields.iter().map(|field| &field.ident).map(|ident| quote! { #ident }).collect::<Vec<_>>();
//     // let field_types = fields.iter().map(|field| &field.ty).map(|ty| quote! { #ty }).collect::<Vec<_>>();
 
//     let table_name = format_ident!("{}Table", ident);
//     let expanded = quote! {
//         pub struct #table_name<'a> {
//             pub entity_table: EntityTable<'a>,
//             pub strings: Vec<&'a str>,
//         }

//         // impl #impl_generics #ident #ty_generics #where_clause {
//         //     pub fn new(#(#field_names: #field_types),*) -> Self { Self { #(#field_names),* } }

//         //     pub fn zero() -> Self { Self { #(#field_names: #field_types::zero()),* } }
//         //     pub fn one() -> Self { Self { #(#field_names: #field_types::one()),* } }
//         //     pub fn min_value() -> Self { Self { #(#field_names: #field_types::min_value()),* } }
//         //     pub fn max_value() -> Self { Self { #(#field_names: #field_types::max_value()),* } }
//         // }
//     };

//     eprintln!("expanded code:\n{}", expanded);
//     TokenStream::from(expanded)
// }
