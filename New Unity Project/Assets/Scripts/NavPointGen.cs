using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class NavPointGen : MonoBehaviour {

	public float offPlanet = 5.0f;
	private Vector3 originP = new Vector3 (0.0f, 0.0f, 0.0f);
	public float fallSpeed = 3.0f;
	public GameObject navNode;
	public bool stillMoving = true;

    int position = -1;

    /// <summary>
    /// This is the unique identifer for each particular node.
    /// </summary>
    public int uid;


    /// <summary>
    /// This is just a list that will contain all of the nodes we can reach
    /// from a given node.
    /// </summary>
    public IList<NavPointGen> navNeighbors = new List<NavPointGen>();
    

	/// <summary>
	/// This is used to have our nodes move towards the center of the planet and stop when they are within a given distance.
	/// </summary>
	public void Update() 
	{

        if (stillMoving)
        {
            transform.position = Vector3.MoveTowards(transform.position, originP, fallSpeed * Time.deltaTime);

            RaycastHit hit;
            Ray center = new Ray(transform.position, originP - transform.position);

            if (Physics.Raycast(center, out hit, 5))
            {
                if (hit.distance <= offPlanet && hit.collider.gameObject.tag == "Planet")
                {
                    stillMoving = false;
                }
            }
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

			
}
