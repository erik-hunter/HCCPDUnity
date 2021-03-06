﻿using UnityEngine;
using System.Collections;
using System.Threading;
using System.Collections.Generic;

public class Nav_Map : MonoBehaviour {
	
    /// <summary>
    /// The lists we will use to access and store our Nav_Points
    /// </summary>
    private GameObject[] points;
    private Nav_Point childNode;
    private GameObject childGO;
    private IList<Nav_Point> childNodes = new List<Nav_Point>();


    void Start () {
        print("Setup Starting");
        StartCoroutine(Wait(3.0f));
    }

    /// <summary>
    /// Used to stall setup until our nodes fall.
    /// </summary>
    /// <param name="secs"> How long to wait before we get all of our nodes</param>
    /// <returns></returns>
    IEnumerator Wait(float secs)
    {
		print ("Waiting wait for drop");
        yield return new WaitForSeconds(secs);
		Setup ();
    }

	/// <summary>
	/// Setup map
	/// </summary>
	void Setup()
	{
		GetAllNodes();
		print("Setup Complete");

        foreach (Nav_Point c in childNodes)
        {

            foreach (Nav_Point d in c)
            {
                print("Node " + c.uid + " has neighbor " + d.uid);
            }
        }
	}
	
	/// <summary>
    /// This will get every node on our map and assign a UID to it. Can access later.
    /// </summary>
    void GetAllNodes()
    {
        // Use this to create a UID for each of our nodes
        int i = 0;

        points =  GameObject.FindGameObjectsWithTag("NavPoint");

        foreach (GameObject child in points)
        {
            if (child.gameObject.tag == "NavPointChild")
            {
                continue;
            }
            // Get the componenet for each child in points
            childNode = child.GetComponent<Nav_Point>();
            // Set the UID
            childNode.uid = i;
            // Add the childNode to our ArrayList of childNodes
            childNodes.Add(childNode);
            i++;
            
        }
    }

}
