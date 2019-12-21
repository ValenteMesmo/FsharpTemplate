namespace Echo.Common

open System
open Microsoft.Xna.Framework.Graphics

module BalloonFactory = 
    open Balloon

    let Create(addToWorld,getTexture : string -> Texture2D, getTouchCollection) =
        let mutable counter = 99
        let random = Random(171)

        let update obj =
            counter <- counter + 1
            if counter > GameConstants.BalloonSize / Balloon.balloonSpeed then
                counter <- 0
                let ballon = Create(getTexture, getTouchCollection)
                ballon.Y <- 700
                ballon.X <- random.Next(0,6) * GameConstants.BalloonSize
                addToWorld ballon
            obj

        GameObject update