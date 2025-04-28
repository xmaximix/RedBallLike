using System;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace RedBallLike.Common.Level
{
    public sealed class AddressablesLevelSpawner : ILevelSpawner, IDisposable
    {
        private AsyncOperationHandle<GameObject>? handle;

        public async UniTask<GameObject> Spawn(LevelConfig config)
        {
            var spawnHandle = config.LevelPrefab.InstantiateAsync();
            handle = spawnHandle;

            var instance = await spawnHandle.Task;
            return instance;
        }

        public void Dispose()
        {
            if (handle.HasValue)
            {
                Addressables.ReleaseInstance(handle.Value);
                handle = null;
            }
        }
    }
}