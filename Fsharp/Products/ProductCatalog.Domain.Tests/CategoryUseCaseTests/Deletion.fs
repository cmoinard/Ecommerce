module ProductCatalog.Domain.Tests.CategoryUseCaseTests.Deletion

open System
open ProductCatalog.Domain
open ProductCatalog.Domain.UseCases
open ProductCatalog.Domain.UseCases.CategoryUseCases
open Utils
open Xunit
open Swensen.Unquote

let exists e = fun _ -> Async.retn e 

[<Fact>]
let ``Cannot delete a non-existing category`` () =
    async {
        let saveStub = fun _ -> AsyncResult.retn ()
        let delete = CategoryUseCases.Delete saveStub (exists false)
    
        let categoryId = CategoryId 1
        let! actual = delete categoryId 
        
        actual =! Error (CategoryNotFound categoryId) 
    }
    
[<Fact>]
let ``Cannot delete when delete crash`` () =
    async {
        let saveStub = fun _ -> AsyncResult.ofError (Exception "Fail")
        let delete = CategoryUseCases.Delete saveStub (exists true)
    
        let categoryId = CategoryId 1
        let! actual = delete categoryId 
        
        actual =! Error (DeletionFailed "Fail") 
    }
    
[<Fact>]
let ``Should delete when everything is ok`` () =
    async {
        let saveStub = fun _ -> AsyncResult.retn ()
        let delete = CategoryUseCases.Delete saveStub (exists true)
    
        let categoryId = CategoryId 1
        let! actual = delete categoryId 
        
        actual =! Ok ()
    }