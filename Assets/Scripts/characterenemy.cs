using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class characterenemy : MonoBehaviour
{
    // Navigational things.
    public Transform patrolParent; // Patrol list parent.
    private ushort patrolRouteCount = 0; // How many patrols exists.
    private bool patrolReverse = false; // Whether or not to follow the patrol in reverse.
    private ushort nodeCount = 0; // How many nodes in a patrol. Flips a coin to see if it should move on or not after reaching the end.
    private int patrolEnd = 0; // Patrol ending time in seconds.
    private float patrolTime = 0.0f; // Patrol current time in seconds.
    private List<Transform> patrolLocations; // All of the locations in a given patrol.
    private List<Transform> patrolList; // All patrols in a given group
    private ushort patrolIndex = 0; // Which node are we working with?
    private UnityEngine.AI.NavMeshAgent navAgent; // The navigational agent.

    // Other AI stuff
    private Transform target;
    private bool chaseMode = false;

    //Misc
    public gman Manager;
    private int enemyHP = 20;

    // Start is called before the first frame update
    void Start()
    {
        name = "Character_Enemy"; //Renames to a generic enemy, so the enemy can't distinguish them. Bullets don't have eyes to distinguish this!
        navAgent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        target = GameObject.Find("Character_Player").transform;

        initPatrolList();
        initNewPatrol();

        Manager = GameObject.Find("GHands").GetComponent<gman>(); //Assign Gman's hands to handle everything.
    }

    // Update is called once per frame
    void Update()
    {
        patrolTime = patrolTime + Time.deltaTime; // Update the current patrol time

        if (patrolTime >= patrolEnd) {
            Debug.Log("My patrol is up. Time to register a new one.");
            initNewPatrol();
        }

        if (navAgent.remainingDistance < 0.4f && !navAgent.pathPending && !chaseMode)
        {
            //Debug.Log("Onwards to the next node.");
            //Debug.Log("Node info:" + patrolTime + "," + patrolEnd + "," + patrolIndex + "," + nodeCount);
            nextPatrolPoint();
        } else if (chaseMode)
        {
            navAgent.destination = target.position;
        }
    }

    void LateUpdate()
    {
        if (enemyHP <= 0)
        {
            Debug.Log("I'm DEAD!");
            Destroy(this.gameObject);
        }
    }

    // OnTriggerEnter is called whenever an object enters the trigger zone.
    void OnTriggerEnter(Collider item)
    {
        switch (item.name) {
            case "Character_Player":
                Debug.Log("Player has entered the zone!");
                patrolTime = 0.0f;
                chaseMode = true;
                break;
            case "Explosion":
                Debug.Log("There was an explosion somewhere around me!");
                break;
            case "Rocket":
                Debug.Log("A rocket has entered my hearing range!");
                break;
            case "Bullet":
                Debug.Log("A bullet whizzed past me!");
                break;
            default:
                Debug.Log("Something unimportant has entered the zone. " + item.name);
                break;
        }
    }
    void OnTriggerExit(Collider item)
    {
        switch (item.name)
        {
            case "Character_Player":
                Debug.Log("Player has exited the zone!");
                chaseMode = false;
                patrolTime = 0.0f;
                break;
            case "Rocket":
                Debug.Log("Relaxing, the rocket has left my range.");
                break;
            case "Bullet":
                Debug.Log("Where did it go?");
                break;
            default:
                Debug.Log("Something unimportant has exited the zone. " + item.name);
                break;
        }
    }

    void OnCollisionEnter(Collision item)
    {
        switch (item.gameObject.name)
        {
            case "Character_Player":
                Debug.Log("GOTCHA!");
                Manager.health = Manager.health - 15;
                patrolTime = 0.0f;
                break;
            case "Explosion":
                Debug.Log("I'm DEAD!");
                Destroy(this);
                break;
            case "Bullet":
                Debug.Log("I've been wounded!");
                enemyHP -= 1;
                break;
            default:
                Debug.Log("Someone touched me but I'm not dead. " + item.gameObject.name);
                break;
        }
    }

    void initPatrolList() //Creates a list of patrols and works with it.
    {
        Debug.Log("Loading patrol lists in: " + patrolParent);
        patrolList = new List<Transform>();
        foreach (Transform child in patrolParent)
        {
            Debug.Log("Patrol: " + child);
            patrolList.Add(child);
            //Debug.Log("Listing: " + patrolList[patrolRouteCount]);
            patrolRouteCount += (ushort)1;
        }
        Debug.Log("Patrol count:" + patrolRouteCount);
    }

    void initNewPatrol() //Picks a patrol and goes with it.
    {
        int patrolCount = (int)patrolRouteCount * 2;
        //Debug.Log("Registering new patrol.");
        patrolLocations = new List<Transform>();
        nodeCount = 0;
        patrolEnd = Random.Range(6, 720);
        //patrolEnd = 0;
        int patrolCast = Random.Range(1, patrolCount);
        if ((patrolCast%2) == 0) { patrolReverse = true; patrolCast = patrolCast / 2;  } else { patrolReverse = false; patrolCast = Mathf.Abs(patrolCast - patrolRouteCount); }
        foreach (Transform child in patrolList[patrolCast])
        {
            //Debug.Log("Node: " + child);
            patrolLocations.Add(child);
            nodeCount += (ushort)1;
        }
        Debug.Log("Patrol Info:" + patrolCast + "," + patrolEnd + "," + patrolReverse + "," + patrolList[patrolCast]);
    }

    void nextPatrolPoint() // Navigates to the next patrol.
    {
        int internalPatrolIndex;
        if (patrolReverse == true)
        {
            internalPatrolIndex = Mathf.Abs(patrolIndex - nodeCount + 1);
        } else
        {
            internalPatrolIndex = patrolIndex;
        }
        navAgent.destination = patrolLocations[internalPatrolIndex].position;
        patrolIndex += 1;
        if (patrolIndex > nodeCount - 1) { patrolIndex = 0; }
        //Debug.Log("Node: " + patrolIndex + "," + nodeCount + "," + internalPatrolIndex + "," + patrolLocations[internalPatrolIndex] + "," + patrolLocations[internalPatrolIndex].position);
    }
}
