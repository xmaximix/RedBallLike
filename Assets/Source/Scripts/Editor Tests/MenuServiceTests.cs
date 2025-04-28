using NUnit.Framework;
using RedBallLike.Common.GameState;
using RedBallLike.Common.Level;
using RedBallLike.Modules.Menu.Domain;

namespace Editor_Tests
{
    [TestFixture]
    public class MenuServiceTests
    {
        private MenuService menuService;
        private LevelSelectionService selection;
        private GameStateMachine stateMachine;

        [SetUp]
        public void SetUp()
        {
            InitializeServices();
        }

        private void InitializeServices()
        {
            selection = new LevelSelectionService();
            stateMachine = new GameStateMachine();
            menuService = new MenuService(selection, stateMachine);
        }

        [Test]
        public void Play_SetsSelectedLevelAndState()
        {
            menuService.Play("03");

            Assert.AreEqual("03", selection.SelectedLevelId);
            Assert.AreEqual(GameState.Playing, stateMachine.State.Value);
        }

        [Test]
        public void Exit_DoesNotThrow()
        {
            Assert.DoesNotThrow(() => menuService.Exit());
        }
    }
}