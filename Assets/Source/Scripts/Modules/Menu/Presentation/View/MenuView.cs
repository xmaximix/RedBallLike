using R3;
using RedBallLike.Common.UI.Buttons;
using UnityEngine;

namespace RedBallLike.Modules.Menu.Presentation.View
{
    public sealed class MenuView : MonoBehaviour, IMenuView
    {
        [SerializeField] private ButtonAdapter playButton;
        [SerializeField] private ButtonAdapter settingsButton;
        [SerializeField] private ButtonAdapter exitButton;

        public Observable<Unit> OnPlay => playButton.OnClick;
        public Observable<Unit> OnSettings => settingsButton.OnClick;
        public Observable<Unit> OnExit => exitButton.OnClick;
    }
}