using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class CircleBoard : MonoBehaviour {

	public static CircleBoard Instance { get; private set;}
	public List<WeightInfo> Weights = new List<WeightInfo>();

	public float RotateTime = 1.0f;
	public float RationMassAngle = 2.0f;

	public bool LeverArm = false;
	public enum State {State_Wait, State_Rotate}
	public State BoardState = State.State_Wait;

	private Quaternion ToAngle = Quaternion.identity;

	void Start () 
	{
		Instance = this;
	}
	
	void Update () 
	{
		switch(BoardState)
		{
		case State.State_Wait:
			break;
		case State.State_Rotate:
			RotateBoard(ToAngle);
            BoardState = State.State_Wait;
			break;
		}
	}

	void CheckMass ()
	{
		Vector2 MassDirection = Vector2.zero;

		foreach(WeightInfo Weight in Weights)
		{
			float Mass = 0.0f;

			if (!LeverArm) 
			{
				Mass = Weight.GetQuantity();	
			} 
			else 
			{
				Mass = Weight.GetQuantity() * Mathf.Abs(Weight.transform.position.z / (transform.localScale.z / 2.0f));
			}
			Mass /= RationMassAngle;

			Vector2 dir = new Vector2 (Weight.transform.position.x - transform.position.x, Weight.transform.position.z - transform.position.z).normalized * Mass;
			MassDirection += dir;
		}
		ToAngle = Quaternion.Euler (new Vector3 ( MassDirection.y, 0.0f, -MassDirection.x));
	}

	void RotateBoard( Quaternion angle)
	{
		StartCoroutine( RotateTo (ToAngle));
	}

	void OnCollisionEnter( Collision col )
	{
		WeightInfo weight = col.transform.GetComponent<WeightInfo>();

		if(weight != null)
		{
			weight.GetComponent<Rigidbody> ().isKinematic = true;
			weight.transform.parent = this.transform.parent;

			Weights.Add (weight);
			CheckMass ();
		}	
	}

	void OnCollisionExit( Collision col )
	{
		WeightInfo weight = col.transform.GetComponent<WeightInfo>();

		if (weight != null) 
		{
			weight.transform.parent = null;
			weight.GetComponent<Rigidbody> ().isKinematic = false;

			Weights.Remove(weight);
			CheckMass ();
		}
	}

	IEnumerator RotateTo(Quaternion Angle)
	{
		for(float t = 0;;t += Time.deltaTime / RotateTime)
		{
            transform.parent.localRotation = Quaternion.Lerp ( transform.parent.localRotation, Angle,t);
			yield return new WaitForEndOfFrame();
		}
	}
}