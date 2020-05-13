module Utils.Entity

let inline equalsOn f x (yObj: obj) =
    match yObj with
    | :? 'T as y -> f x = f y
    | _ -> false

