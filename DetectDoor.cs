using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DetectDoor : MonoBehaviour
{
    public GameObject DoorBox;
    public Button kickLeftButton;
    public Button kickRightButton;
    public Button kickFrontButton;
    // Start is called before the first frame update
    void Start()
    {
        kickLeftButton.gameObject.SetActive(false);
        kickRightButton.gameObject.SetActive(false);
        kickFrontButton.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log("Test1");
        if (other.gameObject.CompareTag("Door"))
        {
            Debug.Log("Test");
            if (Vector3.Dot(this.gameObject.transform.right, other.gameObject.transform.position) < 0)
            {
                if (other.gameObject.GetComponent<RoomGenerate>().dooropen == false)
                {
                    kickLeftButton.gameObject.SetActive(true);
                }
                    //enable button
                    

            }
            else if (Vector3.Dot(this.gameObject.transform.right, other.gameObject.transform.position) > 0)
            {
                if (other.gameObject.GetComponent<RoomGenerate>().dooropen == false)
                {
                    kickRightButton.gameObject.SetActive(true);
                }
                    //enable button


            }
            else if (Vector3.Dot(this.gameObject.transform.forward, other.gameObject.transform.position) > 0)
            {
                if (other.gameObject.GetComponent<RoomGenerate>().dooropen == false)
                {
                    kickFrontButton.gameObject.SetActive(true);
                }

            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Door"))
        {
            Debug.Log("Test");
            if (Vector3.Dot(this.gameObject.transform.right, other.gameObject.transform.position) < 0)
            {
                //enable button
                kickLeftButton.gameObject.SetActive(false);

            }
            else if (Vector3.Dot(this.gameObject.transform.right, other.gameObject.transform.position) > 0)
            {
                //enable button
                kickRightButton.gameObject.SetActive(false);

            }
            else if(Vector3.Dot(this.gameObject.transform.forward, other.gameObject.transform.position) > 0)
            {
                kickFrontButton.gameObject.SetActive(false);
            }
        }
    }
}
