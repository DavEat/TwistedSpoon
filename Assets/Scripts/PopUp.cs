using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PopUp : MonoBehaviour {

    GameObject mMesh;
   public Text mtextMesh;
    public Vector3 Offset;
	// Use this for initialization
	void Update ()
    {
        transform.LookAt(Camera.main.transform);
       transform.position =  Input.mousePosition + Offset;
        transform.rotation = (Quaternion.Euler(Vector3.zero));
    }
}
