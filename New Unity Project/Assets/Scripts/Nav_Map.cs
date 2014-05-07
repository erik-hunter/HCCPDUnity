using UnityEngine;
using System.Collections;
using System.Threading;
using System.Collections.Generic;
using System.Linq;
using System;

public class Nav_Map : MonoBehaviour {
	
    /// <summary>
    /// The lists we will use to access and store our Nav_Points
    /// </summary>
    private GameObject[] points;
    private NavPointGen childNode;
    private GameObject childGO;
    private IList<NavPointGen> childNodes = new List<NavPointGen>();

	private GameObject[] navNodes;

	public List<NavNode> navGraph = new List<NavNode>();
	public List<NavNode> spawnPoints = new List<NavNode>();
	public bool doneSettingUp = false;


	private Vector3 spawnCenter = new Vector3(0.0f, 100.0f, 0.0f);


    void Start () {
        print("Setup Starting");
        StartCoroutine(Wait(6.0f));
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
		print("Setup Complete, Setting up NavGraph");
		CreateNavMap ();
		print ("SpawnPoint size is: " + spawnPoints.Count);

		DeleteNodes ();
		//DestoryExtraComponents ();

		doneSettingUp = true;
	}

	/// <summary>
	/// Deletes the nodes as we have created our graph and no long need them
	/// </summary>
	void DeleteNodes()
	{
		foreach (GameObject node in navNodes)
		{
			Destroy(node);
		}
	}


	void CreateNavMap()
	{
		navNodes = GameObject.FindGameObjectsWithTag ("NavPoint");
		int i = 0;

		foreach (GameObject node in navNodes)
		{
			// Get the componenet for each child in points
			childNode = node.GetComponent<NavPointGen>();

			// Create NavNodes with any/all information we need. The below needs to be edited for additional items 
			// that we want to keep track of.
			NavNode n = new NavNode();

			n.uid = childNode.uid;
			n.pos = new Vector3(childNode.transform.position.x, childNode.transform.position.y, childNode.transform.position.z);
			n.passable = true;
	
			i = 0;

			foreach(NavPointGen neigh in childNode.navNeighbors)
			{
				if(!n.neighbors.Contains(neigh.uid))
				{
					n.neighbors[i] = neigh.uid;
					i++;
				}
			}

			// Add points that are within our spawn region to a list of spawn points we can use later.
			double dis = Distance(spawnCenter, n.pos);
			if(dis < 15.0  && dis > 7.0)
			{
				spawnPoints.Add (n);
			}

			// Add all points to our nav graph
			navGraph.Add(n);

		}
		print ("NavGraph Created"); 


	}

	void DestoryExtraComponents()
	{
		Destroy (this.gameObject.collider);
		Destroy (this.gameObject.renderer);
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
            // Get the componenet for each child in points
            childNode = child.GetComponent<NavPointGen>();
            // Set the UID
            childNode.uid = i;
            // Add the childNode to our ArrayList of childNodes
            childNodes.Add(childNode);
            i++;
            
        }
    }


	/// <summary>
	/// Calculates the straight line distance between two 3D points
	/// </summary>
	/// <param name="start">Start.</param>
	/// <param name="end">End.</param>
	public double Distance(Vector3 start, Vector3 end)
	{
		double xd = (double) start.x - end.x;
		double yd = (double) start.y - end.y;
		double zd = (double) start.z - end.z;

		return (Math.Sqrt (xd * xd + yd * yd + zd * zd));

	}

}

/// <summary>
/// Class to hold all the information we need for our NavGraph
/// </summary>
public class NavNode
{
	public int uid;
	public Vector3 pos;
	public bool passable;
	public int[] neighbors = {-1, -1, -1, -1, -1, -1};
	
}

