using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class Tower_script : MonoBehaviour {
	public GameObject NavMap;


	//List<Nav_Point> surroundingNodes = new List<Nav_Point> ();	//	this is actually used like path in EnemyUnitMovement, until we find the node closest to this tower, then this holds the neighbors of that node.
	List<NavNode> surroundingNodes = new List<NavNode> ();	//	this is actually used like path in EnemyUnitMovement, until we find the node closest to this tower, then this holds the neighbors of that node.
	//public GameObject planet;	//	Has the graph and other game properties
	Nav_Point closestNode = null;
	public GameObject projectile;
	public float speed = 10f;
	bool initDone = false;

	List<GameObject> enemiesInRange = new List<GameObject>();
	
	// Use this for initialization
	void Start () 
	{
		
	}

	void OnTriggerEnter(Collider enemy)
	{
		if (enemy.gameObject.tag == "enemy_cube") 
		{
			enemiesInRange.Add (enemy.gameObject);
			Debug.Log ("Enemy entered");
		}
	}

	void OnTriggerExit(Collider enemy)
	{

		if (enemy.gameObject.tag == "enemy_cube") 
		{
			enemiesInRange.Remove (enemy.gameObject);
			Debug.Log ("Enemy left");
		}
	}
	
	// Update is called once per frame
	void Update () {
		
		if (enemiesInRange.Count > 0)
			fire (enemiesInRange [0].transform.position);
	}
	
	
	
	void fire (Vector3 target) {
		GameObject proj = (GameObject) Instantiate(projectile, transform.position, transform.rotation);
		
		// You can also acccess other components / scripts of the clone
		proj.GetComponent<Projectile_Movement>().SetTarget(target, speed);
	}
}
