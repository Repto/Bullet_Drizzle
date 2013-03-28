using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;

namespace BulletDrizzle
{
    class player
    {
        public Rectangle rectangle;
        public Texture2D texture;
        Texture2D bulletTexture;
        Texture2D laserTexture;
        public Vector2 position;
        Vector2 dimensions;
        int speed = 15;
        int countdown = 5;
        public int laserCooldown = 360;
        public int startingHealth = 500;
        public int health;
        public player(Texture2D inputTexture, Vector2 screenDimensions, Texture2D inputBulletTexture, Texture2D inputLaserTexture)
        {
            health = startingHealth;
            texture = inputTexture;
            bulletTexture = inputBulletTexture;
            position = new Vector2(10, (screenDimensions.Y / 2) - (texture.Height / 2));
            rectangle = new Rectangle((int)position.X, (int)position.Y, texture.Width, texture.Height);
            laserTexture = inputLaserTexture;
            dimensions = new Vector2(texture.Width, texture.Height);
        }
        public void Update(MouseState mouse, Vector2 screenDimensions, List<playerNormalBullet> bulletList, SoundEffect gunShot, KeyboardState keyboard, List<GiantLaser> laserList)
        {
            if (position.X > 0 && position.X < screenDimensions.X - texture.Width && position.Y > 0 && position.Y < screenDimensions.Y - texture.Height)
            {
                if (mouse.X > position.X + texture.Width / 2)
                {
                    position.X += speed;
                    if (mouse.X < position.X + texture.Width / 2)
                    {
                        position.X = mouse.X - texture.Width / 2;
                    }
                }

                if (mouse.X < position.X + texture.Width / 2)
                {
                    position.X -= speed;
                    if (mouse.X > position.X + texture.Width / 2)
                    {
                        position.X = mouse.X - texture.Width / 2;
                    }
                }

                if (mouse.Y > position.Y + texture.Height / 2)
                {
                    position.Y += speed;
                    if (mouse.Y < position.Y + texture.Height / 2)
                    {
                        position.Y = mouse.Y - texture.Height / 2;
                    }
                }

                if (mouse.Y < position.Y + texture.Height / 2)
                {
                    position.Y -= speed;
                    if (mouse.Y > position.Y + texture.Height / 2)
                    {
                        position.Y = mouse.Y - texture.Height / 2;
                    }
                }

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

                if (countdown == 0)
                {
                    if (mouse.LeftButton == ButtonState.Pressed)
                    {
                        bulletList.Add(new playerNormalBullet(position, new Vector2(texture.Width, texture.Height), bulletTexture, (float)(90 * 0.0174532925)));
                        bulletList.Add(new playerNormalBullet(position, new Vector2(texture.Width, texture.Height), bulletTexture, (float)(80 * 0.0174532925)));
                        bulletList.Add(new playerNormalBullet(position, new Vector2(texture.Width, texture.Height), bulletTexture, (float)(100 * 0.0174532925)));
                        countdown = 5;
                    }
                    if (mouse.RightButton == ButtonState.Pressed)
                    {
                        bulletList.Add(new playerNormalBullet(position, new Vector2(texture.Width, texture.Height), bulletTexture, (float)(0 * 0.0174532925)));
                        bulletList.Add(new playerNormalBullet(position, new Vector2(texture.Width, texture.Height), bulletTexture, (float)(180 * 0.0174532925)));
                        countdown = 5;
                    }
                    if (countdown == 5)
                    {
                        gunShot.Play();
                    }
                }
                if (laserCooldown == 0)
                {
                    if (keyboard.IsKeyDown(Keys.D))
                    {
                        laserList.Add(new GiantLaser(laserTexture, position, dimensions, screenDimensions));
                        laserCooldown = 360;
                    }
                }
                if (countdown > 0) { countdown--; }
                if (laserCooldown > 0) { laserCooldown--; }
                rectangle.X = (int)position.X;
                rectangle.Y = (int)position.Y;
            }
        }
        public void draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, rectangle, Color.White);
        }
    }
}

