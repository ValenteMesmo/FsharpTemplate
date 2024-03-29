﻿namespace Echo.Common
open System.Collections.Generic

type World() =        
    let GameObjects = new List<GameObject>()

    member this.GetObjects() =
        GameObjects

    member this.AddObject(object) =
        GameObjects.Add(object)
        ()

    member this.update() =
        for obj in GameObjects.ToArray() do
            if obj.Destroyed then
                GameObjects.Remove(obj) |> ignore
            else
                obj.Update() |> ignore
        ()