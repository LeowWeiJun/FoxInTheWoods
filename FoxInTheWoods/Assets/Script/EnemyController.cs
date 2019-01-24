using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour {

    public float lookRadius;

    Transform target;
    NavMeshAgent agent;
    public GameObject boundary;
    //private bool isWandering;

    public float wanderRadius;
    public float wanderTimer;

    private float timer;

    float distance = 9999;
    public GameObject player;
    PlayerVisible playerscript;


    public float roamRadius;
    Vector3 startPosition;

    public GameObject arrow;
    public float coolDown;

    // Use this for initialization

    void Awake()
    {
        startPosition = transform.position;
    }

    void Start () {


        player = GameObject.Find("Player");
        playerscript = player.GetComponent<PlayerVisible>();

        target = GameObject.FindGameObjectWithTag("Player").transform;
        agent = GetComponent<NavMeshAgent>();
        timer = wanderTimer;
        //isWandering = true;
    }
	
	// Update is called once per frame
	void Update () {

        if (playerscript.visibility == true)
        {
            distance = Vector3.Distance(target.position, transform.position);
        }

        //Debug.Log(playerscript.visibility);
        if (distance <= lookRadius)
        {
            agent.SetDestination(target.position);

            //isWandering = false;

            if (distance <= agent.stoppingDistance)
            {
                FaceTarget();
            }

            ShootArrow();
        }
        else
        {
            FreeRoam();
            //timer += Time.deltaTime;

            //if (timer >= wanderTimer)
            //{
            //    Vector3 newPos = RandomNavSphere(transform.position, wanderRadius, -1);
            //    agent.SetDestination(newPos);
            //    timer = 0;
            //}
            //isWandering = true;
        }
        
        
    }

    void FaceTarget()
    {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lookRadius);
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(startPosition, roamRadius);
    }

    public static Vector3 RandomNavSphere(Vector3 origin, float dist, int layermask)
    {
        Vector3 randDirection = Random.insideUnitSphere * dist;

        randDirection += origin;

        NavMeshHit navHit;

        NavMesh.SamplePosition(randDirection, out navHit, dist, layermask);

        return navHit.position;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.name == "Boundary" || collision.gameObject.tag == "ObjectCollide"  )
        {
            Debug.Log("True");
            
            agent.isStopped = true;
            agent.velocity = Vector3.zero;
            agent.ResetPath();
            agent.isStopped = false;
            
        }

    }

    void FreeRoam()
    {
            timer += Time.deltaTime;

            if (timer >= wanderTimer)
            {
            Vector3 randomDirection = Random.insideUnitSphere * roamRadius;
            randomDirection += startPosition;
            NavMeshHit hit;
            NavMesh.SamplePosition(randomDirection, out hit, roamRadius, 1);
            Vector3 finalPosition = hit.position;
            agent.destination = finalPosition;
            timer = 0;
            }
            //isWandering = true;
    }

    public void ShootArrow()
    {
        coolDown -= Time.deltaTime;
        if(coolDown < 0)
        {
            GameObject newArrow = Instantiate(arrow) as GameObject;
            newArrow.transform.position = transform.TransformPoint(Vector3.forward);

            Rigidbody rb = newArrow.GetComponent<Rigidbody>();
            rb.velocity = transform.forward * 10;
            rb.AddForce(transform.forward);

            coolDown = 3;
        }
    }
}
