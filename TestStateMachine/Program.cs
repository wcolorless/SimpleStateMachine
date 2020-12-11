using System;
using System.IO;
using SimpleStateMachine;
using SimpleStateMachine.core.State;

namespace TestStateMachine
{
    public enum ElectricBikeStates
    {
        Inaction,
        Charging,
        EngineOn,
        RecuperationStopping
    }

    public enum ElectricBikeTriggers
    {
        AccelerationOn,
        AccelerationOff,
        StoppingOn,
        StoppingOff,
        ChargingOn,
        ChargingOff
    }

    class Program
    {
        private static StateMachine _stateMachine;
        static void Main(string[] args)
        {
            InitMachine();

            // Init
            Console.WriteLine("Init state machine");
            _stateMachine.Init(ElectricBikeStates.Charging);
            Console.WriteLine($"Current state: {((ElectricBikeStates)_stateMachine.CurrentState)}");

            // ChargingOff
            Console.WriteLine("Trigger: ElectricBikeTriggers.ChargingOff");
            _stateMachine.EnterTrigger(ElectricBikeTriggers.ChargingOff);
            Console.WriteLine($"Current state: {((ElectricBikeStates)_stateMachine.CurrentState)}");

            // AccelerationOn
            Console.WriteLine("Trigger: ElectricBikeTriggers.AccelerationOn");
            _stateMachine.EnterTrigger(ElectricBikeTriggers.AccelerationOn);
            Console.WriteLine($"Current state: {((ElectricBikeStates)_stateMachine.CurrentState)}");

            // AccelerationOff
            Console.WriteLine("Trigger: ElectricBikeTriggers.AccelerationOff");
            _stateMachine.EnterTrigger(ElectricBikeTriggers.AccelerationOff, "AccelerationOff");
            Console.WriteLine($"Current state: {((ElectricBikeStates)_stateMachine.CurrentState)}");

            // AccelerationOn
            Console.WriteLine("Trigger: ElectricBikeTriggers.AccelerationOn");
            _stateMachine.EnterTrigger(ElectricBikeTriggers.AccelerationOn, "AccelerationOn");
            Console.WriteLine($"Current state: {((ElectricBikeStates)_stateMachine.CurrentState)}");

            // StoppingOn
            Console.WriteLine("Trigger: ElectricBikeTriggers.StoppingOn");
            _stateMachine.EnterTrigger(ElectricBikeTriggers.StoppingOn);
            Console.WriteLine($"Current state: {((ElectricBikeStates)_stateMachine.CurrentState)}");

            // StoppingOff
            Console.WriteLine("Trigger: ElectricBikeTriggers.StoppingOff");
            _stateMachine.EnterTrigger(ElectricBikeTriggers.StoppingOff);
            Console.WriteLine($"Current state: {((ElectricBikeStates)_stateMachine.CurrentState)}");

            // ChargingOn
            Console.WriteLine("Trigger: ElectricBikeTriggers.ChargingOn");
            _stateMachine.EnterTrigger(ElectricBikeTriggers.ChargingOn);
            Console.WriteLine($"Current state: {((ElectricBikeStates)_stateMachine.CurrentState)}");
        }

        static void InitMachine()
        {
            _stateMachine = new StateMachine();

            var inactionNode = StateInfo.Create(ElectricBikeStates.Inaction)
                .TransitionTo(ElectricBikeStates.EngineOn, ElectricBikeTriggers.AccelerationOn)
                .TransitionTo(ElectricBikeStates.Charging, ElectricBikeTriggers.ChargingOn)
                .SetTransitionEvent(InactionStateHandler);

            var chargingNode = StateInfo.Create(ElectricBikeStates.Charging)
                .TransitionTo(ElectricBikeStates.Inaction, ElectricBikeTriggers.ChargingOff);

            var engineOnNode = StateInfo.Create(ElectricBikeStates.EngineOn)
                .TransitionTo(ElectricBikeStates.Inaction, ElectricBikeTriggers.AccelerationOff)
                .TransitionTo(ElectricBikeStates.RecuperationStopping, ElectricBikeTriggers.StoppingOn)
                .SetTransitionEvent(EngineOnHandler);

            var recuperationStoppingNode = StateInfo.Create(ElectricBikeStates.RecuperationStopping)
                .TransitionTo(ElectricBikeStates.Inaction, ElectricBikeTriggers.StoppingOff)
                .TransitionTo(ElectricBikeStates.EngineOn, ElectricBikeTriggers.AccelerationOn);

            _stateMachine.AddState(inactionNode);
            _stateMachine.AddState(chargingNode);
            _stateMachine.AddState(engineOnNode);
            _stateMachine.AddState(recuperationStoppingNode);
        }

        public static void InactionStateHandler(object obj)
        {
            var text = (string) obj;
            Console.WriteLine($"InactionStateHandler: Inaction with text: {text}");
        }

        public static void EngineOnHandler(object obj)
        {
            var text = (string)obj;
            Console.WriteLine($"EngineOnHandler: EngineOn with text: {text}");
        }
    }
}
