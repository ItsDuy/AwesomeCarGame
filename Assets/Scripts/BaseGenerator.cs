using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseGenerator : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform player;
    [SerializeField] private GameObject roadSegmentPrefab;
    
    [Header("Settings")]
    [SerializeField] private float segmentLength = 14f;
    [SerializeField] private int numberOfSegments = 3;
    
    private List<GameObject> roadSegments = new List<GameObject>();
    private float nextRepositionZ = 14f; // When player crosses this, reposition the back segment
    
    void Start()
    {
        InitializeSegments();
    }

    void Update()
    {
        TrackPlayerPosition();
    }
    
    private void InitializeSegments()
    {
        // Create initial road segments at (0,0,0), (0,0,14), (0,0,28)
        for (int i = 0; i < numberOfSegments; i++)
        {
            GameObject segment = Instantiate(roadSegmentPrefab, transform);
            segment.transform.position = new Vector3(0, 0, i * segmentLength);
            roadSegments.Add(segment);
        }
    }
    
    private void TrackPlayerPosition()
    {
        // Check if player has crossed the next reposition threshold
        if (player.position.z > nextRepositionZ)
        {
            RepositionSegment();
            nextRepositionZ += segmentLength;
        }
    }
    
    private void RepositionSegment()
    {
        // Get the segment that's furthest back
        GameObject segmentToMove = roadSegments[0];
        roadSegments.RemoveAt(0);
        
        // Move it to the front (after the last segment)
        GameObject lastSegment = roadSegments[roadSegments.Count - 1];
        float newZ = lastSegment.transform.position.z + segmentLength;
        segmentToMove.transform.position = new Vector3(0, 0, newZ);
        
        // Add it back to the end of the list
        roadSegments.Add(segmentToMove);
    }
}
