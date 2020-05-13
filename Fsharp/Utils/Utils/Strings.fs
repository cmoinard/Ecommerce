namespace Utils

open System

module StringPatterns =
    let (|Empty|NonEmpty|) str =
       match String.IsNullOrWhiteSpace str with
       | true -> Empty
       | _ -> NonEmpty
       
    let (|Contains|_|) (char: string) (str: string) =
        match str.Contains(char) with
        | true -> Some Contains
        | _ -> None
                


type NonEmptyString = private NonEmptyString of string
module NonEmptyString =
    open StringPatterns
    
    let Create s =
        match s with
        | NonEmpty -> Some (NonEmptyString s)
        | Empty -> None