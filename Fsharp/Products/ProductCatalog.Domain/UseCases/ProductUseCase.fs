module ProductCatalog.Domain.UseCases.ProductUseCase

open Utils
open Utils.StringPatterns
open ProductCatalog.Domain

type UnvalidatedDimension =
    { Length: int
      Width: int
      Height: int }

type UnvalidatedProduct =
    { Name: string
      Description: string
      Dimension: UnvalidatedDimension
      Weight: int
      CategoryIds: int list }

type UnsavedProduct =
    { Name: NonEmptyString
      Description: NonEmptyString
      Dimension: Dimension
      Weight: Weight
      Categories: NonEmptyList<CategoryId> }
    
type ProductCreationError =
    SavingFailed    
type CreateProduct = UnvalidatedProduct -> AsyncResult<unit,ProductCreationError>
