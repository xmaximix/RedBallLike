using UnityEngine;
using UnityEngine.AddressableAssets;

namespace RedBallLike.Common.Level
{
    [CreateAssetMenu(menuName = "Configs/LevelConfig")]
    public sealed class LevelConfig : ScriptableObject
    {
        [field: SerializeField] public AssetReferenceGameObject LevelPrefab { get; private set; }
        [field: SerializeField] public Vector3 SpawnPoint { get; private set; }
    }
}