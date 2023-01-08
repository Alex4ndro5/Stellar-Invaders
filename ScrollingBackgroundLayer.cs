using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;


namespace Stellar_Invaders
{
    /// <summary>
    /// Klasa do ScrollingBackground, dziedzicząca po Entity 
    /// </summary>
    internal class ScrollingBackgroundLayer : Entity
    {
        private ScrollingBackground scrollingBackground;
        private Texture2D texture;
        public Texture2D getTexture() { return texture; }
        public int depth = 0;
        public int positionIndex = 0;
        public Vector2 initialPosition;

        public ScrollingBackgroundLayer(ScrollingBackground scrollingBackground, Texture2D texture, int depth, int positionIndex, Vector2 position, Vector2 velocity) : base()
        {
            this.scrollingBackground = scrollingBackground;
            this.texture = texture;
            this.depth = depth;
            this.positionIndex = positionIndex;
            this.position = position;
            initialPosition = this.position;
            body.velocity = velocity;
        }

        public new void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, Color.White);
        }

    }
}
