using System;
using Cysharp.Threading.Tasks;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace RedBallLike.Common.Level
{
    public sealed class AddressablesLevelCatalog : ILevelCatalog
    {
        public async UniTask<LevelConfig> Get(string levelId)
        {
            var key = GetAssetKey(levelId);
            var handle = Addressables.LoadAssetAsync<LevelConfig>(key);

            await handle.Task;
            ValidateHandle(handle, key);

            return handle.Result;
        }

        private string GetAssetKey(string levelId)
        {
            return $"Level_{levelId}";
        }

        private void ValidateHandle(AsyncOperationHandle<LevelConfig> handle, string key)
        {
            if (handle.Status != AsyncOperationStatus.Succeeded)
            {
                throw new InvalidOperationException(
                    $"Failed to load LevelConfig with key: {key}"
                );
            }
        }
    }
}