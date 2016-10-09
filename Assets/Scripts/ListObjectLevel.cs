using UnityEngine;
using System.Collections.Generic;

public class ListObjectLevel : MonoBehaviour {

    private List<WeightInfo> listPlayer = new List<WeightInfo>(), listOrdi = new List<WeightInfo>();
    private int indexPlayer = -1, indexOrdi = -1;

    public WeightInfo GetNextPlayerObject()
    {
        indexPlayer++;
        if (indexPlayer < listPlayer.Count)
            return listPlayer[indexPlayer];
        else return null;
    }

    public WeightInfo GetNextOrdiObject()
    {
        indexOrdi++;
        if (indexOrdi < listOrdi.Count)
            return listOrdi[indexOrdi];
        else return null;
    }

    void Start()
    {
        //-----Player List-----
        listPlayer.Add(new WeightInfo(10, "Cube", units.g));


        //-----Orid List -----
        listOrdi.Add(new WeightInfo(10, "Cube", units.g));
    }
}
