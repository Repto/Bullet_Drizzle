using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BulletDrizzle
{
    class enemyNormalBullet : projectile
    {
        public enemyNormalBullet(Vector2 enemyPosition, Vector2 enemyTextureDimensions, Texture2D inputTexture, float inputDirection)
        {
            texture = inputTexture;
            rectangle = new Rectangle(((int)enemyPosition.X + texture.Width / 2), ((int)enemyPosition.Y + (int)(enemyTextureDimensions.Y / 2) - texture.Height / 2), texture.Width, texture.Height);
            position.X = rectangle.X;
            position.Y = rectangle.Y;
            speed = 10;
            direction = inputDirection;
            damage = 1;
        }
        //moved update to projectile should save us some code
    }
}
