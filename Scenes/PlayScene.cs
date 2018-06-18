using Aiv.Fast2D;
using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aiv.Audio;

namespace TankzC
{
    class PlayScene : Scene
    {
        int space = 200;

        protected List<Player> players;
        protected int currentPlayerIndex;

        protected Background bg_far;
        protected List<TextObject> playersName;
        protected List<WeaponsGUI> weaponsGUI;

        AudioSource sorce;



        //public float MinY { get; protected set; }
        public Player CurrentPlayer { get; protected set; }
        public Timer GetTimer { get { return timer; } }

        public override void Start()
        {
            base.Start();

            Vector2 screenCenter = new Vector2(Game.window.Width / 2, Game.window.Height / 2);

            GfxManager.Load();

            UpdateManager.Init();
            DrawManager.Init();
            PhysicsManager.Init();
            BulletManager.Init();
            CameraManager.Init(screenCenter, screenCenter);
            CameraManager.AddCamera("bg", 0.2f);

            CameraManager.AddCamera("GUI", 0);

            playersName = new List<TextObject>();

            MinY = Game.window.Height -25 ;


            AudioManager.Load();

          

            FontManager.Init();
            FontManager.AddFont("stdFont", "Assets/textSheet.png", 15, 32, 20, 20);
            FontManager.AddFont("comics", "Assets/comics.png", 10, 32, 61, 65);
            timer = new Timer();
            Vector2 positionFrame = new Vector2(50, 550);
            Vector2 bulletFrame = new Vector2(82, 550);

            GfxManager.LoadTiledMap("Assets/Map/map_lv1.tmx");

            players = new List<Player>();

            Player player1 = new Player("playerTank", new Vector2(Game.window.Width / 2 + 250 + space, 10));
            playersName.Add(new TextObject(new Vector2(50, 20), "player 1"));
            players.Add(player1);

            Player player2 = new Player("enemyTank", new Vector2(200, 20),1);
            playersName.Add(new TextObject(new Vector2(50 + 250, 20), "player 2"));            
            players.Add(player2);

            CurrentPlayer = player1;
            currentPlayerIndex = 0;
            CurrentPlayer.Play();
            //controllorare se inseguito da errori

            bg_far = new Background("bg", new Vector2(0, 0), 0);
            bg_far.SetCamera(CameraManager.GetCamera("bg0"));
            weaponsGUI = new List<WeaponsGUI>();


            CameraManager.SetTarget(player1);
        }
        public override void Draw()
        {
            DrawManager.Draw();
        }

        public override void Input()
        {
            for(int i =0; i< players.Count; i++)
            {
                players[i].UpdateFSM();
            }
        }

        public virtual void NextPlayer()
        {
            currentPlayerIndex = (currentPlayerIndex + 1) % players.Count;
            CurrentPlayer = players[currentPlayerIndex];

            CameraManager.MoveCameraTo(CurrentPlayer.Position);
            CameraManager.SetTarget(CurrentPlayer);
            CurrentPlayer.Play();
        }

      


        public override void Update()
        {
            PhysicsManager.Update();
            UpdateManager.Update();
            PhysicsManager.CheckCollisions();
            timer.Update();
           
            CameraManager.Update();            
        }

        public override void OnExit()
        {
            UpdateManager.RemoveAll();
            DrawManager.RemoveAll();
            PhysicsManager.RemoveAll();
            GfxManager.RemoveAll();
            BulletManager.RemoveAll();
            CameraManager.ResetCamera();
            AudioManager.Clear();
        }
    }
}
