using UnityEngine;
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

    Scene
        mScene;

	void Start () 
	{
		Instance = this;
        mScene = GameObject.FindObjectOfType<Scene>();
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
					UpSideMass += w.GetQuantity() * Mathf.Abs(w.transform.position.z / 9.5f);
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
					DownSideMass += w.GetQuantity() * Mathf.Abs(-w.transform.position.z / 9.5f);
				}
			}
		}
		MasseDifference = Mathf.Abs( UpSideMass - DownSideMass );
		MasseDifference /= RationMassAngle;

		MasseDifference = (UpSideMass > DownSideMass) ? MasseDifference : -MasseDifference; 
	}

	void RotateBoard( float angle)
	{
		Quaternion qFinalAngle = Quaternion.AngleAxis(angle,Vector3.left);
		StartCoroutine( RotateTo (qFinalAngle));

		if(qFinalAngle == transform.localRotation)
		{
			BoardState = State.State_Wait;
			GameManager.Instance.SwitchState(GameManager.GameState.GameState_IATurn);
            mScene.CheckRotationPlayLeft(transform.localEulerAngles.x);
        }
	}

	void OnCollisionEnter( Collision col )
	{
        WeightInfo weight = col.transform.GetComponent<WeightInfo>();

		if(weight != null)
		{
            weight.SetUpSide((weight.transform.position.z > 0) ? true : false);            
            weight.GetComponent<Rigidbody> ().isKinematic = true;
            weight.transform.parent = this.transform;
            weight.transform.localRotation =  Quaternion.Euler(Vector3.zero);


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
			transform.localRotation = Quaternion.Lerp (transform.rotation, Angle, t);
			yield return new WaitForEndOfFrame();
		}
	}
}
