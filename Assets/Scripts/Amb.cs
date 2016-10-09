using UnityEngine;
using UnityEngine.Audio;
using System.Collections;

public class Amb : MonoBehaviour {

	public static Amb Instance {get; private set;}
	private AudioSource audioSource;
	public AudioMixer Main_mixer;
	private float fadein_speed = 25.0f;
	private float audio_mood1_vol = -80.0f;

	void Start () 
	{
		Instance = this;
		audioSource = GetComponent<AudioSource> ();
		audioSource.outputAudioMixerGroup = Main_mixer.FindMatchingGroups ("Mood1") [0];
		Main_mixer.SetFloat ("Mood1", audio_mood1_vol);
	}
	
	void Update () 
	{
		if(audioSource.isPlaying)
		{
			Main_mixer.SetFloat ("Mood1", audio_mood1_vol);
			if (audio_mood1_vol < 0.0f) 
			{
				audio_mood1_vol += fadein_speed * Time.deltaTime;	
			}
		}
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
