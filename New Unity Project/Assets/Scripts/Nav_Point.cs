﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Nav_Point : MonoBehaviour,IEnumerator,IEnumerable {
	
	private Nav_Point neighbor;
	public int weight = 1;
    int position = -1;

    private bool wait = false;

    /// <summary>
    /// This is the unique identifer for each particular node.
    /// </summary>
    public int uid;


	/// <summary>
	/// This is just a list that will contain all of the nodes we can reach
	/// from a given node.
	/// </summary>
    public IList<Nav_Point> navNeighbors = new List<Nav_Point>();


	/// <summary>
	/// This will be used to access whether the node has 
	/// a tower on top of it or not.
	/// </summary>
	public bool hasTower = false;


    /// <summary>
    /// Once our Nav_Point falls and lands on the planet, we delete all components 
    /// except for the transform.
    /// TODO: Instantiate the NavPointChilds on collision.
    /// </summary>
    /// <param name="other"></param>
    void OnCollisionStay(Collision other)
    {
        if (other.gameObject.tag == "Planet")
        {

            this.rigidbody.isKinematic = true;

            SphereCollider collid = (SphereCollider)this.collider;
            collid.radius = 1.0f;

            this.collider.isTrigger = true;
            wait = true;
        }
    }

    /// <summary>
    /// This is used to detect all of the neighbors of a given node.
    /// Note: I don't know why, but in it's current state it does not detect
    /// itself, which is what we want.
    /// </summary>
    /// <param name="other"></param>
	void OnTriggerEnter(Collider other)
	{
        if (wait == false)
        {
            return; 
        }
 
		if (other.gameObject.tag == "NavPointChild") 
		{
			neighbor = other.transform.parent.gameObject.GetComponent<Nav_Point>();
			navNeighbors.Add (neighbor);
		}
	}

    // All the code below makes it so we can loop through our navNeighbors list.

    //IEnumerator and IEnumerable require these methods.
    public IEnumerator GetEnumerator()
    {
        return (IEnumerator)this;
    }

    //IEnumerator
    public bool MoveNext()
    {
        position++;
        return (position < navNeighbors.Count);
    }

    //IEnumerable
    public void Reset()
    { position = 0; }

    //IEnumerable
    public object Current
    {
        get { return navNeighbors[position]; }
    }

	void Update()
	{

		//	Uncomment this to see the mesh of nav nodes.
		/*
		foreach (Nav_Point nd in navNeighbors) 
		{
			Debug.DrawLine(new Vector3(this.transform.position.x * 110.0f, this.transform.position.y * 110.0f, this.transform.position.z * 110.0f), new Vector3(nd.transform.position.x * 110.0f, nd.transform.position.y * 110.0f, nd.transform.position.z * 110.0f), Color.red, 1, true);
		}
		*/
	}

}
