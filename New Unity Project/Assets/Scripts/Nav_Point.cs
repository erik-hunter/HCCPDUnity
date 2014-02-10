using UnityEngine;
using System.Collections;

public class Nav_Point : MonoBehaviour {

    /// <summary>
    /// This is the unique identifer for each particular node.
    /// </summary>
    public int uid;

	/// <summary>
	/// This will eventually be used in setting weights between two nodes
	/// </summary>
    public struct Weight
	{
		public int srcID;
		public int destID;
		public int weight;

		public Weight(int src, int dest, int weightval)
		{
			srcID = src;
			destID = dest;
			weight = weightval;
		}
	}

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
            Destroy(this.collider);
        }
    }

}
