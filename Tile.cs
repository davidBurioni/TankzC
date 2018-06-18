using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;
using Aiv.Audio;
using Aiv.Fast2D;


namespace TankzC
{
    class Tile : Groundable
    {
        RocketIco rocket;
        PigIco pig;

        public Tile(Vector2 spritePosition, string textureName = "crate") : base(spritePosition, textureName, DrawManager.Layer.Playground)
        {
            RigidBody = new RigidBody(sprite.position, this);
            RigidBody.Type = (uint)PhysicsManager.ColliderType.Tile;
            RigidBody.SetCollisionMask((uint)PhysicsManager.ColliderType.Tile | (uint)PhysicsManager.ColliderType.Bullet);
            RigidBody.IsGravityAffected = true;

        }
        public override void OnCollide(Collision collisionInfo)
        {
            base.OnCollide(collisionInfo);
            if (collisionInfo.Collider.IsActive != true)
            {
                Particel();
                AudioManager.SetAudio("wood", 0.5f, 50);
            }
        }

        public virtual void OnBulletCollide(Bullet b)
        {
            IsActive = false;
            int percDrop = RandomGenerator.GetRandom(0, 101);

            if (percDrop < 10)
            {
                rocket = new RocketIco(Position, "rocketIco");
            }
            else if (percDrop < 5)
            {
                pig = new PigIco(Position, "pigIco");
            }
        }


        public void Particel()
        {
            Vector2 offsetVectore = new Vector2(sprite.position.X, sprite.position.Y);
            new ParticelExplosion(offsetVectore);
        }




    }
}
