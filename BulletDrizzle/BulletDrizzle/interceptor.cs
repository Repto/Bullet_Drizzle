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
            speed = 1;
            health = 60;
            enemyType = 0;
            bulletTexture = inputBulletTexture;
        }
        public void fire()
        {
            bulletList.Add(new enemyNormalBullet(new Vector2(position.X - (bulletTexture.Width * (float)0.5), position.Y), new Vector2(texture.Width, texture.Height), bulletTexture, (float)(0 * 0.0174532925)));
            bulletList.Add(new enemyNormalBullet(new Vector2(position.X - (bulletTexture.Width * (float)0.5), position.Y), new Vector2(texture.Width, texture.Height), bulletTexture, (float)(180 * 0.0174532925)));
            bulletList.Add(new enemyNormalBullet(new Vector2(position.X + texture.Width - (bulletTexture.Width * (float)1.5), position.Y), new Vector2(texture.Width, texture.Height), bulletTexture, (float)(0 * 0.0174532925)));
            bulletList.Add(new enemyNormalBullet(new Vector2(position.X + texture.Width - (bulletTexture.Width * (float)1.5), position.Y), new Vector2(texture.Width, texture.Height), bulletTexture, (float)(180 * 0.0174532925)));
            bulletCoolDown = 60;
        }
    }
}