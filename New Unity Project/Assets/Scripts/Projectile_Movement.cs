using UnityEngine;
using System.Collections;

public class Projectile_Movement : MonoBehaviour {
	
	private Vector3 target;
	float speed;
	float step;
	
	void Start ()
	{
	}
	
	void Update()
	{
		transform.position = Vector3.MoveTowards(transform.position, target, step);
		transform.LookAt (Camera.main.transform);
	}
	
	public void SetTarget(Vector3 target, float speed)
	{
		this.target = target;
		this.speed = speed;
		this.step = speed * Time.deltaTime;
	}
}
