namespace Echo.Common

module BalloonFactory = 
    open Balloon

    let Create(addToWorld) =
        let mutable counter = 0

        let update obj =
            counter <- counter + 1
            if counter > 100 then
                counter <- 0
                let ballon = Create()
                ballon.Y <- 500
                ballon.X <- 200
                addToWorld ballon
            obj

        GameObject update