using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;

public class Item : MonoBehaviour, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData evenData)
    {
        PickUp();
    }

    private void PickUp()
    {
        Debug.Log("Предмет подобран: " + gameObject.name);
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PickUp();
        }
    }
}
