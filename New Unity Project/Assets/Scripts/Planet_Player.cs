using UnityEngine;
using System.Collections;

public class Planet_Player: MonoBehaviour {
		
	public Transform targetPlanet;
	public Transform target;

    /// <summary>
    /// These are all the attributes of our current unit.
    /// </summary>
	public float runSpeed = 10.0f;
	public float strafeSpeed = 3.5f;
	public float turnSpeed = 100.0f;
	public float jumpSpeed = 10.0f;
	public float gravitySpeed = 100.0f;

	private Vector3 vectorNormal;	
	private Vector3 vectorCross;

	private bool canJump = false;

	void FixedUpdate ()	
	{

        

        // These are used for automated movement
		//transform.position = Vector3.MoveTowards(transform.position, target.position, runSpeed * Time.deltaTime);
        //Vector3 newDir = Vector3.RotateTowards(transform.position, target.position, turnSpeed * Time.deltaTime, 0.0f);
        //transform.rotation = Quaternion.LookRotation(newDir);

        // This is used for manual movement.
	    transform.position += transform.forward * (Input.GetAxis("Vertical") * Time.deltaTime * runSpeed);
        transform.Rotate(new Vector3(0, Input.GetAxis("Horizontal") * Time.deltaTime * turnSpeed, 0));
		

		
		// transform.position += transform.right * (Input.GetAxis("Strafe") * Time.deltaTime * strafeSpeed);

		if (Input.GetButton("Jump") && canJump)	
		{			
			rigidbody.AddForce(vectorNormal * jumpSpeed);			
		}

        // This makes sure that our units stay 'upright' on the planet at any given time.
		vectorNormal = transform.position - targetPlanet.position;		
		vectorCross = -Vector3.Cross(vectorNormal, transform.right);		
		transform.rotation = Quaternion.LookRotation(vectorCross, vectorNormal); 
		
	}

	void OnCollisionStay (Collision other)		
	{		
		if (other.gameObject.tag == "Planet") {canJump = true;}		
	}

	void OnCollisionExit (Collision other)		
	{		
		if (other.gameObject.tag == "Planet") {canJump = false;}		
	}
	
}