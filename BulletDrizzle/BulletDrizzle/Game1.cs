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
        Vector2 screenDimensions = new Vector2(1366, 768);
        //Vector2 screenDimensions = new Vector2(800, 600);

        //Lists of Different Projectiles. Their speeds are in their separate classes.
        List<playerNormalBullet> pNBlist = new List<playerNormalBullet>();
        List<enemyNormalBullet> eNBlist = new List<enemyNormalBullet>();
        List<playerUltraBullet> pUBlist = new List<playerUltraBullet>();
        List<GiantLaser> laserList = new List<GiantLaser>();
        List<playerBigBullet> superBulletList = new List<playerBigBullet>();

        //Lists of Different Enemies
        List<grunt> gruntList = new List<grunt>();
        List<scout> scoutList = new List<scout>();
        List<interceptor> interceptorList = new List<interceptor>();
        List<mediShip> mediList = new List<mediShip>();
        List<List<enemy>> totalList = new List<List<enemy>>();
        List<medBeam> beamList = new List<medBeam>();

        //Effects List
        List<ExplosionParticle> explosionsList = new List<ExplosionParticle>();
        List<playerTenticle> playerTentacles = new List<playerTenticle>();

        //Single instances go here.
        Texture2D playerTexture;
        SpriteFont font;
        SpriteFont bigFont;
        Texture2D menuTexture;
        Texture2D gameOverTexture;
        Texture2D playButtonTexture;
        Texture2D quitButtonTexture;
        Texture2D returnButtonTexture;
        player Player1;
        Bar healthBar;
        Bar laserBar;
        Bar ultraBar;
        Bar superBar;
        Bar adrenalineBar;
        menuButton playButton;
        menuButton quitButton;
        menuButton returnButton;
        bool WaitLetGo = false;
        
        public SoundEffect gunShot;
        public SoundEffect explodeSound;

        //Enemy and Projectile Textures
        Texture2D gruntTexture;
        Texture2D BBTexture;
        Texture2D scoutTexture;
        Texture2D interceptorTexture;
        Texture2D mediTexture;
        Texture2D eNBTexture;
        Texture2D UBTexture;
        Texture2D laserTexture;
        Texture2D tenticleTexture;
        Texture2D pNBTexture;

        //Different explosion textures, for now just one
        Texture2D explosionTextureOne;

        //Random
        Random random = new Random();
        Song level1;

        //Menuness Things
        enum GameState { menu, main, options, gameOver };
        GameState gameState = GameState.menu;

        //Score variables here
        int score = 0;
        int gruntKillScore = 100;
        int scoutKillScore = 200;
        int interceptorKillScore = 500;
        int mediKillScore = 1000;
        int level;
        
        int totalSecondTime = 0;
        int totalMilliTime = 0;

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

            //Miscellaneous
            MediaPlayer.IsRepeating = true;
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
            playerTexture = Content.Load<Texture2D>("spaceship");
            tenticleTexture = Content.Load<Texture2D>("playerTenticle");
            laserTexture = Content.Load<Texture2D>("laser");
            BBTexture = Content.Load<Texture2D>("bigbullet");
            UBTexture = Content.Load<Texture2D>("ultrabullet");
            pNBTexture = Content.Load<Texture2D>("bullet");
            gruntTexture = Content.Load<Texture2D>("enemyGrunt");
            scoutTexture = Content.Load<Texture2D>("enemyScout");
            interceptorTexture = Content.Load<Texture2D>("enemyInterceptor");
            mediTexture = Content.Load<Texture2D>("medic");
            eNBTexture = Content.Load<Texture2D>("ememybullet"); //for moment using normal bullet tex for enemy bullet tex, dunno if we'll change this
            explosionTextureOne = Content.Load<Texture2D>("explosion");
            menuTexture = Content.Load<Texture2D>("menuscreen");
            gameOverTexture = Content.Load<Texture2D>("gameoverscreen");
            playButtonTexture = Content.Load<Texture2D>("playbutton");
            quitButtonTexture = Content.Load<Texture2D>("quitbutton");
            

            level1 = Content.Load<Song>("DST-2ndBallad");
            returnButtonTexture = Content.Load<Texture2D>("returnbutton");

            font = Content.Load<SpriteFont>("SpriteFont1");
            bigFont = Content.Load<SpriteFont>("SpriteFont2");

            gunShot = Content.Load<SoundEffect>("Single machinegun shot");
            explodeSound = Content.Load<SoundEffect>("Explosion, explode");

            playButton = new menuButton(playButtonTexture, screenDimensions, 45);
            quitButton = new menuButton(quitButtonTexture, screenDimensions, 80);
            returnButton = new menuButton(returnButtonTexture, screenDimensions, 60);

            gameReset();
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
                gameReset();
                gameState = GameState.menu;
            }

            //Information gathering here
            KeyboardState keyState = Keyboard.GetState();
            MouseState mouseState = Mouse.GetState();

            if (gameState == GameState.main)
            {

                //Single Instance Updates

                //Update before player because they want to use the lasercooldown in conjunction with the d-key, and the player would affect that.
                Player1.Update(mouseState, screenDimensions, pNBlist, gunShot, keyState, laserList, pUBlist, superBulletList);
                healthBar.Update(Player1.health);
                laserBar.Update(Player1.laserReturn - Player1.laserCooldown);
                ultraBar.Update(Player1.USreturn - Player1.USCountdown);
                superBar.Update(Player1.superReturn - Player1.superCooldown);
                adrenalineBar.Update(Player1.adrenalineReturn - Player1.adrenalineCountdown);

                //Projectiles Update
                foreach (playerNormalBullet bulletHandling in pNBlist)
                {
                    bulletHandling.Update();
                }
                foreach (playerUltraBullet bulletHandling in pUBlist)
                {
                    bulletHandling.Update();
                }
                foreach (playerBigBullet bulletHandling in superBulletList)
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

                spawnControl.spawn(level, screenDimensions, gruntList, scoutList, interceptorList, mediList);

                /*if (gruntSpawnCountdown == 0)
                {
                    Vector2 spawnPosition = new Vector2(screenDimensions.X, random.Next(0, (int)(screenDimensions.Y - gruntTexture.Height)));
                    gruntList.Add(new grunt(spawnPosition, screenDimensions, gruntTexture, eNBTexture));
                    gruntSpawnCountdown = gruntSpawnCountdownReturn;
                }

                if (interceptorSpawnCountdown == 0)
                {
                    Vector2 spawnPosition = new Vector2(screenDimensions.X, random.Next(0, (int)(screenDimensions.Y - gruntTexture.Height)));
                    interceptorList.Add(new interceptor(spawnPosition, screenDimensions, interceptorTexture, eNBTexture));
                    interceptorSpawnCountdown = interceptorSpawnCountdownReturn;
                }

                if (scoutSpawnCountdown == 0)
                {
                    Vector2 spawnPosition = new Vector2(screenDimensions.X, random.Next(0, (int)(screenDimensions.Y - scoutTexture.Height)));
                    scoutList.Add(new scout(spawnPosition, screenDimensions, scoutTexture, eNBTexture));
                    scoutSpawnCountdown = scoutSpawnCountdownReturn;
                }*/

                //Enemy Update
                for (int i = 0; i < gruntList.Count; i++)
                {
                    gruntList[i].Update(eNBlist, pNBlist);
                    if (gruntList[i].bulletCoolDown == 0) { gruntList[i].fire(); }
                }

                for (int i = 0; i < scoutList.Count; i++)
                {
                    scoutList[i].ScoutUpdate(eNBlist, (int)Player1.position.Y);
                    if (scoutList[i].bulletCoolDown == 0) { scoutList[i].fire(); }
                }

                foreach (interceptor interceptorHandling in interceptorList)
                {
                    interceptorHandling.Update(eNBlist, pNBlist);
                    if (interceptorHandling.bulletCoolDown == 0) { interceptorHandling.fire(); }
                }
                beamList.Clear();
                foreach (mediShip mediHandling in mediList)
                {
                    totalList.Clear(); totalList.Add(gruntList.Cast<enemy>().ToList()); totalList.Add(scoutList.Cast<enemy>().ToList()); totalList.Add(interceptorList.Cast<enemy>().ToList());
                    mediHandling.Update(eNBlist, pNBlist);
                    mediHandling.Heal(totalList);
                    mediHandling.manageBeams(beamList);
                    //Doesn't fire.
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
                for (int i = 0; i < pUBlist.Count; i++)
                {
                    for (int j = 0; j < gruntList.Count; j++)
                    {
                        if (pUBlist[i].rectangle.Intersects(gruntList[j].rectangle))
                        {
                            pUBlist[i].deleteMark = true;
                            gruntList[j].health -= pUBlist[i].damage;
                        }
                    }
                }

                for (int i = 0; i < pNBlist.Count; i++)
                {
                    for (int j = 0; j < scoutList.Count; j++)
                    {
                        if (pNBlist[i].rectangle.Intersects(scoutList[j].rectangle))
                        {
                            pNBlist[i].deleteMark = true;
                            scoutList[j].health -= pNBlist[i].damage;
                        }
                    }
                }

                for (int i = 0; i < pUBlist.Count; i++)
                {
                    for (int j = 0; j < scoutList.Count; j++)
                    {
                        if (pUBlist[i].rectangle.Intersects(scoutList[j].rectangle))
                        {
                            pUBlist[i].deleteMark = true;
                            scoutList[j].health -= pUBlist[i].damage;
                        }
                    }
                }

                for (int i = 0; i < pNBlist.Count; i++)
                {
                    for (int j = 0; j < interceptorList.Count; j++)
                    {
                        if (pNBlist[i].rectangle.Intersects(interceptorList[j].rectangle))
                        {
                            pNBlist[i].deleteMark = true;
                            interceptorList[j].health -= pNBlist[i].damage;
                        }
                    }
                }

                for (int i = 0; i < pUBlist.Count; i++)
                {
                    for (int j = 0; j < interceptorList.Count; j++)
                    {
                        if (pUBlist[i].rectangle.Intersects(interceptorList[j].rectangle))
                        {
                            pUBlist[i].deleteMark = true;
                            interceptorList[j].health -= pUBlist[i].damage;
                        }
                    }
                }

                for (int i = 0; i < pNBlist.Count; i++)
                {
                    for (int j = 0; j < mediList.Count; j++)
                    {
                        if (pNBlist[i].rectangle.Intersects(mediList[j].rectangle))
                        {
                            pNBlist[i].deleteMark = true;
                            mediList[j].health -= pNBlist[i].damage;
                        }
                    }
                }

                for (int i = 0; i < pUBlist.Count; i++)
                {
                    for (int j = 0; j < mediList.Count; j++)
                    {
                        if (pUBlist[i].rectangle.Intersects(mediList[j].rectangle))
                        {
                            pUBlist[i].deleteMark = true;
                            mediList[j].health -= pUBlist[i].damage;
                        }
                    }
                }

                for (int i = 0; i < superBulletList.Count; i++)
                {
                    for (int j = 0; j < gruntList.Count; j++)
                    {
                        if (superBulletList[i].rectangle.Intersects(gruntList[j].rectangle))
                        {
                            gruntList[j].health -= superBulletList[i].damage;
                        }
                    }
                }
                for (int i = 0; i < superBulletList.Count; i++)
                {
                    for (int j = 0; j < interceptorList.Count; j++)
                    {
                        if (superBulletList[i].rectangle.Intersects(interceptorList[j].rectangle))
                        {
                            interceptorList[j].health -= superBulletList[i].damage;
                        }
                    }
                }
                if (superBulletList.Count > 0)
                {
                    for (int i = 0; i < superBulletList.Count; i++)
                    {
                        for (int j = 0; j < scoutList.Count; j++)
                        {
                            if (superBulletList[i].rectangle.Intersects(scoutList[j].rectangle))
                            {
                                scoutList[j].health -= superBulletList[i].damage;
                            }
                        }
                    }
                }

                //This next sequence will annoy you, but I wanted it working and it threw me annoying errors.
                if (laserList.Count > 0)
                {
                    foreach (grunt gruntHandling in gruntList)
                    {
                        if (gruntHandling.rectangle.Intersects(laserList[0].rectangle))
                        {
                            gruntHandling.health -= laserList[0].damage;
                        }
                    }

                    foreach (scout scoutHandling in scoutList)
                    {
                        if (scoutHandling.rectangle.Intersects(laserList[0].rectangle))
                        {
                            scoutHandling.health -= laserList[0].damage;
                        }
                    }

                    foreach (interceptor interceptorHandling in interceptorList)
                    {
                        if (interceptorHandling.rectangle.Intersects(laserList[0].rectangle))
                        {
                            interceptorHandling.health -= laserList[0].damage;
                        }
                    }

                    foreach (mediShip mediHandling in mediList)
                    {
                        if (mediHandling.rectangle.Intersects(laserList[0].rectangle))
                        {
                            mediHandling.health -= laserList[0].damage;
                        }
                    }
                }
                
                foreach (enemyNormalBullet bulletHandling in eNBlist)
                {
                    if (bulletHandling.rectangle.Intersects(Player1.rectangle))
                    {
                        bulletHandling.deleteMark = true;
                        if (!(Player1.ultraShoot))
                        {
                            Player1.health -= bulletHandling.damage;
                        }
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
                for (int i = 0; i < pUBlist.Count; i++)
                {
                    if (pUBlist[i].deleteMark == true)
                    {
                        pUBlist.RemoveAt(i);
                        i--;
                    }
                }
                for (int i = 0; i < gruntList.Count; i++)
                {
                    if (gruntList[i].health < 1)
                    {
                        explosionsList.Add(new ExplosionParticle(explosionTextureOne, new Vector2(gruntList[i].position.X + gruntList[i].rectangle.Width / 2, gruntList[i].position.Y + gruntList[i].rectangle.Height / 2), random));
                        explosionsList.Add(new ExplosionParticle(explosionTextureOne, new Vector2(gruntList[i].position.X + gruntList[i].rectangle.Width / 2, gruntList[i].position.Y + gruntList[i].rectangle.Height / 2), random));
                        explosionsList.Add(new ExplosionParticle(explosionTextureOne, new Vector2(gruntList[i].position.X + gruntList[i].rectangle.Width / 2, gruntList[i].position.Y + gruntList[i].rectangle.Height / 2), random));
                        gruntList.RemoveAt(i);
                        i--;
                        explodeSound.Play(0.2F,0,0);
                        score += gruntKillScore;
                    }
                }

                for (int i = 0; i < scoutList.Count; i++)
                {
                    if (scoutList[i].health < 0)
                    {
                        explosionsList.Add(new ExplosionParticle(explosionTextureOne, new Vector2(scoutList[i].position.X + scoutList[i].rectangle.Width / 2, scoutList[i].position.Y + scoutList[i].rectangle.Height / 2), random));
                        explosionsList.Add(new ExplosionParticle(explosionTextureOne, new Vector2(scoutList[i].position.X + scoutList[i].rectangle.Width / 2, scoutList[i].position.Y + scoutList[i].rectangle.Height / 2), random));
                        explosionsList.Add(new ExplosionParticle(explosionTextureOne, new Vector2(scoutList[i].position.X + scoutList[i].rectangle.Width / 2, scoutList[i].position.Y + scoutList[i].rectangle.Height / 2), random));
                        scoutList.RemoveAt(i);
                        i--;
                        explodeSound.Play(0.2F,0,0);
                        score += scoutKillScore;
                    }
                }

                for (int i = 0; i < interceptorList.Count; i++)
                {
                    if (interceptorList[i].health < 0)
                    {
                        for (int j = 200; j <= 340; j += 10)
                        {
                            eNBlist.Add(new enemyNormalBullet(interceptorList[i].position, new Vector2(interceptorList[i].rectangle.Width, interceptorList[i].rectangle.Height), eNBTexture, (float)(j * 0.0174532925)));
                        }
                        explosionsList.Add(new ExplosionParticle(explosionTextureOne, new Vector2(interceptorList[i].position.X + interceptorList[i].texture.Width / 2, interceptorList[i].position.Y + interceptorList[i].texture.Height / 2), random));
                        explosionsList.Add(new ExplosionParticle(explosionTextureOne, new Vector2(interceptorList[i].position.X + interceptorList[i].texture.Width / 2, interceptorList[i].position.Y + interceptorList[i].texture.Height / 2), random));
                        explosionsList.Add(new ExplosionParticle(explosionTextureOne, new Vector2(interceptorList[i].position.X + interceptorList[i].texture.Width / 2, interceptorList[i].position.Y + interceptorList[i].texture.Height / 2), random));
                        interceptorList.RemoveAt(i);
                        i--;
                        explodeSound.Play(0.2F,0,0);
                        score += interceptorKillScore;
                    }
                }

                for (int i = 0; i < mediList.Count; i++)
                {
                    if (mediList[i].health < 0)
                    {
                        explosionsList.Add(new ExplosionParticle(explosionTextureOne, new Vector2(mediList[i].position.X + mediList[i].texture.Width / 2, mediList[i].position.Y + mediList[i].texture.Height / 2), random));
                        explosionsList.Add(new ExplosionParticle(explosionTextureOne, new Vector2(mediList[i].position.X + mediList[i].texture.Width / 2, mediList[i].position.Y + mediList[i].texture.Height / 2), random));
                        explosionsList.Add(new ExplosionParticle(explosionTextureOne, new Vector2(mediList[i].position.X + mediList[i].texture.Width / 2, mediList[i].position.Y + mediList[i].texture.Height / 2), random));
                        mediList.RemoveAt(i);
                        i--;
                        explodeSound.Play(0.2F, 0, 0);
                        score += mediKillScore;
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
                    if (pNBlist[i].rectangle.X > screenDimensions.X || pNBlist[i].rectangle.X + pNBlist[i].texture.Width < 0 || pNBlist[i].rectangle.Y + pNBlist[i].texture.Height < 0 || pNBlist[i].rectangle.Y > screenDimensions.Y)
                    {
                        pNBlist.RemoveAt(i);
                        if (i > 0) { i--; } else { break; }
                    }
                }
                for (int i = 0; i < pUBlist.Count; i++)
                {
                    if (pUBlist[i].rectangle.X > screenDimensions.X || pUBlist[i].rectangle.X < 0 || pUBlist[i].rectangle.Y + pUBlist[i].texture.Height < 0 || pUBlist[i].rectangle.Y > screenDimensions.Y)
                    {
                        pUBlist.RemoveAt(i);
                        if (i > 0) { i--; } else { break; }
                    }
                }
                for (int i = 0; i < superBulletList.Count; i++)
                {
                    if (superBulletList[i].rectangle.X > screenDimensions.X || superBulletList[i].rectangle.X < 0)
                    {
                        superBulletList.RemoveAt(i);
                        if (i > 0) { i--; } else { break; }
                    }
                }
                for (int i = 0; i < eNBlist.Count; i++)
                {
                    if (eNBlist[i].rectangle.X > screenDimensions.X || eNBlist[i].rectangle.X < 0 || eNBlist[i].rectangle.Y + eNBlist[i].texture.Height < 0 || eNBlist[i].rectangle.Y > screenDimensions.Y)
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
                        score -= gruntKillScore * 5;
                        if (i > 0) { i--; } else { break; }
                    }
                }

                for (int i = 0; i < mediList.Count; i++)
                {
                    if (mediList[i].rectangle.X < -mediList[i].texture.Width)
                    {
                        mediList.RemoveAt(i);
                        score -= mediKillScore * 5;
                        if (i > 0) { i--; } else { break; }
                    }
                }

                for (int i = 0; i < scoutList.Count; i++)
                {
                    if (scoutList[i].rectangle.X < -scoutList[i].texture.Width)
                    {
                        scoutList.RemoveAt(i);
                        score -= scoutKillScore * 5;
                        if (i > 0) { i--; } else { break; }
                    }
                }

                for (int i = 0; i < interceptorList.Count; i++)
                {
                    if (interceptorList[i].rectangle.X < -interceptorList[i].texture.Width)
                    {
                        interceptorList.RemoveAt(i);
                        score -= interceptorKillScore * 5;
                        if (i > 0) { i--; } else { break; }
                    }
                }

                totalMilliTime += gameTime.ElapsedGameTime.Milliseconds;
                totalSecondTime = (int)((float)totalMilliTime / 1000);


                if (Player1.health < 1) 
                {
                    gameReset();
                    gameState = GameState.gameOver;
                }

            }

            else if (gameState == GameState.menu)
            {
                playButton.Update(mouseState);
                quitButton.Update(mouseState);
                if (playButton.clicked)
                {
                    gameState = GameState.main; 
                    score = 0; 
                    this.IsMouseVisible = false;
                    Mouse.SetPosition((int)(Player1.position.X + Player1.texture.Width / 2), (int)(Player1.position.Y + Player1.texture.Height / 2)); 
                    MediaPlayer.Play(level1);
                    level = 0;
                }
                if (quitButton.clicked) { this.Exit(); }
            }

            else if (gameState == GameState.gameOver)
            {
                if (WaitLetGo)
                {
                    returnButton.Update(mouseState);
                    quitButton.Update(mouseState);
                    if (returnButton.clicked)
                    {
                        gameState = GameState.main; score = 0; this.IsMouseVisible = false; Mouse.SetPosition((int)(Player1.position.X + Player1.texture.Width / 2), (int)(Player1.position.Y + Player1.texture.Height / 2)); returnButton.preclicked = false; returnButton.clicked = false; MediaPlayer.Play(level1);
                    }
                    if (quitButton.clicked) { this.Exit(); }
                }
                else if (mouseState.LeftButton == ButtonState.Released)
                {
                    WaitLetGo = true;
                }
            }

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
            if (gameState == GameState.main)
            {
                foreach (medBeam beam in beamList)
                {
                    beam.Draw(spriteBatch);
                }
                //Draw projectile lists
                foreach (playerNormalBullet bulletHandling in pNBlist)
                {
                    bulletHandling.Draw(spriteBatch);
                }
                foreach (enemyNormalBullet bulletHandling in eNBlist)
                {
                    bulletHandling.Draw(spriteBatch);
                }
                foreach (playerUltraBullet bulletHandling in pUBlist)
                {
                    bulletHandling.Draw(spriteBatch);
                }
                foreach (playerBigBullet bulletHandling in superBulletList)
                {
                    bulletHandling.Draw(spriteBatch);
                }

                //Draw enemy lists
                foreach (grunt gruntHandling in gruntList)
                {
                    gruntHandling.Draw(spriteBatch);
                }

                foreach (scout scoutHandling in scoutList)
                {
                    scoutHandling.Draw(spriteBatch);
                }
                foreach (interceptor interceptorHandling in interceptorList)
                {
                    interceptorHandling.Draw(spriteBatch);
                }
                foreach (mediShip mediHandling in mediList)
                {
                    mediHandling.Draw(spriteBatch);
                }

                //Draw Tentacles and other graphical effects
                foreach (playerTenticle tentacleHandling in playerTentacles)
                {
                    tentacleHandling.Draw(spriteBatch);
                }

                //Single Instance Draws
                Player1.draw(spriteBatch);
                healthBar.Draw(spriteBatch);
                laserBar.Draw(spriteBatch);
                ultraBar.Draw(spriteBatch);
                superBar.Draw(spriteBatch);
                adrenalineBar.Draw(spriteBatch);

                foreach (GiantLaser laser in laserList)
                {
                    if (laser.deathCountdown > 0)
                    {
                        laser.Draw(spriteBatch);
                    }
                }

                //Draw effects
                foreach (ExplosionParticle EPHandling in explosionsList)
                {
                    EPHandling.Draw(spriteBatch);
                }

                spriteBatch.DrawString(font, "Score: " + score.ToString(), new Vector2(screenDimensions.X - 100 - (score.ToString().Length * 10), 16), Color.White);
                spriteBatch.DrawString(font, "Time Elapsed: " + totalSecondTime.ToString(), new Vector2(screenDimensions.X - 175 - (10 * totalSecondTime.ToString().Length), 42), Color.White);
            }
            else if (gameState == GameState.menu)
            {
                spriteBatch.Draw(menuTexture, new Rectangle(0, 0, (int)screenDimensions.X, (int)screenDimensions.Y), Color.White);
                playButton.Draw(spriteBatch);
                quitButton.Draw(spriteBatch);
            }

            else if (gameState == GameState.gameOver)
            {
                spriteBatch.Draw(gameOverTexture, new Rectangle(0, 0, (int)screenDimensions.X, (int)screenDimensions.Y), Color.White);
                returnButton.Draw(spriteBatch);
                quitButton.Draw(spriteBatch);
                spriteBatch.DrawString(bigFont, "Your Score Was:" + score.ToString(), new Vector2((screenDimensions.X / 2) - (((48 * score.ToString().Length) / 2 + (6 * 48))), (int)(screenDimensions.Y * 0.45)), Color.White);
            }


            spriteBatch.End();

            base.Draw(gameTime);
        }

        public void gameReset()
        {
            playButton.clicked = false;
            playButton.preclicked = false;
            this.IsMouseVisible = true;
            pNBlist.Clear();
            pUBlist.Clear();
            eNBlist.Clear();
            explosionsList.Clear();
            interceptorList.Clear();
            scoutList.Clear();
            gruntList.Clear();
            mediList.Clear();
            laserList.Clear();
            totalSecondTime = 0;
            totalMilliTime = 0;
            MediaPlayer.Stop();
            superBulletList.Clear();
            playerTentacles.Clear();
            playerTentacles.Add(new playerTenticle(tenticleTexture, 100));
            playerTentacles.Add(new playerTenticle(tenticleTexture, 130));
            playerTentacles.Add(new playerTenticle(tenticleTexture, 160));
            Player1 = new player(playerTexture, screenDimensions, pNBTexture, laserTexture, UBTexture, BBTexture, playerTentacles);
            healthBar = new Bar(Content.Load<Texture2D>("white"), 20, 20, 50, 350, Player1.startingHealth, Color.White, true);
            laserBar = new Bar(Content.Load<Texture2D>("white"), 390, 50, 20, 250, Player1.laserReturn, Color.Red, false);
            ultraBar = new Bar(Content.Load<Texture2D>("white"), 660, 50, 20, 250, Player1.USreturn, Color.Purple, false);
            superBar = new Bar(Content.Load<Texture2D>("white"), 390, 20, 20, 250, Player1.superReturn, Color.Yellow, false);
            adrenalineBar = new Bar(Content.Load<Texture2D>("white"), 660, 20, 20, 250, Player1.adrenalineReturn, Color.Blue, false);
            healthBar.barColor = Color.Green;
            WaitLetGo = false;
            spawnControl.setup(gruntTexture, eNBTexture, scoutTexture, interceptorTexture, mediTexture, Content.Load<Texture2D>("beamSlice"));
            spawnControl.characterNo = 0;
            level = 0;
        }
    }
}
