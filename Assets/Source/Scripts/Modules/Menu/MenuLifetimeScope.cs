using System;
using UnityEngine;
using VContainer;
using VContainer.Unity;
using RedBallLike.Modules.Menu.Domain;
using RedBallLike.Modules.Menu.Presentation;
using RedBallLike.Modules.Menu.Presentation.View;

namespace RedBallLike.Modules.Menu
{
    public sealed class MenuLifetimeScope : LifetimeScope
    {
        [SerializeField] private MenuView view;

        protected override void Awake()
        {
            if (view == null)
                throw new InvalidOperationException($"{nameof(view)} is not assigned.");
            base.Awake();
        }

        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterInstance<IMenuView>(view);
            builder.Register<MenuService>(Lifetime.Scoped)
                .As<IMenuService>();
            builder.Register<MenuPresenter>(Lifetime.Scoped)
                .As<IStartable>()
                .As<IDisposable>();
        }
    }
}