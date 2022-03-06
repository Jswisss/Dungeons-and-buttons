using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DetectPot : MonoBehaviour
{
    //public Button Pickupbutton, kickrightdoor, Healbutton;
    public int numberhealthpot = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Pickup"))
        {
            /*if(kickrightdoor.gameObject.activeSelf==true)
            {
                //Pickupbutton.gameObject.SetActive(true);
            }

            if(Healbutton.gameObject.activeSelf == true)
            {
                //Pickupbutton.gameObject.SetActive(false);
            }
            else
            {
               // Pickupbutton.gameObject.SetActive(true);
            }*/
            numberhealthpot++;
            Destroy(other.gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Pickup"))
        {
            //Pickupbutton.gameObject.SetActive(false);
        }
    }

    
}
