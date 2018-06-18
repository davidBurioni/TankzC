using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;

namespace TankzC
{
    class Stone : Tile
    {
        public Stone(Vector2 spritePosition, string textureName = "crate") : base(spritePosition, textureName)
        {
            //IsGrounded = false;
            RigidBody.IsGravityAffected = false;
            RigidBody.IsCollisionsAffected = true;
            RigidBody.Type = (uint)PhysicsManager.ColliderType.Tile;
            RigidBody.SetCollisionMask((uint)PhysicsManager.ColliderType.Tile);
        }

        public override void OnBulletCollide(Bullet b)
        {
            //base.OnBulletCollide(b);
        }
    }
}
