using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using System.Linq;

public class Board : MonoBehaviour 
{
	public List<WeightInfo> listWeight = new List<WeightInfo> ();
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

		foreach(WeightInfo w in listWeight)
		{
			if(w.GetUpSide() )
			{
				UpSideMass += w.GetQuantity();	
			}
			else
			{
				DownSideMass += w.GetQuantity();
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
        WeightInfo weight = col.transform.GetComponent<WeightInfo>();

		if(weight != null)
		{
            weight.SetUpSide((weight.transform.position.z > 0) ? true : false);            
            weight.GetComponent<Rigidbody> ().isKinematic = true;
            weight.transform.parent = this.transform;            

            listWeight.Add (weight);
			CheckMass ();
		}	
	}

	void OnCollisionExit( Collision col )
	{
		WeightInfo w = col.transform.GetComponent<WeightInfo>();

		if (w != null) 
		{
			w.transform.parent = null;
			w.GetComponent<Rigidbody> ().isKinematic = false;

            listWeight.Remove(w);
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
