namespace Echo.Common

open Microsoft.Xna.Framework
open Microsoft.Xna.Framework.Graphics
open Microsoft.Xna.Framework.Input

type Game1 (contextLoader : IContentLoader, RuningOnAndroid: bool) as this =
    inherit Game()
 
    let graphics = new GraphicsDeviceManager(this)
    let mutable spriteBatch = Unchecked.defaultof<_>
    let mutable textures = Unchecked.defaultof<_>
    let camera = Camera()
    let world = World()
    let touchCollection = TouchController(camera)

    do
        camera.Position <- Vector2(1179.0f, 0.0f)
        let balloonFactory = BalloonFactory.Create(world.AddObject, touchCollection.GetTouches)
        balloonFactory.Y <- -50
        world.AddObject(balloonFactory)
        this.Content.RootDirectory <- "Content"
        this.IsMouseVisible <- true

    override this.Initialize() =
        base.Initialize()

        if RuningOnAndroid then
            graphics.IsFullScreen <- true
            graphics.PreferredBackBufferWidth <- this.GraphicsDevice.DisplayMode.Width
            graphics.PreferredBackBufferHeight <- this.GraphicsDevice.DisplayMode.Height
        else
            graphics.IsFullScreen <- false        
            graphics.PreferredBackBufferWidth <- int GameConstants.ScreenWidth
            graphics.PreferredBackBufferHeight <- int GameConstants.ScreenHeight

        graphics.ApplyChanges()
        ()

    override this.LoadContent() =
        spriteBatch <- new SpriteBatch(this.GraphicsDevice)
        textures <- contextLoader.LoadTextures(this.Content)
 
    override this.Update (gameTime) =
        if
            GamePad.GetState(PlayerIndex.One).Buttons.Back = ButtonState.Pressed
            || Keyboard.GetState().IsKeyDown(Keys.Escape)
        then
            this.Exit()

        touchCollection.Update()
        world.update() 
        camera.Update(this.GraphicsDevice)
        base.Update(gameTime)
 
    override this.Draw (gameTime) =
        this.GraphicsDevice.Clear Color.CornflowerBlue
        
        spriteBatch.Begin(
            SpriteSortMode.Deferred,
            BlendState.AlphaBlend,
            null,
            null,
            null,
            null,
            camera.Transform
        )

        world.GetObjects() 
        |> Seq.iter (fun f -> 
            spriteBatch.Draw(
                textures.["Block"]
                , Rectangle(f.X, f.Y, f.Width, f.Height)
                , Color.White
            ))

        spriteBatch.End()
        
        base.Draw gameTime