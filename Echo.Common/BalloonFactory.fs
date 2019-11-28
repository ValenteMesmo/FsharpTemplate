namespace Echo.Common

open System

module BalloonFactory = 
    open Balloon

    let Create(addToWorld, getTouchCollection) =
        let mutable counter = 99
        let random = Random(171)

        let update obj =
            counter <- counter + 1
            if counter > GameConstants.BalloonSize / Balloon.balloonSpeed then
                counter <- 0
                let ballon = Create(getTouchCollection)
                ballon.Y <- 700
                ballon.X <- random.Next(0,6) * GameConstants.BalloonSize
                addToWorld ballon
            obj

        GameObject update