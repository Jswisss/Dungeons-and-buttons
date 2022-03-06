using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using UnityEngine.AI;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerActions : MonoBehaviour
{
    public Transform newposition;
    public GameObject target;
   // public Animation playeranimator;
    public NavMeshAgent agent;
    public GameObject[] secrets;
    public GameObject[] doors;
    public Button Attackbutton, rectify;
    public Button Heal;
    public int damage = 15;
    Animator Anime;
    public float Health = 100;
    int HealthPotions = 5;
    public Text Healthamount;
    float cooldown = 2.5f;
    float coolddowntimer = 0;
    float buttoncooldown = 1;
    float buttoncooldowntimer = 0;
    
    // Start is called before the first frame update
    void Start()
    {
        
        target = null;
        Anime = this.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Healthamount.text = "Health: " + Health;
        Heal.gameObject.GetComponentInChildren<Text>().text = "Health Potions " + HealthPotions + "x"; 
        if(Health<50 && HealthPotions>0)
        {
            Heal.gameObject.SetActive(true);
        }
        else
        {
            Heal.gameObject.SetActive(false);
        }

        if(Health<=0)
        {
            SceneManager.LoadScene(0);
        }

        if (target!= null)
        {
        if(target.gameObject.CompareTag("Enemy"))
        {
            float distance = Vector3.Distance(target.transform.position, transform.position);
            if (distance <= agent.stoppingDistance)
            {
                if(coolddowntimer<=Time.time)
                    {
                        Attackbutton.gameObject.SetActive(true);
                    }
                else
                    {
                        Attackbutton.gameObject.SetActive(false);
                    }
                FaceTarget();

            }
            else
            {
                Attackbutton.gameObject.SetActive(false);
            }

        }
        if(target.gameObject.CompareTag("Floor"))
        {
            Attackbutton.gameObject.SetActive(false);
        }
            float distances = Vector3.Distance(target.transform.position, this.transform.position);
            if (distances <= agent.stoppingDistance)
            {
                Anime.SetBool("Walking", false);
                if (Anime.GetBool("Attacking") == true)
                {
                    return;
                }
                else if (Anime.GetBool("Attacking") == false)
                {
                    Anime.SetBool("Idling", true);
                    Anime.SetInteger("Condition", 0);
                }
                
                
                //agent.SetDestination(this.gameObject.transform.position);
                
                FaceTarget();
            }

            if (target.gameObject.CompareTag("artifact"))
            {
            //float distances = Vector3.Distance(target.transform.position, this.transform.position);
            if (distances <= 3)
            {
                    rectify.gameObject.SetActive(true);
            }
            else
                {
                    rectify.gameObject.SetActive(false);
                }
        }
        }
        

            movement();
    }

    

    void movement()
    {
        if (buttoncooldowntimer < Time.time)
        {


            if (Input.touchCount == 1)
            {
                // moves player
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit isSelected;
                if (Physics.Raycast(ray, out isSelected))
                {
                    if (isSelected.transform.gameObject.tag == "Floor" || isSelected.transform.gameObject.tag == "Enemy" || isSelected.transform.gameObject.tag == "artifact")
                    {
                        target = isSelected.transform.gameObject;
                        //target.transform.position = new Vector3(target.transform.position.x, this.gameObject.transform.position.y, target.transform.position.z);
                        //target.transform.position = new Vector3(target.transform.position.x, this.gameObject.transform.position.y, target.transform.position.z);
                        agent.SetDestination(target.transform.position);

                        Anime.SetBool("Walking", true);
                        Anime.SetInteger("Condition", 1);
                        //playeranimator.Play();
                        //float dist = Vector3.Distance(target.gameObject.transform.position, transform.position);
                        //print("Distance to other: " + dist);


                    }

                }

            }
        }
    }

    void FaceTarget()
    {
        Vector3 direction = (target.transform.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5);

    }

    public void Attack()
    {
        
        Anime.SetBool("Idling", false);
        Anime.SetBool("Attacking", true);
        Anime.SetInteger("Condition", 2);
        target.gameObject.GetComponent<Enemy>().Health = target.gameObject.GetComponent<Enemy>().Health - damage;
        buttoncooldowntimer = Time.time + buttoncooldown;
        StartCoroutine(AttackRoutine());
        coolddowntimer = Time.time + cooldown;
        
    }

    IEnumerator AttackRoutine()
    {
        yield return new WaitForSeconds(1);
        Anime.SetInteger("Condition", 0);
        Anime.SetBool("Attacking", false);
    }

    public void InvestigateArea()
    {
        for(int i = 0; i<secrets.Length;i++)
        {
            if(Vector3.Distance(this.gameObject.transform.position, secrets[i].gameObject.transform.position)<=2)
            {

                Discover(secrets[i]);
            }
        }
    }

    void Discover(GameObject Secret)
    {
        Secret.gameObject.SetActive(false);
        Secret.GetComponent<RoomGenerate>().SpawnRooms();
    }
    

    public void KickinLeftDoor()
    {
        for (int i = 0; i < doors.Length; i++)
        {
            if (Vector3.Distance(this.gameObject.transform.position, doors[i].gameObject.transform.position) <= 4 &&
                Vector3.Distance(this.gameObject.transform.position, doors[i].gameObject.transform.position) >= 0)
            {
                Debug.Log("test1");
                if (Vector3.Dot(this.gameObject.transform.right, doors[i].gameObject.transform.position)<0)
                {
                    //enable button
                    Debug.Log("test2");
                    doors[i].GetComponentInChildren<Transform>().Rotate(0, 120, 0);
                    doors[i].GetComponent<RoomGenerate>().SpawnRooms();
                }

                
            }
        }
    }

    public void KickinRightDoor()
    {
        for (int i = 0; i < doors.Length; i++)
        {
            //Debug.Log("NewTest");
            if (Vector3.Distance(this.gameObject.transform.position, doors[i].gameObject.transform.position) <= 4 &&
                Vector3.Distance(this.gameObject.transform.position, doors[i].gameObject.transform.position) >= 0)
            {
                Debug.Log("NewTest");
                if (Vector3.Dot(this.gameObject.transform.right, doors[i].gameObject.transform.position) > 0)
                {
                    //enable button

                    doors[i].GetComponentInChildren<Transform>().Rotate(0, -120, 0);
                    doors[i].GetComponent<RoomGenerate>().SpawnRooms();
                }


            }
        }
    }

    public void KickinFrontDoor()
    {
        for (int i = 0; i < doors.Length; i++)
        {
            //Debug.Log("NewTest");
            if (Vector3.Distance(this.gameObject.transform.position, doors[i].gameObject.transform.position) <= 4 &&
                Vector3.Distance(this.gameObject.transform.position, doors[i].gameObject.transform.position) >= 0)
            {
                //Debug.Log("NewTest");
                if (Vector3.Dot(this.gameObject.transform.forward, doors[i].gameObject.transform.position) > 0)
                {
                    //enable button
                    //if(doors[i].GetComponent<RoomGenerate>().dooropen==false)
                    //{
                    doors[i].GetComponentInChildren<Transform>().Rotate(0, -120, 0);
                    doors[i].GetComponent<RoomGenerate>().SpawnRooms();
                   // }
                    
                }


            }
        }
    }

    public void healplayer()
    {
        Health = Health + 50;
        HealthPotions--;
    }

    public void wingame()
    {
        SceneManager.LoadScene(0);
    }
}
