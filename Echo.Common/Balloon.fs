﻿namespace Echo.Common

module Balloon =
    let moveUp (obj : GameObject) =        
        obj.Y <- obj.Y - 1
        obj

    let destroyWhenOffScreen (obj : GameObject) =
        if obj.Y > -100 then
            obj.Destroy()
        obj

    let Create() =
        let obj = 
            GameObject
                (
                    moveUp
                    , destroyWhenOffScreen
                )
        obj.Width <- GameConstants.BalloonSize
        obj.Height <- GameConstants.BalloonSize
        obj