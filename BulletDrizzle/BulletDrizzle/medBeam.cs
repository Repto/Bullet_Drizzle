using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BulletDrizzle
{
    class medBeam
    {
        Texture2D texture;
        Rectangle rectangle;
        float direction;
        float deltaX;
        float deltaY;
        float hypotenuseSquared;
        Vector2 temporaryVector;

        public medBeam(Texture2D inputTexture, Vector2 shipPosition, Rectangle medicRectangle, Vector2 woundedPosition, Rectangle woundedRectangle)
        {
            texture = inputTexture;
            rectangle.X = (int)shipPosition.X + rectangle.Width / 2;
            rectangle.Y = (int)shipPosition.Y + rectangle.Height / 2;
            rectangle.Height = texture.Height;
            //Minus What? What???
            temporaryVector = new Vector2(woundedPosition.X + woundedRectangle.Width / 2, woundedPosition.Y - woundedRectangle.Height / 2 );
            rectangle.Width = (int)workOutDistance(new Vector2(rectangle.X, rectangle.Y), temporaryVector);
            direction = workOutDirection(new Vector2(rectangle.X, rectangle.Y), temporaryVector);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, rectangle, null, Color.White, direction, new Vector2(0, rectangle.Height / 2), SpriteEffects.None, 1f);
        }

        public float workOutDistance(Vector2 medShip, Vector2 otherShip)
        {
            deltaX = otherShip.X - medShip.X;
            deltaY = otherShip.Y - medShip.Y;
            hypotenuseSquared = (deltaX * deltaX) + (deltaY * deltaY);
            return (float)(Math.Sqrt(hypotenuseSquared));
        }

        public float workOutDirection(Vector2 medShip, Vector2 otherShip)
        {
            deltaX = otherShip.X - medShip.X;
            deltaY = otherShip.Y - medShip.Y;
            return (float)(Math.Atan2(deltaX, deltaY)) - (float)(90 * 0.0174532925);
        }
    }
}