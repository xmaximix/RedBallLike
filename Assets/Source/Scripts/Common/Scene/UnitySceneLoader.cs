using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace RedBallLike.Common.Scene
{
    public sealed class UnitySceneLoader : ISceneLoader
    {
        public async UniTask Load(string sceneName)
        {
            var operation = BeginLoad(sceneName);
            await operation;
        }

        private AsyncOperation BeginLoad(string sceneName)
        {
            var operation = SceneManager.LoadSceneAsync(sceneName);
            EnableSceneActivation(operation);
            return operation;
        }

        private void EnableSceneActivation(AsyncOperation operation)
        {
            operation.allowSceneActivation = true;
        }
    }
}