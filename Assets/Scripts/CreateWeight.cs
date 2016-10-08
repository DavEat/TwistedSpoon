using UnityEngine;
using System.Collections.Generic;

public class CreateWeight : MonoBehaviour {

    public GameObject weight;

    public List<GameObject> listWeight;

    void Start()
    {
        CreateObj(50, meshName.Cube, units.kg, Vector3.zero);
    }

    public void CreateObj(int _quantity, meshName _meshName, units _units, Vector3 position)
    {
        GameObject newWeight = Instantiate(weight, position, Quaternion.Euler(Vector3.zero)) as GameObject;
        newWeight.GetComponent<WeightObjectInfo>().SetWeightInfo(new WeightInfo(_quantity, _meshName, _units));
        GameObject mesh = Instantiate(Resources.Load("WeightMesh/" + newWeight.GetComponent<WeightObjectInfo>().GetWeightInfo().GetMeshName()), newWeight.transform) as GameObject;
        mesh.transform.localPosition = Vector3.zero;

        listWeight.Add(newWeight);
    }
}
