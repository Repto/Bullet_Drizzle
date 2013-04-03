using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BulletDrizzle
{
    class scout : enemy
    {
        public scout(Vector2 spawnPosition, Vector2 screenDimensions, Texture2D inputTexture, Texture2D inputBulletTexture)
        {
            texture = inputTexture;
            rectangle = new Rectangle((int)(screenDimensions.X), (int)(spawnPosition.Y), texture.Width, texture.Height);
            rectangle.Width = 100;
            rectangle.Height = 50;
            position.X = rectangle.X;
            position.Y = rectangle.Y;
            speed = 7;
            startingHealth = 6;
            health = startingHealth;
            bulletTexture = inputBulletTexture;
        }
        public void ScoutUpdate(List<enemyNormalBullet> inputBulletList, int playerY)
        {
            rectangle.X -= speed;
            bulletList = inputBulletList;
            if (bulletCoolDown > 0) { bulletCoolDown--; }
            if (rectangle.Y > playerY)
            {
                rectangle.Y--;
            }

            if (rectangle.Y < playerY)
            {
                rectangle.Y++;
            }
            position.X = rectangle.X;
            position.Y = rectangle.Y;
        }
        public void fire()
        {
            bulletList.Add(new enemyNormalBullet(position, new Vector2(texture.Width, texture.Height), bulletTexture, (float)(270 * 0.0174532925)));
            bulletCoolDown = 5;
        }
    }
}