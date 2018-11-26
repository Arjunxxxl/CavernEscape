using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour {

	UserInput input;
	Vector2 dir;
	float dis;
	bool jump;

	Vector2 force;
	float angularVelocity;
	public float force_correction = 0.06f;
	public float angularVelocity_correction = .5f;
	float rotation_dir = 1f;

	Rigidbody2D rb;

	// Use this for initialization
	void Start () {
		input = UserInput.Insatnce;
		dir = input.Pass_Direction();
		dis = dir.magnitude;
		jump = input.Pass_choosed();

		rb = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update ()
	{
		dir = input.Pass_Direction();
		dis = input.Pass_Distance();
		jump = input.Pass_choosed();

		if(jump)
		{	
			//dir.Normalize();
			force = dir.normalized * dis * (-1f) * force_correction;

			if(dir.normalized.x >= 0)
			{
				rotation_dir = 1f;
			}
			else
			{
				rotation_dir = -1f;
			}

			angularVelocity = force.magnitude * angularVelocity_correction * rotation_dir;
			Debug.Log(force.magnitude);
			rb.velocity = force;
			rb.angularVelocity = angularVelocity;
		}
	}
}
