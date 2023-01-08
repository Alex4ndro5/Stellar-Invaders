using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Stellar_Invaders
{
    internal class BomberShip : Enemy
    {
        private int shootDelay = 60;
        private int shootTick = 0;
        public bool canShoot = false;
        public BomberShip(Texture2D texture, Vector2 position, Vector2 velocity) : base(texture, position, velocity)
        {
            
        }
        public override void Update(GameTime gameTime)
        {
            if (!canShoot)
            {
                if (shootTick < shootDelay)
                {
                    shootTick++;
                }
                else
                {
                    canShoot = true;
                }
            }

            sprite.Update(gameTime);

            base.Update(gameTime);
        }
        public void resetCanShoot()
        {
            canShoot = false;
            shootTick = 0;
        }
    }
}
