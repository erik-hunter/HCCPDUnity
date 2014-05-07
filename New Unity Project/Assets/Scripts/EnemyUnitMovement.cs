using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class EnemyUnitMovement : MonoBehaviour {
	
	List<NavNode> path = new List<NavNode> ();
	public GameObject NavMap;	//	Has the graph and other game properties
	
	// Use this for initialization
	void Start () {
		
	}

	private void setMyPath() 
	{
		
		/*
		//	Find a path from the bottom to the top
		//Debug.LogError ("Finding the path");
		Rigidbody start = null;
		Nav_Point end = null;
		foreach (Rigidbody navNode in planet.GetComponent<Ico_Sphere>().navNodes) 
		{
			Vector3 pos = navNode.position;
			if(start == null && pos.y < (-0.5f * planet.GetComponent<Ico_Sphere>().radius))
			{
				start = navNode;
			}
			if(pos.x == 0.0f && pos.z == 0.0f && pos.y > 0.0f)
			{
				end = navNode.GetComponent<Nav_Point>();
			}
			
			if(end != null && start != null)
				break;
		}
		*/
		NavNode end = null;
		foreach (NavNode node in NavMap.GetComponent<Nav_Map>().navGraph) 
		{
			if(node.pos.x == 0.0f && node.pos.z == 0.0f && node.pos.y < 0.0f)
			{
				end = node;
				break;
			}
		}

		List<NavNode> spawnPts = NavMap.GetComponent<Nav_Map> ().spawnPoints;
		System.Random rnd = new System.Random ();
		int spwnPt = rnd.Next (spawnPts.Count);
		//Debug.LogError ("spawn point = " + spwnPt + " length of list = " + spawnPts.Count);
		path.Add (spawnPts [spwnPt]);

		while (true) 
		{
			List<NavNode> neighbors = new List<NavNode>();
			foreach(int nbr in path[path.Count-1].neighbors)
			{
				if(!(nbr == -1))
					neighbors.Add(NavMap.GetComponent<Nav_Map>().navGraph[nbr]);
			}


			if(neighbors == null || neighbors.Count == 0)
				Debug.LogError("neighbors is null or empty!");
			
			NavNode next = neighbors[0];
			foreach(NavNode neighbor in neighbors)
			{
				Vector3 endPos = end.pos;
				Vector3 possibleNextPos = neighbor.pos;
				Vector3 currentNextPos = next.pos;
				float possibleDist = (float)Math.Sqrt( Math.Pow(endPos.x - possibleNextPos.x, 2.0f) + Math.Pow(endPos.y - possibleNextPos.y, 2.0f) + Math.Pow(endPos.z - possibleNextPos.z, 2.0f) );
				
				
				float currentDist = (float)Math.Sqrt( Math.Pow(endPos.x - currentNextPos.x, 2.0f) + Math.Pow(endPos.y - currentNextPos.y, 2.0f) + Math.Pow(endPos.z - currentNextPos.z, 2.0f) );
				
				if(possibleDist < currentDist && !path.Contains(neighbor))
					next = neighbor;
			}
			
			if(next.pos.Equals(end.pos))
			{
				path.Add(end);
				break;
			}
			
			path.Add(next);
		}

		//	set my position to start
		this.transform.position = path[0].pos;
	}

	// Update is called once per frame
	void Update () {
		if (path.Count == 0 && NavMap.GetComponent<Nav_Map>().doneSettingUp)
			setMyPath ();

		if (!NavMap.GetComponent<Nav_Map> ().doneSettingUp)
			return;

		//	See if I'm already at the targeted node:
		if (this.transform.position == this.path[0].pos) 
		{
			if(path.Count == 1)
			{
				path.Clear();
				return;
			}
			else
			{
				path.RemoveAt(0);
				
			}
			
		}
		
		// The step size is equal to speed times frame time.
		float step = 20.0f * Time.deltaTime;
		
		// Move our position a step closer to the target.
		transform.position = Vector3.MoveTowards(transform.position, path[0].pos, step);

		/*
		if (path.Count == 0)
			setMyPath ();
		
		//	See if I'm already at the targeted node:
		if (this.transform.position == this.path[0].transform.position) 
		{
			if(path.Count == 1)
			{
				path.Clear();
				return;
			}
			else
			{
				path[0].enemy = null;
				path.RemoveAt(0);
				
			}
			
		}
		
		// The step size is equal to speed times frame time.
		float step = 20.0f * Time.deltaTime;
		
		// Move our position a step closer to the target.
		transform.position = Vector3.MoveTowards(transform.position, path[0].transform.position, step);
		path [0].enemy = this;
		*/
	}
}
