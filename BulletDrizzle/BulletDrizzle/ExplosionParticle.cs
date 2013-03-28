using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace BulletDrizzle
{
    class ExplosionParticle
    {
        public Texture2D texture;
        public Vector2 position;
        Rectangle rectangle;
        float direction;
        float velocity;
        public int countdown = 60;
        int colourDecrease = 0;

        public ExplosionParticle(Texture2D inputTexture, Vector2 emitterPosition, Random random)
        {
            texture = inputTexture;
            position = emitterPosition;
            direction = random.Next(1, 360);
            velocity = random.Next(1, 10);
            rectangle = new Rectangle((int)position.X, (int)position.Y, texture.Width, texture.Height);
        }

        public void Update()
        {
            position.X += (float)(Math.Sin(direction) * velocity);
            position.Y += (float)(Math.Cos(direction) * velocity);

            rectangle.X = (int)position.X;
            rectangle.Y = (int)position.Y;
            countdown--;
            colourDecrease += 4;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, rectangle, new Color(255 - colourDecrease, 255- colourDecrease, 255 - colourDecrease));
        }
    }
}
