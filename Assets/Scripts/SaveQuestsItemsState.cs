using PixelCrushers.DialogueSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveQuestsItemsState : MonoBehaviour
{
    public GameObject picAreas;
    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.GetInt("inMasjidQuest") == 1) {
            DialogueLua.SetVariable("Mosq.Pictures", 0);
            DialogueManager.SendUpdateTracker();
            picAreas.SetActive(true);
        }
        if(DialogueLua.GetVariable("Tower.Switch").asBool)
        {
            FanAnimation.animator.speed = 1;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void QuestStarted()
    {
        PlayerPrefs.SetInt("inMasjidQuest", 1);
    }

    public void QuestFinished()
    {
        PlayerPrefs.SetInt("inMasjidQuest", 0);
    }
}
