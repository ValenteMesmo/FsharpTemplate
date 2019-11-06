namespace Echo.Android

open System

open Android.App
open Android.Content
open Android.OS
open Android.Runtime
open Android.Views
open Android.Widget
open MGNamespace
open Android.Content.PM

//type Resources = Echo.Android.Resource

[<Activity (
    Label = "Echo.Android"
    , MainLauncher = true
    , Icon = "@mipmap/icon"
    , LaunchMode =  LaunchMode.SingleInstance
)>]
type MainActivity () =
    inherit Microsoft.Xna.Framework.AndroidGameActivity ()

    let mutable count:int = 1

    override this.OnCreate (bundle) =

        base.OnCreate (bundle)
        let game = new Game1()
        game.Run();

        let view = game.Services.GetService<View>()
        this.SetContentView(view);