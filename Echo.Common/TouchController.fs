namespace Echo.Common

    open Microsoft.Xna.Framework.Input.Touch
    open System.Collections.Generic
    open Microsoft.Xna.Framework
    open Microsoft.Xna.Framework.Input

    type TouchController(Camera: Camera) =
        let touches = List<Vector2>()
        member this.Update() =
            touches.Clear()

            let touchCollection = TouchPanel.GetState()
            for touch in touchCollection do
                touches.Add(Camera.GetWorldPosition(touch.Position))
            
            let mouse = Mouse.GetState()
            if mouse.LeftButton = ButtonState.Pressed then
                touches.Add(Camera.GetWorldPosition(mouse.Position.ToVector2()))

            ()

        member this.GetTouches() : List<Vector2> =
            touches