namespace Echo.Common

open Microsoft.Xna.Framework
open Microsoft.Xna.Framework.Graphics
open Microsoft.Xna.Framework.Input

type Game1 (contextLoader : IContentLoader) as this =
    inherit Game()
 
    let graphics = new GraphicsDeviceManager(this)
    let mutable spriteBatch = Unchecked.defaultof<_>
    let mutable textures = Unchecked.defaultof<_>
    let world = World()

    do
        let balloonFactory = BalloonFactory.Create(world.AddObject)
        balloonFactory.Y <- -50
        world.AddObject(balloonFactory)
        this.Content.RootDirectory <- "Content"
        this.IsMouseVisible <- true

    override this.Initialize() =
        base.Initialize()

    override this.LoadContent() =
        spriteBatch <- new SpriteBatch(this.GraphicsDevice)
        textures <- contextLoader.LoadTextures(this.Content)
 
    override this.Update (gameTime) =
        if 
            GamePad.GetState(PlayerIndex.One).Buttons.Back = ButtonState.Pressed 
            || Keyboard.GetState().IsKeyDown(Keys.Escape)
        then
            this.Exit()

        world.update() 
    
        base.Update(gameTime)
 
    override this.Draw (gameTime) =
        this.GraphicsDevice.Clear Color.CornflowerBlue
        
        spriteBatch.Begin()

        world.GetObjects() 
        |> Seq.iter (fun f -> 
            spriteBatch.Draw(
                textures.["Block"]
                , Rectangle(f.X, f.Y, 100, 100)
                , Color.White
            ))

        spriteBatch.End()
        
        base.Draw gameTime