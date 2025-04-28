using UnityEngine;
using VContainer.Unity;
using VContainer;
using RedBallLike.Common.GameState;
using RedBallLike.Modules.Boot;

namespace RedBallLike.Modules.Gameplay.Presentation
{
    [RequireComponent(typeof(Collider2D))]
    public sealed class DeathZone : MonoBehaviour
    {
        private IGameStateMachine gsm;

        private void Awake()
        {
            InitializeGameStateMachine();
        }

        private void InitializeGameStateMachine()
        {
            gsm = LifetimeScope
                .Find<BootLifetimeScope>()
                .Container
                .Resolve<IGameStateMachine>();
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent<PlayerView>(out _))
            {
                gsm.SetState(GameState.GameOver);
            }
        }
    }
}