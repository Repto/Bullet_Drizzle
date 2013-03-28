using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace BulletDrizzle
{
    class GiantLaser
    {
        Texture2D texture;
        public Rectangle rectangle;
        Color color;
        public int deathCountdown;
        public int damage = 10;

        public GiantLaser(Texture2D inputTexture, Vector2 playerPosition, Vector2 playerTextureDimensions, Vector2 screenDimensions)
        {
            texture = inputTexture;
            rectangle = new Rectangle((int)(playerPosition.X + playerTextureDimensions.X), (int)(playerPosition.Y + playerTextureDimensions.Y / 2 - texture.Height / 2), (int)(screenDimensions.X - playerPosition.X), texture.Height);
            deathCountdown = 60;
            color = new Color(255, 255, 255, 125);
        }

        public void Update(Vector2 playerPosition, Vector2 playerTextureDimensions, Vector2 screenDimensions)
        {
            deathCountdown--;
            color.R -= 4;
            color.B -= 4;
            color.G -= 4;
            rectangle = new Rectangle((int)(playerPosition.X + playerTextureDimensions.X), (int)(playerPosition.Y + playerTextureDimensions.Y / 2 - texture.Height / 2), (int)(screenDimensions.X - playerPosition.X), texture.Height);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, rectangle, color);
        }
    }
}