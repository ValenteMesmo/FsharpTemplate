namespace Echo.Common

open System
open Microsoft.Xna.Framework.Graphics
open System.Collections.Generic
open Microsoft.Xna.Framework

type SpriteData() =
    let mutable texture = Unchecked.defaultof<Texture2D>
    let mutable targetRectangle = Rectangle.Empty
    let mutable sourceRectangle = Nullable Rectangle.Empty

    member this.Texture
        with get() = texture
        and set(value) = texture <- value

    member this.SourceRectangle
        with get() = sourceRectangle
        and set(value) = sourceRectangle <- value

    member this.TargetRectangle
        with get() = targetRectangle
        and set(value) = targetRectangle <- value

type GameObject([<ParamArray>] updates : (GameObject -> GameObject)[]) =
    let mutable x = 0
    let mutable y = 0
    let mutable width = 0
    let mutable height = 0
    let mutable destroyed = false
    let Sprites = new List<SpriteData>()
    
    member this.GetSprites() =
        Sprites

    member this.ClearSprites() =
        Sprites.Clear()

    member this.AddSprite(sprite: SpriteData) =
        Sprites.Add(sprite)
    
    member this.AddObject(sprite) =
        Sprites.Add(sprite)
        ()

    member this.X
        with get () = x
        and set (value) = x <- value

    member this.Y
        with get () = y
        and set (value) = y <- value

    member this.Width
        with get () = width
        and set (value) = width <- value

    member this.Height
        with get () = height
        and set (value) = height <- value

    member this.Update() =
        for update in updates do
            update (this) |> ignore

    member this.Destroy() =
        destroyed <- true

    member this.Destroyed
        with get () = destroyed

module SpriteBatchHelp =
    let DrawSprite spriteBatch spriteData =
        ()
