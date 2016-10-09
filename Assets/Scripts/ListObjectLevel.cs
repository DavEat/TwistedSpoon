﻿using UnityEngine;
using System.Collections.Generic;

public class ListObjectLevel : MonoBehaviour {

    [SerializeField]
    private GameObject weight;

    public List<GameObject> listAllObject;
    private List<WeightJustInfo> listPlayer = new List<WeightJustInfo>(), listOrdi = new List<WeightJustInfo>();
    private int indexPlayer = 0, indexOrdi = 0;

    public WeightJustInfo GetNextPlayerObject()
    {
		return listPlayer[indexPlayer++ % listPlayer.Count];
    }

    public GameObject GetNextOrdiObject()
    {
		return Create(listOrdi[indexOrdi++ % listOrdi.Count]);
    }
    public WeightJustInfo GetCurrentOrdiInfo()
    {
        return listOrdi[indexOrdi % listOrdi.Count];
    }

    private GameObject Create(WeightJustInfo info)
    {
        weight.GetComponent<WeightInfo>().Init(info.quantity, info.meshName, info.unit);        
        GameObject mesh = Instantiate(Resources.Load("WeightMesh/" + weight.GetComponent<WeightInfo>().GetMeshName()), weight.transform) as GameObject;
        mesh.transform.localPosition = Vector3.zero;
        return weight;
    }

    public void Awake()
    {


        //-----Player List-----
        WeightJustInfo w = new WeightJustInfo(10, "Bouteil_A_01", units.g);
        listPlayer.Add(w);
        w = new WeightJustInfo(10, "Bouteil_A_01", units.g);
        listPlayer.Add(w);
        w = new WeightJustInfo(10, "Bouteil_A_01", units.g);
        listPlayer.Add(w);
        w = new WeightJustInfo(10, "Bouteil_A_01", units.g);
        listPlayer.Add(w);
        w = new WeightJustInfo(10, "Bouteil_A_01", units.g);
        listPlayer.Add(w);
        w = new WeightJustInfo(10, "Bouteil_A_01", units.g);
        listPlayer.Add(w);
        w = new WeightJustInfo(10, "Bouteil_A_01", units.g);
        listPlayer.Add(w);


        //-----Ordi List -----
        w = new WeightJustInfo(10, "Bouteil_A_01", units.g);
        listOrdi.Add(w);
        w = new WeightJustInfo(10, "Bouteil_A_01", units.g);
        listOrdi.Add(w);
        w = new WeightJustInfo(10, "Bouteil_A_01", units.g);
        listOrdi.Add(w);
        w = new WeightJustInfo(10, "Bouteil_A_01", units.g);
        listOrdi.Add(w);
        w = new WeightJustInfo(10, "Bouteil_A_01", units.g);
        listOrdi.Add(w);
        w = new WeightJustInfo(10, "Bouteil_A_01", units.g);
        listOrdi.Add(w);
        w = new WeightJustInfo(10, "Bouteil_A_01", units.g);
        listOrdi.Add(w);

    }

    public void ReStartLevel()
    {
  

        for (int i = listAllObject.Count - 1; i >= 0; i--)
        {
            DestroyObject(listAllObject[i]);
        }
    }
}
