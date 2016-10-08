﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using System.Linq;

public class Board : MonoBehaviour 
{
	public static Board Instance { get; private set;}
	public List<WeightInfo> listWeight = new List<WeightInfo> ();
	public float RotateTime = 1.0f;
	public float RationMassAngle = 2.0f;

	public bool LeverArm = false;
	public enum State {State_Wait, State_Rotate}
	public State BoardState = State.State_Wait;

	private float MasseDifference = 0.0f;

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
			RotateBoard(-MasseDifference);
				break;
		}
	}

	void CheckMass ()
	{
		float UpSideMass = 0.0f;
		float DownSideMass = 0.0f;

		foreach(WeightInfo w in listWeight)
		{
			if(w.GetUpSide() )
			{
				if (!LeverArm) 
				{
					UpSideMass += w.GetQuantity();	
				} 
				else 
				{
					UpSideMass += w.GetQuantity() * Mathf.Abs(w.transform.position.z / (transform.localScale.z / 2.0f));
				}
			}
			else
			{
				if (!LeverArm) 
				{
					DownSideMass += w.GetQuantity();
				} 
				else 
				{
					DownSideMass += w.GetQuantity() * Mathf.Abs(-w.transform.position.z / (transform.localScale.z / 2.0f));
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
			transform.localRotation = Quaternion.Lerp ( transform.rotation, Angle,t);
			yield return new WaitForEndOfFrame();
		}
	}
}
