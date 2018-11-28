using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using FarseerPhysics.Dynamics;
using FarseerPhysics.Factories;
using FarseerPhysics.Dynamics.Joints;

namespace SpaceX_new
{
    public enum Movement
    {
        Up,
        Left,
        Right
    }

    class Rocket
    {
        public const float unitToPixel = 100.0f;
        public const float pixelToUnit = 1 / unitToPixel;
        private Random rand;

        private Body body;
        private Vector2 size;
        private Texture2D texture;
        private bool isMoving = false;
        private KeyboardState kStateOld = Keyboard.GetState();
        private AnimatedSprite anim;



        public Rocket(World world,Vector2 size, Texture2D texture, Texture2D burner)
        {
            this.size = size;
            body = BodyFactory.CreateRectangle(world, size.X * pixelToUnit, size.Y * pixelToUnit, 1);
            body.BodyType = BodyType.Static;
            body.CollisionCategories = Category.Cat1;
            this.texture = texture;
            rand = new Random();
            anim = new AnimatedSprite (burner,1,6);
        }

        public Vector2 Position { get => body.Position * unitToPixel; set => body.Position = value * pixelToUnit; }
        public Vector2 Size { get => size * unitToPixel; set => size = value * pixelToUnit; }
        public Body Body { get => body; set => body = value; }
        public bool IsMoving { get => isMoving; set => isMoving = value; }

       


        public void Update(GameTime gameTime)
        {
            KeyboardState kstate = Keyboard.GetState();
            float dt = (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (isMoving) // apply animation
                anim.Update(gameTime);
            else //player will appear as standing with frame [0] from the atlas.
                anim.CurrentFrame = 0;

            isMoving = false;

            if (kstate.IsKeyDown(Keys.Right))
            {
                isMoving = true;
                body.ApplyLinearImpulse(new Vector2(0.009f, 0.0f));
                body.Rotation += 0.002f;

            }

            if (kstate.IsKeyDown(Keys.Left))
            {
               
                isMoving = true;
                body.ApplyLinearImpulse(new Vector2(-0.009f, 0.0f));
                body.Rotation += -0.002f;
            }

            if (kstate.IsKeyDown(Keys.Space))
            {
                isMoving = true;
                body.ApplyLinearImpulse(new Vector2(0.0f, -0.02f));
                body.Rotation += randRotation(-1,1) * (float)gameTime.ElapsedGameTime.TotalSeconds;
            }

            if (kstate.IsKeyUp(Keys.Space))
            {
                isMoving = false;
            }

            Console.WriteLine(body.Rotation);
            Console.WriteLine(body.Position);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            Vector2 scale = new Vector2(size.X / (float)texture.Width, size.Y / (float)texture.Height);
            Vector2 postion2Draw = new Vector2(Position.X, Position.Y + 129);
            spriteBatch.Draw(texture, postion2Draw, null, Color.White, Body.Rotation, new Vector2(texture.Width, texture.Height), scale, SpriteEffects.None, 0);
            Vector2 burner_Pos = new Vector2(postion2Draw.X -145,postion2Draw.Y -4);
            anim.Draw(spriteBatch, burner_Pos);
        }

        public float randRotation(int min,int max)
        {
            
            double mantissa = (rand.NextDouble() * 2.0) - 1.0;
            double exponent = Math.Pow(2.0, rand.Next(min, max));
            return (float)(mantissa * exponent);
        }
           
    }


}
