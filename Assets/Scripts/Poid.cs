using UnityEngine;
using System.Collections;

public class Poid : MonoBehaviour {

	public bool upSide;
	public float mass;

	void Start () 
	{
		mass = GetComponent<Rigidbody> ().mass;
	}
	
	void Update () 
	{
	
	}
}
