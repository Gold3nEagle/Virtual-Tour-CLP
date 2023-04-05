using System.Collections.Generic;
using UnityEngine;

public class ScatterBuildingsBrush : MonoBehaviour
{
    [SerializeField] private GameObject[] buildingPrefabs;
    [SerializeField] private Vector2 range = new Vector2(10, 10);
    [SerializeField] private int numberOfBuildings = 10;
    private List<GameObject> lastGeneratedBuildings = new List<GameObject>();


    [Header("Buildings Rotation")]
    [SerializeField] private float minRotation = 0;
    [SerializeField] private float maxRotation = 360;


    [Header("Buildings Length")]
    [SerializeField] private float minLength = 0.4f;
    [SerializeField] private float maxLength = 1.0f;


    [Header("Buildings Height")]
    [SerializeField] private float minHeight = 0.4f;
    [SerializeField] private float maxHeight = 1.0f;


    [Header("Buildings Width")]
    [SerializeField] private float minWidth = 0.4f;
    [SerializeField] private float maxWidth = 1.0f;


    private List<GameObject> generatedBuildings = new List<GameObject>();

    [ContextMenu("Generate Buildings")]
    private void GenerateBuildings()
    {
        for (int i = 0; i < numberOfBuildings; i++)
        {
            lastGeneratedBuildings.Clear();

            Vector3 position = GetRandomPosition();
            position.y = GetTerrainHeight(position);

            float randomBuildingRotation = Random.Range(minRotation, maxRotation);
            Vector3 newBuildingRotation = new Vector3(0, randomBuildingRotation, 0);
            Quaternion rot = Quaternion.Euler(newBuildingRotation.x, newBuildingRotation.y, newBuildingRotation.z);

            int randomBuildingIndex = Random.Range(0, buildingPrefabs.Length);

            GameObject building = Instantiate(buildingPrefabs[randomBuildingIndex], position, rot, this.transform);
            building.transform.localScale = GetRandomBuildingScaling();
            generatedBuildings.Add(building);
            lastGeneratedBuildings.Add(building);
        }
    }

    [ContextMenu("Revert Buildings")]
    private void RevertBuildings()
    {
        foreach (GameObject building in lastGeneratedBuildings)
        {
            generatedBuildings.Remove(building);
            DestroyImmediate(building);
        }
        lastGeneratedBuildings.Clear();
    }

    private Vector3 GetRandomPosition()
    {
        float x = Random.Range(-range.x / 2, range.x / 2);
        float z = Random.Range(-range.y / 2, range.y / 2);
        return new Vector3(x, 0, z) + transform.position;
    }

    private Vector3 GetRandomBuildingScaling()
    {
        float buildingLength = Random.Range(minLength, maxLength); // X-Axis
        float buildingHeight = Random.Range(minHeight, maxHeight); // Y-Axis
        float buildingWidth = Random.Range(minWidth, maxWidth);    // Z-Axis

        return new Vector3(buildingLength, buildingHeight, buildingWidth);
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