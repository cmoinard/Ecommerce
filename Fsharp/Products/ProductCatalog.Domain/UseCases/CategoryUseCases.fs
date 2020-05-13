module ProductCatalog.Domain.UseCases.CategoryUseCases

open Utils
open Utils.StringPatterns
open ProductCatalog.Domain

type UnvalidatedCategory = { Name: string }
type UnsavedCategory = { Name: NonEmptyString }

type CategoryCreationError =
    | EmptyName
    | NameWithInvalidCharacters
    | SavingFailed of string
type CreateCategory = UnvalidatedCategory -> AsyncResult<unit, CategoryCreationError>

let Create (save: UnsavedCategory -> AsyncResult<unit, exn>) : CreateCategory =
    fun category ->
        let validatedName =
            match category.Name with
            | Contains "\n" -> Error NameWithInvalidCharacters
            | _ ->
                category.Name
                |> NonEmptyString.Create
                |> Result.ofOption EmptyName        
        
        match validatedName with
        | Error e -> AsyncResult.ofError e
        | Ok v ->
            save { Name = v }
            |> AsyncResult.mapError (fun e -> SavingFailed e.Message)




type DeletionError =
    | CategoryNotFound of CategoryId
    | DeletionFailed of string
type DeleteCategory = CategoryId -> AsyncResult<unit, DeletionError>

let Delete
    (delete: CategoryId -> AsyncResult<unit, exn>)
    (exists: CategoryId -> Async<bool>)
    : DeleteCategory =
    fun categoryId -> 
            
        asyncResult {
            let! categoryExists = AsyncResult.ofAsync <| exists categoryId
            if not categoryExists then
                return! AsyncResult.ofError <| CategoryNotFound categoryId
                
            do! delete categoryId
                |> AsyncResult.mapError (fun e -> DeletionFailed e.Message)
        }
    