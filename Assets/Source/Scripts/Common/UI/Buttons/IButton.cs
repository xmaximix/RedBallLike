using R3;

namespace RedBallLike.Common.UI.Buttons
{
    public interface IButton
    {
        Observable<Unit> OnClick { get; }
    }
}