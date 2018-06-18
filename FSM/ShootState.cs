using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TankzC
{
    class ShootState:State
    {
        public override void Update()
        {
            if(machine.Owner.LastShotBullet==null || machine.Owner.LastShotBullet.IsActive == false)
            {
                machine.Switch((int)States.Wait);
            }
        }
    }
}
