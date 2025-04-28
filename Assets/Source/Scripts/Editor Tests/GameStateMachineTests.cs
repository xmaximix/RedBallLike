using NUnit.Framework;
using R3;
using RedBallLike.Common.GameState;
using Assert = UnityEngine.Assertions.Assert;

namespace Editor_Tests
{
    [TestFixture]
    public class GameStateMachineTests
    {
        private GameStateMachine machine;

        [SetUp]
        public void SetUp()
        {
            machine = new GameStateMachine();
        }

        [Test]
        public void InitialState_IsMenu()
        {
            Assert.AreEqual(GameState.Menu, machine.State.Value);
        }

        [Test]
        public void SetState_UpdatesState()
        {
            machine.SetState(GameState.Playing);
            Assert.AreEqual(GameState.Playing, machine.State.Value);

            machine.SetState(GameState.Paused);
            Assert.AreEqual(GameState.Paused, machine.State.Value);
        }

        [Test]
        public void State_ReactivePropertyEmits()
        {
            GameState? observed = null;
            using var subscription = machine
                .State
                .Subscribe(s => observed = s);

            machine.SetState(GameState.Completed);
            Assert.AreEqual(GameState.Completed, observed);
        }
    }
}