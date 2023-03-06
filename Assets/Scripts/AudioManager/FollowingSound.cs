using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowingSound : MonoBehaviour
{
    private GameObject objToFollow;

    void Update()
    {
        if(transform.position != objToFollow.transform.position)
        {
            transform.position = objToFollow.transform.position;
        }
    }

    public void SetFollowObject(GameObject obj)
    {
        objToFollow = obj;
    }
}
