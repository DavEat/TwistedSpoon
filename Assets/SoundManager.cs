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


	private float time = 0.0f;

	void Start () 
	{
		Instance = this;
		audioSource = GetComponent<AudioSource> ();
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

	public AudioClip PlayCollisionSound (string name, bool metal)
	{
		AudioClip sound = null;
		foreach(AudioClip clip in collisionSound)
		{
            if (name == "Bouteil_A_01" || name == "Bouteil_A_02")
            {
                if (metal)
                    sound = collisionSound[0];
                else sound = collisionSound[1];
            }
            else if (name == "Lingo_01")
            {
                if (metal)
                    sound = collisionSound[2];
                else sound = collisionSound[3];
            }
            else if (name == "Piece_A" || name == "Piece_B" || name == "Piece_C")
            {
                if (metal)
                    sound = collisionSound[4];
                else sound = collisionSound[5];
            }
            else if (name == "Tonneau")
            {
                if (metal)
                    sound = collisionSound[6];
                else sound = collisionSound[7];
            }
            else if (name == "Collier")
            {
                if (metal)
                    sound = collisionSound[8];
                else sound = collisionSound[9];
            }
            else Debug.Log("Error : name of sound not good");


        }
        if (sound == null)
            sound = collisionSound[4];
        return sound;
	}

}
