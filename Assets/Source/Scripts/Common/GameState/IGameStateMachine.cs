using R3;

namespace RedBallLike.Common.GameState
{
    public enum GameState
    {
        Menu = 0, Playing = 1, Paused = 2,
        Completed = 3, GameOver = 4
    }

    public interface IGameStateMachine
    {
        ReactiveProperty<GameState> State { get; }
        void SetState(GameState state);
    }
}