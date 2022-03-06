using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomGenerate : MonoBehaviour
{

    public GameObject[] roominternal;
    public bool dooropen=false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SpawnRooms()
    {
        if(dooropen==false)
        {
        for(int i= 0; i<roominternal.Length;i++)
        {
            roominternal[i].gameObject.SetActive(true);
        }
            dooropen = true;
        }

        
    }
}
