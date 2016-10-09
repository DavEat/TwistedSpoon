using UnityEngine;
using System.Collections.Generic;

public class CreateWeight : MonoBehaviour {

    public GameObject weight;
    [SerializeField]
    private ListObjectLevel listObjectLevel;

    [SerializeField]
    private List<Transform> listPosInventory;
    public List<GameObject> listWeight;
    public List<GameObject> listMesh;

    public void Start()
    {
        int iLastRandom = Random.Range(0, listMesh.Count);
        foreach (Transform t in listPosInventory)
        {
            /*if (iLastRandom <= listMesh.Count / 2)
                iLastRandom = Random.Range(iLastRandom, listMesh.Count);
            else
                iLastRandom = Random.Range(0, iLastRandom);

            int iQuantity = listMesh[iLastRandom].GetComponent<Weight_Mesh>().iQuantity;
            units eUnits = listMesh[iLastRandom].GetComponent<Weight_Mesh>().eUnits;
            CreateObj(iQuantity, listMesh[iLastRandom].name, eUnits, t);*/

            CreateObj(t);
        }

        
    }

    public void CreateObj(int _quantity, string _meshName, units _units, Transform parentt)
    {
        GameObject newWeight = Instantiate(weight) as GameObject;
        //newWeight.GetComponent<WeightInfo>().Init(_quantity, _meshName, _units, parentt);
        newWeight.GetComponent<WeightInfo>().Init(_quantity, _meshName, _units, parentt);
        GameObject mesh = Instantiate(Resources.Load("WeightMesh/" + newWeight.GetComponent<WeightInfo>().GetMeshName()), newWeight.transform) as GameObject;
        mesh.transform.localPosition = Vector3.zero;

        newWeight.transform.parent = parentt;
        newWeight.transform.localEulerAngles = Vector3.zero;
        newWeight.transform.localScale = Vector3.one * 2;
        newWeight.transform.localPosition = Vector3.zero + new Vector3(0, 0, -1.5f);

        listWeight.Add(newWeight);
    }

    public void CreateObj(Transform parentt)
    {
        WeightInfo wInfo = listObjectLevel.GetNextPlayerObject();

        GameObject newWeight = Instantiate(weight) as GameObject;
        //newWeight.GetComponent<WeightInfo>().Init(_quantity, _meshName, _units, parentt);
        newWeight.GetComponent<WeightInfo>().Init(wInfo.GetQuantity(), wInfo.GetMeshName(), wInfo.GetUnit(), parentt);
        GameObject mesh = Instantiate(Resources.Load("WeightMesh/" + newWeight.GetComponent<WeightInfo>().GetMeshName()), newWeight.transform) as GameObject;
        mesh.transform.localPosition = Vector3.zero;

        newWeight.transform.parent = parentt;
        newWeight.transform.localEulerAngles = Vector3.zero;
        newWeight.transform.localScale = Vector3.one * 2;
        newWeight.transform.localPosition = Vector3.zero + new Vector3(0, 0, -1.5f);

        listWeight.Add(newWeight);

        IgnoreCol(newWeight);
    }

    public void IgnoreCol(GameObject w)
    {
        foreach (GameObject x in listWeight)
            if (w != x)
                Physics.IgnoreCollision(w.GetComponent<Collider>(), x.transform.GetChild(0).GetComponent<Collider>());
    }
}
