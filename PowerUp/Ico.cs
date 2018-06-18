using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aiv.Fast2D;
using OpenTK;


namespace TankzC
{
    class Ico : GameObject
    {
        public WeaponsGUI Weapons { get; set; }

        public Ico(Vector2 spritePosition, string textureName, DrawManager.Layer drawLayer = DrawManager.Layer.Playground, int spriteWidth = 0, int spriteHeight = 0) : base(spritePosition, textureName, drawLayer, spriteWidth, spriteHeight)
        {
            RigidBody = new RigidBody(sprite.position, this);
            RigidBody.Type = (uint)PhysicsManager.ColliderType.PowerUp;
            RigidBody.SetCollisionMask((uint)PhysicsManager.ColliderType.Actor);

            //Weapons = new WeaponsGUI();

        }

        public override void OnCollide(Collision collisionInfo)
        {
            //base.OnCollide(collisionInfo);
            if (collisionInfo.Collider is Actor)
            {
                IsActive = false;
            }
        }
    }
}
