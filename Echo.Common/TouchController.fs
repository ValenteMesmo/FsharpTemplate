namespace Echo.Common

    open Microsoft.Xna.Framework.Input.Touch
    open System.Collections.Generic
    open Microsoft.Xna.Framework

    type TouchController(Camera: Camera) =
        let touches = List<Vector2>()
        member this.Update() =
            touches.Clear()

            let touchCollection = TouchPanel.GetState()
            for touch in touchCollection do
                touches.Add(Camera.GetWorldPosition(touch.Position))
            ()

        member this.GetTouches() : List<Vector2> =
            touches