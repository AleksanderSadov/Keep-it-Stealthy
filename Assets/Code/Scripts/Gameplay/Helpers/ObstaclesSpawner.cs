using KeepItStealthy.Core;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace KeepItStealthy.Gameplay.Helpers
{
    public class ObstaclesSpawner
    {
        private readonly NavMeshObstacle obstaclePrefab;
        private readonly List<SpawnGridPoint> obstaclesGridPoints;
        private Vector3 obstacleHalfSize;
        private readonly List<string> forbiddenCollisionByTags;

        public ObstaclesSpawner(
            Vector3 topLeftPoint,
            Vector3 bottomRightPoint,
            NavMeshObstacle obstaclePrefab,
            float minFreePathWidth,
            List<string> forbiddenCollisionByTags = null)
        {
            this.obstaclePrefab = obstaclePrefab;
            this.forbiddenCollisionByTags = forbiddenCollisionByTags;

            obstacleHalfSize = MeshAbsoluteSize.Calculate(obstaclePrefab.GetComponent<MeshFilter>()) / 2f;

            obstaclesGridPoints = GridGeneration<SpawnGridPoint>.Generate(
                topLeftPoint: new Vector3(
                    topLeftPoint.x + obstacleHalfSize.x,
                    topLeftPoint.y + obstacleHalfSize.y,
                    topLeftPoint.z - obstacleHalfSize.z
                ),
                bottomRightPoint: new Vector3(
                    bottomRightPoint.x - obstacleHalfSize.x,
                    bottomRightPoint.y + obstacleHalfSize.y,
                    bottomRightPoint.z + obstacleHalfSize.z
                ),
                gridStep: minFreePathWidth
            );
        }

        public bool TrySpawnNextObstacle(out NavMeshObstacle obstacle)
        {
            while (TryGetNextSpawnPoint(out SpawnGridPoint nextObstaclePoint))
            {
                if (IsCollidingWithForbiddenObjects(nextObstaclePoint.point))
                {
                    nextObstaclePoint.isAvailable = false;
                    continue;
                }

                nextObstaclePoint.isAvailable = false;
                obstacle = Object.Instantiate(
                    obstaclePrefab,
                    nextObstaclePoint.point,
                    obstaclePrefab.transform.rotation
                );
                return true;
            }

            obstacle = null;
            return false;
        }

        private bool TryGetNextSpawnPoint(out SpawnGridPoint obstaclePoint)
        {
            List<SpawnGridPoint> availableObstaclePoints = obstaclesGridPoints.FindAll(p => p.isAvailable);

            if (availableObstaclePoints.Count > 0)
            {
                int randomIndex = Random.Range(0, availableObstaclePoints.Count);
                obstaclePoint = availableObstaclePoints[randomIndex];
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
            Collider[] hitColliders = Physics.OverlapBox(center: spawnPosition, halfExtents: obstacleHalfSize);

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

