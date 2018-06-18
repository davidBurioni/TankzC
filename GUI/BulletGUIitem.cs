using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TankzC
{
    class BulletGUIitem:GUIitem
    {
        protected int numBullets;
        protected TextObject numBulletsTxt;
        protected bool isInfinite;

        public bool IsAvailable { get; set; }

        public int NumBullets
        {
            get
            {
                return numBullets;
            }
            set
            {
                int oldVal = numBullets;
                numBullets = value;

                if (numBullets <= 0)//bullets finished
                {
                    IsAvailable = false;
                    SetColor(new Vector4(1.0f, 0, 0, -0.4f));
                    numBulletsTxt.IsActive = false;
                }
                else
                {// numBullets > 0
                    if (oldVal <= 0)
                    {
                        IsAvailable = true;
                        numBulletsTxt.IsActive = true;
                        SetColor(new Vector4(1, 1, 1, 1));
                    }
                    numBulletsTxt.Text = numBullets.ToString();
                }
                    
            }
        }
        public bool IsInfinite {
            get { return isInfinite; }
            set {
                isInfinite = value;
                numBulletsTxt.IsActive = !isInfinite;
            }
        }

        public BulletGUIitem(Vector2 position, string textureName, GameObject guiOwner, int numBullets, int width = 0, int height = 0) : base(position, textureName, guiOwner, width, height)
        {
            numBulletsTxt = new TextObject(new Vector2(position.X-20,position.Y), numBullets.ToString());
            NumBullets = numBullets;
        }

        public void DecrementBullets()
        {
            NumBullets = numBullets - 1;
        }
    }
}
