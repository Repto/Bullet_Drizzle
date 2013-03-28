﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BulletDrizzle
{
    class enemy
    {
        public int health;
        public Vector2 position;
        public Texture2D texture;
        public Texture2D bulletTexture;
        protected int speed;
        public Rectangle rectangle;
        public int enemyType;
        protected List<enemyNormalBullet> bulletList;
        public int bulletCoolDown;

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, rectangle, Color.White);
        }

        public void Update(List<enemyNormalBullet> inputBulletList, List<playerNormalBullet> pNBlist)
        {
            position.X -= speed;
            rectangle.X = (int)(position.X);
            rectangle.Y = (int)(position.Y);
            bulletList = inputBulletList;
            if (bulletCoolDown > 0) { bulletCoolDown--; }
        }
    }
}
