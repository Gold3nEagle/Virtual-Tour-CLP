using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointManager : MonoBehaviour
{
    public static WaypointManager instance;
    public GameObject waypointPrefab;
    public GameObject questWaypointPrefab;
    public GameObject carWaypointPrefab;
    public List<Waypoint> waypointsList;

    void Awake()
    {
        instance = this;
        waypointsList = new List<Waypoint>();
    }

    void Start()
    {
        
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

    public Waypoint AddQuestWaypoint(string waypointID, GameObject objectToFollow)
    {
        if (GetWaypoint(waypointID) != null)
        {
            Debug.LogWarning("Cannot create waypoint with existed ID!");
            return null;
        }
        GameObject waypointOBJ = Instantiate(questWaypointPrefab, transform);
        Waypoint thisWayp = waypointOBJ.GetComponent<Waypoint>();
        thisWayp.SetValues(waypointID, objectToFollow);
        thisWayp.enabled = true;
        waypointsList.Add(thisWayp);
        return thisWayp;
    }

    public Waypoint AddCarWaypoint()
    {
        GameObject carWaypoint = Instantiate(carWaypointPrefab, transform);
        Waypoint thisWayp = carWaypoint.GetComponent<Waypoint>();
        thisWayp.SetValues("Car", GameManager.instance.car);
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

    public Waypoint AddQuestWaypoint(string waypointID, Vector3 posToFollow)
    {
        if (GetWaypoint(waypointID) != null)
        {
            Debug.LogWarning("Cannot create waypoint with existed ID!");
            return null;
        }
        GameObject waypointOBJ = Instantiate(questWaypointPrefab, transform);
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
