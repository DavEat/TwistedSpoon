using UnityEngine;
using System.Collections.Generic;

public class ReplaceItemOfInventory : MonoBehaviour {

    [SerializeField]
    private List<Transform> listT, listTarget;

	void Start ()
    {
	    for(int i = 0; i < listT.Count; i++)
        {
            listT[i].position = listTarget[i].position;
            //listT[i].GetChild(0).localEulerAngles = Vector3.zero;
        }
    }

    public List<Transform> GetListTarget()
    {
        return this.listTarget;
    }
}
