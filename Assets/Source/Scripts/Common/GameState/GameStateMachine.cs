using R3;

namespace RedBallLike.Common.GameState
{
    public sealed class GameStateMachine : IGameStateMachine
    {
        public ReactiveProperty<GameState> State { get; } = new(GameState.Menu);

        public void SetState(GameState state)
        {
            UpdateState(state);
        }

        private void UpdateState(GameState state)
        {
            State.Value = state;
        }
    }
}