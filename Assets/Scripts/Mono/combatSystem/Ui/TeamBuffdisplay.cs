using CombatSystem;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TeamBuffdisplay : MonoBehaviour
{
    List<GameObject> child;
    [SerializeField]
    TeamBuffContainer controller;
    [SerializeField]
    GameObject prefab;
    private void Awake()
    {
        child= new List<GameObject>();
       controller.BuffDisplayChange .AddListener( SetIcon);
    }
    public void SetIcon(List<BuffIconDisplay.DisplayInfo> spriteList)
    {
        // Destroy existing child objects
        foreach (GameObject childObject in child)
        {
            Destroy(childObject);
        }
        child.Clear();
        // Instantiate new child objects and set the Info
        foreach (BuffIconDisplay.DisplayInfo Info in spriteList)
        {
              GameObject newObject = Instantiate(prefab, transform);
            BuffIconDisplay imageComponent = newObject.GetComponent<BuffIconDisplay>();
              if (imageComponent != null)
              {
                imageComponent.Set(Info);
                  child.Add(newObject);
              }
        }
    }
}
