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
        Texture2D ultraBulletTexture;
        Texture2D superBulletTexture;
        public Vector2 position;
        Vector2 dimensions;
        int countdown = 5;
        public int laserReturn = 720;
        public int laserCooldown = 0;
        public int startingHealth = 1000;
        public int health;

        public bool ultraShoot = false;
        public int USreturn = 6000;
        public int USCountdown = 0;
        int USLast;
        int USDuration = 300;

        public int superReturn = 120;
        public int superCooldown = 0;

        List<playerTenticle> tentacles = new List<playerTenticle>();

        public player(Texture2D inputTexture, Vector2 screenDimensions, Texture2D inputBulletTexture, Texture2D inputLaserTexture, Texture2D inputUltraBulletTexture, Texture2D inputSuperBulletTexture, List<playerTenticle> inputTentacles)
        {
            health = startingHealth;
            texture = inputTexture;
            superBulletTexture = inputSuperBulletTexture;
            bulletTexture = inputBulletTexture;
            ultraBulletTexture = inputUltraBulletTexture;
            position = new Vector2(10, (screenDimensions.Y / 2) - (texture.Height / 2));
            rectangle = new Rectangle((int)position.X, (int)position.Y, texture.Width, texture.Height);
            laserTexture = inputLaserTexture;
            dimensions = new Vector2(texture.Width, texture.Height);
            tentacles = inputTentacles;
        }
        public void Update(MouseState mouse, Vector2 screenDimensions, List<playerNormalBullet> bulletList, SoundEffect gunShot, KeyboardState keyboard, List<GiantLaser> laserList, List<playerUltraBullet> ultraBulletList, List<playerBigBullet> superBulletList)
        {
            if (position.X > 0 && position.X < screenDimensions.X - rectangle.Width && position.Y > 0 && position.Y < screenDimensions.Y - rectangle.Height)
            {
                position.X = mouse.X;
                position.Y = mouse.Y;

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

                //update Tentacles
                foreach (playerTenticle tentacleHandling in tentacles)
                {
                    tentacleHandling.Update(keyboard, mouse, position, screenDimensions, rectangle, ultraShoot, laserCooldown, superCooldown);
                }
                
                //Abilities
                if (countdown == 0)
                {
                    if (ultraShoot)
                    {
                        //why for loops are useful:
                        for (int degrees = 0; degrees < 181; degrees += 10)
                        {
                            ultraBulletList.Add(new playerUltraBullet(position, new Vector2(texture.Width, texture.Height), ultraBulletTexture, (float)(degrees * 0.0174532925)));
                        }

                        //Right sided, as > 180
                        for (int degrees = 180; degrees < 361; degrees += 10)
                        {
                            ultraBulletList.Add(new playerUltraBullet(position, new Vector2(0, texture.Height), ultraBulletTexture, (float)(degrees * 0.0174532925)));
                        }
                        countdown = 5;
                        if (USLast < 1) { ultraShoot = false; }
                    }

                    else if (mouse.LeftButton == ButtonState.Pressed)
                    {
                        bulletList.Add(new playerNormalBullet(position, new Vector2(texture.Width, texture.Height), bulletTexture, (float)(90 * 0.0174532925)));
                        bulletList.Add(new playerNormalBullet(position, new Vector2(texture.Width, texture.Height), bulletTexture, (float)(80 * 0.0174532925)));
                        bulletList.Add(new playerNormalBullet(position, new Vector2(texture.Width, texture.Height), bulletTexture, (float)(100 * 0.0174532925)));
                        countdown = 5;
                    }
                    else if (mouse.RightButton == ButtonState.Pressed)
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
                        laserCooldown = laserReturn;
                    }
                }

                if (USCountdown == 0)
                {
                    if (keyboard.IsKeyDown(Keys.W))
                    {
                        ultraShoot = true;
                        USLast = USDuration;
                        USCountdown = USreturn;
                    }
                }

                if (superCooldown == 0)
                {
                    if (keyboard.IsKeyDown(Keys.S))
                    {
                        superCooldown = superReturn;
                        superBulletList.Add(new playerBigBullet(position, new Vector2(texture.Width, texture.Height), superBulletTexture, (float)(90 * 0.0174532925)));
                    }
                }
                if (ultraShoot) USLast--;
                if (countdown > 0) { countdown--; }
                if (laserCooldown > 0) { laserCooldown--; }
                if (USCountdown > 0) { USCountdown--; }
                if (superCooldown > 0) { superCooldown--; }
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

