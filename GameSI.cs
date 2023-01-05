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
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        // Lista zawierajaca textury tła (background)
        private List<Texture2D> texBgs = new List<Texture2D>();
        // Obiekty zawierajacy tekstury
        private Texture2D texNautolanShipScout;
        private Texture2D texMainShip;
        // Obiekt zawierajacy wystrzal laseru
        public SoundEffect laser_shot;




        public GameSI()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here

            //Wczytujemy tekstury do obiektow
            texMainShip = Content.Load<Texture2D>(path + "\\MainShip\\MainShipBaseSlightdamage");
            texNautolanShipScout = Content.Load<Texture2D>(path + "\\Enemies\\NautolanShipBomberBase");
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}