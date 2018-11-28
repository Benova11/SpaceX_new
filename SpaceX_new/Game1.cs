using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System;
using FarseerPhysics.Dynamics;
using FarseerPhysics.Factories;

namespace SpaceX_new
{

    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        Texture2D rocket_Sprite;
        Texture2D burner_Sprite;
        Texture2D land_Sprite;

        SpriteFont font;

        KeyboardState prevKeyboardState;

        Rocket player;
        Land landingSpot;

        World world;



        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            graphics.PreferredBackBufferWidth = 1280;
            graphics.PreferredBackBufferHeight = 720;
        }

        
        protected override void Initialize()
        {

            base.Initialize();
        }

       
        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            rocket_Sprite = Content.Load<Texture2D>("rocket_body2");
            burner_Sprite = Content.Load<Texture2D>("bruner");
            land_Sprite = Content.Load<Texture2D>("HUD");

            font = Content.Load<SpriteFont>("timerFont");


            world = new World(new Vector2(0,1.8f));

            player = new Rocket(world, new Vector2(rocket_Sprite.Width, rocket_Sprite.Height), rocket_Sprite, burner_Sprite);
            landingSpot = new Land(world, new Vector2(land_Sprite.Width, land_Sprite.Height), land_Sprite);

            player.Position = new Vector2(GraphicsDevice.Viewport.Width / 2.0f, -GraphicsDevice.Viewport.Height  + 600);
            landingSpot.Position = new Vector2(GraphicsDevice.Viewport.Width / 2.0f, GraphicsDevice.Viewport.Height - 80);

        }

        
        protected override void UnloadContent()
        {
        }

        
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            player.Update(gameTime);

            KeyboardState keyboardState = Keyboard.GetState();
            if (keyboardState.IsKeyDown(Keys.Enter) && (!prevKeyboardState.IsKeyDown(Keys.Enter)))
            {
                player.Body.BodyType = BodyType.Dynamic;
            }

            world.Step((float)gameTime.ElapsedGameTime.TotalSeconds);

            base.Update(gameTime);
        }

        
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();

            player.Draw(spriteBatch);
            landingSpot.Draw(spriteBatch);


            spriteBatch.DrawString(font, "Rotation: " + player.Body.Rotation.ToString(), new Vector2(3, 0),Color.White);

            spriteBatch.End();

            base.Draw(gameTime);
        }


    }
}
