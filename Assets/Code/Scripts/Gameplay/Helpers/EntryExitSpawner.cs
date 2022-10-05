using KeepItStealthy.Gameplay.Components;
using KeepItStealthy.Gameplay.ScriptableObjects;
using UnityEngine;

namespace KeepItStealthy.Gameplay.Helpers
{
    public class EntryExitSpawner
    {
        private readonly WorldSizeSO worldSizeSO;
        private readonly EntryPoint entryPrefab;
        private readonly ExitPoint exitPrefab;

        public EntryExitSpawner(WorldSizeSO worldSizeSO, EntryPoint entryPrefab, ExitPoint exitPrefab)
        {
            this.worldSizeSO = worldSizeSO;
            this.entryPrefab = entryPrefab;
            this.exitPrefab = exitPrefab;
        }

        public void SpawnEntry()
        {
            Vector3 entrySize = Vector3.Scale(
                entryPrefab.transform.localScale,
                entryPrefab.GetComponent<MeshFilter>().sharedMesh.bounds.size
            );

            EntryPoint entryPoint = Object.Instantiate(
                entryPrefab,
                new Vector3(
                    worldSizeSO.BottomLeftPoint.x + entrySize.x / 2,
                    worldSizeSO.BottomLeftPoint.y,
                    worldSizeSO.BottomLeftPoint.z + entrySize.z / 2
                ),
                entryPrefab.transform.rotation
            );
        }

        public void SpawnExit()
        {
            Vector3 exitSize = Vector3.Scale(
                exitPrefab.transform.localScale,
                exitPrefab.GetComponent<MeshFilter>().sharedMesh.bounds.size
            );

            ExitPoint exitPoint = Object.Instantiate(
                exitPrefab,
                new Vector3(
                    worldSizeSO.TopRightPoint.x - exitSize.x / 2,
                    worldSizeSO.TopRightPoint.y,
                    worldSizeSO.TopRightPoint.z - exitSize.z / 2
                ),
                exitPrefab.transform.rotation
            );
        }
    }
}

