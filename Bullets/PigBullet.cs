using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;

namespace TankzC
{
    class PigBullet : Bullet
    {
        protected bool RocketIsOn;
        protected float pigCounter;
        protected float accAngle;

        protected int pigShootCounter=0;


        public PigBullet() : base("pig")
        {
            //sprite.scale = new Vector2(0.7f, 0.7f);
            pigCounter = 0;
            accAngle = 0.174533f;//10 deg

            Type = BulletManager.BulletType.PigBullet;
        }

      

        public void Shootchild(BulletManager.BulletType type, Vector2 position)
        {
            Bullet b = BulletManager.GetBullet(type);
            if (b != null)
            {
                Vector2 direction = new Vector2(0, 10);
                b.Shoot(position, direction);
            }
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
                if (!RocketIsOn && (Math.Abs(sprite.Rotation) <= accAngle) || (Math.Abs(sprite.Rotation) >= Math.PI - accAngle))
                {
                    sprite.Rotation = 0;                
                    RigidBody.SetXVelocity(900 * Math.Sign(Velocity.X));

                    BulletManager.BulletType current = BulletManager.BulletType.Minipig;

                    if (pigShootCounter <=3)
                    {
                        float random = (float)RandomGenerator.GetRandom(-200, 200);
                        Shootchild(current, new Vector2(sprite.position.X + random,sprite.position.Y + 30));
                
                        pigCounter = 0f;
                        pigShootCounter++;
                    }
                    else
                    {
                        OnDie();
                    }

                }
            }
        }
    }
}

