using Cysharp.Threading.Tasks;

namespace RedBallLike.Common.Scene
{
    public interface ISceneLoader
    {
        UniTask Load(string sceneName);
    }
}