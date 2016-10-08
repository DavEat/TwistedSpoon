using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using System.Linq;

public class Board : MonoBehaviour 
{
	public List<Poid> poidsOnBoard = new List<Poid> ();
	public float RotateTime = 1.0f;
	public float RationMassAngle = 2.0f;

	void Start () 
	{
		
	}
	
	void Update () 
	{
	
	}

	void CheckMass ()
	{
		float Difference;
		float UpSideMass = 0.0f;
		float DownSideMass = 0.0f;

		foreach(Poid poid in poidsOnBoard)
		{
			if( poid.upSide )
			{
				UpSideMass += poid.mass;	
			}
			else
			{
				DownSideMass += poid.mass;
			}
		}

		Difference = Mathf.Abs( UpSideMass - DownSideMass );
		Difference /= RationMassAngle;

		if(UpSideMass < DownSideMass)
		{
			RotateBoard(Difference);
		}
		else
		{
			RotateBoard(-Difference);
		}
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
			poid.upSide = (poid.transform.position.z > 0) ? true : false;
			poid.transform.parent = this.transform;
			poid.GetComponent<Rigidbody> ().isKinematic = true;

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
			transform.rotation = Quaternion.Lerp ( transform.rotation, Angle,t);
			yield return new WaitForEndOfFrame();
		}
		yield return null;
	}
}
