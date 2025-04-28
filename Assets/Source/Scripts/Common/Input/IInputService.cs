using R3;

namespace RedBallLike.Common.Input
{
    public interface IInputService
    {
        ReadOnlyReactiveProperty<float> Axis { get; }

        Observable<Unit> Jump { get; }
    }
}