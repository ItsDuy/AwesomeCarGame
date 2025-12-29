
using UnityEditor;
using UnityEngine;
[ExecuteInEditMode]
public class GridSnap : MonoBehaviour
{
    public float gridSize = 1f;
    public Vector3 offset = Vector3.zero;
    void Update()
    {
        if (!Application.isPlaying)
        {
            Vector3 position = transform.position;
            float snappedPositionX = Mathf.Round(position.x / gridSize) * gridSize + offset.x;
            float snappedPositionY = Mathf.Round(position.y / gridSize) * gridSize + offset.y;
            float snappedPositionZ = Mathf.Round(position.z / gridSize) * gridSize + offset.z;

            Vector3 snappedPosition = new Vector3(snappedPositionX, snappedPositionY, snappedPositionZ);
            transform.position = snappedPosition;
        }
    }
}
