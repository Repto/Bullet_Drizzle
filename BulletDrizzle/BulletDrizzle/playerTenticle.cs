using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;

namespace BulletDrizzle
{
    class playerTenticle
    {
        Texture2D texture;
        Rectangle rectangle;
        Rectangle sourceRectangle; //allows us to shrink texture later
        Vector2 position;
        Vector2 origin;
        float scale = 1;
        float restDirection;
        float direction;
        bool forwards = false;
        bool backward = false;
        bool small = false;
        bool large = false;
        public playerTenticle(Texture2D inputTexture, float inputDirection)
        {
            texture = inputTexture;
            scale = 1;
            direction = inputDirection;
            restDirection = direction;
            rectangle = new Rectangle(0, 0, inputTexture.Width, inputTexture.Height);
            sourceRectangle = rectangle;
            origin = new Vector2(0, rectangle.Height / 2);
            forwards = false; backward = false; small = false; large = false;
        }
        public void Update(KeyboardState keyboard, MouseState mouse, Vector2 playerPosition, Vector2 screenDimensions, Rectangle playerRectangle, bool UShoot, int laserCooldown, int superCooldown)
        {
            position.X = mouse.X + playerRectangle.Width / 8;
            position.Y = mouse.Y + playerRectangle.Height / 2 - rectangle.Height;
            if (position.X < 20)
            {
                position.X = 20;
            }

            if (position.X + texture.Width > screenDimensions.X - 120)
            {
                position.X = screenDimensions.X - texture.Width - 120;
            }

            if (position.Y < 20)
            {
                position.Y = 20;
            }

            if (position.Y + texture.Height > screenDimensions.Y - 20)
            {
                position.Y = screenDimensions.Y - texture.Height - 20;
            }
            if (UShoot)
            {
                direction += 12;
            }

            else if (keyboard.IsKeyDown(Keys.D) && laserCooldown == 0)
            {
                forwards = true;
            }
            if (keyboard.IsKeyDown(Keys.S) && superCooldown == 0)
            {
                large = true;
            }
            if (small)
            {
                scale -= 0.1f;
                if (scale <= 1)
                {
                    small = false;
                    scale = 1;
                }
            }
            if (large)
            {
                scale += 0.1f;
                if (scale > 1.5)
                {
                    large = false;
                    small = true;
                }
            }
            if (forwards)
            {
                direction += 24;
                if (direction - restDirection >= 220)
                {
                    backward = true;
                    forwards = false;
                }
            }
           else if (backward)
                {
                    direction -= 24;
                    if ((direction - restDirection) > -6 && (direction - restDirection) < 6)
                    {
                        backward = false;
                        direction = restDirection;
                    }
                }
           else if (UShoot) { }
           else { direction = restDirection; }
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, sourceRectangle, Color.White, (float)(direction * 0.0174532925), origin, scale, SpriteEffects.None, 1);
        }
    }
}