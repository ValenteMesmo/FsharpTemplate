namespace Echo.Common

open Microsoft.Xna.Framework
open Microsoft.Xna.Framework.Graphics
open System

type Camera() =
    let defaultZoom = 0.5f
    let mutable zoom = defaultZoom
    let mutable position = Vector2.Zero
    let mutable rotation = 0.0f
    let mutable transform = Matrix()
    
    member this.Position
        with get () = position
        and set (value) = position <- value

    member this.Transform
        with get () = Nullable transform

    member this.GetWorldPosition(position2: Vector2): Vector2 =
        let a = Vector2.Transform(position2, Matrix.Invert(transform))
        a

    member this.GetScreenPosition(position2: Vector2): Vector2 =
        Vector2.Transform(position2, transform)
   
    member this.Update(graphicsDevice: GraphicsDevice) =
        let widthDiff = float32 graphicsDevice.Viewport.Width / GameConstants.ScreenWidth
        let HeightDiff = float32 graphicsDevice.Viewport.Height / GameConstants.ScreenHeight

        transform 
            <- Matrix.CreateTranslation
                (
                    new Vector3(-position.X, -position.Y, 0.0f))
                      * Matrix.CreateRotationZ(rotation)
                      * Matrix.CreateScale(new Vector3(zoom * widthDiff, zoom * HeightDiff, 1.0f))
                      * Matrix.CreateTranslation
                        (
                            new Vector3
                                (
                                    float32 graphicsDevice.Viewport.Width * 0.5f
                                    , float32 graphicsDevice.Viewport.Height * 0.5f
                                    , 0.0f
                                )
                        )

        ()