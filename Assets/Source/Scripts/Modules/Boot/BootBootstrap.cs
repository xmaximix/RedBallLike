using System;
using Cysharp.Threading.Tasks;
using R3;
using RedBallLike.Common.GameState;
using RedBallLike.Common.Scene;
using UnityEngine.SceneManagement;
using VContainer.Unity;

namespace RedBallLike.Modules.Boot
{
    public sealed class BootBootstrap : IStartable, IDisposable
    {
        private readonly ISceneLoader sceneLoader;
        private readonly IGameStateMachine stateMachine;
        private readonly CompositeDisposable disposables = new();

        public BootBootstrap(ISceneLoader sceneLoader, IGameStateMachine stateMachine)
        {
            this.sceneLoader = sceneLoader ?? throw new ArgumentNullException(nameof(sceneLoader));
            this.stateMachine = stateMachine ?? throw new ArgumentNullException(nameof(stateMachine));
        }

        public void Start()
        {
            SubscribeToStateChanges();
            stateMachine.SetState(GameState.Menu);
        }

        public void Dispose()
        {
            disposables.Dispose();
        }

        private void SubscribeToStateChanges()
        {
            stateMachine.State
                .DistinctUntilChanged()
                .Subscribe(OnStateChanged)
                .AddTo(disposables);
        }

        private async void OnStateChanged(GameState state)
        {
            switch (state)
            {
                case GameState.Menu:
                    await sceneLoader.Load(SceneNames.Menu);
                    break;
                case GameState.Playing:
                    await LoadGameplaySceneIfNeeded();
                    break;
            }
        }

        private async UniTask LoadGameplaySceneIfNeeded()
        {
            if (SceneManager.GetActiveScene().name != SceneNames.Gameplay)
            {
                await sceneLoader.Load(SceneNames.Gameplay);
            }
        }
    }
}