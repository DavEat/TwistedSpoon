using UnityEngine;
using System.Collections.Generic;

public class ReplaceItemOfInventory : MonoBehaviour {

    [SerializeField]
    private List<Transform> listT, listTarget;

	void Update ()
    {
	    for(int i = 0; i < listT.Count; i++)
            listT[i].position = listTarget[i].position;
	}

    public List<Transform> GetListTarget()
    {
        return this.listTarget;
    }
}
