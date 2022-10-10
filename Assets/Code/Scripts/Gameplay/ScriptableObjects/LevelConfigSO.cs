using UnityEngine;

namespace KeepItStealthy.Gameplay.ScriptableObjects
{
    [CreateAssetMenu(fileName = "LevelConfig", menuName = "ScriptableObjects/LevelConfig")]
    public class LevelConfigSO : ScriptableObject
    {
        private static int MIN_WORLD_SQUARE_SIZE = 6;
        private static int MAX_WORLD_SQUARE_SIZE = 100;

        [SerializeField]
        [Tooltip("Size of square world in meters")]
        private int levelSquareSize = 10;
        [SerializeField]
        [Tooltip("Max number of obstacles on the level")]
        private int maxObstacles = 10;
        [SerializeField]
        [Tooltip("Max number of enemies on the level")]
        private int maxEnemies = 1;

        private Vector3 topLeftPoint;
        private Vector3 topRightPoint;
        private Vector3 bottomRightPoint;
        private Vector3 bottomLeftPoint;

        private Vector3 topCenterPoint;
        private Vector3 rightCenterPoint;
        private Vector3 bottomCenterPoint;
        private Vector3 leftCenterPoint;

        public int LevelSquareSize { get => levelSquareSize; private set => levelSquareSize = value; }
        public int MaxObstacles { get => maxObstacles; private set => maxObstacles = value; }

        public Vector3 TopLeftPoint { get => topLeftPoint; private set => topLeftPoint = value; }
        public Vector3 TopRightPoint { get => topRightPoint; private set => topRightPoint = value; }
        public Vector3 BottomRightPoint { get => bottomRightPoint; private set => bottomRightPoint = value; }
        public Vector3 BottomLeftPoint { get => bottomLeftPoint; private set => bottomLeftPoint = value; }
        public Vector3 TopCenterPoint { get => topCenterPoint; private set => topCenterPoint = value; }
        public Vector3 RightCenterPoint { get => rightCenterPoint; private set => rightCenterPoint = value; }
        public Vector3 BottomCenterPoint { get => bottomCenterPoint; private set => bottomCenterPoint = value; }
        public Vector3 LeftCenterPoint { get => leftCenterPoint; private set => leftCenterPoint = value; }
        public int MaxEnemies { get => maxEnemies; private set => maxEnemies = value; }

        private void Awake()
        {
            CalculateBounds();
        }

        private void OnValidate()
        {
            levelSquareSize = Mathf.Clamp(levelSquareSize, MIN_WORLD_SQUARE_SIZE, MAX_WORLD_SQUARE_SIZE);
            maxObstacles = Mathf.Clamp(maxObstacles, 0, levelSquareSize * levelSquareSize);

            CalculateBounds();
        }

        private void CalculateBounds()
        {
            TopLeftPoint = new(-levelSquareSize / 2f, 0, levelSquareSize / 2f);
            TopRightPoint = new(levelSquareSize / 2f, 0, levelSquareSize / 2f);
            BottomRightPoint = new(levelSquareSize / 2f, 0, -levelSquareSize / 2f);
            BottomLeftPoint = new(-levelSquareSize / 2f, 0, -levelSquareSize / 2f);

            TopCenterPoint = new(0, 0, levelSquareSize / 2f);
            RightCenterPoint = new(levelSquareSize / 2f, 0, 0);
            BottomCenterPoint = new(0, 0, -levelSquareSize / 2f);
            LeftCenterPoint = new(-levelSquareSize / 2f, 0, 0);
        }
    }
}

