using KeepItStealthy.Core;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace KeepItStealthy.Gameplay.Helpers
{
    public class ObstaclesSpawner
    {
        private class ObstaclesGridPoint : GridPoint
        {
            public bool isAvailable = true;
        }

        private readonly NavMeshObstacle obstaclePrefab;
        private GridDivision<ObstaclesGridPoint> gridDivision;
        private readonly List<ObstaclesGridPoint> obstaclesGridPoints;
        private Vector3 obstacleHalfExtent;
        private readonly List<string> forbiddenCollisionByTags;

        public ObstaclesSpawner(
            Vector3 topLeftBoundPoint,
            Vector3 topRightBoundPoint,
            NavMeshObstacle obstaclePrefab, List<string> forbiddenCollisionByTags = null)
        {
            this.obstaclePrefab = obstaclePrefab;
            this.forbiddenCollisionByTags = forbiddenCollisionByTags;

            gridDivision = new(topLeftBoundPoint, topRightBoundPoint, gridStep: 0.1f);
            obstaclesGridPoints = gridDivision.GenerateGrid();

            obstacleHalfExtent = Vector3.Scale(
                obstaclePrefab.transform.localScale / 2f,
                obstaclePrefab.GetComponent<MeshFilter>().sharedMesh.bounds.size
            );

            foreach (var gridPoint in obstaclesGridPoints)
            {
                gridPoint.point.y += obstacleHalfExtent.y;
            }
        }

        public bool TrySpawnNextObstacle(out NavMeshObstacle obstacle)
        {
            while (TryGetNextSpawnPoint(out ObstaclesGridPoint nextObstaclePoint))
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

        private bool TryGetNextSpawnPoint(out ObstaclesGridPoint obstaclePoint)
        {
            List<ObstaclesGridPoint> availableObstaclePoints = obstaclesGridPoints.FindAll(p => p.isAvailable);

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
            Collider[] hitColliders = Physics.OverlapBox(center: spawnPosition, halfExtents: obstacleHalfExtent);

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

