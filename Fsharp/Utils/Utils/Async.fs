namespace Utils

[<RequireQualifiedAccess>]
module Async =

    /// Lift a function to Async
    let map f xA = 
        async { 
        let! x = xA
        return f x 
        }

    /// Lift a value to Async
    let retn x = 
        async.Return x

    /// Apply an Async function to an Async value 
    let apply fA xA = 
        async { 
         // start the two asyncs in parallel
        let! fChild = Async.StartChild fA  // run in parallel
        let! x = xA
        // wait for the result of the first one
        let! f = fChild
        return f x 
        }

    /// Apply a monadic function to an Async value  
    let bind f xA = async.Bind(xA,f)
