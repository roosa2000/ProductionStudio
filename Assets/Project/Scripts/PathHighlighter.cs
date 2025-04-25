using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PathHighlighter : MonoBehaviour
{
    public static PathHighlighter Instance;
    public GameObject pathPrefab; // Assign in Inspector
    public Transform exitPoint;
    public Transform player;
    
    public float tileSpacing = 1.0f; // Adjust for smoother paths

    private List<GameObject> spawnedPaths = new List<GameObject>(); // Track spawned path tiles

    private void Awake()
    {
        Instance = this;
        pathPrefab.SetActive(false);
    }

    public void ShowExitPath()
    {
        if (exitPoint == null || player == null || pathPrefab == null)
        {
            Debug.LogError("Exit point, player, or pathPrefab reference missing!");
            return;
        }

        NavMeshPath path = new NavMeshPath();
        if (NavMesh.CalculatePath(player.position, exitPoint.position, NavMesh.AllAreas, path))
        {
            ClearExistingPath(); // Remove old path before drawing a new one
            GenerateContinuousPath(path);
        }
        else
        {
            Debug.LogError("No valid path found!");
        }
    }

    private void GenerateContinuousPath(NavMeshPath path)
    {
        for (int i = 0; i < path.corners.Length - 1; i++)
        {
            Vector3 start = path.corners[i];
            Vector3 end = path.corners[i + 1];

            float distance = Vector3.Distance(start, end);
            int steps = Mathf.CeilToInt(distance / tileSpacing);

            for (int j = 0; j <= steps; j++)
            {
                Vector3 position = Vector3.Lerp(start, end, j / (float)steps) + Vector3.up * 0.001f; // Offset slightly above ground
                GameObject pathTile = Instantiate(pathPrefab, position, Quaternion.identity);
                pathPrefab.SetActive(true);
                spawnedPaths.Add(pathTile);
            }
        }
    }

    private void ClearExistingPath()
    {
        foreach (GameObject tile in spawnedPaths)
        {
            Destroy(tile);
        }
        spawnedPaths.Clear();
    }
}
