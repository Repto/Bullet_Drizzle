using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BulletDrizzle
{
    class playerBigBullet : projectile
    {
        public playerBigBullet(Vector2 playerPosition, Vector2 playerTextureDimensions, Texture2D inputTexture, float inputDirection)
        {
            texture = inputTexture;
            rectangle = new Rectangle(((int)playerPosition.X + (int)playerTextureDimensions.X - texture.Width / 2), ((int)playerPosition.Y + (int)(playerTextureDimensions.Y / 2) - texture.Height / 2), texture.Width, texture.Height);
            position.X = rectangle.X;
            position.Y = rectangle.Y;
            speed = 10;
            direction = inputDirection;
            damage = 20;
        }
    }
}
