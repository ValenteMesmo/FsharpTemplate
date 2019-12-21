namespace Echo.Common

open Microsoft.Xna.Framework
open Microsoft.Xna.Framework.Graphics
open Microsoft.Xna.Framework.Input

type Game1 (contextLoader : IContentLoader, RuningOnAndroid: bool) as this =
    inherit Game()
 
    let graphics = new GraphicsDeviceManager(this)
    let mutable spriteBatch = Unchecked.defaultof<_>
    let mutable textures = Unchecked.defaultof<_>
    let mutable font = Unchecked.defaultof<_>
    let camera = Camera()
    let world = World()
    let touchCollection = TouchController(camera)
    let FramerateCounter = FramerateCounter()

    do
        camera.Position <- Vector2(1179.0f, 0.0f)

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
            graphics.SynchronizeWithVerticalRetrace <- true
            graphics.PreferredBackBufferWidth <- int GameConstants.ScreenWidth
            graphics.PreferredBackBufferHeight <- int GameConstants.ScreenHeight
        graphics.ApplyChanges()
        ()

    override this.LoadContent() =
        spriteBatch <- new SpriteBatch(this.GraphicsDevice)
        textures <- contextLoader.LoadTextures(this.Content)
        font <- this.Content.Load<SpriteFont>("Font")

        
        let getTexture name = 
            textures.[name]

        let balloonFactory = BalloonFactory.Create(world.AddObject, getTexture, touchCollection.GetTouches)
        balloonFactory.Y <- -50
        world.AddObject(balloonFactory)
 
    override this.Update(gameTime) =
        if
            GamePad.GetState(PlayerIndex.One).Buttons.Back = ButtonState.Pressed
            || Keyboard.GetState().IsKeyDown(Keys.Escape)
        then
            this.Exit()

        touchCollection.Update()
        world.update()
        camera.Update(this.GraphicsDevice)

        FramerateCounter.Update(float32 gameTime.ElapsedGameTime.TotalSeconds)
        base.Update(gameTime)
 
    override this.Draw(gameTime) =
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

        let renderSprites (spriteData : SpriteData)= 
            spriteBatch.Draw(
                spriteData.Texture
                , spriteData.TargetRectangle
                , spriteData.SourceRectangle
                , Color.White
            )

        let renderObjectSprites (obj : GameObject) =
            obj.GetSprites()
            |> Seq.iter renderSprites

        world.GetObjects() 
        |> Seq.iter renderObjectSprites

        spriteBatch.End()
        
        spriteBatch.Begin()
        spriteBatch.DrawString(font, FramerateCounter.AverageFramesPerSecond.ToString(), Vector2(100.0f, 100.0f), Color.White)
        spriteBatch.End()
        
        base.Draw gameTime