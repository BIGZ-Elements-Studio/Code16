using CombatSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class activeByMode : MonoBehaviour
{
    [SerializeField]
    List<GameObject> DisableIn2d = new List<GameObject>();
    [SerializeField]
    List<GameObject> DisableIn3d = new List<GameObject>();
    private void Awake()
    {
        GameModeController.ModeChangediFTo2D += switchD;
    }
    void switchD(bool to2d)
    {
        if (to2d)
        {
            foreach (GameObject g in DisableIn2d)
            {
                g?.SetActive(false);
            }
            foreach (GameObject g in DisableIn3d)
            {
                g?.SetActive(true);
            }
        }
        else
        {
            foreach (GameObject g in DisableIn3d)
            {
                g?.SetActive(false);

            }
            foreach (GameObject g in DisableIn2d)
            {
                g?.SetActive(true);

            }
        }
    }
}
