namespace Echo.Desktop
open Echo.Common

module Program =

    open System
    open Microsoft.Xna.Framework

    [<EntryPoint>]
    let main argv =
        use game = new Game1(ContetLoader(), false)
        game.Run()
        0