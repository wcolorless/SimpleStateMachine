using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleStateMachine.core.State
{
    public class Transition
    {
        public int State { get; set; }
        public int Trigger { get; set; }
    }
}
