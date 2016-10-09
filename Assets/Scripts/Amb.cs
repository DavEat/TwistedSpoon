using UnityEngine;
using System.Collections;

public class Amb : MonoBehaviour {

	public static Amb Instance {get; private set;}
	private AudioSource audioSource;

	void Start () 
	{
		Instance = this;
		audioSource = GetComponent<AudioSource> ();
	}
	
	void Update () 
	{
	
	}

	public void PlayAmbianteSound()
	{
		audioSource.Play ();
	}

	public void StopAmbianteSound()
	{
		audioSource.Stop();
	}
}
