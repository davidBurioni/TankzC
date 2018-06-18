using Aiv.Fast2D;
using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TankzC
{
    class Player : Tank
    {
        protected float shootDelay;
        protected Bar loadingBar;
        protected Bar nrgBar;
        protected int playerNum;
        protected int joystickIndex;

        protected Vector2 barOffset;

        protected float currentLoadingVal;
        protected float maxLoadingVal;
        protected float loadIncrease;

        protected StateMachine stateMachine;

        protected bool IsSpacePressed;
        protected bool IsTabPressed;


        public bool IsLoading { get; protected set; }
        public Bullet LastShotBullet { get; protected set; }
        protected bool IsCPressed;
        public WeaponsGUI Weapons { get; set; }
        protected List<WeaponsGUI> weaponsGUI;



        public Player(string fileName, Vector2 spritePosition, int playerIndex = 0) : base(spritePosition, fileName)
        {
            //sprite.scale = new Vector2(0.3f, 0.3f);

            playerNum = playerIndex;
            joystickIndex = playerIndex;

            horizontalSpeed = 300;
            shootDelay = 0.45f;
            //Nrg = MaxNrg;
            loadingBar = new Bar(new Vector2(20, 20), "playerBar");
            loadingBar.BarOffset = new Vector2(4, 4);
            loadingBar.IsActive = false;

            nrgBar = new Bar(new Vector2(30 + 250 * playerNum, 50), "playerBar", MaxNrg);
            nrgBar.BarOffset = new Vector2(4, 4);
            nrgBar.SetCamera(CameraManager.GetCamera("GUI"));

            Nrg = 100;

            barOffset = new Vector2(-loadingBar.Width / 2, -150);

            maxLoadingVal = 100;
            loadIncrease = 80;

            currentBulletType = BulletManager.BulletType.StdBullet;
            //currentBulletType2 = BulletManager.BulletType.PigBullet;

            joystickIndex = 0;

            stateMachine = new StateMachine(this);
            stateMachine.RegisterState((int)States.Play, new PlayState());
            stateMachine.RegisterState((int)States.Shoot, new ShootState());
            stateMachine.RegisterState((int)States.Wait, new WaitState());
            weaponsGUI = new List<WeaponsGUI>();

            weaponsGUI.Add(new WeaponsGUI(new Vector2(Position.X - 150, Game.window.Height - 50)));
            Weapons = weaponsGUI.Last();

        }

        public virtual void Play()
        {
            stateMachine.Switch((int)States.Play);
            shootCounter = 0;
        }

        public virtual void Wait()
        {
            stateMachine.Switch((int)States.Wait);
        }
        protected override void SetNrg(float newValue)
        {
            base.SetNrg(newValue);
            nrgBar.SetValue(nrg);
        }

        public override Bullet Shoot(BulletManager.BulletType type, float speedPercentage = 1)
        {
            LastShotBullet = base.Shoot(type, speedPercentage);
            shootCounter = shootDelay;
            currentBulletType = Weapons.DecrementBullets();
            stateMachine.Switch((int)States.Shoot);

            return LastShotBullet;
        }

        public override bool AddDamage(float damage)
        {
            bool isDead = base.AddDamage(damage);
            //SetNrg(nrg - damage);
            if (isDead)
            {
                nrgBar.SetValue(0);
            }
            return isDead;
        }

        public override void OnDie()
        {
            //base.OnDie();
            Game.CurrentScene.IsPlaying = false;
        }

        protected void StartLoading()
        {
            loadingBar.IsActive = true;
            IsLoading = true;
            currentLoadingVal = 0;
            loadingBar.Position = Position + barOffset;
        }

        protected void StopLoading()
        {
            loadingBar.IsActive = false;
            IsLoading = false;
        }

        public virtual void UpdateFSM()
        {
            stateMachine.Run();
        }

        public override void Update()
        {
            base.Update();

            if (IsLoading)
            {
                currentLoadingVal += Game.window.deltaTime * loadIncrease;

                if (currentLoadingVal > maxLoadingVal)
                {
                    currentLoadingVal = maxLoadingVal;
                    loadIncrease = -loadIncrease;
                }
                else if (currentLoadingVal < 0)
                {
                    currentLoadingVal = 0;
                    loadIncrease = -loadIncrease;
                }

                loadingBar.SetValue(currentLoadingVal);
            }
            //if()
        }
        public override void OnCollide(Collision collisionInfo)
        {
            base.OnCollide(collisionInfo);
            if (collisionInfo.Collider is StdBullet)
            {
                AddDamage(100);
            }
            if (collisionInfo.Collider is PigBullet)
            {
                AddDamage(50);
            }
            if (collisionInfo.Collider is MiniPig)
            {
                AddDamage(30);
            }
            if (collisionInfo.Collider is Rocket)
            {
                AddDamage(50);
            }
            if (collisionInfo.Collider is RocketIco)
            {
                Weapons.AddBullets(BulletManager.BulletType.Rocket, 1);
            }
            if (collisionInfo.Collider is PigIco)
            {
                Weapons.AddBullets(BulletManager.BulletType.PigBullet, 1);
            }
        }
        public void Input()
        {
            shootCounter -= Game.DeltaTime;
            jumpCounter -= Game.DeltaTime;


            if (Game.NumJoysticks > 0)
            {
                Vector2 axis = Game.window.JoystickAxisLeft(joystickIndex);

                RigidBody.Velocity = axis * horizontalSpeed;

                if (shootCounter <= 0 && Game.window.JoystickA(joystickIndex))
                {
                    Shoot(currentBulletType);
                }
            }
            else
            {

                if (!IsLoading && Game.window.GetKey(KeyCode.Right))
                {
                    RigidBody.SetXVelocity(horizontalSpeed);
                }
                else if (!IsLoading && Game.window.GetKey(KeyCode.Left))
                {
                    RigidBody.SetXVelocity(-horizontalSpeed);
                }
                else
                {
                    RigidBody.SetXVelocity(0);
                }
              
                if (Game.window.GetKey(KeyCode.X))
                {
                    if (jumpCounter <= 0)
                    {
                        RigidBody.SetYVelocity(-200);
                        jumpCounter = 10;
                    }

                }


                if (Game.window.GetKey(KeyCode.Up))
                {
                    turret.Rotation -= Game.DeltaTime;
                    if (turret.Rotation < maxAngle)
                    {
                        turret.Rotation = maxAngle;
                    }
                }
                else if (Game.window.GetKey(KeyCode.Down))
                {
                    turret.Rotation += Game.DeltaTime;
                    if (turret.Rotation > minAngle)
                    {
                        turret.Rotation = minAngle;
                    }
                }

                if (Game.window.GetKey(KeyCode.R))
                {
                    Position = new Vector2(800, 100);
                }

                if (Game.window.GetKey(KeyCode.Tab))
                {
                    if (!IsTabPressed)
                    {
                        IsTabPressed = true;
                        if (Game.window.GetKey(KeyCode.ShiftLeft) || Game.window.GetKey(KeyCode.ShiftRight))
                        {
                            currentBulletType = Weapons.NextWeapon(-1);
                        }
                        else
                        {
                            currentBulletType = Weapons.NextWeapon(1);
                        }
                    }
                }
                else if (IsTabPressed)
                {
                    IsTabPressed = false;
                }

                if (shootCounter <= 0)
                {
                    if (Game.window.GetKey(KeyCode.Space))
                    {
                        if (!IsSpacePressed)
                        {
                            StartLoading();
                            IsSpacePressed = true;
                        }

                    }
                    else if (IsSpacePressed && IsLoading)
                    {
                        StopLoading();
                        Shoot(currentBulletType, currentLoadingVal / maxLoadingVal);
                        IsSpacePressed = false;
                    }
                }

                //if (shootCounter <= 0)
                //{
                //    if (Game.window.GetKey(KeyCode.Space))
                //    {
                //        if (!IsSpacePressed)
                //        {
                //            StartLoading();
                //            IsSpacePressed = true;
                //        }

                //    }
                //    else if (IsSpacePressed)
                //    {
                //        StopLoading();
                //        Shoot(currentBulletType, currentLoadingVal / maxLoadingVal);
                //        IsSpacePressed = false;
                //    }
                //    if (Game.window.GetKey(KeyCode.C))
                //    {
                //        if (!IsCPressed)
                //        {
                //            StartLoading();
                //            IsCPressed = true;
                //        }

                //    }
                //    else if (IsCPressed)
                //    {
                //        StopLoading();
                //        Shoot(currentBulletType2, currentLoadingVal / maxLoadingVal);
                //        shootCounter = shootDelay;
                //        IsCPressed = false;
                //    }
                //}
            }
        }

    }
}
