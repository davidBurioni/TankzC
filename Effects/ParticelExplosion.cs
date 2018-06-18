using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;
using Aiv.Fast2D;

namespace TankzC
{
    class ParticelExplosion : Effects
    {
        
        public ParticelExplosion(Vector2 spritePosition) : base(spritePosition,"particel",128,128)
        {
            //sprite.scale = new Vector2(0.8f, 0.8f);
            sprite.pivot = new Vector2(sprite.Width / 2, sprite.Height / 2);
            Animation.IsActive = true;
        }
    }
}
