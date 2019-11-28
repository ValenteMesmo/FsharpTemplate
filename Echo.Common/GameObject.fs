namespace Echo.Common

open System

type GameObject([<ParamArray>] updates : (GameObject -> GameObject)[]) =
    let mutable x = 0
    let mutable y = 0
    let mutable width = 0
    let mutable height = 0
    let mutable destroyed = false

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

