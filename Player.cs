using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Stellar_Invaders
{
    internal class Player : Entity
    {
        public Texture2D texture;
        public AnimatedSprite sprite;
        public float moveSpeed = 4;
        private bool _dead = false;
        public bool isDead() { return _dead; }
        public void setDead(bool isDead) { _dead = isDead; }
        public Player(Texture2D texture, Vector2 position) : base()
        {
            this.texture = texture;
            sprite = new AnimatedSprite(this.texture, 48, 48, 10);
            this.position = position;
            sourceOrigin = new Vector2(sprite.frameWidth * 0.5f, sprite.frameHeight * 0.5f);
            destOrigin = new Vector2((sprite.frameWidth * 0.5f) * scale.X, (sprite.frameHeight * 0.5f) * scale.Y);
            setupBoundingBox(sprite.frameWidth, sprite.frameHeight);
        }
        public void MoveUp()
        {
            body.velocity.Y = -moveSpeed;
        }

        public void MoveDown()
        {
            body.velocity.Y = moveSpeed;
        }

        public void MoveLeft()
        {
            body.velocity.X = -moveSpeed;
        }

        public void MoveRight()
        {
            body.velocity.X = moveSpeed;
        }
        public void Update(GameTime gameTime)
        {
            sprite.Update(gameTime);

            base.Update(gameTime);
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            if (!_dead)
            {
                Rectangle destRect = new Rectangle((int)position.X, (int)position.Y, (int)(sprite.frameWidth * scale.X), (int)(sprite.frameHeight * scale.Y));
                spriteBatch.Draw(texture, destRect, sprite.sourceRect, Color.White);
            }
        }
    }
}
