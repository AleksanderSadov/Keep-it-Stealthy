using System.Collections.Generic;
using UnityEngine;

namespace KeepItStealthy.Core
{
    public class GridDivision<T> where T : GridPoint, new()
    {
        private Vector3 topLeftPoint;
        private Vector3 bottomRightPoint;
        private float gridStep = 0.1f;
        private float gridWidth;
        private float gridHeight;
        private List<T> gridPoints;

        public GridDivision(Vector3 topLeftPoint, Vector3 bottomRightPoint, float gridStep)
        {
            this.topLeftPoint = topLeftPoint;
            this.bottomRightPoint = bottomRightPoint;
            this.gridStep = gridStep;

            gridWidth = Mathf.Abs(bottomRightPoint.x - topLeftPoint.x);
            gridHeight = Mathf.Abs(bottomRightPoint.z - topLeftPoint.z);
        }

        public List<T> GenerateGrid()
        {
            gridPoints = new List<T>();
            
            float currentPositionX = topLeftPoint.x;
            float currentPositionY = topLeftPoint.y;
            float currentPositionZ = topLeftPoint.z;

            int tryCount = 0;
            int maxGridTries = ((int)Mathf.Floor(gridWidth / gridStep)) * ((int)Mathf.Floor(gridHeight / gridStep));

            while (currentPositionZ >= bottomRightPoint.z && tryCount < maxGridTries)
            {
                while (currentPositionX <= bottomRightPoint.x && tryCount < maxGridTries)
                {
                    T currentPoint = new();
                    currentPoint.point = new Vector3(currentPositionX, currentPositionY, currentPositionZ);
                    gridPoints.Add(currentPoint);
                    currentPositionX += gridStep;
                    tryCount++;
                }

                currentPositionX = topLeftPoint.x;
                currentPositionZ -= gridStep;
            }

            return gridPoints;
        }
    }
}

