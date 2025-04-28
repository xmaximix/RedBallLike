using Cysharp.Threading.Tasks;

namespace RedBallLike.Common.Level
{
    public interface ILevelCatalog
    {
        UniTask<LevelConfig> Get(string levelId);
    }
}