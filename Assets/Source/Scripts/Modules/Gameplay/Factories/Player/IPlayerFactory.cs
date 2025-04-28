using RedBallLike.Modules.Gameplay.Presentation;
using UnityEngine;

namespace RedBallLike.Modules.Gameplay.Factories
{
    public interface IPlayerFactory
    {
        PlayerView Spawn(Vector3 spawnPoint);
    }
}