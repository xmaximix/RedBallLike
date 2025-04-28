using Cysharp.Threading.Tasks;
using UnityEngine;

namespace RedBallLike.Common.Level
{
    public interface ILevelSpawner
    {
        UniTask<GameObject> Spawn(LevelConfig config);
    }
}