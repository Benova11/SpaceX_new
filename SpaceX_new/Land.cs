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

namespace SpaceX_new
{
    class Land
    {
        public const float unitToPixel = 100.0f;
        public const float pixelToUnit = 1 / unitToPixel;

        private Body body;
        private Vector2 size;
        private Texture2D texture;

        public Land(World world, Vector2 size, Texture2D texture)
        {
            
            body = BodyFactory.CreateRectangle(world, size.X * pixelToUnit, size.Y * pixelToUnit,1);
            body.BodyType = BodyType.Static;
            //body.CollisionCategories = Category.Cat2;
            this.Size = size;
            this.texture = texture;
        }

        public Vector2 Position { get =>body.Position * unitToPixel; set => body.Position = value * pixelToUnit; }
        public Vector2 Size { get => size * unitToPixel; set => size = value * pixelToUnit; }

        public void Draw(SpriteBatch spriteBatch)
        {
            Vector2 scale = new Vector2(Size.X / (float)texture.Width, Size.Y / (float)texture.Height);
            spriteBatch.Draw(texture,Position, null, Color.White, body.Rotation, new Vector2(texture.Width / 2.0f, texture.Height / 2.0f), scale, SpriteEffects.None, 0);
        }
    }
}
