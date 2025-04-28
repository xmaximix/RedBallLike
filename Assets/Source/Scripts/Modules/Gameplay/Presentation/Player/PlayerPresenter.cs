using System;
using R3;
using RedBallLike.Common.GameState;
using RedBallLike.Common.GameTime;
using RedBallLike.Common.Input;
using RedBallLike.Common.Utils;
using RedBallLike.Modules.Gameplay.Domain;
using UnityEngine;
using VContainer.Unity;

namespace RedBallLike.Modules.Gameplay.Presentation
{
    public sealed class PlayerPresenter : IStartable, IFixedTickable, IDisposable
    {
        private readonly IInputService input;
        private readonly ITimeService time;
        private readonly IGameStateMachine gsm;
        private readonly CompositeDisposable disp = new();
        private readonly IMovement movement;

        public PlayerPresenter(
            PlayerMovement movement,
            IInputService input,
            ITimeService time,
            IGameStateMachine gsm)
        {
            this.movement = movement ?? throw new ArgumentNullException(nameof(movement));
            this.input = input ?? throw new ArgumentNullException(nameof(input));
            this.time = time ?? throw new ArgumentNullException(nameof(time));
            this.gsm = gsm ?? throw new ArgumentNullException(nameof(gsm));
        }

        public void Start()
        {
            SubscribeToJump();
        }

        public void FixedTick()
        {
            if (time.Scale == 0 || gsm.State.Value != GameState.Playing)
                return;

            ProcessMovement();
        }

        public void Dispose()
        {
            FixedTickRunner.Instance.Remove(this);
            disp.Dispose();
        }

        private void SubscribeToJump()
        {
            input.Jump
                .Subscribe(_ => movement.RequestJump())
                .AddTo(disp);
        }

        private void ProcessMovement()
        {
            movement.SetMoveAxis(input.Axis.CurrentValue);
            movement.Tick(Time.fixedDeltaTime);
        }
    }
}