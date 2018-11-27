﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace SpaceX_new
{
         class AnimatedSprite
        {
            private int currentFrame;
            private int totalFrames;
            private double timer;
            private double speed;

            public Texture2D Texture { get; set; }
            public int Rows { get; set; }
            public int Columns { get; set; }
            public int CurrentFrame { get => currentFrame; set => currentFrame = value; }

            public AnimatedSprite(Texture2D texture, int rows, int columns)
            {
                Texture = texture;
                Rows = rows;
                Columns = columns;
                CurrentFrame = 0;
                totalFrames = Rows * Columns;
                speed = 0.15D;
                timer = speed;

            }

            public void Update(GameTime gameTime)
            {
                timer -= gameTime.ElapsedGameTime.TotalSeconds;
                if (timer <= 0)
                {
                    CurrentFrame++;
                    timer = speed;
                }
                if (CurrentFrame == totalFrames)
                    CurrentFrame = 0;
            }

            public void Draw(SpriteBatch spriteBatch, Vector2 location)
            {
                int width = Texture.Width / Columns;
                int height = Texture.Height / Rows;
                int row = (int)((float)CurrentFrame / (float)Columns);
                int column = CurrentFrame % Columns;

                Rectangle sourceRectangle = new Rectangle(width * column, height * row, width, height);
                Rectangle destinationRectangle = new Rectangle((int)location.X, (int)location.Y, width, height);


                spriteBatch.Draw(Texture, destinationRectangle, sourceRectangle, Color.White);

            }
        }
    }


