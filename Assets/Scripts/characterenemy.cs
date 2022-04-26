using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class characterenemy : MonoBehaviour
{
    // Navigational things.
    public Transform patrolParent; // Patrol list parent.
    private ushort patrolRouteCount = 0; // How many patrols exists.
    private bool patrolReverse = false; // Whether or not to follow the patrol in reverse.
    private ushort patrolNumber = 0; // Which patrol to work with. Might want to remove it.
    private int patrolEnd = 80000; // Patrol ending time in seconds.
    private float patrolTime = 0.0f; // Patrol current time in seconds.
    private List<Transform> patrolLocations; // All of the locations in a given patrol.
    private List<Transform> patrolList; // All patrols in a given group
    private ushort patrolIndex = 0;
    private UnityEngine.AI.NavMeshAgent navAgent;

    // Start is called before the first frame update
    void Start()
    {
        name = "Character_Enemy"; //Renames to a generic enemy, so the enemy can't distinguish them. Bullets don't have eyes to distinguish this!
        navAgent = GetComponent<UnityEngine.AI.NavMeshAgent>();

        initPatrolList();
        initNewPatrol();
    }

    // Update is called once per frame
    void Update()
    {
        patrolTime = patrolTime + Time.deltaTime; // Update the current patrol time.

        if (patrolTime >= patrolEnd) {
            Debug.Log("My patrol is up. Time to register a new one.");
            initNewPatrol();
        }
    }

    // OnTriggerEnter is called whenever an object enters the trigger zone.
    void OnTriggerEnter(Collider item)
    {
        switch (item.name) {
            case "Character_Player":
            Debug.Log("Player has entered the zone!");
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
            case "Explosion":
                Debug.Log("I'm DEAD!");
                Destroy(this);
                break;
            case "Bullet":
                Debug.Log("I've been wounded!");
                break;
            default:
                Debug.Log("Someone touched me but I'm not dead. " + item.gameObject.name);
                break;
        }
    }

    void initPatrolList() //Creates a list of patrols and works with it.
    {
        Debug.Log("Loading patrol lists in: " + patrolParent);
        List<Transform> patrolList = new List<Transform>();
        foreach (Transform child in patrolParent)
        {
            Debug.Log("Patrol: " + child);
            patrolList.Add(child);
            patrolRouteCount += (ushort)1;
        }
        Debug.Log("Patrol count:" + patrolRouteCount);
    }

    void initNewPatrol() //Picks a patrol and goes with it.
    {
        int patrolCount = (int)patrolRouteCount * 2 + 1;
        Debug.Log("Registering new patrol.");
        List<Transform> patrolLocations = new List<Transform>();
        patrolEnd = Random.Range(6, 720);
        //patrolEnd = 0;
        int patrolCast = Random.Range(1, patrolCount);
        if ((patrolCast%2) == 0) { patrolReverse = true; patrolCast = patrolCast / 2;  } else { patrolReverse = false; }
        patrolCast = patrolCast - 1;
        /*foreach (Transform child in patrolList[patrolCast])
        {
            Debug.Log("Node: " + child);
            patrolLocations.Add(child);
        }*/
        Debug.Log("Patrol Info:" + patrolCast + "," + patrolEnd + "," + patrolReverse + ",");
        Debug.Log("Test" + patrolList[1]);
    }

}
