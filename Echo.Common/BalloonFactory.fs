namespace Echo.Common

module BalloonFactory = 
    open Balloon

    let Create(addToWorld, getTouchCollection) =
        let mutable counter = 99
        let mutable change = 0
        let update obj =
            counter <- counter + 1
            if counter > 100 then
                counter <- 0
                let ballon = Create(getTouchCollection)
                ballon.Y <- 700
                ballon.X <- change * GameConstants.BalloonSize
                addToWorld ballon

                change <- change + 1
                if change > 6 then
                    change <- 0
            obj

        GameObject update