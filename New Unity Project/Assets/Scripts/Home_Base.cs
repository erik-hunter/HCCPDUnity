using UnityEngine;
using System.Collections;

public class Home_Base : MonoBehaviour {


    public GUIText livesRemaining;
    private int lives = 10;


    void Start()
    {
        livesRemaining.text = "Lives Remaining: " + lives.ToString();
    }
    /// <summary>
    /// This is used by the "Home Base" as a way to 'destroy' a unit once it reaches
    /// it's destination.
    /// </summary>
    /// <param name="other"></param>
	void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "Player")
        {
            Destroy(other.gameObject);
            lives--;
            livesRemaining.text = "Lives Remaining: " + lives.ToString();
        }
	}
}
