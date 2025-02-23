using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PathDetection : MonoBehaviour
{
    public static PathDetection Instance;
    public LineRenderer lineRenderer; // Assign in the inspector
    public Transform exitPoint;
    public Transform player;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        lineRenderer.enabled = false;
    }

    public void ShowExitPath()
    {
        if (exitPoint == null || player == null)
        {
            Debug.LogError("Exit point or player reference missing!");
            return;
        }

        NavMeshPath path = new NavMeshPath();
        if (NavMesh.CalculatePath(player.position, exitPoint.position, NavMesh.AllAreas, path))
        {
            DrawPath(path);
        }
        else
        {
            Debug.LogError("No valid path found!");
        }
    }

    // private void DrawPath(NavMeshPath path)
    // {
    //     lineRenderer.positionCount = path.corners.Length;
    //     lineRenderer.SetPositions(path.corners);
    //     lineRenderer.enabled = true;
    // }
    
    private void DrawPath(NavMeshPath path)
    {
        lineRenderer.positionCount = path.corners.Length;
    
        Vector3[] adjustedCorners = new Vector3[path.corners.Length];
        for (int i = 0; i < path.corners.Length; i++)
        {
            adjustedCorners[i] = path.corners[i] + Vector3.up * 0.2f; // Lift path slightly
        }

        lineRenderer.SetPositions(adjustedCorners);
        lineRenderer.enabled = true;
    }
}
