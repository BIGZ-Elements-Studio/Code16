using CombatSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuffIcondisplay : MonoBehaviour
{
    List<GameObject> child;
    [SerializeField]
    GameObject prefab;
    [SerializeField]
    BuffContainer controller;

    private void Awake()
    {
        child= new List<GameObject>();
        controller.BuffIconChange .AddListener( SetIcon);
    }
    public void SetIcon(List<Sprite> spriteList)
    {
        // Destroy existing child objects
        foreach (GameObject childObject in child)
        {
            Destroy(childObject);
        }
        child.Clear();

        // Instantiate new child objects and set the sprite
        foreach (Sprite sprite in spriteList)
        {
            GameObject newObject = Instantiate(prefab, transform);
            Image imageComponent = newObject.GetComponent<Image>();
            if (imageComponent != null)
            {
                imageComponent.sprite = sprite;
                child.Add(newObject);
            }
        }
    }
}
