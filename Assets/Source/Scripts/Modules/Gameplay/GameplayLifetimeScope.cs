using System;
using UnityEngine;
using VContainer;
using VContainer.Unity;
using RedBallLike.Common.Cam;
using RedBallLike.Common.Level;
using RedBallLike.Modules.Gameplay.Domain;
using RedBallLike.Modules.Gameplay.Factories;
using RedBallLike.Modules.Gameplay.Presentation;

namespace RedBallLike.Modules.Gameplay
{
    public sealed class GameplayLifetimeScope : LifetimeScope
    {
        [SerializeField] private CameraController cameraPrefab;
        [SerializeField] private PlayerView playerPrefab;
        [SerializeField] private PlayerConfig movementConfig;

        protected override void Configure(IContainerBuilder builder)
        {
            RegisterCameraServices(builder);
            RegisterLevelServices(builder);
            RegisterPlayerServices(builder);
            builder.RegisterEntryPoint<GameplayBootstrap>();
        }

        private void RegisterCameraServices(IContainerBuilder builder)
        {
            builder.RegisterComponentInNewPrefab(cameraPrefab, Lifetime.Scoped)
                .As<CameraController>();
            builder.Register<SimpleCameraService>(Lifetime.Scoped)
                .As<ICameraService>();
        }

        private void RegisterLevelServices(IContainerBuilder builder)
        {
            builder.Register<AddressablesLevelSpawner>(Lifetime.Scoped)
                .As<ILevelSpawner>()
                .As<IDisposable>();
        }

        private void RegisterPlayerServices(IContainerBuilder builder)
        {
            builder.RegisterInstance(playerPrefab);
            builder.Register<PlayerFactory>(Lifetime.Scoped)
                .As<IPlayerFactory>();
            builder.RegisterInstance(movementConfig)
                .As<PlayerConfig>();
        }
    }
}