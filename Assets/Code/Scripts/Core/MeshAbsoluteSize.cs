using UnityEngine;

namespace KeepItStealthy.Core
{
    public static class MeshAbsoluteSize
    {
        public static Vector3 Calculate(MeshFilter meshFilter)
        {
            return Vector3.Scale(meshFilter.transform.localScale, meshFilter.sharedMesh.bounds.size);
        }
    }
}

