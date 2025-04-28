using NUnit.Framework;
using RedBallLike.Common.Level;

namespace Editor_Tests
{
    [TestFixture]
    public class LevelSelectionServiceTests
    {
        private LevelSelectionService service;

        [SetUp]
        public void SetUp()
        {
            service = new LevelSelectionService();
        }

        [Test]
        public void DefaultSelectedLevelId_Is01()
        {
            Assert.AreEqual("01", service.SelectedLevelId);
        }

        [Test]
        public void CanChangeSelectedLevelId()
        {
            service.SelectedLevelId = "05";
            Assert.AreEqual("05", service.SelectedLevelId);
        }
    }
}