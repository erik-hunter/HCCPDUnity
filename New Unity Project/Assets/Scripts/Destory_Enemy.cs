using UnityEngine;
using System.Collections;

public class Destory_Enemy : MonoBehaviour {

    /// <summary>
    /// This is used by the "Home Base" as a way to 'destroy' a unit once it reaches
    /// it's destination.
    /// </summary>
    /// <param name="other"></param>
	void OnTriggerEnter(Collider other) {
		if (other.gameObject.tag == "Player")
			Destroy (other.gameObject);
	}
}
