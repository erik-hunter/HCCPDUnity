using UnityEngine;
using System.Collections;

public class NavPointGen : MonoBehaviour {

	public float offPlanet = 5.0f;
	private Vector3 originP = new Vector3 (0.0f, 0.0f, 0.0f);
	public float fallSpeed = 3.0f;
	public GameObject navNode;
	public bool stillMoving = true;
	//public GameObject navMap;

	public void Start()
	{
		while (stillMoving) 
		{
			MoveToFinal();
		}
	}

	public void MoveToFinal() 
	{

		transform.position = Vector3.MoveTowards (transform.position, originP, fallSpeed * Time.deltaTime);
		
			RaycastHit hit;
			Ray center = new Ray (transform.position, originP - transform.position);

			if (Physics.Raycast(center, out hit, 5))
			{
				if(hit.distance <= offPlanet && hit.collider.gameObject.tag == "Planet")
				{
					stillMoving = false;
				}
			}
			
		return;
	}
			
}
