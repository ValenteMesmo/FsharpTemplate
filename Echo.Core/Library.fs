namespace Echo.Core
open System.Collections.Generic


module aaaa =    

    type GameObject(updateHanlder : GameObject -> unit) =
        let mutable x = 0
        let mutable y = 0

        member this.X
            with get () = x
            and set (value) = x <- value

        member this.Y
            with get () = y
            and set (value) = y <- value

        member this.Update() =
            updateHanlder(this)
            ()

    type World() =
        member this.GameObjects = new List<GameObject>()

        member this.update() =
            for obj in this.GameObjects do
                obj.Update()
            ()

    let moveUpAndDown (obj : GameObject) =
        if obj.X < 100 then
            obj.X <- obj.X + 1
        else
            obj.X <- 0
        ()

    type Ballon() =
        inherit GameObject(moveUpAndDown)
    