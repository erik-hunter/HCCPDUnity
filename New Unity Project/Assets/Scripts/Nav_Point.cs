using UnityEngine;
using System.Collections;

public class Nav_Point : MonoBehaviour {

    /// <summary>
    /// This is the unique identifer for each particular node.
    /// </summary>
    public int uid;

    public int[] weight = new int[1];


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
