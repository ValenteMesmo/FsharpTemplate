namespace Echo.Common

open Microsoft.Xna.Framework
open Microsoft.Xna.Framework.Graphics
open System

type Camera() =
    let mutable zoom = 0.5f
    let mutable position = Vector2.Zero
    let mutable visibleArea = Rectangle.Empty
    let mutable transform = Matrix()
    let mutable bounds =  Rectangle.Empty
    let defaultZoom = 0.050f

    let updateVisibleArea() =
        let inverseViewMatrix = Matrix.Invert(transform)

        let tl = Vector2.Transform(Vector2.Zero, inverseViewMatrix)
        let tr = Vector2.Transform(Vector2(float32 bounds.X, 0.0f), inverseViewMatrix)
        let bl = Vector2.Transform(Vector2(0.0f, float32 bounds.Y), inverseViewMatrix)
        let br = Vector2.Transform(Vector2(float32 bounds.Width,float32 bounds.Height), inverseViewMatrix)

        let min = 
            Vector2(
                MathHelper.Min(tl.X, MathHelper.Min(tr.X, MathHelper.Min(bl.X, br.X)))
                , MathHelper.Min(tl.Y, MathHelper.Min(tr.Y, MathHelper.Min(bl.Y, br.Y))))

        let max = 
            Vector2(
                MathHelper.Max(tl.X, MathHelper.Max(tr.X, MathHelper.Max(bl.X, br.X))),
                MathHelper.Max(tl.Y, MathHelper.Max(tr.Y, MathHelper.Max(bl.Y, br.Y))))

        visibleArea <- Rectangle(int min.X, int min.Y, int (max.X - min.X), int (max.Y - min.Y))

    let resetZoom() =
        zoom <- defaultZoom

    let updateMatrix() =
        transform <-
            Matrix.CreateTranslation(Vector3(-position.X, -position.Y, 0.0f))
            * Matrix.CreateScale(zoom)
            * Matrix.CreateTranslation(new Vector3((float32 bounds.Width) * 0.5f, (float32 bounds.Height) * 0.5f, 0.0f))

        updateVisibleArea()    

    let limitZoom() =
        if (zoom < 0.01f) then
            zoom <- 0.01f
    
        if (zoom > 0.5f) then
            zoom <- 0.5f

            
    member this.Position
        with get () = position
        and set (value) = position <- value

    member this.AdjustZoom(zoomamount : float32) =
        zoom <- zoom  + zoomamount
        limitZoom()

    member this.SetZoom(value : float32) =
        zoom <- value
        limitZoom()

    member this.ToWorld(position: Point) : Point =
        Vector2.Transform(position.ToVector2(), Matrix.Invert(transform)).ToPoint()

    member this.ToScreen(position: Point) : Point =
        Vector2.Transform(position.ToVector2(), transform).ToPoint()

    member this.UpdateCamera(viewport: Viewport) =
        bounds <- viewport.Bounds
        updateMatrix()

    member this.GetTransform(): Nullable<Matrix> =
        Nullable transform