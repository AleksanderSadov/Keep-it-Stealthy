using KeepItStealthy.Core;
using KeepItStealthy.Gameplay.Components;
using KeepItStealthy.Gameplay.Constants;
using KeepItStealthy.Gameplay.Events;
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
        private LevelConfigSO levelSizeSO;
        [SerializeField]
        private TeamsListSO teamsListSO;
        [SerializeField]
        private PlayerController playerPrefab;
        [SerializeField]
        private EnemyController enemyPrefab;
        [SerializeField]
        private NavMeshObstacle obstaclePrefab;
        [SerializeField]
        private EntryPoint entryPrefab;
        [SerializeField]
        private ExitPoint exitPrefab;

        private void OnEnable()
        {
            EventsManager.AddListener<GameStartedEvent>(OnGameStarted);
        }

        private void OnDisable()
        {
            EventsManager.RemoveListener<GameStartedEvent>(OnGameStarted);
        }

        public void GenerateLevel()
        {
            SpawnLevelBounds();
            EntryPoint entryPoint = SpawnEntry();
            ExitPoint exitPoint = SpawnExit();
            SpawnPlayer(entryPoint.transform.position);
            SpawnEnemies();
            SpawnObstacles();
        }

        private void OnGameStarted(GameStartedEvent evt) => GenerateLevel();

        private void SpawnLevelBounds()
        {
            BoundsSpawner levelBoundsSpawner = new(levelSizeSO.TopLeftPoint, levelSizeSO.BottomRightPoint, obstaclePrefab);
            levelBoundsSpawner.Spawn();
        }

        private EntryPoint SpawnEntry()
        {
            Vector3 entrySize = MeshAbsoluteSize.Calculate(entryPrefab.GetComponent<MeshFilter>());
            Vector3 entryPosition = new Vector3(
                levelSizeSO.BottomLeftPoint.x + entrySize.x / 2,
                levelSizeSO.BottomLeftPoint.y,
                levelSizeSO.BottomLeftPoint.z + entrySize.z / 2
            );
            EntryPoint entryPoint = Instantiate(entryPrefab, entryPosition, entryPrefab.transform.rotation);
            return entryPoint;
        }

        private ExitPoint SpawnExit()
        {
            Vector3 exitSize = MeshAbsoluteSize.Calculate(exitPrefab.GetComponent<MeshFilter>());
            Vector3 exitPosition = new Vector3(
                levelSizeSO.TopRightPoint.x - exitSize.x / 2,
                levelSizeSO.TopRightPoint.y,
                levelSizeSO.TopRightPoint.z - exitSize.z / 2
            );
            ExitPoint exitPoint = Instantiate(exitPrefab, exitPosition, entryPrefab.transform.rotation);
            return exitPoint;
        }

        private PlayerController SpawnPlayer(Vector3 spawnPosition)
        {
            PlayerController player = Instantiate(playerPrefab, spawnPosition, playerPrefab.transform.rotation);
            return player;
        }

        private void SpawnEnemies()
        {
            EnemiesSpawner enemiesSpawner = new(
                levelSizeSO.TopLeftPoint,
                levelSizeSO.BottomRightPoint,
                enemyPrefab,
                forbiddenCollisionByTags: new List<string>{
                    TagsConstants.PLAYER,
                    TagsConstants.ENEMY,
                    TagsConstants.OBSTACLE,
                    TagsConstants.ENTRY,
                    TagsConstants.EXIT
                });
            int enemiesCount = 0;
            while (
                enemiesCount < levelSizeSO.MaxEnemies
                && enemiesSpawner.TrySpawnNextEnemy(out EnemyController enemy)
            )
            {
                enemiesCount++;
            }
        }

        private void SpawnObstacles()
        {
            ObstaclesSpawner obstaclesSpawner = new(
                levelSizeSO.TopLeftPoint,
                levelSizeSO.BottomRightPoint,
                obstaclePrefab,
                minFreePathWidth: 1f,
                forbiddenCollisionByTags: new List<string>{
                    TagsConstants.PLAYER,
                    TagsConstants.ENEMY,
                    TagsConstants.OBSTACLE,
                    TagsConstants.ENTRY,
                    TagsConstants.EXIT 
                });
            int obstaclesCount = 0;
            while (
                obstaclesCount < levelSizeSO.MaxObstacles
                && obstaclesSpawner.TrySpawnNextObstacle(out NavMeshObstacle obstacle)
            )
            {
                obstaclesCount++;
            }
        }
    }
}
