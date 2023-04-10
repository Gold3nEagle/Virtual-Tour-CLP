using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointManager : MonoBehaviour
{
    public static WaypointManager instance;
    public GameObject waypointPrefab;
    public List<GameObject> waypointsList;

    void Start()
    {
        instance = this;
        waypointsList = new List<GameObject>();
    }

    public GameObject AddWaypoint(string waypointID, GameObject objectToFollow)
    {
        if(GetWaypoint(waypointID) != null)
        {
            Debug.LogWarning("Cannot create waypoint with existed ID!");
            return null;
        }
        GameObject waypointOBJ = Instantiate(waypointPrefab, transform);
        Waypoint thisWayp = waypointOBJ.GetComponent<Waypoint>();
        thisWayp.SetValues(waypointID, objectToFollow);
        thisWayp.enabled = true;
        waypointsList.Add(waypointOBJ);
        return waypointOBJ;
    }

    public GameObject AddWaypoint(string waypointID, Vector3 posToFollow)
    {
        if (GetWaypoint(waypointID) != null)
        {
            Debug.LogWarning("Cannot create waypoint with existed ID!");
            return null;
        }
        GameObject waypointOBJ = Instantiate(waypointPrefab, transform);
        Waypoint thisWayp = waypointOBJ.GetComponent<Waypoint>();
        thisWayp.SetValues(waypointID, posToFollow);
        thisWayp.enabled = true;
        waypointsList.Add(waypointOBJ);
        return waypointOBJ;
    }

    public GameObject GetWaypoint(string waypointID)
    {
        foreach (GameObject waypoint in waypointsList)
        {
            if(waypoint.GetComponent<Waypoint>().waypointID == waypointID)
            {
                return waypoint;
            }
        }
        return null;
    }

    public void RemoveWaypoint(string waypointID)
    {
        foreach (GameObject waypoint in waypointsList)
        {
            if (waypoint.GetComponent<Waypoint>().waypointID == waypointID)
            {
                Destroy(waypoint);
                return;
            }
        }
        Debug.LogWarning("Cannot remove nonexistent waypoint!");
    }
}
