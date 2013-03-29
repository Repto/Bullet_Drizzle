using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace BulletDrizzle
{
    class menuButton
    {
        Texture2D texture;
        Rectangle rectangle;
        Color color = new Color(125, 125, 125);
        public bool clicked = false;

        public menuButton(Texture2D inputTexture, Vector2 screenDimensions, int percentHeight)
        {
            texture = inputTexture;
            rectangle = new Rectangle(0, (int)(screenDimensions.Y * (percentHeight / (float)100)), (int)(texture.Width * (screenDimensions.X / 800)), (int)(texture.Height * (screenDimensions.Y / 600)));
            rectangle.X = (int)(screenDimensions.X / 2 - rectangle.Width / 2);
        }

        public void Update(MouseState mouse)
        {
            if (new Rectangle(mouse.X, mouse.Y, 1, 1).Intersects(rectangle))
            {
                color = new Color(255, 255, 255);
                if (mouse.LeftButton == ButtonState.Pressed)
                {
                    clicked = true;
                }
            }
            else
            {
                color = new Color(125, 125, 125);
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, rectangle, color);
        }

    }
}
