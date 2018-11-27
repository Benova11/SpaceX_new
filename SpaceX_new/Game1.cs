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
        Texture2D land_Sprite;

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
            land_Sprite = Content.Load<Texture2D>("HUD");

            world = new World(new Vector2(0,1.8f));

            player = new Rocket(world, new Vector2(rocket_Sprite.Width, rocket_Sprite.Height), rocket_Sprite);
            landingSpot = new Land(world, new Vector2(land_Sprite.Width, land_Sprite.Height), land_Sprite);

            player.Position = new Vector2(GraphicsDevice.Viewport.Width / 2.0f, GraphicsDevice.Viewport.Height-590);
            landingSpot.Position = new Vector2(GraphicsDevice.Viewport.Width / 2.0f, GraphicsDevice.Viewport.Height-30);

        }

        
        protected override void UnloadContent()
        {
        }

        
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            KeyboardState keyboardState = Keyboard.GetState();
            if (keyboardState.IsKeyDown(Keys.Enter) && (!prevKeyboardState.IsKeyDown(Keys.Enter)))
            {
                player.Body.BodyType = BodyType.Dynamic;
            }
             if (keyboardState.IsKeyDown(Keys.Space))
             {
                 player.Fly(gameTime);

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

            spriteBatch.End();

            base.Draw(gameTime);
        }


    }
}
