using KeepItStealthy.Gameplay.Components;
using System.Collections.Generic;
using UnityEngine;

namespace KeepItStealthy.Gameplay.ScriptableObjects
{
    [CreateAssetMenu(fileName = "TeamsList", menuName = "ScriptableObjects/TeamsList")]
    public class TeamsListSO : ScriptableObject
    {
        [Header("Runtime")]
        private List<PlayerController> players = new();
        private List<EnemyController> enemies = new();

        public List<PlayerController> Players { get => players; private set => players = value; }
        public List<EnemyController> Enemies { get => enemies; private set => enemies = value; }

        private void OnEnable()
        {
            players.Clear();
            enemies.Clear();
        }

        private void OnDisable()
        {
            players.Clear();
            enemies.Clear();
        }
    }
}

