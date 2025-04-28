using System;
using R3;
using UnityEngine.InputSystem;

namespace RedBallLike.Common.Input
{
    public sealed class UnityInputService : IInputService, IDisposable
    {
        private readonly GameplayControls controls;
        private readonly ReactiveProperty<float> axis = new(0f);
        private readonly Subject<Unit> jump = new();

        public ReadOnlyReactiveProperty<float> Axis => axis;
        public Observable<Unit> Jump => jump;

        public UnityInputService()
        {
            controls = new GameplayControls();
            SubscribeInputEvents();
            controls.Enable();
        }

        public void Dispose()
        {
            UnsubscribeInputEvents();
            controls.Disable();
            axis.Dispose();
            jump.Dispose();
        }

        private void SubscribeInputEvents()
        {
            controls.Gameplay.Move.performed += OnMovePerformed;
            controls.Gameplay.Move.canceled += OnMoveCanceled;
            controls.Gameplay.Jump.performed += OnJumpPerformed;
        }

        private void UnsubscribeInputEvents()
        {
            controls.Gameplay.Move.performed -= OnMovePerformed;
            controls.Gameplay.Move.canceled -= OnMoveCanceled;
            controls.Gameplay.Jump.performed -= OnJumpPerformed;
        }

        private void OnMovePerformed(InputAction.CallbackContext ctx)
        {
            axis.Value = ctx.ReadValue<float>();
        }

        private void OnMoveCanceled(InputAction.CallbackContext _)
        {
            axis.Value = 0f;
        }

        private void OnJumpPerformed(InputAction.CallbackContext _)
        {
            jump.OnNext(Unit.Default);
        }
    }
}