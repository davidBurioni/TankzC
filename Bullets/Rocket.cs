using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TankzC
{
    class Rocket : Bullet
    {
        protected bool RocketIsOn;
        protected float accAngle;
        public Rocket() : base("rocket")
        {
            accAngle = 0.174533f;//10 deg
            Type = BulletManager.BulletType.Rocket;
            damage = 30;
        }

        public  void Reset()
        {
            RocketIsOn = false;
        }

        public override void Update()
        {
            
            base.Update();
            if (IsActive)
            {
                if (!RocketIsOn && (Math.Abs(sprite.Rotation)<=accAngle) || (Math.Abs(sprite.Rotation) >= Math.PI - accAngle) )
                {
                    RocketIsOn = true;
                    RigidBody.SetXVelocity(1900* Math.Sign(Velocity.X));

                }
            }
        }
    }
}
