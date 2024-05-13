using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonRPG.Scripts.Interfaces
{
    internal interface IHitbox
    {
        public float GetDamage();
        public bool CanStun();
    }
}
