using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public float LookRadius = 5f;

    public GameObject target;
    public NavMeshAgent agent;
    private Animator EnemyAnimations;
    public GameObject[] Waypointmarkers;
    float cooldown = 4;
    float coolddowntimer = 0;
    int waypointcounter = 0;
    public bool walker= false;

    public bool Seestarget = false;
    public float Health;

    public GameObject healthpot;
    public GameObject[] Weapon;

    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player");
        agent = GetComponent<NavMeshAgent>();
        EnemyAnimations = GetComponent<Animator>();

        
    }


    // Update is called once per frame
    void Update()
    {
        

        float distance = Vector3.Distance(target.transform.position, transform.position);

        if (distance <= LookRadius && distance > agent.stoppingDistance)
        {
            Seestarget = true;
            //EnemyAnimations.SetBool("Idle", false);
            EnemyAnimations.SetBool("Walking", true);
            //EnemyAnimations.Play("Walk");
            EnemyAnimations.SetInteger("Condition", 1);
            agent.SetDestination(target.transform.position);
        }
        if (distance <= LookRadius && distance <= agent.stoppingDistance)
        {
            EnemyAnimations.SetBool("Walking", false);
            if(EnemyAnimations.GetBool("Attacking")==true)
            {
                return;
            }
            else if(EnemyAnimations.GetBool("Dieing") == true)
            {
                return;
            }
            else if(EnemyAnimations.GetBool("Attacking") == false)
            {
                EnemyAnimations.SetBool("Idling", true);
                EnemyAnimations.SetInteger("Condition", 0);
            }
            
            //EnemyAnimations.Play("Idle");
            FaceTarget();
            if(coolddowntimer<Time.time)
            {
                Attack();

            }
            
        }

        if ( walker== true)
        {
            Waypoints();
        }

        if(Health<=0)
        {
            agent.SetDestination(this.transform.position);
            /*EnemyAnimations.SetBool("Idling", false);
            EnemyAnimations.SetBool("Attacking", false);
            EnemyAnimations.SetBool("Dieing", true);
            EnemyAnimations.SetInteger("Condition", 3);*/
            lootdrop();
            this.gameObject.SetActive(false);
            
        }
    }

    void FaceTarget()
    {
        Vector3 direction = (target.transform.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5);

    }
    void FaceWay()
    {
        Vector3 direction = (Waypointmarkers[waypointcounter].transform.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5);
    }

    void Attack()
    {
        //Attack Animation

        //Debug.Log("ertl;rghr");
        int chance = Random.Range(0, 100);
        EnemyAnimations.SetBool("Idling", false);
        EnemyAnimations.SetBool("Attacking", true);
        EnemyAnimations.SetInteger("Condition", 2);
        if (chance >= 50)
        {
            target.GetComponent<PlayerActions>().Health -= 10;
           
        }
        StartCoroutine (AttackRoutine());
        
        coolddowntimer = Time.time + cooldown;
    }

    IEnumerator AttackRoutine()
    {
        yield return new WaitForSeconds(1);
        EnemyAnimations.SetInteger("Condition", 0);
        EnemyAnimations.SetBool("Attacking", false);
    }

    void Waypoints()
    {
        if(Seestarget==true)
        {
            agent.SetDestination(target.transform.position);
            walker = false;
        }
        else
        {
            EnemyAnimations.SetBool("Walking", true);
            //EnemyAnimations.Play("Walk");
            EnemyAnimations.SetInteger("Condition", 1);
            FaceWay();
            agent.SetDestination(Waypointmarkers[waypointcounter].transform.position);
            float distance = Vector3.Distance(Waypointmarkers[waypointcounter].transform.position, transform.position);
            if(distance<=1)
            {
                waypointcounter = (waypointcounter + 1)%2;
                //Debug.Log(waypointcounter);
            }
        }
    }

    void lootdrop()
    {
        int chance = Random.Range(0, 100);

        if(chance<=100)
        {
            //Instantiate(healthpot, new Vector3(this.gameObject.transform.position.x, this.gameObject.transform.position.y, this.gameObject.transform.position.z), Quaternion.identity);
            /*if(chance<=15)
            {
                int weapondrop = Random.Range(0, Weapon.Length);
                
                Instantiate(Weapon[weapondrop], new Vector3(this.gameObject.transform.position.x, this.gameObject.transform.position.y, this.gameObject.transform.position.z), Quaternion.identity);

            }*/
        }
    }
}
