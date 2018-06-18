using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;
using Aiv.Fast2D;

namespace TankzC
{
    class WeaponsGUI : GameObject
    {
        
        protected BulletGUIitem[] weapons;
        protected string[] textureNames = { "bulletIco", "pigIco", "rocketIco" };
        protected int selectedWeapon;

        protected Sprite selection;
        protected Texture selectionTexture;
        protected float itemWidth = 32.0f;

        public int SelectedWeapon
        {
            get
            {
                return selectedWeapon;
            }
            set
            {
                selectedWeapon = value;
                selection.position = weapons[selectedWeapon].Position;
            }

        }
        public WeaponsGUI(Vector2 position, int width = 0, int height = 0) : base(position, "weaponFrame", DrawManager.Layer.GUI, width, height)
        {

            sprite.pivot = Vector2.Zero;
            sprite.Camera = CameraManager.GetCamera("GUI");
            weapons = new BulletGUIitem[(int)BulletManager.BulletType.Minipig];

            float yPos = this.Position.Y + this.Height / 2;

            for (int i = 0; i < weapons.Length; i++)
            {
                weapons[i] = new BulletGUIitem(new Vector2(this.Position.X + 7 + (itemWidth/2) + i * (itemWidth + 4), yPos), textureNames[i], this,2, (int)itemWidth, (int)itemWidth);

                if(i==0)
                {//first standard weapon
                    //weapons[i].NumBullets = 1;
                    weapons[i].IsSelected = true;
                    weapons[i].IsAvailable = true;
                    weapons[i].IsInfinite = true;
                }
            }

            //selectionTexture = GfxManager.GetSpritesheet("weaponSelection");
            Tuple<Texture, List<Animation>> aa = GfxManager.GetSpritesheet("weaponSelection");
            selectionTexture = aa.Item1;

            selection = new Sprite(selectionTexture.Width, selectionTexture.Height);
            selection.pivot = new Vector2(selection.Width/2, selection.Height/2);
            SelectedWeapon = 0;
            selection.Camera = CameraManager.GetCamera("GUI");

        }

        public override void Draw()
        {
            base.Draw();
            //for (int i = 0; i < weapons.Length; i++)
            //    weapons[i].Draw();

            selection.DrawTexture(selectionTexture);
        }

        public BulletManager.BulletType NextWeapon(int direction=1)
        {
            int currWeapon = selectedWeapon;
            do
            {
                selectedWeapon += direction;
                if (selectedWeapon >= weapons.Length)
                    selectedWeapon = 0;
                else if (selectedWeapon < 0)
                    selectedWeapon = weapons.Length - 1;
            } while (weapons[selectedWeapon].IsAvailable==false && selectedWeapon != currWeapon);

            //selection.position = weapons[selectedWeapon].Position;

            //in order to update selection sprite position
            SelectedWeapon = selectedWeapon;

            return (BulletManager.BulletType)selectedWeapon;
        }

        public BulletManager.BulletType DecrementBullets()
        {
            if (!weapons[selectedWeapon].IsInfinite)
            {
                weapons[selectedWeapon].DecrementBullets();

                if (!weapons[selectedWeapon].IsAvailable)
                {//no more available
                    SelectedWeapon = (int)NextWeapon();
                    //selection.position = weapons[selectedWeapon].Position;
                }
            }

            return (BulletManager.BulletType)selectedWeapon;
        }

        public void AddBullets(BulletManager.BulletType type, int numBullets)
        {
            weapons[(int)type].NumBullets=weapons[(int)type].NumBullets + numBullets;
        }
    }
}
