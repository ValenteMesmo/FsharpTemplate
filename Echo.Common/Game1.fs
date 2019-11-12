namespace MGNamespace

open Microsoft.Xna.Framework
open Microsoft.Xna.Framework.Graphics
open Microsoft.Xna.Framework.Input

type Game1 (contextLoader : IContentLoader) as this =
    inherit Game()
 
    let graphics = new GraphicsDeviceManager(this)
    let mutable spriteBatch = Unchecked.defaultof<_>
    let mutable textures = Unchecked.defaultof<_>

    do
        this.Content.RootDirectory <- "Content"
        this.IsMouseVisible <- true

    override this.Initialize() =
        base.Initialize()

    override this.LoadContent() =
        spriteBatch <- new SpriteBatch(this.GraphicsDevice)
        textures <- contextLoader.LoadTextures(this.Content)
        //textures <- Dictionary<string,Texture2D>()
        //textures.Add("Block", this.Content.Load<Texture2D>("Textures/Block"))
 
    override this.Update (gameTime) =
        if 
            GamePad.GetState(PlayerIndex.One).Buttons.Back = ButtonState.Pressed 
            || Keyboard.GetState().IsKeyDown(Keys.Escape)
        then
            this.Exit()
    
        base.Update(gameTime)
 
    override this.Draw (gameTime) =
        this.GraphicsDevice.Clear Color.CornflowerBlue
        
        spriteBatch.Begin()

        spriteBatch.Draw(
            textures.["Block"]
            , Rectangle(0, 0, 100, 100)
            , Color.White
        )
        spriteBatch.Draw(
            textures.["Block"]
            , Rectangle(100, 100, 100, 100)
            , Color.White
        )

        spriteBatch.End()
        
        base.Draw(gameTime)