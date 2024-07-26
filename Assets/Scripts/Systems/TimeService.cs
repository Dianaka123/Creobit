using Assets.Scripts.Systems.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Systems
{
    public class TimeService : ITimeService
    {
        public DateTime Now => DateTime.Now;
    }
}
