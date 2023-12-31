using oct.InventorySystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class deadZoneTwoD : MonoBehaviour
{
    public UnityEvent die;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<playerSettings>() != null)
        {
            die?.Invoke();
            
        }
    }
}
