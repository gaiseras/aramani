﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AramaniIR.Variables;

namespace AramaniIR.Commands
{
    class Set : Command
    {

        public Location Source { get; set; }

        public StackVariable Target { get; set; }

    }
}
