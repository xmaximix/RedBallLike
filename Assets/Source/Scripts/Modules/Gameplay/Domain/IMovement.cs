namespace RedBallLike.Modules.Gameplay.Domain
{
    public interface IMovement
    {
        void SetMoveAxis(float axis);
        void RequestJump();
        void Tick(float deltaTime);
    }
}