using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minimap : MonoBehaviour
{
    public float walkingDistance;
    public float drivingDistance;
    public float sizeChangeSpeed = 2.0f;
    private Camera thisCam;

    void Start()
    {
        thisCam = GetComponent<Camera>();
    }

    void Update()
    {
        Vector3 newPos = FollowingTransform().position;
        newPos.y = transform.position.y;
        transform.position = newPos;

        float targetOrthographicSize = GameManager.instance.isInVehicle ? drivingDistance : walkingDistance;
        thisCam.orthographicSize = Mathf.Lerp(thisCam.orthographicSize, targetOrthographicSize, sizeChangeSpeed * Time.deltaTime);
    }

    private Transform FollowingTransform()
    {
        if (GameManager.instance.isInVehicle)
        {
            return GameManager.instance.car.transform;
        }
        return GameManager.instance.characterOBJ.transform;
    }
}
