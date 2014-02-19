using UnityEngine;
using System.Collections;

public class Planet_Gravity : MonoBehaviour {

    // Our force of gravity. 100 is fine for now
	public float gravityMagnitude = 100;

	private Vector3 vectorNormal;

    /// <summary>
    /// This method 'pushes' everything towards the center of our planet.
    /// </summary>
    /// <param name="other"></param>
	void OnTriggerStay (Collider other)		
	{		
		if (other.rigidbody != null && other.gameObject.name != "Unit")			
		{			
			vectorNormal = transform.position - other.transform.position;			
			other.rigidbody.AddForce(vectorNormal * Time.deltaTime * gravityMagnitude);			
		}		
	}	
}