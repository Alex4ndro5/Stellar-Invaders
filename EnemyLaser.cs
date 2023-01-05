using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace Stellar_Invaders
{
    internal class EnemyLaser : Entity
    {
        private Texture2D texture;
        public EnemyLaser(Texture2D texture, Vector2 position, Vector2 velocity) : base()
        {
            this.texture = texture;
            this.position = position;
            body.velocity = velocity;

            setupBoundingBox(this.texture.Width, this.texture.Height);
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
