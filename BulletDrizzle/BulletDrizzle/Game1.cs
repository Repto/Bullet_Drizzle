using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace BulletDrizzle
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        //Screendimensions and related info here:
        Vector2 screenDimensions = new Vector2(1920, 1080);

        //Lists of Different Projectiles. Their speeds are in their separate classes.
        List<playerNormalBullet> pNBlist = new List<playerNormalBullet>();
        List<enemyNormalBullet> eNBlist = new List<enemyNormalBullet>();
        List<GiantLaser> laserList = new List<GiantLaser>();

        //Lists of Different Enemies
        List<grunt> gruntList = new List<grunt>();

        //Explosion List
        List<ExplosionParticle> explosionsList = new List<ExplosionParticle>();

        //Single instances go here.
        player Player1;
        Bar healthBar;
        Bar laserBar;
        GiantLaser laser;
        public SoundEffect gunShot;
        public SoundEffect explodeSound;
        
        //Enemy and Projectile Textures
        Texture2D gruntTexture;
        Texture2D eNBTexture;
        Texture2D laserTexture;

        //Different explosion textures, for now just one
        Texture2D explosionTextureOne;

        //Random
        Random random = new Random();

        //Countdowns go here
        const int gruntSpawnCountdownReturn = 60;
        int gruntSpawnCountdown = gruntSpawnCountdownReturn;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            //Graphics modifications
            graphics.PreferredBackBufferHeight = (int)screenDimensions.Y;
            graphics.PreferredBackBufferWidth = (int)screenDimensions.X;
            graphics.IsFullScreen = true;
            this.IsMouseVisible = true;
            graphics.ApplyChanges();
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            laserTexture = Content.Load<Texture2D>("laser");
            Player1 = new player(Content.Load<Texture2D>("spaceship"), screenDimensions, Content.Load<Texture2D>("bullet"), laserTexture);
            gruntTexture = Content.Load<Texture2D>("enemyGrunt");
            eNBTexture = Content.Load<Texture2D>("bullet"); //for moment using normal bullet tex for enemy bullet tex, dunno if we'll change this
            healthBar = new Bar(Content.Load<Texture2D>("white"), 20, 20, 20, 250, Player1.startingHealth, Color.White, true);
            laserBar = new Bar(Content.Load<Texture2D>("white"), 21, 50, 20, 250, Player1.laserCooldown, Color.Red, false);
            healthBar.barColor = Color.Green;
            explosionTextureOne = Content.Load<Texture2D>("explosion");


            gunShot = Content.Load<SoundEffect>("Single machinegun shot");
            explodeSound = Content.Load<SoundEffect>("Explosion, explode");
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            // TODO: Add your update logic here

            //for testing purposes
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                this.Exit();
            }

            //Information gathering here
            KeyboardState keyState = Keyboard.GetState();
            MouseState mouseState = Mouse.GetState();

            //Single Instance Updates
            Player1.Update(mouseState, screenDimensions, pNBlist, gunShot, keyState, laserList);
            healthBar.Update(Player1.health);
            laserBar.Update(360 - Player1.laserCooldown);
            //moved color change to Bar.cs

            //Projectiles Update
            foreach (playerNormalBullet bulletHandling in pNBlist)
            {
                bulletHandling.Update();
            }
            foreach (enemyNormalBullet bulletHandling in eNBlist)
            {
                bulletHandling.Update();
            }
            foreach (GiantLaser laser in laserList)
            {
                if (laser.deathCountdown > 0)
                {
                    laser.Update(Player1.position, new Vector2(Player1.texture.Width, Player1.texture.Height), screenDimensions);
                }
            }

            //Explosion Update
            foreach (ExplosionParticle EPHandling in explosionsList)
            {
                EPHandling.Update();
            }

            //Testing over: all spawns go in this section (using different countdowns)
            if (gruntSpawnCountdown == 0)
            {
                Vector2 spawnPosition = new Vector2(screenDimensions.X, random.Next((int)(screenDimensions.Y - gruntTexture.Height)));
                gruntList.Add(new grunt(spawnPosition, screenDimensions, gruntTexture, eNBTexture));
                gruntSpawnCountdown = gruntSpawnCountdownReturn;
            }

            //Enemy Update
            for (int i = 0; i < gruntList.Count; i++)
            {
                gruntList[i].Update(eNBlist, pNBlist);
                if (gruntList[i].bulletCoolDown == 0) { gruntList[i].fire(); }
            }

            //Enemy hit test
            for (int i = 0; i < pNBlist.Count; i++)
            {
                for (int j = 0; j < gruntList.Count; j++)
                {
                    if (pNBlist[i].rectangle.Intersects(gruntList[j].rectangle))
                    {
                        pNBlist[i].deleteMark = true;
                        gruntList[j].health -= pNBlist[i].damage;
                    }
                }
            }

            //This next sequence will annoy you, but I wanted it working and it threw me annoying errors.
            if (laserList.Count > 0)
            {
                if (laserList[0].deathCountdown > 0)
                {
                    foreach (grunt gruntHandling in gruntList)
                    {
                        if (gruntHandling.rectangle.Intersects(laserList[0].rectangle))
                        {
                            gruntHandling.health -= laserList[0].damage;
                        }
                    }
                }
            }

            //If we're not removing them in this loop, can we just use a foreach?
            for (int i = 0; i < eNBlist.Count; i++)
            {
                if (eNBlist[i].rectangle.Intersects(Player1.rectangle))
                {
                    eNBlist[i].deleteMark = true;
                    Player1.health -= eNBlist[i].damage;
                }
            }

            //Deletion of marked projectiles and enemies - this feels very clumsy, tell me if there is a better way to do it
            for (int i = 0; i < pNBlist.Count; i++)
            {
                if (pNBlist[i].deleteMark == true)
                {
                    pNBlist.RemoveAt(i);
                    i--;
                }
            }
            for (int i = 0; i < gruntList.Count; i++)
            {
                if (gruntList[i].health < 0)
                {
                    explosionsList.Add(new ExplosionParticle(explosionTextureOne, new Vector2(gruntList[i].position.X + gruntList[i].texture.Width / 2, gruntList[i].position.Y + gruntList[i].texture.Height / 2), random));
                    explosionsList.Add(new ExplosionParticle(explosionTextureOne, new Vector2(gruntList[i].position.X + gruntList[i].texture.Width / 2, gruntList[i].position.Y + gruntList[i].texture.Height / 2), random));
                    explosionsList.Add(new ExplosionParticle(explosionTextureOne, new Vector2(gruntList[i].position.X + gruntList[i].texture.Width / 2, gruntList[i].position.Y + gruntList[i].texture.Height / 2), random));
                    gruntList.RemoveAt(i);
                    i--;
                    explodeSound.Play();
                }
            }

            for (int i = 0; i < explosionsList.Count; i++)
            {
                if (explosionsList[i].countdown < 1)
                {
                    explosionsList.RemoveAt(i);
                    if (i > 0) i--; else { break; }
                }
            }
            for (int i = 0; i < laserList.Count; i++)
            {
                if (laserList[i].deathCountdown == 0) { laserList.RemoveAt(i); }
            }
                

            //Bullets offscreen section
            for (int i = 0; i < pNBlist.Count; i++)
            {
                if (pNBlist[i].rectangle.X > screenDimensions.X || pNBlist[i].rectangle.X < 0)
                {
                    pNBlist.RemoveAt(i);
                    if (i > 0) { i--; } else { break; }
                }
            }
            for (int i = 0; i < eNBlist.Count; i++)
            {
                if (eNBlist[i].rectangle.X > screenDimensions.X || eNBlist[i].rectangle.X < 0)
                {
                    eNBlist.RemoveAt(i);
                    if (i > 0) { i--; } else { break; }
                }
            }
            //Enemies offscreen deletion - TODO make every enemy that gets through penalize player
            for (int i = 0; i < gruntList.Count; i++)
            {
                if (gruntList[i].rectangle.X < -gruntList[i].texture.Width)
                {
                    gruntList.RemoveAt(i);
                    if (i > 0) { i--; } else { break; }
                }
            }

            //Countdown updates
            if (gruntSpawnCountdown > 0) gruntSpawnCountdown--;

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            // TODO: Add your drawing code here
            spriteBatch.Begin();

            //Draw projectile lists
            foreach (playerNormalBullet bulletHandling in pNBlist)
            {
                bulletHandling.Draw(spriteBatch);
            }
            foreach (enemyNormalBullet bulletHandling in eNBlist)
            {
                bulletHandling.Draw(spriteBatch);
            }

            //Draw enemy lists
            foreach (grunt gruntHandling in gruntList)
            {
                gruntHandling.Draw(spriteBatch);
            }

            //Single Instance Draws
            Player1.draw(spriteBatch);
            healthBar.Draw(spriteBatch);
            laserBar.Draw(spriteBatch);

            foreach (GiantLaser laser in laserList)
            {
                if (laser.deathCountdown > 0)
                {
                    laser.Draw(spriteBatch);
                }
            }

            //Draw explosions, at high level but slightly transparent.
            foreach (ExplosionParticle EPHandling in explosionsList)
            {
                EPHandling.Draw(spriteBatch);
            }


            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
