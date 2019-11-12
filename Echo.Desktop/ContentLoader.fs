namespace MGNamespace

open Microsoft.Xna.Framework.Content
open System.Collections.Generic
open Microsoft.Xna.Framework.Graphics
open System.IO

type ContetLoader () =
    interface IContentLoader with
        member this.LoadTextures(contentManager: ContentManager): Dictionary<string,Texture2D> = 
            let result = Dictionary<string,Texture2D>()
            let files = Directory.GetFiles("Content/Textures")
            
            files
            |> Seq.iter(fun f-> 
                    let key = Path.GetFileNameWithoutExtension f
                    let path = sprintf "Textures/%s" key
                    result.Add(key, contentManager.Load<Texture2D>(path))
                )
            |> ignore

            result