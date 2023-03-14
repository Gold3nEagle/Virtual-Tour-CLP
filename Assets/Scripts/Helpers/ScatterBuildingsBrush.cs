using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScatterBuildingsBrush : MonoBehaviour
{
    public GameObject buildingPrefab;
    public int numBuildings;
    public Transform cursorTransform; // The transform of the cursor used to define the circular area

    private List<GameObject> spawnedBuildings = new List<GameObject>();

    [ContextMenu("Generate City")]
    void GenerateCity()
    {
        Terrain terrain = Terrain.activeTerrain;

        // Get the radius of the circular area from the cursor Transform's scale
        float radius = cursorTransform.localScale.x * 0.5f;

        // Spawn the buildings and add them to the spawnedBuildings list
        for (int i = 0; i < numBuildings; i++)
        {
            Vector2 randomCirclePos = Random.insideUnitCircle * radius;
            Vector3 position = new Vector3(randomCirclePos.x, 0, randomCirclePos.y) + cursorTransform.position;
            position.y = terrain.SampleHeight(position) + 0.5f * buildingPrefab.transform.localScale.y;
            Quaternion rotation = Quaternion.Euler(0, Random.Range(0, 360), 0);
            GameObject newBuilding = Instantiate(buildingPrefab, position, rotation);
            newBuilding.transform.parent = transform;
            spawnedBuildings.Add(newBuilding);
        }
    }

    [ContextMenu("Undo Last Generation")]
    void UndoLastGeneration()
    {
        // Remove the last n spawned buildings from the list and destroy them
        int n = Mathf.Min(numBuildings, spawnedBuildings.Count);
        for (int i = 0; i < n; i++)
        {
            GameObject buildingToDestroy = spawnedBuildings[spawnedBuildings.Count - 1];
            spawnedBuildings.RemoveAt(spawnedBuildings.Count - 1);
            DestroyImmediate(buildingToDestroy);
        }
    }
}