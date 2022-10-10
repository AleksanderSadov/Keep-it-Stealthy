using KeepItStealthy.Core;
using KeepItStealthy.Gameplay.Components;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace KeepItStealthy.Gameplay.Helpers
{
    public class EnemiesSpawner
    {
        private readonly EnemyController enemyPrefab;
        private readonly List<SpawnGridPoint> spawnPoints;
        private Vector3 enemyHalfSize;
        private readonly List<string> forbiddenCollisionByTags;

        public EnemiesSpawner(
            Vector3 topLeftPoint,
            Vector3 bottomRightPoint,
            EnemyController enemyPrefab,
            List<string> forbiddenCollisionByTags = null)
        {
            this.enemyPrefab = enemyPrefab;
            this.forbiddenCollisionByTags = forbiddenCollisionByTags;

            enemyHalfSize = new Vector3(0.4f, 1.35f / 2f, 0.4f);

            spawnPoints = GridGeneration<SpawnGridPoint>.Generate(
                topLeftPoint: new Vector3(
                    topLeftPoint.x + enemyHalfSize.x,
                    topLeftPoint.y,
                    topLeftPoint.z - enemyHalfSize.z
                ),
                bottomRightPoint: new Vector3(
                    bottomRightPoint.x - enemyHalfSize.x,
                    bottomRightPoint.y,
                    bottomRightPoint.z + enemyHalfSize.z
                ),
                gridStep: 1f
            );
        }

        public bool TrySpawnNextEnemy(out EnemyController enemy)
        {
            while (TryGetNextSpawnPoint(out SpawnGridPoint nextSpawnPoint))
            {
                if (IsCollidingWithForbiddenObjects(nextSpawnPoint.point))
                {
                    nextSpawnPoint.isAvailable = false;
                    continue;
                }

                nextSpawnPoint.isAvailable = false;
                enemy = Object.Instantiate(
                    enemyPrefab,
                    nextSpawnPoint.point,
                    enemyPrefab.transform.rotation
                );
                return true;
            }

            enemy = null;
            return false;
        }

        private bool TryGetNextSpawnPoint(out SpawnGridPoint obstaclePoint)
        {
            List<SpawnGridPoint> availableSpawnPoints = spawnPoints.FindAll(p => p.isAvailable);

            if (availableSpawnPoints.Count > 0)
            {
                int randomIndex = Random.Range(0, availableSpawnPoints.Count);
                obstaclePoint = availableSpawnPoints[randomIndex];
                return true;
            }
            else
            {
                obstaclePoint = null;
                return false;
            }
        }

        private bool IsCollidingWithForbiddenObjects(Vector3 spawnPosition)
        {
            Collider[] hitColliders = Physics.OverlapBox(center: spawnPosition, halfExtents: enemyHalfSize);

            foreach (Collider collider in hitColliders)
            {
                foreach (var tag in forbiddenCollisionByTags)
                {
                    if (collider.CompareTag(tag))
                    {
                        return true;
                    }
                }
                
                continue;
            }

            return false;
        }
    }
}

