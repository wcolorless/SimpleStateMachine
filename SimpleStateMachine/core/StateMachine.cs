using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using SimpleStateMachine.core.State;

namespace SimpleStateMachine
{
    public class StateMachine
    {
        public object CurrentState { get; private set; }
        private StateInfo _currentStateInfo;
        private readonly List<StateInfo> _infos = new List<StateInfo>();
        public StateMachine()
        {

        }

        public void AddState(StateInfo stateInfo)
        {
            if (!_infos.Contains(stateInfo))
            {
                _infos.Add(stateInfo);
            }
            else
            {
                throw new Exception("StructBuilder AddState Error: Node already exist");
            }
        }

        public void Init(object initState, object inputData = null)
        {
            CurrentState = initState;
            var state = _infos.FirstOrDefault(x => x.StateType == (int)initState);
            _currentStateInfo = state ?? throw new Exception($"StateMachine Init Error: Can't find state");
            if (inputData != null)
            {
                try
                {
                    _currentStateInfo.EventHandler(inputData);
                }
                catch (Exception e)
                {
                    throw new Exception($"StateMachine EnterTrigger Invoke Event in node Error: {e.Message}");
                }
            }
        }

        public void EnterTrigger(object trigger, object inputData = null)
        {
            var innerTrigger = (int) trigger;
            if(_currentStateInfo == null) throw new Exception("StateMachine EnterTrigger Error: currentStateInfo is null (maybe need to init)");
            var jumpNode= _currentStateInfo.Transitions.FirstOrDefault(x => x.Trigger == (int) trigger);
            if(jumpNode == null) throw new Exception("StateMachine EnterTrigger Error: Can't find trigger");
            var newStateInt = jumpNode.State;
            var newState = _infos.FirstOrDefault(x => x.StateType == newStateInt);
            CurrentState = newStateInt;
            _currentStateInfo = newState ?? throw new Exception($"StateMachine EnterTrigger Error: Can't find next state; current state: {_currentStateInfo.StateType}");
            if (inputData != null)
            {
                try
                {
                    _currentStateInfo.EventHandler(inputData);
                }
                catch (Exception e)
                {
                    throw new Exception($"StateMachine EnterTrigger Invoke Event in node Error: {e.Message}");
                }
            }
        }
    }
}
