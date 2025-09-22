using UnityEngine;

public class GridManager : MonoBehaviour
{
    public float cellSize = 10f;

    public Vector3 GetNearestGridPosition(Vector3 worldPosition)
    {
        float x = Mathf.Round(worldPosition.x / cellSize) * cellSize;
        float y = Mathf.Round(worldPosition.y / cellSize) * cellSize;
        float z = Mathf.Round(worldPosition.z / cellSize/2) * cellSize;

        return new Vector3(x, y, z);
    }
}