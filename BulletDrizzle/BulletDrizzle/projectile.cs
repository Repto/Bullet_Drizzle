using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace BulletDrizzle
{
    class projectile
    {
        public Texture2D texture;
        public Rectangle rectangle;
        protected Vector2 position;
        protected int speed;
        public float direction;
        public int damage; // how much damage a bullet does when it hits
        public Boolean deleteMark = false;//marks bullet for deletion

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, rectangle, Color.White);
        }

        public void Update()
        {
            position.X += (float)(Math.Sin(direction) * speed);
            position.Y += (float)(Math.Cos(direction) * speed);
            rectangle.X = (int)position.X;
            rectangle.Y = (int)position.Y;
        }
    }
}
