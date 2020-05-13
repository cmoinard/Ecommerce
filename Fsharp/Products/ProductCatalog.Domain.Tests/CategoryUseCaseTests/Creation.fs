module Creation

open System
open ProductCatalog.Domain.UseCases
open ProductCatalog.Domain.UseCases.CategoryUseCases
open Utils
open Xunit
open Swensen.Unquote

let dummySave = fun _ -> AsyncResult.retn ()

[<Fact>]
let ``Cannot create a category without a name`` () =
    async {
        let create = CategoryUseCases.Create dummySave
    
        let! actual = create { Name = "" }
        
        actual =! Error EmptyName   
    }
    
[<Fact>]
let ``Cannot create a category without invalid characters`` () =
    async {
        let create = CategoryUseCases.Create dummySave
    
        let! actual = create { Name = "line1\nline2" }
        
        actual =! Error NameWithInvalidCharacters   
    }
    
[<Fact>]
let ``Cannot create a category when save crashes`` () =
    async {
        let crashingSave = fun _ -> AsyncResult.ofError (Exception "Fail")
        let create = CategoryUseCases.Create crashingSave
    
        let! actual = create { Name = "Keyboards" }
        
        actual =! Error (SavingFailed "Fail")   
    }
    
[<Fact>]
let ``Should create when everything is ok`` () =
    async {
        let createStub = fun _ -> AsyncResult.retn ()
        let create = CategoryUseCases.Create createStub
    
        let! actual = create { Name = "Keyboards" }
        
        actual =! Ok ()   
    }