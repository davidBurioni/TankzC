using Aiv.Fast2D;
using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TankzC
{
    class Background : GameObject
    {
        protected Sprite bottomSprite;
        public float ScrollX { get; set; }
        public Background(string fileName, Vector2 spritePosition, float scrollSpeed=0 ) : base(spritePosition, fileName, DrawManager.Layer.Background)
        {
            sprite.pivot = new Vector2(70,750);
            //sprite.scale = new Vector2(2, 2);
            bottomSprite = new Sprite(texture.Width, texture.Height);
            bottomSprite.position.X = sprite.position.X ;
            bottomSprite.position.Y = sprite.position.Y - 747;
            ScrollX = scrollSpeed;
        }

        public override void Update()
        {
            if (IsActive)
            {
                sprite.position.X += ScrollX * Game.DeltaTime;

                bottomSprite.position.X = sprite.position.X + Width - 70;

                //if (sprite.position.X <= -Width)
                //{
                //    sprite.position.X = bottomSprite.position.X + Width;
                //    Sprite first = sprite;
                //    sprite = bottomSprite;
                //    bottomSprite = first;
                //}
            }
        }

        public override void SetCamera(Camera camera)
        {
            base.SetCamera(camera);
            bottomSprite.Camera = camera;
        }

        public override void Draw()
        {
            base.Draw();
            bottomSprite.DrawTexture(texture);
        }
    }
}
