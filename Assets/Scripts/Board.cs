using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using System.Linq;

public class Board : MonoBehaviour 
{
	public List<Poid> poidsOnBoard = new List<Poid> ();
	public float RotateTime = 1.0f;
	public float RationMassAngle = 2.0f;

	public bool LeverArm = false;
	public enum State {State_Wait, State_Rotate}
	public State BoardState = State.State_Wait;

	private float MasseDifference = 0.0f;

	void Start () 
	{
		
	}
	
	void Update () 
	{
		switch(BoardState)
		{
			case State.State_Wait:
				break;
			case State.State_Rotate:
			RotateBoard(-MasseDifference);
				break;
		}
	}

	void CheckMass ()
	{
		float UpSideMass = 0.0f;
		float DownSideMass = 0.0f;

		foreach(Poid poid in poidsOnBoard)
		{
			if( poid.upSide )
			{
				if (!LeverArm) 
				{
					UpSideMass += poid.mass;	
				} 
				else 
				{
					UpSideMass += poid.mass * Mathf.Abs(poid.transform.position.z / (transform.localScale.z / 2.0f));
				}
			}
			else
			{
				if (!LeverArm) 
				{
					DownSideMass += poid.mass;
				} 
				else 
				{
					DownSideMass += poid.mass * Mathf.Abs(-poid.transform.position.z / (transform.localScale.z / 2.0f));
				}
			}
		}
		MasseDifference = Mathf.Abs( UpSideMass - DownSideMass );
		MasseDifference /= RationMassAngle;

		MasseDifference = (UpSideMass > DownSideMass) ? MasseDifference : -MasseDifference; 
	}

	void RotateBoard( float angle)
	{
		Quaternion test = Quaternion.AngleAxis(angle,Vector3.left); // definier l'angle.
		StartCoroutine( RotateTo (test));
	}

	void OnCollisionEnter( Collision col )
	{
		Poid poid = col.transform.GetComponent<Poid>();

		if(poid != null)
		{
			poid.GetComponent<Rigidbody> ().isKinematic = true;
			poid.upSide = (poid.transform.position.z > 0) ? true : false;
			poid.transform.parent = this.transform;

			poidsOnBoard.Add (poid);
			CheckMass ();
		}	
	}

	void OnCollisionExit( Collision col )
	{
		Poid poid = col.transform.GetComponent<Poid>();

		if (poid != null) 
		{
			poid.transform.parent = null;
			poid.GetComponent<Rigidbody> ().isKinematic = false;

			poidsOnBoard.Remove(poid);
			CheckMass ();
		}
	}

	IEnumerator RotateTo(Quaternion Angle)
	{
		for(float t = 0;;t += Time.deltaTime / RotateTime)
		{
			transform.localRotation = Quaternion.Lerp ( transform.rotation, Angle,t);
			yield return new WaitForEndOfFrame();
		}
	}
}
