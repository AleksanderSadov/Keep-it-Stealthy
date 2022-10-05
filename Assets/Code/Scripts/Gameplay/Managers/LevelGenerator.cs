using KeepItStealthy.Core;
using KeepItStealthy.Gameplay.Components;
using KeepItStealthy.Gameplay.Constants;
using KeepItStealthy.Gameplay.Helpers;
using KeepItStealthy.Gameplay.ScriptableObjects;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace KeepItStealthy.Gameplay.Managers
{
    public class LevelGenerator : MonoBehaviour
    {
        [SerializeField]
        private LevelSizeSO levelSizeSO;
        [SerializeField]
        private NavMeshObstacle obstaclePrefab;
        [SerializeField]
        private EntryPoint entryPrefab;
        [SerializeField]
        private ExitPoint exitPrefab;

        public void GenerateLevel()
        {
            SpawnLevelBounds();
            SpawnEntry();
            SpawnExit();
            SpawnObstacles();
        }

        private void SpawnLevelBounds()
        {
            BoundsSpawner levelBoundsSpawner = new(levelSizeSO.TopLeftPoint, levelSizeSO.BottomRightPoint, obstaclePrefab);
            levelBoundsSpawner.Spawn();
        }

        private void SpawnEntry()
        {
            Vector3 entrySize = MeshAbsoluteSize.Calculate(entryPrefab.GetComponent<MeshFilter>());
            Vector3 entryPosition = new Vector3(
                levelSizeSO.BottomLeftPoint.x + entrySize.x / 2,
                levelSizeSO.BottomLeftPoint.y,
                levelSizeSO.BottomLeftPoint.z + entrySize.z / 2
            );
            EntryPoint entryPoint = Instantiate(entryPrefab, entryPosition, entryPrefab.transform.rotation);
        }

        private void SpawnExit()
        {
            Vector3 exitSize = MeshAbsoluteSize.Calculate(exitPrefab.GetComponent<MeshFilter>());
            Vector3 exitPosition = new Vector3(
                levelSizeSO.TopRightPoint.x - exitSize.x / 2,
                levelSizeSO.TopRightPoint.y,
                levelSizeSO.TopRightPoint.z - exitSize.z / 2
            );
            ExitPoint exitPoint = Instantiate(exitPrefab, exitPosition, entryPrefab.transform.rotation);
        }

        private void SpawnObstacles()
        {
            ObstaclesSpawner obstaclesSpawner = new(
                levelSizeSO.TopLeftPoint,
                levelSizeSO.BottomRightPoint,
                obstaclePrefab,
                forbiddenCollisionByTags: new List<string>{ TagsConstants.OBSTACLE, TagsConstants.ENTRY, TagsConstants.EXIT });
            int obstaclesCount = 0;
            while (obstaclesCount < levelSizeSO.MaxObstacles
                && obstaclesSpawner.TrySpawnNextObstacle(out NavMeshObstacle obstacle))
            {
                obstaclesCount++;
            }
        }
    }
}
