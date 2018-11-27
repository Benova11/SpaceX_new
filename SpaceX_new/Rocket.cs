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
        private float speed = 700;
        


        public Rocket(World world,Vector2 size, Texture2D texture)
        {
            this.size = size;
            body = BodyFactory.CreateRectangle(world, size.X * pixelToUnit, size.Y * pixelToUnit, 1);
            body.BodyType = BodyType.Static;
            //Body.CollisionCategories = Category.Cat1;
            this.texture = texture;
            rand = new Random();


        }

        public Vector2 Position { get => body.Position * unitToPixel; set => body.Position = value * pixelToUnit; }
        public Vector2 Size { get => size * unitToPixel; set => size = value * pixelToUnit; }
        public Body Body { get => body; set => body = value; }

        public void Draw(SpriteBatch spriteBatch)
        {
            Vector2 scale = new Vector2(size.X / (float)texture.Width, size.Y / (float)texture.Height);
            spriteBatch.Draw(texture, Position, null, Color.White, Body.Rotation, new Vector2(texture.Width, texture.Height), scale, SpriteEffects.None, 0);
        }

        public float randRotation()
        {
            
            double mantissa = (rand.NextDouble() * 2.0) - 1.0;
            // choose -149 instead of -126 to also generate subnormal floats (*)
            double exponent = Math.Pow(2.0, rand.Next(-1, 1));
            Console.WriteLine((float)(mantissa * exponent));
            return (float)(mantissa * exponent);
        }

            public void Fly(GameTime gameTime)
            {
                float dt = (float)gameTime.ElapsedGameTime.TotalSeconds;
            //Position = new Vector2()
            // Position = new Vector2(Position.X,Position.Y - (speed * dt)); 
            body.IgnoreGravity = true;
            Position = new Vector2(Position.X, Position.Y - 2.3f);
            body.Rotation += randRotation() * (float)gameTime.ElapsedGameTime.TotalSeconds;
            }
           
    }


}
