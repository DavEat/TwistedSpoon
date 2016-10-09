using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using System.Linq;

public class IA : MonoBehaviour {

	public static IA Instance { get; private set;}
	public enum BoardType
	{
		BoardType_Simple,
		BoardType_Cylinder
	};

	public BoardType boardType = BoardType.BoardType_Simple;
	public bool RandomSide = false;
	public List<BoxCollider> SpawningZoneTypeOne = new List<BoxCollider> ();
	public GameObject SpawningZoneTypeTwo;

	public GameObject objectToInstantiate;
	public float radiusZoneTwo = 10.0f;

	private ListObjectLevel listObject;

	void Start () 
	{
		Instance = this;
		listObject = GameObject.FindGameObjectWithTag ("ListObject").GetComponent<ListObjectLevel> ();
		SpawningZoneTypeOne = transform.FindChild ("SpawnBoard").GetComponents<BoxCollider> ().ToList<BoxCollider>();
		SpawningZoneTypeTwo = transform.FindChild ("SpawnCircle").gameObject;
	}
	
	void Update () 
	{
		if(Input.GetKeyDown(KeyCode.E))
		{
			//SpawningtypeTwo ();
		}
	}

	public void Play()
	{
		objectToInstantiate = listObject.GetNextOrdiObject ().gameObject; 
		switch (boardType) 
		{
		case BoardType.BoardType_Simple:
			SpawningtypeOne ();
				break;

		case BoardType.BoardType_Cylinder:
			SpawningtypeTwo ();
				break;
		}
		GameManager.Instance.SwitchState (GameManager.GameState.GameState_PlayerTurn);
	}

	void SpawningtypeOne()
	{
		Vector3 RandomPos = Vector3.zero;
		BoxCollider col = SpawningZoneTypeOne[0];

		if( RandomSide == false )
		{
			col = SpawningZoneTypeOne [Random.Range(0, SpawningZoneTypeOne.Count)];
		}

		RandomPos = new Vector3 (col.center.x + Random.Range (-col.size.x / 2.0f, col.size.x / 2.0f),
								col.center.y,
								col.center.z + Random.Range (-col.size.z , col.size.z ));
		
		while (!CheckSpawnPosition (RandomPos, BoardType.BoardType_Simple)) 
		{
			RandomPos = new Vector3 (col.center.x + Random.Range (-col.size.x / 2.0f, col.size.x / 2.0f),
									col.center.y,
									col.center.z + Random.Range (-col.size.z , col.size.z ));
		}
		Instantiate (objectToInstantiate,RandomPos, Quaternion.identity);
	}

	void SpawningtypeTwo()
	{
		Vector3 RandomPos = Vector3.zero;

		RandomPos = new Vector3 (SpawningZoneTypeTwo.transform.position.x + Random.Range (-radiusZoneTwo / 2.0f, radiusZoneTwo / 2.0f),
								SpawningZoneTypeTwo.transform.position.y,
								SpawningZoneTypeTwo.transform.position.z + Random.Range (-radiusZoneTwo / 2.0f , radiusZoneTwo /2.0f ));
		while (!CheckSpawnPosition (RandomPos, BoardType.BoardType_Simple)) 
		{
			RandomPos = new Vector3 (SpawningZoneTypeTwo.transform.position.x + Random.Range (-radiusZoneTwo / 2.0f, radiusZoneTwo / 2.0f),
									SpawningZoneTypeTwo.transform.position.y,
									SpawningZoneTypeTwo.transform.position.z + Random.Range (-radiusZoneTwo / 2.0f , radiusZoneTwo /2.0f ));
		}
		Instantiate (objectToInstantiate,RandomPos, Quaternion.identity);
	}

	public static bool CheckSpawnPosition( Vector3 position, BoardType type)
	{
		RaycastHit hit;
		if (Physics.Raycast(position, Vector3.down, out hit)) 
		{
			WeightInfo weight = hit.transform.GetComponent<WeightInfo> ();

			if (weight == null)
			{
				if(CheckDistance( type, hit.point ))
				{
					return true;
				}
			}
		}
		return false;
	}

	private static bool CheckDistance(BoardType type, Vector3 pos)
	{
		List<WeightInfo> list;
		if(type == BoardType.BoardType_Simple)
		{
			list = Board.Instance.listWeight;
		}
		else
		{
			list = CircleBoard.Instance.Weights;
		}
			
		foreach( WeightInfo weight in list)
		{
			float fDistance = (new Vector2 (pos.x, pos.z) - new Vector2 (weight.transform.position.x, weight.transform.position.z)).magnitude;
			if (fDistance <=  weight.GetRadius() + 1.0f )
			{
				return false;
			}
		}
		return true;
	}
}
