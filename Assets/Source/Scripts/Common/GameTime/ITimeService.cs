namespace RedBallLike.Common.GameTime
{
    public interface ITimeService
    {
        float Scale { get; }
        void Pause();
        void Resume();
    }
}