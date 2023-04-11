using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointManager : MonoBehaviour
{
    public static WaypointManager instance;
    public GameObject waypointPrefab;
    public List<Waypoint> waypointsList;

    void Start()
    {
        instance = this;
        waypointsList = new List<Waypoint>();
    }

    public Waypoint AddWaypoint(string waypointID, GameObject objectToFollow)
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
        waypointsList.Add(thisWayp);
        return thisWayp;
    }

    public Waypoint AddWaypoint(string waypointID, Vector3 posToFollow)
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
        waypointsList.Add(thisWayp);
        return thisWayp;
    }

    public Waypoint GetWaypoint(string waypointID)
    {
        foreach (Waypoint waypoint in waypointsList)
        {
            if(waypoint.waypointID == waypointID)
            {
                return waypoint;
            }
        }
        return null;
    }

    public void RemoveWaypoint(string waypointID)
    {
        foreach (Waypoint waypoint in waypointsList)
        {
            if (waypoint.waypointID == waypointID)
            {
                Destroy(waypoint.gameObject);
                return;
            }
        }
        Debug.LogWarning("Cannot remove nonexistent waypoint!");
    }
}
