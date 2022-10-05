using UnityEngine;

namespace KeepItStealthy.Gameplay.ScriptableObjects
{
    [CreateAssetMenu(fileName = "WorldSize", menuName = "ScriptableObjects/WorldSize")]
    public class WorldSizeSO : ScriptableObject
    {
        private static int MIN_WORLD_SQUARE_SIZE = 6;
        private static int MAX_WORLD_SQUARE_SIZE = 100;

        [SerializeField]
        [Tooltip("Size of square world in meters")]
        private int worldSquareSize = 10;

        private Vector3 topLeftPoint;
        private Vector3 topRightPoint;
        private Vector3 bottomRightPoint;
        private Vector3 bottomLeftPoint;

        private Vector3 topCenterPoint;
        private Vector3 rightCenterPoint;
        private Vector3 bottomCenterPoint;
        private Vector3 leftCenterPoint;

        public int WorldSquareSize { get => worldSquareSize; private set => worldSquareSize = value; }

        public Vector3 TopLeftPoint { get => topLeftPoint; private set => topLeftPoint = value; }
        public Vector3 TopRightPoint { get => topRightPoint; private set => topRightPoint = value; }
        public Vector3 BottomRightPoint { get => bottomRightPoint; private set => bottomRightPoint = value; }
        public Vector3 BottomLeftPoint { get => bottomLeftPoint; private set => bottomLeftPoint = value; }
        public Vector3 TopCenterPoint { get => topCenterPoint; private set => topCenterPoint = value; }
        public Vector3 RightCenterPoint { get => rightCenterPoint; private set => rightCenterPoint = value; }
        public Vector3 BottomCenterPoint { get => bottomCenterPoint; private set => bottomCenterPoint = value; }
        public Vector3 LeftCenterPoint { get => leftCenterPoint; private set => leftCenterPoint = value; }

        private void Awake()
        {
            CalculateBounds();
        }

        private void OnValidate()
        {
            worldSquareSize = Mathf.Clamp(worldSquareSize, MIN_WORLD_SQUARE_SIZE, MAX_WORLD_SQUARE_SIZE);
            CalculateBounds();
        }

        private void CalculateBounds()
        {
            TopLeftPoint = new(-worldSquareSize / 2f, 0, worldSquareSize / 2f);
            TopRightPoint = new(worldSquareSize / 2f, 0, worldSquareSize / 2f);
            BottomRightPoint = new(worldSquareSize / 2f, 0, -worldSquareSize / 2f);
            BottomLeftPoint = new(-worldSquareSize / 2f, 0, -worldSquareSize / 2f);

            TopCenterPoint = new(0, 0, worldSquareSize / 2f);
            RightCenterPoint = new(worldSquareSize / 2f, 0, 0);
            BottomCenterPoint = new(0, 0, -worldSquareSize / 2f);
            LeftCenterPoint = new(-worldSquareSize / 2f, 0, 0);
        }
    }
}

