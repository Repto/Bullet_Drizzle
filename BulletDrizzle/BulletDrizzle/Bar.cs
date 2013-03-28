using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace BulletDrizzle
{
    class Bar
    {
        Texture2D texture;
        Rectangle rectangle;
        int maxLength;
        int maxVariable;
        public Color barColor;
        Color backColor;
        Boolean changeColour;

        public Bar(Texture2D inputTexture, int x, int y, int height, int length, int inputMaxVariable, Color inputBarColor, Boolean inputChangeColour)
        {
            texture = inputTexture;
            rectangle = new Rectangle(x, y, 0, height);
            maxLength = length;
            maxVariable = inputMaxVariable;
            barColor = inputBarColor;
            changeColour = inputChangeColour;
        }

        public void Update(int number)
        {
            rectangle.Width = (int)(((float)number / (float)maxVariable) * (float)maxLength);
            if (changeColour == true && number < maxVariable / 2)
            {
                if (number < maxVariable / 4)
                {
                    barColor = Color.Red;
                }
                else
                {
                    barColor = Color.YellowGreen;
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            backColor.R = (byte)(barColor.R / 2);
            backColor.G = (byte)(barColor.G / 2);
            backColor.B = (byte)(barColor.B / 2);
            backColor.A = 255;
            spriteBatch.Draw(texture, new Rectangle(rectangle.X, rectangle.Y, maxLength, rectangle.Height), backColor);
            spriteBatch.Draw(texture, rectangle, barColor);
        }
    }
}