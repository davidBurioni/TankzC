using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;
using Aiv.Fast2D;

namespace TankzC
{
    abstract class Effects : GameObject
    {
        public Effects(Vector2 spritePosition, string textureName, int spriteWidth = 0, int spriteHeight = 0) :
            base(spritePosition, textureName, DrawManager.Layer.Playground, spriteWidth, spriteHeight)
        {

        }

        public override void Draw()
        {
            if (IsActive)
                sprite.DrawTexture(texture, Animation.OffsetX, Animation.OffsetY, (int)sprite.Width, (int)sprite.Height);
        }

        void OnExplosionEndes()
        {
            DrawManager.RemoveItem(this);
            UpdateManager.RemoveItem(this);
        }



        public override void Update()
        {
            //base.Update();
            if (IsActive)
            {
                if (Animation.IsActive)
                {
                    Animation.Update();
                }
                else
                {
                    OnExplosionEndes();
                }
            }
        }
    }
}
