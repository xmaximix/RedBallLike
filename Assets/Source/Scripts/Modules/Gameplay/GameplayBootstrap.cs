using System;
using Cysharp.Threading.Tasks;
using R3;
using UnityEngine;
using RedBallLike.Common.Cam;
using RedBallLike.Common.GameState;
using RedBallLike.Common.GameTime;
using RedBallLike.Common.Input;
using RedBallLike.Common.Level;
using RedBallLike.Common.Utils;
using RedBallLike.Modules.Gameplay.Domain;
using RedBallLike.Modules.Gameplay.Factories;
using RedBallLike.Modules.Gameplay.Presentation;
using VContainer.Unity;
using ILogger = RedBallLike.Common.Logging.ILogger;

namespace RedBallLike.Modules.Gameplay
{
    public sealed class GameplayBootstrap : IStartable, IDisposable
    {
        private readonly ILevelSelectionService selection;
        private readonly ILevelCatalog catalog;
        private readonly ILevelSpawner spawner;
        private readonly IPlayerFactory factory;
        private readonly ICameraService camera;
        private readonly IGameStateMachine gsm;
        private readonly IInputService input;
        private readonly ITimeService time;
        private readonly PlayerConfig moveCfg;
        private readonly ILogger logger;

        private readonly CompositeDisposable disposables = new();
        private PlayerView playerView;
        private Vector3 spawnPoint;
        private PlayerPresenter presenter;

        public GameplayBootstrap(
            ILevelSelectionService selection,
            ILevelCatalog catalog,
            ILevelSpawner spawner,
            IPlayerFactory factory,
            ICameraService camera,
            IGameStateMachine gsm,
            IInputService input,
            ITimeService time,
            PlayerConfig moveCfg,
            ILogger logger)
        {
            this.selection = selection ?? throw new ArgumentNullException(nameof(selection));
            this.catalog = catalog ?? throw new ArgumentNullException(nameof(catalog));
            this.spawner = spawner ?? throw new ArgumentNullException(nameof(spawner));
            this.factory = factory ?? throw new ArgumentNullException(nameof(factory));
            this.camera = camera ?? throw new ArgumentNullException(nameof(camera));
            this.gsm = gsm ?? throw new ArgumentNullException(nameof(gsm));
            this.input = input ?? throw new ArgumentNullException(nameof(input));
            this.time = time ?? throw new ArgumentNullException(nameof(time));
            this.moveCfg = moveCfg ?? throw new ArgumentNullException(nameof(moveCfg));
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async void Start()
        {
            var config = await LoadLevelConfig();
            spawnPoint = config.SpawnPoint;
            await SpawnLevel(config);

            SpawnPlayer(config);
            SetupPresenter();
            SubscribeToGameState();

            gsm.SetState(GameState.Playing);
        }

        public void Dispose()
        {
            presenter.Dispose();
            disposables.Dispose();
        }

        private UniTask<LevelConfig> LoadLevelConfig()
        {
            return catalog.Get(selection.SelectedLevelId);
        }

        private async UniTask SpawnLevel(LevelConfig config)
        {
            await spawner.Spawn(config);
        }

        private void SpawnPlayer(LevelConfig config)
        {
            playerView = factory.Spawn(config.SpawnPoint);
            camera.SetFollowTarget(playerView.transform);
        }

        private void SetupPresenter()
        {
            var movement = new PlayerMovement(playerView.Rigidbody2D, moveCfg);
            presenter = new PlayerPresenter(movement, input, time, gsm);
            presenter.Start();
            FixedTickRunner.Instance.Add(presenter);
        }

        private void SubscribeToGameState()
        {
            gsm.State
                .Skip(1)
                .Subscribe(OnStateChanged)
                .AddTo(disposables);
        }

        private async void OnStateChanged(GameState state)
        {
            switch (state)
            {
                case GameState.GameOver:
                    HandleGameOver();
                    break;
                case GameState.Completed:
                    await HandleGameCompleted();
                    break;
            }
        }

        private void HandleGameOver()
        {
            logger.Warn("You Lose");
            playerView.transform.position = spawnPoint;
            StopPlayer();
            gsm.SetState(GameState.Playing);
        }

        private async UniTask HandleGameCompleted()
        {
            logger.Log("You Win");
            StopPlayer();
            await UniTask.Delay(3000);
            gsm.SetState(GameState.Menu);
        }

        private void StopPlayer()
        {
            var rb = playerView.Rigidbody2D;
            rb.velocity = Vector2.zero;
            rb.angularVelocity = 0f;
        }
    }
}