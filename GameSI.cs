using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;
using System.Collections.Generic;
using System;

namespace Stellar_Invaders
{
    public class GameSI : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private List<Texture2D> _textures = new List<Texture2D>();
        private Texture2D Starfield_01;
        private Texture2D Blue_Nebula_03;
        private Texture2D NautolanShipScoutBase;
        private Texture2D NautolanShipScout;
        private Texture2D NautolanShipScoutEngineEffect;
        private Texture2D PickupIconEnginesSuperchargedEngine;
        private Texture2D PickupIconEnginesBurstEngine;
        private Texture2D PickupIconEnginesBigPulseEngine;
        private Texture2D PickupIconEnginesBaseEngine;
        private Texture2D MainShipBaseFullhealth;
        private Texture2D MainShipBaseSmallhealth;



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