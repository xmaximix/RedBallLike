using System;
using UnityEngine;
using RedBallLike.Modules.Gameplay.Presentation;
using Object = UnityEngine.Object;

namespace RedBallLike.Modules.Gameplay.Factories
{
    public sealed class PlayerFactory : IPlayerFactory
    {
        private readonly PlayerView playerPrefab;

        public PlayerFactory(PlayerView playerPrefab) =>
            this.playerPrefab = playerPrefab ?? throw new ArgumentNullException(nameof(playerPrefab));

        public PlayerView Spawn(Vector3 spawnPoint)
        {
            return Object.Instantiate(playerPrefab, spawnPoint, Quaternion.identity);
        }
    }
}