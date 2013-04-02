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
        float restDirection;
        float direction;
        bool forwards = false;
        bool backward = false;
        public playerTenticle(Texture2D inputTexture, float inputDirection)
        {
            texture = inputTexture;
            direction = inputDirection;
            restDirection = direction;
            rectangle = new Rectangle(0, 0, inputTexture.Width, inputTexture.Height);
            sourceRectangle = rectangle;
            origin = new Vector2(0, rectangle.Height / 2);
        }
        public void Update(KeyboardState keyboard, Vector2 playerPosition, Rectangle playerRectangle, bool UShoot, int laserCooldown)
        {
            position.X = playerPosition.X + playerRectangle.Width / 8;
            position.Y = playerPosition.Y + playerRectangle.Height / 2 - rectangle.Height;
            if (UShoot)
            {
                direction += 12;
            }
            else if (keyboard.IsKeyDown(Keys.D) && laserCooldown == 0)
            {
                forwards = true;
            }
            if (forwards)
            {
                direction += 24;
                if (direction - restDirection >= 180)
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
            spriteBatch.Draw(texture, position, sourceRectangle, Color.White, (float)(direction * 0.0174532925), origin, 1.0f, SpriteEffects.None, 1);
        }
    }
}