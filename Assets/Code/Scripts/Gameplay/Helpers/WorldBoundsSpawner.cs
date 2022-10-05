using KeepItStealthy.Gameplay.ScriptableObjects;
using UnityEngine;
using UnityEngine.AI;

namespace KeepItStealthy.Gameplay.Helpers
{
    public class WorldBoundsSpawner
    {
        private readonly WorldSizeSO worldSizeSO;
        private readonly NavMeshObstacle obstaclePrefab;

        public WorldBoundsSpawner(WorldSizeSO worldSizeSO, NavMeshObstacle obstaclePrefab)
        {
            this.worldSizeSO = worldSizeSO;
            this.obstaclePrefab = obstaclePrefab;
        }

        public void Spawn()
        {
            Vector3 obstacleSize = Vector3.Scale(
                obstaclePrefab.transform.localScale,
                obstaclePrefab.GetComponent<MeshFilter>().sharedMesh.bounds.size
            );

            NavMeshObstacle topBoundObstacle = Object.Instantiate(
                obstaclePrefab,
                new Vector3(
                    worldSizeSO.TopCenterPoint.x,
                    worldSizeSO.TopCenterPoint.y + obstacleSize.y / 2f,
                    worldSizeSO.TopCenterPoint.z + obstacleSize.z / 2f
                ),
                obstaclePrefab.transform.rotation
            );
            topBoundObstacle.transform.localScale = new(
                worldSizeSO.WorldSquareSize + obstacleSize.x * 2,
                obstacleSize.y,
                obstacleSize.z
            );

            NavMeshObstacle rightBoundObstacle = Object.Instantiate(
                obstaclePrefab,
                new Vector3(
                    worldSizeSO.RightCenterPoint.x + obstacleSize.x / 2f,
                    worldSizeSO.RightCenterPoint.y + obstacleSize.y / 2f,
                    worldSizeSO.RightCenterPoint.z
                ),
                obstaclePrefab.transform.rotation
            );
            rightBoundObstacle.transform.localScale = new(
                obstacleSize.x,
                obstacleSize.y,
                worldSizeSO.WorldSquareSize + obstacleSize.z * 2
            );

            NavMeshObstacle bottomBoundObstacle = Object.Instantiate(
                obstaclePrefab,
                new Vector3(
                    worldSizeSO.BottomCenterPoint.x,
                    worldSizeSO.BottomCenterPoint.y + obstacleSize.y / 2f,
                    worldSizeSO.BottomCenterPoint.z - obstacleSize.z / 2f
                ),
                obstaclePrefab.transform.rotation
            );
            bottomBoundObstacle.transform.localScale = new(
                worldSizeSO.WorldSquareSize + obstacleSize.x * 2,
                obstacleSize.y,
                obstacleSize.z
            );

            NavMeshObstacle leftBoundObstacle = Object.Instantiate(
                obstaclePrefab,
                new Vector3(
                    worldSizeSO.LeftCenterPoint.x - obstacleSize.x / 2f,
                    worldSizeSO.LeftCenterPoint.y + obstacleSize.y / 2f,
                    worldSizeSO.LeftCenterPoint.z
                ),
                obstaclePrefab.transform.rotation
            );
            leftBoundObstacle.transform.localScale = new(
                obstacleSize.x,
                obstacleSize.y,
                worldSizeSO.WorldSquareSize + obstacleSize.z * 2
            );
        }
    }
}

