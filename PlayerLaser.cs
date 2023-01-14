using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Stellar_Invaders
{
    internal class PlayerLaser : Entity
    {
        protected AnimatedSprite sprite;
        private Texture2D texture;
        public float angle { get; set; }
        public PlayerLaser(Texture2D texture, Vector2 position, Vector2 velocity) : base()
        {
            this.texture = texture;
            sprite = new AnimatedSprite(this.texture, 32, 32, 10);
            this.position = position;
            body.velocity = velocity;

            setupBoundingBox(sprite.frameWidth, sprite.frameHeight);
        }
        public new void Update(GameTime gameTime)
        {
            sprite.Update(gameTime);
            base.Update(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (isRotatable)
            {
                Rectangle destRect = new Rectangle((int)position.X, (int)position.Y, (int)(sprite.frameWidth * scale.X), (int)(sprite.frameHeight * scale.Y));
                spriteBatch.Draw(texture, destRect, sprite.sourceRect, Color.White, MathHelper.ToRadians(angle), sourceOrigin, SpriteEffects.None, 0);

            }
            else
            {
                Rectangle destRect = new Rectangle((int)position.X, (int)position.Y, (int)(sprite.frameWidth * scale.X), (int)(sprite.frameHeight * scale.Y));
                spriteBatch.Draw(texture, destRect, sprite.sourceRect, Color.White, MathHelper.ToRadians(angle), sourceOrigin, SpriteEffects.None, 0);
            }
        }
    }
}
