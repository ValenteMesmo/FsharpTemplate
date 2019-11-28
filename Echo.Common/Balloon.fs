namespace Echo.Common

open System.Collections.Generic
open Microsoft.Xna.Framework

module Balloon =
    let moveUp (obj : GameObject) =        
        obj.Y <- obj.Y - 5
        obj

    let destroyWhenOffScreen (obj : GameObject) =
        if obj.Y < -1200 then
            obj.Destroy()
        obj    

    let Create(getTouches : unit -> List<Vector2>) =
        let destroyedOnTouch (obj : GameObject) =
            for touch in getTouches() do
                if Rectangle(obj.X, obj.Y, obj.Width, obj.Height).Contains(touch) then
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