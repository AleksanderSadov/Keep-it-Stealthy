using KeepItStealthy.Gameplay.Components;
using KeepItStealthy.Gameplay.Helpers;
using KeepItStealthy.Gameplay.ScriptableObjects;
using UnityEngine;
using UnityEngine.AI;

namespace KeepItStealthy.Gameplay.Managers
{
    public class LevelGenerator : MonoBehaviour
    {
        [SerializeField]
        private WorldSizeSO worldSizeSO;
        [SerializeField]
        private NavMeshObstacle obstaclePrefab;
        [SerializeField]
        private EntryPoint entryPrefab;
        [SerializeField]
        private ExitPoint exitPrefab;

        public void GenerateLevel()
        {
            WorldBoundsSpawner worldBoundsSpawner = new(worldSizeSO, obstaclePrefab);
            worldBoundsSpawner.Spawn();

            EntryExitSpawner entryExitSpawner = new(worldSizeSO, entryPrefab, exitPrefab);
            entryExitSpawner.SpawnEntry();
            entryExitSpawner.SpawnExit();
        }
    }
}
