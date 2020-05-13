namespace ProductCatalog.Domain

open System
open Utils

type Dimension = {
    Length: PositiveInt
    Width: PositiveInt
    Height: PositiveInt
}

type Weight = private Weight of PositiveInt
module Weight =
    open PositiveInt
    
    let private Kilo = TryCreate 1000
    
    let FromGrams grams = Weight grams
    let FromKg kg = Weight <| kg * Kilo
    

type ProductId = ProductId of Guid

[<NoEquality; NoComparison>]
type Product =
    { Id: ProductId
      Name: NonEmptyString
      Description: NonEmptyString
      Dimension: Dimension
      Weight: Weight
      Categories: NonEmptyList<CategoryId> }