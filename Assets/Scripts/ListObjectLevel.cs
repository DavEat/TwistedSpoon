using UnityEngine;
using System.Collections.Generic;

public class ListObjectLevel : MonoBehaviour {

    private List<WeightInfo> listPlayer = new List<WeightInfo>(), listOrdi = new List<WeightInfo>();
    private int indexPlayer = 0, indexOrdi = 0;

    public WeightInfo GetNextPlayerObject()
    {
		return listPlayer[indexPlayer++ % listPlayer.Count];
    }

    public WeightInfo GetNextOrdiObject()
    {
		Debug.Log (listOrdi[indexOrdi % listOrdi.Count]);
		return listOrdi[indexOrdi++ % listOrdi.Count];
    }

    void Awake()
    {
        WeightInfo w = new WeightInfo();

        //-----Player List-----
        w.Init(10, "Bouteil_A_01", units.g);
        listPlayer.Add(w);
        w.Init(10, "Bouteil_A_01", units.g);
        listPlayer.Add(w);
        w.Init(10, "Bouteil_A_01", units.g);
        listPlayer.Add(w);
        w.Init(10, "Bouteil_A_01", units.g);
        listPlayer.Add(w);
        w.Init(10, "Bouteil_A_01", units.g);
        listPlayer.Add(w);
        w.Init(10, "Bouteil_A_01", units.g);
        listPlayer.Add(w);


        //-----Ordi List -----
        w.Init(10, "Bouteil_A_01", units.g);
		listOrdi.Add(w);
        w.Init(10, "Bouteil_A_01", units.g);
		listOrdi.Add(w);
        w.Init(10, "Bouteil_A_01", units.g);
		listOrdi.Add(w);
        w.Init(10, "Bouteil_A_01", units.g);
		listOrdi.Add(w);
        w.Init(10, "Bouteil_A_01", units.g);
		listOrdi.Add(w);
        w.Init(10, "Bouteil_A_01", units.g);
		listOrdi.Add(w);

    }
}
