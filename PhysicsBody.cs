using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace Stellar_Invaders
{
    internal class PhysicsBody
    {
        public Vector2 velocity = new Vector2(0, 0);
        public Rectangle boundingBox;
    }
}
