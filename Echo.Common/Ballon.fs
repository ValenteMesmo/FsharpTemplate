namespace Echo.Common

module Ballon =
    let moveUp (obj : GameObject) =        
        obj.Y <- obj.Y - 1
        obj

    let destroyWhenOffScreen (obj : GameObject) =
        if obj.Y > -100 then
            obj.Destroy()
        obj

    let Create() =
        GameObject(
            moveUp
            , destroyWhenOffScreen
        )