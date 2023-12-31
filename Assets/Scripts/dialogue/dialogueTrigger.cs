using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class dialogueTrigger : MonoBehaviour
{
    public dialogueData data;
   public List<dialogueDisplay> participator;
    public UnityEvent<dialogueData> finishedEvent;
    public Transform center;
   public GameObject ui;
    public GameObject StartButtom;
    public void triggerDialogue()
    {
        dialogeController.StartDialogue(participator, data,this);
        StartButtom.SetActive(false);
    }

    public void EnterTrigger(Collider c)
    {
        Debug.Log(c.gameObject.GetComponent<playerSettings>());
        if (c.gameObject.GetComponent <playerSettings>()!=null)
        {
            ui.SetActive(true);
            Debug.Log("!!!");
        }
    }
    public void ExitTrigger(Collider c)
    {
        Debug.Log(c.gameObject.GetComponent<playerSettings>());
        if (c.gameObject.GetComponent<playerSettings>() != null)
        {
            ui.SetActive(false);
            Debug.Log("!!!");

        }
    }
    public void End(dialogueData a)
    {
        finishedEvent?.Invoke(a);
        StartButtom.SetActive(true);
    }

}
