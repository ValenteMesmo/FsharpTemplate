namespace Echo.Common

open System.Collections.Generic
open Microsoft.Xna.Framework

module Balloon =
    let minSpeed = 2
    let maxSpeed = 20
    let acceleration = 1
    let breakSpeed = 2

    let mutable balloonSpeed = minSpeed

    let moveUp (obj : GameObject) =        
        obj.Y <- obj.Y - balloonSpeed
        obj

    let destroyWhenOffScreen (obj : GameObject) =
        if obj.Y < -1200 then
            balloonSpeed <- balloonSpeed - breakSpeed
            if balloonSpeed < minSpeed then
                balloonSpeed <- minSpeed
            obj.Destroy()
        obj    

    let Create(getTouches : unit -> List<Vector2>) =
        let destroyedOnTouch (obj : GameObject) =
            for touch in getTouches() do
                if Rectangle(obj.X, obj.Y, obj.Width, obj.Height + balloonSpeed * maxSpeed).Contains(touch) then
                    balloonSpeed <- balloonSpeed + acceleration
                    if balloonSpeed > maxSpeed then
                        balloonSpeed <- maxSpeed
                    obj.Destroy()
                ()
            obj

        let obj = 
            GameObject
                (
                    moveUp
                    , destroyWhenOffScreen
                    , destroyedOnTouch
                )
        obj.Width <- GameConstants.BalloonSize
        obj.Height <- GameConstants.BalloonSize
        obj