using UnityEngine;
using System.Collections.Generic;

public class CreateWeight : MonoBehaviour {

    public GameObject weight;

    [SerializeField]
    private List<Transform> listPosInventory;
    public List<GameObject> listWeight;

    void Start()
    {
        foreach (Transform t in listPosInventory)
            CreateObj(10, meshName.Cube, units.kg, t);
    }

    public void CreateObj(int _quantity, meshName _meshName, units _units, Transform parentt)
    {
        GameObject newWeight = Instantiate(weight) as GameObject;
        newWeight.GetComponent<WeightInfo>().Init(_quantity, _meshName, _units, parentt);
        GameObject mesh = Instantiate(Resources.Load("WeightMesh/" + newWeight.GetComponent<WeightInfo>().GetMeshName()), newWeight.transform) as GameObject;
        mesh.transform.localPosition = Vector3.zero;

        foreach(GameObject w in listWeight)
            Physics.IgnoreCollision(newWeight.GetComponent<Collider>(), w.GetComponent<Collider>());

        newWeight.transform.parent = parentt;
        newWeight.transform.localEulerAngles = Vector3.zero;
        newWeight.transform.localScale = Vector3.one * 2;
        newWeight.transform.localPosition = Vector3.zero + new Vector3(0, 0, -1.5f);

        listWeight.Add(newWeight);
    }
}
