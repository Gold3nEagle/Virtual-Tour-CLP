using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class ScatterBuildingsBrush : MonoBehaviour
{
    public GameObject[] buildingPrefabs;
    public int numberOfBuildings;
    public GameObject brush;

    private List<GameObject> spawnedBuildings = new List<GameObject>();

    [ContextMenu("Generate Buildings")]
    void GenerateBuildings()
    {
        if (buildingPrefabs == null || buildingPrefabs.Length == 0)
        {
            Debug.LogError("No building prefabs assigned.");
            return;
        }

        for (int i = 0; i < numberOfBuildings; i++)
        {
            Vector3 randomPoint = Random.insideUnitCircle * brush.transform.localScale.x;
            Vector3 spawnPosition = brush.transform.position + new Vector3(randomPoint.x, 0, randomPoint.y);

            RaycastHit hit;
            if (Physics.Raycast(spawnPosition + Vector3.up * 1000f, Vector3.down, out hit, Mathf.Infinity))
            {
                if (hit.collider.CompareTag("Terrain"))
                {
                    spawnPosition.y = hit.point.y;

                    GameObject buildingPrefab = buildingPrefabs[Random.Range(0, buildingPrefabs.Length)];
                    GameObject spawnedBuilding = Instantiate(buildingPrefab, spawnPosition, Quaternion.identity);
                    spawnedBuildings.Add(spawnedBuilding);
                }
            }
        }
    }

    [ContextMenu("Revert Last Buildings")]
    void RevertLastBuildings()
    {
        if (spawnedBuildings.Count == 0)
        {
            Debug.LogWarning("No buildings to revert.");
            return;
        }

        for (int i = 0; i < numberOfBuildings && spawnedBuildings.Count > 0; i++)
        {
            GameObject lastBuilding = spawnedBuildings[spawnedBuildings.Count - 1];
            spawnedBuildings.RemoveAt(spawnedBuildings.Count - 1);
            DestroyImmediate(lastBuilding);
        }
    }
}
