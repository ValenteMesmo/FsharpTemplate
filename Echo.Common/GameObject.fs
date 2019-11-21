namespace Echo.Common

open System

type GameObject([<ParamArray>] updates : (GameObject -> GameObject)[]) =
    let mutable x = 0
    let mutable y = 0

    let mutable destroyed = false

    member this.X
        with get () = x
        and set (value) = x <- value

    member this.Y
        with get () = y
        and set (value) = y <- value

    member this.Update() =
        for update in updates do
            update (this) |> ignore

    member this.Destroy() =
        destroyed <- true;

