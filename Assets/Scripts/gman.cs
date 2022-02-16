using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Rise and shine, Mr. Freeman...
//Rise, and smell the ashes.
public class gman : MonoBehaviour
{
    //Internals
    private int itemCount = 4;
    private int playerHP = 100;

    //Externals
    public int items
    {
        get { return itemCount; }
        set
        { 
           itemCount = value;
            Debug.Log("Item count: {0}", itemCount);
        } 
    }

    public int Health
    {
        get { return playerHP; }
        set
        {
            playerHP = value;
            Debug.Log("HP is at:", playerHP);
        }
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
