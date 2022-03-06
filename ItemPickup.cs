using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    Item item;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void PickUp()
    {
        Debug.Log("Picking up " + item.name);
        Inventory.instance.Add(item);   // Add to inventory

        Destroy(gameObject);    // Destroy item from scene
    }
}
