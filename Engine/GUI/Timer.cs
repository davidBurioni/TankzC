using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;

namespace TankzC
{
    class Timer
    {
       protected float counter;
       protected TextObject time;

       const float Max_Time = 16f;

        public float GetTimer { get; protected set; }

        public Timer()
        {
            counter = 15f;
            time = new TextObject(new Vector2(Game.window.Width / 2, 50),counter.ToString(),FontManager.GetFont("comics"));
        }

   

        public void Reset()
        {
            counter = Max_Time;

        }
        public void Update()
        {
            counter -= Game.DeltaTime;
            time.Text = ((int)counter).ToString();
           

            if(counter <=0 )
            {
                ((PlayScene)Game.CurrentScene).CurrentPlayer.Wait();
                //counter = Max_Time;
                Reset();
            }
        }
     }
}
