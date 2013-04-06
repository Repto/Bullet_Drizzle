using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BulletDrizzle
{
    class interceptor : enemy
    {
        public interceptor(Vector2 spawnPosition, Vector2 screenDimensions, Texture2D inputTexture, Texture2D inputBulletTexture)
        {
            texture = inputTexture;
            rectangle = new Rectangle((int)(screenDimensions.X), (int)(spawnPosition.Y), texture.Width, texture.Height);
            position.X = rectangle.X;
            position.Y = rectangle.Y;
            speed = 2;
            startingHealth = 60;
            health = 60;
            bulletTexture = inputBulletTexture;
        }
        public void fire()
        {
            bulletList.Add(new enemyNormalBullet(position, new Vector2(rectangle.Width, rectangle.Height), bulletTexture, (float)(275 * 0.0174532925)));
            bulletList.Add(new enemyNormalBullet(position, new Vector2(rectangle.Width, rectangle.Height), bulletTexture, (float)(265 * 0.0174532925)));
            bulletCoolDown = 60;
        }
    }
}