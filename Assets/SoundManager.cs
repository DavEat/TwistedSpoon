using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class SoundManager : MonoBehaviour {

	public AudioSource audioSource;
	public List<AudioClip> ambianceRandom = new List<AudioClip>();
	public float everyXTime = 10.0f;

	private float time = 0.0f;

	void Start () 
	{
		audioSource = GetComponent<AudioSource> ();
	}
	
	void Update () 
	{
		time += Time.deltaTime;
		if( audioSource.clip != null && !audioSource.isPlaying && time > everyXTime)
		{
			time = 0.0f;
			audioSource.clip = null;
		}
		else if (audioSource.clip == null && time >= everyXTime)
		{
			audioSource.clip = 	ambianceRandom [Random.Range (0, ambianceRandom.Count)];
			audioSource.Play ();
		}
	}
}
