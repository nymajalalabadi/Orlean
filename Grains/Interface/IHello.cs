﻿using Orleans;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grains.Interface
{
    public interface IHello : IGrainWithIntegerKey
    {
        Task<string> SayHello(string greeting);   
    }
}
