using System;
using UnityEngine;
using RedBallLike.Common.GameState;
using RedBallLike.Common.Level;

namespace RedBallLike.Modules.Menu.Domain
{
    public sealed class MenuService : IMenuService
    {
        private readonly ILevelSelectionService selection;
        private readonly IGameStateMachine stateMachine;

        public MenuService(
            ILevelSelectionService selection,
            IGameStateMachine stateMachine)
        {
            this.selection = selection ?? throw new ArgumentNullException(nameof(selection));
            this.stateMachine = stateMachine ?? throw new ArgumentNullException(nameof(stateMachine));
        }

        public void Play(string levelId)
        {
            selection.SelectedLevelId = levelId;
            stateMachine.SetState(GameState.Playing);
        }

        public void Exit()
        {
            Application.Quit();
        }
    }
}