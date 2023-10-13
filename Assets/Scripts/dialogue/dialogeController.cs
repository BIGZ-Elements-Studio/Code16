using oct.cameraControl;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class dialogeController : MonoBehaviour
{
    public dialogueDisplay mainCharacter;
    static dialogueData current;
    static public int currentIndex;
    static List<dialogueDisplay> currentList;
   static dialogueTrigger Dtrigger;
    private static dialogeController instance;

    public static dialogeController Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<dialogeController>();
            }
            return instance;
        }
    }
    static public void StartDialogue(List<dialogueDisplay> dialogeList, dialogueData data, dialogueTrigger trigger)
    {
        MainCameraController.setToDialogueMode(trigger.center);
        Debug.Log("start");
        action.Enable();
        currentIndex = 0;
        currentList = dialogeList;
        Debug.Log(currentList.Count);
        current = data;
        Dtrigger = trigger;
        Move();
    }
   static PlayerInput action;
    private void Start()
    {
        action = new PlayerInput();
        action.Disable();
        action.dialogue.next.started += ctx => { Move(); };
    }

    static void Move()
    {if (currentIndex>= current.data.Count)
        {
            Debug.Log("stop");
            Stop();
            return;
        }
        Debug.Log(currentIndex);
        individualdialogue d = current.data[currentIndex];
        int charaIndex =d.character-1;
        Debug.Log(charaIndex + "  "+ d.content);
        if (charaIndex>=0) {
            
            display(currentList[charaIndex], d.content);
        }
        else
        {
            display(Instance.mainCharacter, d.content);
        }
        currentIndex += 1;
        
    }
   static void Stop()
    {
        Dtrigger.End(current);
        action.Disable();
        Instance. mainCharacter.background.enabled = false;
        Instance.mainCharacter.textMeshPro.enabled = false;
        Instance.mainCharacter.textMeshPro.text = "";
        foreach (dialogueDisplay a in currentList)
        {
            a.background.enabled = false;
            a.textMeshPro.enabled = false;
            a.textMeshPro.text = "";
        }
        current=null;
        currentIndex=-1;
        Dtrigger = null;
        currentList = null;
        MainCameraController.EndDialogueMode();
    }
    static void display(dialogueDisplay target, string text)
    {
        foreach (dialogueDisplay a in currentList)
        {
            a.background.enabled = false;
            a.textMeshPro.enabled = false;
            a.textMeshPro.text = "";
        }
        Instance.mainCharacter.background.enabled = false;
        Instance.mainCharacter.textMeshPro.enabled = false;
        Instance.mainCharacter.textMeshPro.text = "";
        target.background.enabled=true;
        target.textMeshPro.enabled = true;
        target.textMeshPro.text = text;
    }
}
