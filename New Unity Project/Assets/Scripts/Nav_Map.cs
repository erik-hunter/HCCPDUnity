using UnityEngine;
using System.Collections;
using System.Threading;
using System.Collections.Generic;
using System.Linq;

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
		print("Setup Complete");
		CreateNavMap ();
		print ("NavMap size is: " + navGraph.Count);

		// 2345  (-38.5, -79.1, -48.9) (-36.6, -79.5, -49.1)
		print (navGraph [0].uid);
		print (navGraph [0].passable);
		print (navGraph [0].pos);
		for(int i = 0; i < 6; i++) 
		{
			print (navGraph[0].neighbors[i]);
		}

		DeleteNodes ();


	}

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

			navGraph.Add(n);

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
            // Get the componenet for each child in points
            childNode = child.GetComponent<NavPointGen>();
            // Set the UID
            childNode.uid = i;
            // Add the childNode to our ArrayList of childNodes
            childNodes.Add(childNode);
            i++;
            
        }
    }

}

public class NavNode
{
	public int uid;
	public Vector3 pos;
	public bool passable;
	public int[] neighbors = {-1, -1, -1, -1, -1, -1};
	
}

