using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System;
using FarseerPhysics.Dynamics;
using FarseerPhysics.Factories;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;

namespace SpaceX_new
{

    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        Texture2D rocket_Sprite;
        Texture2D burner_Sprite;
        Texture2D land_Sprite;

        Song computer_Control;
        Song landing;
        

        SpriteFont font;

        KeyboardState keyboardState;
        KeyboardState prevKeyboardState;

        Rocket player;
        Land landingSpot;

        bool islanded = false;

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

            computer_Control = Content.Load<Song>("control");
            landing = Content.Load<Song>("landing");


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

            

            keyboardState = Keyboard.GetState();

            if (keyboardState.IsKeyDown(Keys.Enter) && (!prevKeyboardState.IsKeyDown(Keys.Enter)))
            {
                MediaPlayer.Play(computer_Control);
                player.Body.BodyType = BodyType.Dynamic;
            }

            if ((player.Body.Position.Y >= 5.4f) && ((player.Body.Position.X > landingSpot.Body.Position.X - 1.5f) && (player.Body.Position.X < ((landingSpot.Size.X) / 100f + landingSpot.Body.Position.X - 1.5f))) && (player.Body.Rotation < 0.2f && player.Body.Rotation > -0.2f) && !islanded)
            {
                islanded = true;
                player.Body.Rotation = 0.0f;
                player.Body.BodyType = BodyType.Static;
                MediaPlayer.Play(landing);
            }

            player.Update(gameTime);

            world.Step((float)gameTime.ElapsedGameTime.TotalSeconds);

            base.Update(gameTime);
        }

        
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();

            player.Draw(spriteBatch);
            landingSpot.Draw(spriteBatch);
            
            spriteBatch.DrawString(font, "Rotation: " + player.Body.Rotation.ToString("0.000"), new Vector2(3, 0),Color.White);
            
            if (islanded)
            {
                spriteBatch.DrawString(font, "Seccessfully Landed", new Vector2(GraphicsDevice.Viewport.Width / 2.0f - 100, -GraphicsDevice.Viewport.Height + 800), Color.White);

            }

            spriteBatch.End();

            base.Draw(gameTime);
        }


    }
}
