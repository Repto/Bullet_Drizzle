using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BulletDrizzle
{
    class mediShip : enemy
    {
        int healSpeed = 1;
        public mediShip(Vector2 spawnPosition, Vector2 screenDimensions, Texture2D inputTexture)
        {
            texture = inputTexture;
            rectangle = new Rectangle((int)(screenDimensions.X), (int)(spawnPosition.Y), texture.Width, texture.Height);
            position.X = rectangle.X;
            position.Y = rectangle.Y;
            rectangle.Width = texture.Width;
            rectangle.Height = texture.Height;
            speed = 2;
            health = 50;
            enemyType = 0;
        }
        public void Heal()
        {
           //Can't heal scouts, as they die first, so they're not available for healing.
           
        }

        //public void workOutDistance
    }
}
