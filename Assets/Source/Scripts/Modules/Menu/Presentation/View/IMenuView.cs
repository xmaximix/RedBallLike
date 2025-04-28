using R3;

namespace RedBallLike.Modules.Menu.Presentation.View
{
    public interface IMenuView
    {
        Observable<Unit> OnPlay { get; }
        Observable<Unit> OnSettings { get; }
        Observable<Unit> OnExit { get; }
    }
}