using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Nav_Point : MonoBehaviour {

	private Nav_Point neighbor;
	public int weight = 0;

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
    /// </summary>
    /// <param name="other"></param>
    void OnCollisionStay(Collision other)
    {
        if (other.gameObject.tag == "Planet")
        {
            Destroy(this.rigidbody);
            this.collider.isTrigger = true;
        }
    }

	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.tag == "NavPoint") 
		{
			print ("Adding to our NavNeighbors List");
			neighbor = other.gameObject.GetComponent<Nav_Point>();
			navNeighbors.Add (neighbor);
		}
	}

}
