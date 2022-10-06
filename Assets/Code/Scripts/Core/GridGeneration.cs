using System.Collections.Generic;
using UnityEngine;

namespace KeepItStealthy.Core
{
    public static class GridGeneration<T> where T : GridPoint, new()
    {
        public static List<T> Generate(Vector3 topLeftPoint, Vector3 bottomRightPoint, float gridStep)
        {
            float gridWidth = Mathf.Abs(bottomRightPoint.x - topLeftPoint.x);
            float gridHeight = Mathf.Abs(bottomRightPoint.z - topLeftPoint.z);

            List<T> gridPoints = new();
            
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

