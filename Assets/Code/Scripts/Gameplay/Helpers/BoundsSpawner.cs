using KeepItStealthy.Core;
using UnityEngine;
using UnityEngine.AI;

namespace KeepItStealthy.Gameplay.Helpers
{
    public class BoundsSpawner
    {
        private Vector3 topCenterPoint;
        private Vector3 rightCenterPoint;
        private Vector3 bottomCenterPoint;
        private Vector3 leftCenterPoint;
        private readonly float boundsWidth;
        private readonly float boundsHeight;
        private readonly NavMeshObstacle obstaclePrefab;

        public BoundsSpawner(Vector3 topLeftPoint, Vector3 bottomRightPoint, NavMeshObstacle obstaclePrefab)
        {
            this.obstaclePrefab = obstaclePrefab;

            boundsWidth = Mathf.Abs(bottomRightPoint.x - topLeftPoint.x);
            boundsHeight = Mathf.Abs(bottomRightPoint.x - topLeftPoint.x);

            topCenterPoint = new Vector3(topLeftPoint.x + boundsWidth / 2, topLeftPoint.y, topLeftPoint.z);
            rightCenterPoint = new Vector3(bottomRightPoint.x, topLeftPoint.y, topLeftPoint.z - boundsWidth / 2);
            bottomCenterPoint = new Vector3(bottomRightPoint.x - boundsWidth / 2, bottomRightPoint.y, bottomRightPoint.z);
            leftCenterPoint = new Vector3(topLeftPoint.x, topLeftPoint.y, topLeftPoint.z - boundsWidth / 2);
        }

        public void Spawn()
        {
            Vector3 obstacleSize = MeshAbsoluteSize.Calculate(obstaclePrefab.GetComponent<MeshFilter>());

            NavMeshObstacle topBoundObstacle = Object.Instantiate(
                obstaclePrefab,
                new Vector3(
                    topCenterPoint.x,
                    topCenterPoint.y + obstacleSize.y / 2f,
                    topCenterPoint.z + obstacleSize.z / 2f
                ),
                obstaclePrefab.transform.rotation
            );
            topBoundObstacle.transform.localScale = new(
                boundsWidth + obstacleSize.x * 2,
                obstacleSize.y,
                obstacleSize.z
            );

            NavMeshObstacle rightBoundObstacle = Object.Instantiate(
                obstaclePrefab,
                new Vector3(
                    rightCenterPoint.x + obstacleSize.x / 2f,
                    rightCenterPoint.y + obstacleSize.y / 2f,
                    rightCenterPoint.z
                ),
                obstaclePrefab.transform.rotation
            );
            rightBoundObstacle.transform.localScale = new(
                obstacleSize.x,
                obstacleSize.y,
                boundsHeight + obstacleSize.z * 2
            );

            NavMeshObstacle bottomBoundObstacle = Object.Instantiate(
                obstaclePrefab,
                new Vector3(
                    bottomCenterPoint.x,
                    bottomCenterPoint.y + obstacleSize.y / 2f,
                    bottomCenterPoint.z - obstacleSize.z / 2f
                ),
                obstaclePrefab.transform.rotation
            );
            bottomBoundObstacle.transform.localScale = new(
                boundsWidth + obstacleSize.x * 2,
                obstacleSize.y,
                obstacleSize.z
            );

            NavMeshObstacle leftBoundObstacle = Object.Instantiate(
                obstaclePrefab,
                new Vector3(
                    leftCenterPoint.x - obstacleSize.x / 2f,
                    leftCenterPoint.y + obstacleSize.y / 2f,
                    leftCenterPoint.z
                ),
                obstaclePrefab.transform.rotation
            );
            leftBoundObstacle.transform.localScale = new(
                obstacleSize.x,
                obstacleSize.y,
                boundsHeight + obstacleSize.z * 2
            );
        }
    }
}

