using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;

namespace TankzC
{
    class MiniPig : Bullet
    {   

        public MiniPig() : base("minipig")
        {
            //sprite.scale = new Vector2(1.5f, 1.5f);
            shootSpeed = 50;
            Type = BulletManager.BulletType.Minipig;
        }

        public override void Update()
        {
            base.Update();
        }
    }
}
