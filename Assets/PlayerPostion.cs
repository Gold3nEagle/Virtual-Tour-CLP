using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPostion : MonoBehaviour, ISaveable
{
    //Save player position and rotation
    public object CaptureState()
    {
        //pos = transform.position;
        return new SaveData
        {
            //playerPostion = pos
            playerPostionX = transform.position.x,
            playerPostionY = transform.position.y,
            playerPostionZ = transform.position.z,
            playerRotationY = transform.rotation.y
        };
    }

    public void RestoreState(object state)
    {
        var saveData = (SaveData)state;
        Vector3 savedPos = new Vector3(saveData.playerPostionX, saveData.playerPostionY, saveData.playerPostionZ);
        transform.position = savedPos;
        Quaternion playerRotation = new Quaternion(transform.rotation.x, saveData.playerRotationY, transform.rotation.z, transform.rotation.w);
        transform.rotation = playerRotation;

    }

    [Serializable]
    public struct SaveData
    {
        public float playerPostionX;
        public float playerPostionY;
        public float playerPostionZ;
        public float playerRotationY;
    }
}