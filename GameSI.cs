﻿using Microsoft.Xna.Framework;
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
        // Obiekt zawierajacy wystrzal laseru
        public SoundEffect laser_shot;
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
        private GameState _gameState;
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

            //Wczytujemy tekstury do obiektow
            texMainShip = Content.Load<Texture2D>(path + "\\MainShip\\MainShipBaseFullHealth");
            texNautolanShipScout = Content.Load<Texture2D>(path + "\\Enemies\\NautolanShipScoutBase");
            texNautolanShipBomber = Content.Load<Texture2D>(path + "\\Enemies\\NautolanShipBomberBase");
            texNautolanShipDreadnought = Content.Load<Texture2D>(path + "\\Enemies\\NautolanShipDreadnoughtBase");

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
            switch (_gameState)
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
        private void UpdateMainMenu(GameTime gameTime)
        {


        }

        private void UpdateGameplay(GameTime gameTime)
        {

        }

        private void UpdateGameOver(GameTime gameTime)
        {


        }
        private void resetGameplay()
        {

        }

        private void changeGameState(GameState gameState)
        {

        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            // TODO: Add your drawing code here
            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointWrap);

            switch (_gameState)
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


        }

        private void DrawGameplay(SpriteBatch spriteBatch)
        {


        }

        private void DrawGameOver(SpriteBatch spriteBatch)
        {


        }


    }
}