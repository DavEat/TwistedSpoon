using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class SoundManager : MonoBehaviour {

	public static SoundManager Instance { get; private set;}
	public AudioSource audioSource;
	public List<AudioClip> ambianceRandom = new List<AudioClip>();
	public List<AudioClip> collisionSound = new List<AudioClip>();

	public float everyXTime = 10.0f;

	private AudioSource AmbAudioSource;

	private float time = 0.0f;

	void Start () 
	{
		Instance = this;
		audioSource = GetComponent<AudioSource> ();
		AmbAudioSource = GetComponentInChildren<AudioSource> ();
	}
	
	void Update () 
	{
		if (GameManager.Instance.gameState != GameManager.GameState.GameState_Menu) 
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

	public AudioClip PlayCollisionSound ( string name )
	{
		AudioClip sound = null;
		foreach(AudioClip clip in collisionSound)
		{
			if(clip.name == name)
			{
				return clip;
			}
		}
		return sound;
	}

}
