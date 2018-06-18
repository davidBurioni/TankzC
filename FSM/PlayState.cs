using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TankzC
{
    class PlayState:State
    {
        public override void Update()
        {
            machine.Owner.Input();
        }
        public override void Enter()
        {
            ((Scene)Game.CurrentScene).GetTimer.Reset();
            //((PlayScene2)Game.CurrentScene).GetTimer.Reset();

        }
    }
}
