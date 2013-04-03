using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BulletDrizzle
{
    class grunt : enemy
    {
        public grunt(Vector2 spawnPosition, Vector2 screenDimensions, Texture2D inputTexture, Texture2D inputBulletTexture)
        {
            texture = inputTexture;
            rectangle = new Rectangle((int)(screenDimensions.X), (int)(spawnPosition.Y), texture.Width, texture.Height);
            position.X = rectangle.X;
            position.Y = rectangle.Y;
            rectangle.Width = 120;
            rectangle.Height = 40;
            speed = 5;
            startingHealth = 16;
            health = startingHealth;
            bulletTexture = inputBulletTexture;
        }
        public void fire()
        {
            bulletList.Add(new enemyNormalBullet(position, new Vector2(rectangle.Width, rectangle.Height), bulletTexture, (float)(270 * 0.0174532925)));
            bulletCoolDown = 30;
        }
    }
}
