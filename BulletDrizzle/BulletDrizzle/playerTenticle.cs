﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;

namespace BulletDrizzle
{
    class playerTenticle
    {
        Texture2D texture;
        Rectangle rectangle;
        Rectangle sourceRectangle; //allows us to shrink texture later
        Vector2 position;
        Vector2 origin;
        int direction;
        bool spin;
        public playerTenticle(Texture2D inputTexture, int inputDirection)
        {
            texture = inputTexture;
            direction = inputDirection;
            rectangle = new Rectangle(0, 0, inputTexture.Width, inputTexture.Height);
            sourceRectangle = rectangle;
            origin = new Vector2(0, rectangle.Height / 2);
        }
        public void Update(KeyState keyboard, Vector2 playerPosition, Texture2D playerRectangle)
        {
            position.X = playerPosition.X + playerRectangle.Width / 8;
            position.Y = playerPosition.Y + playerRectangle.Height / 2 - rectangle.Height;
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            
            spriteBatch.Draw(texture, position, sourceRectangle, Color.White, angle, origin, 1.0f, SpriteEffects.None, 1);
        }
    }
}