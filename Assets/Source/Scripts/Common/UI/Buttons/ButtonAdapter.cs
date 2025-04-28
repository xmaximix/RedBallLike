using R3;
using UnityEngine;
using UnityEngine.UI;

namespace RedBallLike.Common.UI.Buttons
{
    [RequireComponent(typeof(Button))]
    public sealed class ButtonAdapter : MonoBehaviour, IButton
    {
        private Button button;

        private void Awake()
        {
            button = GetComponent<Button>();
        }

        public Observable<Unit> OnClick => button.OnClickAsObservable();
    }
}