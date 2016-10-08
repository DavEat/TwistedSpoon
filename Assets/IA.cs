using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using System.Linq;

public class IA : MonoBehaviour {

	public enum BoardType
	{
		BoardType_Simple,
		BoardType_Cylinder
	};

	public BoardType boardType = BoardType.BoardType_Simple;
	public bool RandomSide = false;
	public List<BoxCollider> SpawningZoneTypeOne = new List<BoxCollider> ();
	public GameObject SpawningZoneTypeTwo;

	void Start () 
	{
		SpawningZoneTypeOne = transform.FindChild ("SpawnBoard").GetComponents<BoxCollider> ().ToList<BoxCollider>();
	}
	
	void Update () 
	{
	
	}

	void SpawningtypeOne()
	{
		GameObject obj = transform.FindChild("SpawnBoard").gameObject;
		Vector3 RandomPos = Vector3.zero;
		BoxCollider col = SpawningZoneTypeOne[0];

		if( RandomSide )
		{
			col = SpawningZoneTypeOne [Random.Range(0, SpawningZoneTypeOne.Count)];
		}

		RandomPos = new Vector3 (col.center.x + Random.Range (-col.size.x, col.size.x), col.center.y, col.center.z + Random.Range (-col.size.z, col.size.z));

		while (!CheckSpawnPosition (RandomPos, BoardType.BoardType_Simple)) 
		{
			RandomPos = new Vector3 (col.center.x + Random.Range (-col.size.x, col.size.x), col.center.y, col.center.z + Random.Range (-col.size.z, col.size.z));	
		}
		//Instantiate (,RandomPos,);
	}

	void SpawningtypeTwo()
	{
		
	}

	bool CheckSpawnPosition( Vector3 position, BoardType type)
	{
		RaycastHit hit;
		if (Physics.Raycast(position, Vector3.down, hit)) 
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

	bool CheckDistance(BoardType type, Vector3 pos)
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
			//if ((pos - weight.transform.position).magnitude <  weight.radius )
			//{
				return false;
			//}
		}
		return true;
	}
}
