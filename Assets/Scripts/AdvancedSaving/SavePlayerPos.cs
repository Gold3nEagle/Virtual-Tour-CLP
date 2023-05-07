using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SavePlayerPos : MonoBehaviour, ISaveable
{
    public object CaptureState()
    {
        //pos = transform.position;
        if (GameManager.instance.isInVehicle)
        {
            return new SaveData
            {
                playerPostionX = GameManager.instance.GetPlayerOffset().x,
                playerPostionY = GameManager.instance.GetPlayerOffset().y,
                playerPostionZ = GameManager.instance.GetPlayerOffset().z,
                playerRotationY = GameManager.instance.characterOBJ.transform.localRotation.y
            };
        }
        else
        {
            return new SaveData
            {
                playerPostionX = GameManager.instance.characterOBJ.transform.position.x,
                playerPostionY = GameManager.instance.characterOBJ.transform.position.y,
                playerPostionZ = GameManager.instance.characterOBJ.transform.position.z,
                playerRotationY = GameManager.instance.characterOBJ.transform.localRotation.y
            };
        }

    }

    public void RestoreState(object state)
    {
        var saveData = (SaveData)state;
        // Debug.Log("saved rotation: " + saveData.playerRotationY);
        Vector3 savedPos = new Vector3(saveData.playerPostionX, saveData.playerPostionY, saveData.playerPostionZ);
        GameManager.instance.characterOBJ.transform.position = savedPos;
        Quaternion playerRotation = new Quaternion(GameManager.instance.characterOBJ.transform.localRotation.x,
            saveData.playerRotationY, GameManager.instance.characterOBJ.transform.localRotation.z,
            GameManager.instance.characterOBJ.transform.localRotation.w);
        GameManager.instance.characterOBJ.transform.localRotation = playerRotation;

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
