using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class EnemyUnitMovement : MonoBehaviour {
	
	List<NavNode> path = new List<NavNode> ();
	public GameObject NavMap;	//	Has the graph and other game properties

	public Transform targetPlanet;
	private Vector3 vectorNormal;	
	private Vector3 vectorCross;

	// Use this for initialization
	void Start () {
		
	}

	void OnTriggerEnter(Collider other)
	{

		if (other.gameObject.tag == "tower") 
		{
			other.gameObject.GetComponent<Tower_script> ().enemiesInRange.Add (this.gameObject);
			Debug.Log ("Enemy entered");
		} 
		else if (other.gameObject.tag == "Projectile")
						Destroy (other.gameObject);
	}

	void OnTriggerExit(Collider other)
	{
		
		if (other.gameObject.tag == "tower") 
		{
			other.gameObject.GetComponent<Tower_script>().enemiesInRange.Remove (this.gameObject);
			Debug.Log ("Enemy left");
		}
	}

	private void setMyPath() 
	{
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
		/*
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
			
		}*/

		if (Distance(this.transform.position, this.path[0].pos) < 7.5) 
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

		//vectorNormal = transform.position - targetPlanet.position;		
		//vectorCross = -Vector3.Cross(vectorNormal, transform.right);		
		//transform.rotation = Quaternion.LookRotation(vectorCross, vectorNormal); 

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
