﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Systems.Interfaces
{
    public interface ITimeService
    {
        public DateTime Now { get; }
    }
}
