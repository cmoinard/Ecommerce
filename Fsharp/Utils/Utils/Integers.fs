namespace Utils

module Integers =
        
    let (|Negative|Zero|Positive|) =
        function
        | i when i < 0 -> Negative
        | i when i > 0 -> Positive
        | 0 -> Zero

type PositiveInt = private PositiveInt of int
module PositiveInt =
    open Integers
    
    let min = PositiveInt 1
    
    let TryCreate i =
        match i with
        | Positive -> PositiveInt i
        | _ -> invalidOp <| sprintf "%i is not positive" i        
    
    let Create i =
        match i with
        | Positive -> Some <| PositiveInt i
        | _ -> None
         
    let private Compute f x y =
        let (PositiveInt x') = x
        let (PositiveInt y') = y
        PositiveInt <| f x' y'
        
    let (+) = Compute (+)
    let (*) = Compute (*)