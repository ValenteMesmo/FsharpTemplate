namespace Echo.Common

open System.Collections.Generic
open Microsoft.Xna.Framework
open Microsoft.Xna.Framework.Graphics

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

    let Create(getTexture : string -> Texture2D, getTouches : unit -> List<Vector2>) =

        let animate (obj : GameObject) =
            let data = new SpriteData()
            
            data.TargetRectangle <- 
                Rectangle(obj.X, obj.Y, obj.Width, obj.Height)

            data.SourceRectangle <- 
                System.Nullable 
                <| Rectangle(0, 0, 64, 93)

            data.Texture <- getTexture "Balloon"

            obj.ClearSprites()
            obj.AddSprite(data)
            obj

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
                    , animate
                )
        obj.Width <- GameConstants.BalloonSize
        obj.Height <- GameConstants.BalloonSize
        obj