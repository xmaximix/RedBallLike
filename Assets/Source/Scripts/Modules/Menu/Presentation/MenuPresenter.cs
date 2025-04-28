using System;
using R3;
using RedBallLike.Common.Logging;
using RedBallLike.Common.UI.Buttons;
using RedBallLike.Modules.Menu.Domain;
using RedBallLike.Modules.Menu.Presentation.View;
using VContainer.Unity;

namespace RedBallLike.Modules.Menu.Presentation
{
    public sealed class MenuPresenter : IStartable, IDisposable
    {
        private readonly IMenuView view;
        private readonly IMenuService service;
        private readonly ILogger logger;
        private readonly CompositeDisposable disposables = new();

        public MenuPresenter(
            IMenuView view,
            IMenuService service,
            ILogger logger)
        {
            this.view = view ?? throw new ArgumentNullException(nameof(view));
            this.service = service ?? throw new ArgumentNullException(nameof(service));
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public void Start()
        {
            SubscribePlay();
            SubscribeSettings();
            SubscribeExit();
        }

        public void Dispose()
        {
            disposables.Dispose();
        }

        private void SubscribePlay()
        {
            view.OnPlay
                .Subscribe(_ =>
                {
                    logger.Log("Play clicked");
                    service.Play("01");
                })
                .AddTo(disposables);
        }

        private void SubscribeSettings()
        {
            view.OnSettings
                .Subscribe(_ =>
                    logger.Log("Settings clicked (not implemented)")
                )
                .AddTo(disposables);
        }

        private void SubscribeExit()
        {
            view.OnExit
                .Subscribe(_ =>
                {
                    logger.Log("Exit clicked");
                    service.Exit();
                })
                .AddTo(disposables);
        }
    }
}