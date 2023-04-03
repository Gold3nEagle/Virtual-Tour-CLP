using System.Collections.Generic;
using UnityEngine;

public class ScatterBuildingsBrush : MonoBehaviour
{
    [SerializeField] private GameObject[] buildingPrefabs;
    [SerializeField] private Vector2 range = new Vector2(10, 10);
    [SerializeField] private int numberOfBuildings = 10;
    [SerializeField] private float minRotation = 0;
    [SerializeField] private float maxRotation = 360;

    private List<GameObject> generatedBuildings = new List<GameObject>();

    [ContextMenu("Generate Buildings")]
    private void GenerateBuildings()
    {
        for (int i = 0; i < numberOfBuildings; i++)
        {
            int randomBuildingIndex = Random.Range(0, buildingPrefabs.Length);

            Vector3 position = GetRandomPosition();
            position.y = GetTerrainHeight(position);

            new Vector3();
            float randomBuildingRotation = Random.Range(minRotation, maxRotation);
            Vector3 newBuildingRotation = new Vector3(0, randomBuildingRotation, 0);
            Quaternion rot = Quaternion.Euler(newBuildingRotation.x, newBuildingRotation.y, newBuildingRotation.z);

            // GameObject building = Instantiate(buildingPrefabs[randomBuildingIndex], position, Quaternion.identity, this.transform);
            GameObject building = Instantiate(buildingPrefabs[randomBuildingIndex], position, rot, this.transform);
            generatedBuildings.Add(building);
        }
    }

    [ContextMenu("Revert Buildings")]
    private void RevertBuildings()
    {
        if (generatedBuildings.Count > 0)
        {
            GameObject lastBuilding = generatedBuildings[generatedBuildings.Count - 1];
            generatedBuildings.RemoveAt(generatedBuildings.Count - 1);
            DestroyImmediate(lastBuilding);
        }
    }

    private Vector3 GetRandomPosition()
    {
        float x = Random.Range(-range.x / 2, range.x / 2);
        float z = Random.Range(-range.y / 2, range.y / 2);
        return new Vector3(x, 0, z) + transform.position;
    }

    private float GetTerrainHeight(Vector3 position)
    {
        RaycastHit hit;
        if (Physics.Raycast(position + Vector3.up * 1000, Vector3.down, out hit, Mathf.Infinity, LayerMask.GetMask("Terrain")))
        {
            return hit.point.y;
        }

        return 0;
    }
}