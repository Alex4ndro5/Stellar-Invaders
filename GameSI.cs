using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;
using System.Collections.Generic;
using System;
using System.IO;

namespace Stellar_Invaders
{
    public class GameSI : Game
    {
        //Sciezka do plikow
        public string path = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName + "\\Content\\bin\\Assets";
        // Przygotowanie obiektow 
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;
        // Lista zawierajaca textury tła (background)
        private List<Texture2D> texBgs = new List<Texture2D>();
        // Obiekty zawierajacy tekstury
        private Texture2D texNautolanShipScout;
        private Texture2D texNautolanShipBomber;
        private Texture2D texNautolanShipDreadnought;
        private Texture2D texMainShip;
        private Texture2D texMainShipLaserProjectileBSG;
        private Texture2D texBtnPlay;
        private Texture2D texBtnPlayDown;
        private Texture2D texBtnPlayHover;
        private Texture2D texBtnRestart;
        private Texture2D texBtnRestartDown;
        private Texture2D texBtnRestartHover;
        private Texture2D texExplosion;

        public SoundEffect sndBtnDown;
        public SoundEffect sndBtnOver;
        // Obiekt zawierajacy wystrzal laseru
        public SoundEffect laser_shot;
        public SoundEffect sndExplode;
        // Obiekt zawierajacy czcionke
        private SpriteFont fontArial;
        /// <summary>
        /// Enumerat statusu gry: 0 - Menu, 1 - Gameplay, 2 - Gameover
        /// </summary>
        enum GameState
        {
            MainMenu,
            Gameplay,
            GameOver
        }
        /// <summary>
        /// Przetrzymuje status gry
        /// </summary>
        private GameState gameState;
        public static int randInt(int minNumber, int maxNumber)
        {
            return new Random().Next(minNumber, maxNumber);
        }

        public static float randFloat(float minNumber, float maxNumber)
        {
            return (float)new Random().NextDouble() * (maxNumber - minNumber) + minNumber;
        }

        public GameSI()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }


        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            graphics.PreferredBackBufferWidth = 480;
            graphics.PreferredBackBufferHeight = 640;
            graphics.ApplyChanges();

            IsMouseVisible = true;
            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here

            //Wczytywanie teł
            for (int i = 1; i < 3; i++)
            {
                texBgs.Add(Content.Load<Texture2D>(path + "\\Background\\Starfield_" + i));
            }

            //Wczytujemy tekstury do obiektow
            texMainShip = Content.Load<Texture2D>(path + "\\MainShip\\MainShipBaseFullHealth");
            texNautolanShipScout = Content.Load<Texture2D>(path + "\\Enemies\\NautolanShipScoutBase");
            texNautolanShipBomber = Content.Load<Texture2D>(path + "\\Enemies\\NautolanShipBomberBase");
            texNautolanShipDreadnought = Content.Load<Texture2D>(path + "\\Enemies\\NautolanShipDreadnoughtBase");
            texExplosion = Content.Load<Texture2D>(path + "\\Enemies\\AsteroidFlame");

            //Wczytywanie laserów
            texMainShipLaserProjectileBSG = Content.Load<Texture2D>(path + "\\MainShip\\MainshipweaponProjectileBigSpaceGun");

            //Tekstu do przyciskow
            texBtnPlay = Content.Load<Texture2D>(path + "\\GUI\\sprBtnPlay");
            texBtnPlayDown = Content.Load<Texture2D>(path + "\\GUI\\sprBtnPlayDown");
            texBtnPlayHover = Content.Load<Texture2D>(path + "\\GUI\\sprBtnPlayHover");

            texBtnRestart = Content.Load<Texture2D>(path + "\\GUI\\sprBtnRestart");
            texBtnRestartDown = Content.Load<Texture2D>(path + "\\GUI\\sprBtnRestartDown");
            texBtnRestartHover = Content.Load<Texture2D>(path + "\\GUI\\sprBtnRestartHover");

            // Load sounds
            sndBtnDown = Content.Load<SoundEffect>(path + "\\SpaceMusicPack\\sndBtnDown");
            sndBtnOver = Content.Load<SoundEffect>(path + "\\SpaceMusicPack\\sndBtnOver");
            laser_shot = Content.Load<SoundEffect>(path + "\\SpaceMusicPack\\laser_shot");
            sndExplode = Content.Load<SoundEffect>(path + "\\SpaceMusicPack\\scream");

            // Load sprite fonts
            fontArial = Content.Load<SpriteFont>("arialHeading");
            scrollingBackground = new ScrollingBackground(texBgs);
            playButton = new MenuButton(this, new Vector2(graphics.PreferredBackBufferWidth * 0.5f - (int)(texBtnPlay.Width * 0.5), graphics.PreferredBackBufferHeight * 0.5f), texBtnPlay, texBtnPlayDown, texBtnPlayHover); restartButton = new MenuButton(this, new Vector2(graphics.PreferredBackBufferWidth * 0.5f - (int)(texBtnPlay.Width * 0.5), graphics.PreferredBackBufferHeight * 0.5f), texBtnRestart, texBtnRestartDown, texBtnRestartHover);

            changeGameState(GameState.MainMenu);
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            keyState = Keyboard.GetState();

            scrollingBackground.Update(gameTime);

            switch (gameState)
            {
                case GameState.MainMenu:
                    {
                        UpdateMainMenu(gameTime);
                        break;
                    }

                case GameState.Gameplay:
                    {
                        UpdateGameplay(gameTime);
                        break;
                    }

                case GameState.GameOver:
                    {
                        UpdateGameOver(gameTime);
                        break;
                    }
            }

            base.Update(gameTime);
        }

        //Kiedy myszka na przycisku, pojawi się inny obraz przycisku i muzyczka
        private void UpdateMainMenu(GameTime gameTime)
        {
            if (playButton.isActive)
            {
                MouseState mouseState = Mouse.GetState();

                if (playButton.boundingBox.Contains(mouseState.Position))
                {
                    if (mouseState.LeftButton == ButtonState.Pressed)
                    {
                        playButton.SetDown(true);
                        playButton.SetHovered(false);
                    }
                    else
                    {
                        playButton.SetDown(false);
                        playButton.SetHovered(true);
                    }

                    if (mouseState.LeftButton == ButtonState.Released && playButton.lastIsDown)
                    {
                        changeGameState(GameState.Gameplay);
                    }
                }
                else
                {
                    playButton.SetDown(false);
                    playButton.SetHovered(false);
                }

                playButton.lastIsDown = mouseState.LeftButton == ButtonState.Pressed ? true : false;
            }
            else
            {
                playButton.isActive = true;
            }

        }

        //Określanie restart timera, ruchów po planszy, zakresu ruchu, 
        private void UpdateGameplay(GameTime gameTime)
        {
            if (player == null)
            {
                player = new Player(texMainShip, new Vector2(graphics.PreferredBackBufferWidth * 0.5f, graphics.PreferredBackBufferHeight * 0.5f));
            }
            else
            {
                player.body.velocity = new Vector2(0, 0);

                if (player.isDead())
                {
                    if (restartTick < restartDelay)
                    {
                        restartTick++;
                    }
                    else
                    {
                        changeGameState(GameState.GameOver);
                        restartTick = 0;
                    }
                }
                else
                {
                    if (keyState.IsKeyDown(Keys.W))
                    {
                        player.MoveUp();
                    }
                    if (keyState.IsKeyDown(Keys.S))
                    {
                        player.MoveDown();
                    }
                    if (keyState.IsKeyDown(Keys.A))
                    {
                        player.MoveLeft();
                    }
                    if (keyState.IsKeyDown(Keys.D))
                    {
                        player.MoveRight();
                    }
                    if (keyState.IsKeyDown(Keys.Space))
                    {
                        if (playerShootTick < playerShootDelay)
                        {
                            playerShootTick++;
                        }
                        else
                        {
                            laser_shot.Play();
                            PlayerLaser laser = new PlayerLaser(texMainShipLaserProjectileBSG, new Vector2(player.position.X + player.destOrigin.X, player.position.Y), new Vector2(0, -10));
                            playerLasers.Add(laser);
                            playerShootTick = 0;
                        }
                    }
                }

                player.Update(gameTime);

                player.position.X = MathHelper.Clamp(player.position.X, 0, graphics.PreferredBackBufferWidth - player.body.boundingBox.Width);
                player.position.Y = MathHelper.Clamp(player.position.Y, 0, graphics.PreferredBackBufferHeight - player.body.boundingBox.Height);
            }


            for (int i = 0; i < playerLasers.Count; i++)
            {
                playerLasers[i].Update(gameTime);

                if (playerLasers[i].position.Y < 0)
                {
                    playerLasers.Remove(playerLasers[i]);
                    continue;
                }
            }

            for (int i = 0; i < enemyLasers.Count; i++)
            {
                enemyLasers[i].Update(gameTime);

                if (player != null)
                {
                    if (!player.isDead())
                    {
                        if (player.body.boundingBox.Intersects(enemyLasers[i].body.boundingBox))
                        {
                            sndExplode.Play();
                            Explosion explosion = new Explosion(texExplosion, new Vector2(player.position.X + player.destOrigin.X, player.position.Y + player.destOrigin.Y));
                            explosions.Add(explosion);

                            player.setDead(true);
                        }
                    }
                }

                if (enemyLasers[i].position.Y > GraphicsDevice.Viewport.Height)
                {
                    enemyLasers.Remove(enemyLasers[i]);
                }

            }

            for (int i = 0; i < enemies.Count; i++)
            {
                enemies[i].Update(gameTime);

                if (player != null)
                {
                    if (!player.isDead())
                    {
                        if (player.body.boundingBox.Intersects(enemies[i].body.boundingBox))
                        {
                            sndExplode.Play();
                            Explosion explosion = new Explosion(texExplosion, new Vector2(player.position.X + player.destOrigin.X, player.position.Y + player.destOrigin.Y));
                            explosions.Add(explosion);

                            player.setDead(true);
                        }

                        if (enemies[i].GetType() == typeof(BomberShip))
                        {
                            BomberShip enemy = (BomberShip)enemies[i];

                            if (enemy.canShoot)
                            {
                                EnemyLaser laser = new EnemyLaser(texMainShipLaserProjectileBSG, new Vector2(enemy.position.X, enemy.position.Y), new Vector2(0, 5));
                                enemyLasers.Add(laser);

                                enemy.resetCanShoot();
                            }
                        }
                        if (enemies[i].GetType() == typeof(ScoutShip))
                        {
                            ScoutShip enemy = (ScoutShip)enemies[i];

                            if (Vector2.Distance(enemies[i].position, player.position + player.destOrigin) < 320)
                            {
                                enemy.SetState(ScoutShip.States.CHASE);
                            }

                            if (enemy.GetState() == ScoutShip.States.CHASE)
                            {
                                Vector2 direction = (player.position + player.destOrigin) - enemy.position;
                                direction.Normalize();

                                float speed = 3;
                                enemy.body.velocity = direction * speed;

                                if (enemy.position.X + (enemy.destOrigin.X) < player.position.X + (player.destOrigin.X))
                                {
                                    enemy.angle = enemy.angle - 5;
                                }
                                else
                                {
                                    enemy.angle = enemy.angle + 5;
                                }
                            }
                        }
                    }
                }

                if (enemies[i].position.Y > GraphicsDevice.Viewport.Height)
                {
                    enemies.Remove(enemies[i]);
                }
            }

            for (int i = 0; i < explosions.Count; i++)
            {
                explosions[i].Update(gameTime);

                if (explosions[i].sprite.isFinished())
                {
                    explosions.Remove(explosions[i]);
                }
            }

            for (int i = 0; i < playerLasers.Count; i++)
            {
                bool shouldDestroyLaser = false;
                for (int j = 0; j < enemies.Count; j++)
                {
                    if (playerLasers[i].body.boundingBox.Intersects(enemies[j].body.boundingBox))
                    {
                        sndExplode.Play();

                        Explosion explosion = new Explosion(texExplosion, new Vector2(enemies[j].position.X, enemies[j].position.Y));
                        explosion.scale = enemies[j].scale;

                        Console.WriteLine("Shot enemy.  Origin: " + enemies[j].destOrigin + ", pos: " + enemies[j].position);

                        explosion.position.Y += enemies[j].body.boundingBox.Height * 0.5f;
                        explosions.Add(explosion);

                        enemies.Remove(enemies[j]);

                        shouldDestroyLaser = true;
                    }
                }

                if (shouldDestroyLaser)
                {
                    playerLasers.Remove(playerLasers[i]);
                }
            }


            // Enemy spawning
if (spawnEnemyTick < spawnEnemyDelay)
{
	spawnEnemyTick++;
}
else
{
	Enemy enemy = null;
           	 
	if (randInt(0, 10) <= 3)
	{
    	Vector2 spawnPos = new Vector2(randFloat(0, graphics.PreferredBackBufferWidth), -128);
    	enemy = new ScoutShip(texNautolanShipScout, spawnPos, new Vector2(0, randFloat(1, 3)));
	}
	else if (randInt(0, 10) >= 5)
	{
    	Vector2 spawnPos = new Vector2(randFloat(0, graphics.PreferredBackBufferWidth), -128);
    	enemy = new BomberShip(texNautolanShipBomber, spawnPos, new Vector2(0, randFloat(1, 3)));
	}
	else
	{
    	Vector2 spawnPos = new Vector2(randFloat(0, graphics.PreferredBackBufferWidth), -128);
    	enemy = new DreadnoughtShip(texNautolanShipDreadnought, spawnPos, new Vector2(0, randFloat(1, 3)));
	}

	enemies.Add(enemy);

	spawnEnemyTick = 0;
}

        }

        private void UpdateGameOver(GameTime gameTime)
        {
            if (restartButton.isActive)
            {
                MouseState mouseState = Mouse.GetState();

                if (restartButton.boundingBox.Contains(mouseState.Position))
                {
                    if (mouseState.LeftButton == ButtonState.Pressed)
                    {
                        restartButton.SetDown(true);
                        restartButton.SetHovered(false);
                    }
                    else
                    {
                        restartButton.SetDown(false);
                        restartButton.SetHovered(true);
                    }

                    if (mouseState.LeftButton == ButtonState.Released && restartButton.lastIsDown)
                    {
                        changeGameState(GameState.Gameplay);
                    }
                }
                else
                {
                    restartButton.SetDown(false);
                    restartButton.SetHovered(false);
                }

                restartButton.lastIsDown = mouseState.LeftButton == ButtonState.Pressed ? true : false;
            }
            else
            {
                restartButton.isActive = true;
            }

        }
        private void resetGameplay()
        {
            if (player != null)
            {
                player.setDead(false);
                player.position = new Vector2((int)(graphics.PreferredBackBufferWidth * 0.5), (int)(graphics.PreferredBackBufferHeight * 0.5));
            }
        }

        //Czyszczenie list obiektów, zmiana stanu gry 

        private void changeGameState(GameState gameState)
        {
            playButton.isActive = false;
            restartButton.isActive = false;
            explosions.Clear();
            enemies.Clear();
            playerLasers.Clear();
            enemyLasers.Clear();
            resetGameplay();

            gameState = gameState;
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            // TODO: Add your drawing code here
            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointWrap);
            
            scrollingBackground.Draw(spriteBatch);

            switch (gameState)
            {
                case GameState.MainMenu:
                    {
                        DrawMainMenu(spriteBatch);
                        break;
                    }

                case GameState.Gameplay:
                    {
                        DrawGameplay(spriteBatch);
                        break;
                    }

                case GameState.GameOver:
                    {
                        DrawGameOver(spriteBatch);
                        break;
                    }
            }

            spriteBatch.End();

            base.Draw(gameTime);
        }
        //Poruszanie się i definicja przycisków start i restart
        private KeyboardState keyState = Keyboard.GetState();
        private MenuButton playButton;
        private MenuButton restartButton;

        //Liczenie eksplozji, wrogów, laserów
        private List<Explosion> explosions = new List<Explosion>();
        private List<Enemy> enemies = new List<Enemy>();
        private List<EnemyLaser> enemyLasers = new List<EnemyLaser>();
        private List<PlayerLaser> playerLasers = new List<PlayerLaser>();
        private Player player = null;
        private ScrollingBackground scrollingBackground;

        //Timer, po tym jak gracz zostanie zniszczony 
        private int restartDelay = 60 * 2;
        private int restartTick = 0;

        //Czas pojawiania się wroga
        private int spawnEnemyDelay = 60;
        private int spawnEnemyTick = 0;

        //Odstęp pomiędzy pociskami, które wystrzeliwuje gracz 
        private int playerShootDelay = 15;
        private int playerShootTick = 0;

        private void DrawMainMenu(SpriteBatch spriteBatch)
        {
            string title = "SPACE SHOOTER";
            spriteBatch.DrawString(fontArial, title, new Vector2(graphics.PreferredBackBufferWidth * 0.5f - (fontArial.MeasureString(title).X * 0.5f), graphics.PreferredBackBufferHeight * 0.2f), Color.White);

            playButton.Draw(spriteBatch);
        }

        private void DrawGameplay(SpriteBatch spriteBatch)
        {
            for (int i = 0; i < enemies.Count; i++)
            {
                enemies[i].Draw(spriteBatch);
            }

            for (int i = 0; i < playerLasers.Count; i++)
            {
                playerLasers[i].Draw(spriteBatch);
            }

            for (int i = 0; i < enemyLasers.Count; i++)
            {
                enemyLasers[i].Draw(spriteBatch);
            }

            for (int i = 0; i < explosions.Count; i++)
            {
                explosions[i].Draw(spriteBatch);
            }

            if (player != null)
            {
                player.Draw(spriteBatch);
            }
        }

        private void DrawGameOver(SpriteBatch spriteBatch)
        {
            string title = "GAME OVER";
            spriteBatch.DrawString(fontArial, title, new Vector2(graphics.PreferredBackBufferWidth * 0.5f - (fontArial.MeasureString(title).X * 0.5f), graphics.PreferredBackBufferHeight * 0.2f), Color.White);

            restartButton.Draw(spriteBatch);
        }


    }
}