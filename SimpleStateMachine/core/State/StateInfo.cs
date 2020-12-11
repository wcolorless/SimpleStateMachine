using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SimpleStateMachine.core.State
{
    public class StateInfo
    {
        public int StateType { get; set; }
        public List<Transition> Transitions { get; set; } = new List<Transition>();
        public Action<object> EventHandler { get; private set; }

        public StateInfo()
        {

        }

        private StateInfo(object state)
        {
            StateType = (int)state;
        }

        public static StateInfo Create(object state)
        {
            return new StateInfo((int)state);
        }

        public StateInfo TransitionTo(object state, object trigger)
        {
            if (Transitions.FirstOrDefault(x => x.State == (int)state && x.Trigger == (int)trigger) == null)
            {
                Transitions.Add(new Transition()
                {
                    State = (int)state,
                    Trigger = (int)trigger
                });
            }
            return this;
        }

        public StateInfo SetTransitionEvent(Action<object> eventHandler)
        {
            EventHandler = eventHandler;
            return this;
        }
    }
}
