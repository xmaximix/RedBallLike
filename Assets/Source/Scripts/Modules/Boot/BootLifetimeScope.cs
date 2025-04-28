using RedBallLike.Common.GameState;
using RedBallLike.Common.GameTime;
using RedBallLike.Common.Input;
using RedBallLike.Common.Level;
using RedBallLike.Common.Logging;
using RedBallLike.Common.Scene;
using UnityEngine;
using VContainer;
using VContainer.Unity;
using ILogger = RedBallLike.Common.Logging.ILogger;

namespace RedBallLike.Modules.Boot
{
    public sealed class BootLifetimeScope : LifetimeScope
    {
        protected override void Awake()
        {
            if (IsDuplicateInstance())
            {
                Destroy(gameObject);
                return;
            }

            DontDestroyOnLoad(gameObject);
            base.Awake();
        }

        protected override void Configure(IContainerBuilder builder)
        {
            RegisterServices(builder);
            builder.RegisterEntryPoint<BootBootstrap>();
        }

        private static bool IsDuplicateInstance()
        {
            return FindObjectsOfType<BootLifetimeScope>().Length > 1;
        }

        private static void RegisterServices(IContainerBuilder builder)
        {
            builder.Register<GameStateMachine>(Lifetime.Singleton)
                .As<IGameStateMachine>();

            builder.Register<LevelSelectionService>(Lifetime.Singleton)
                .As<ILevelSelectionService>();

            builder.Register<ConsoleLogger>(Lifetime.Singleton)
                .As<ILogger>();

            builder.Register<UnitySceneLoader>(Lifetime.Singleton)
                .As<ISceneLoader>();

            builder.Register<UnityTimeService>(Lifetime.Singleton)
                .As<ITimeService>();

            builder.Register<UnityInputService>(Lifetime.Singleton)
                .As<IInputService>();

            builder.Register<AddressablesLevelCatalog>(Lifetime.Singleton)
                .As<ILevelCatalog>();
        }
    }
}