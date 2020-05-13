namespace ProductCatalog.Domain

open Utils

type CategoryId = CategoryId of int

[<NoEquality; NoComparison>]
type Category =
    { Id: CategoryId
      Name: NonEmptyString }