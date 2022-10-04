using System.Collections;
using UnityEngine;

namespace KeepItStealthy.Gameplay.Managers
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField]
        private GameObject floorTilePrefab;
        [SerializeField]
        private int floorSquareSize = 1;

        private void Start()
        {
            StartCoroutine(StartGame());
        }

        private IEnumerator StartGame()
        {
            yield return null;
            GenerateLevel();
        }

        private void GenerateLevel()
        {
            for (int i = 0; i < floorSquareSize; i++)
            {

            }
        }
    }
}

