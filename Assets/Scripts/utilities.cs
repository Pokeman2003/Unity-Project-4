using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class utilities : MonoBehaviour
{
    //Going to be honest, I was going to be a HELL'VA lot clever with this(offloading the functions to determine a new patrol) but unfortunately, I've been forced to relegate to just cloning the book because I wasn't very smart about HOW I was being clever.
    public static ushort playerDeathCount = 0;
    public static ushort enemyDeathCount = 0;

    public static void levelRestart()
    {
        SceneManager.LoadScene(0);
        Debug.Log("START!");
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
