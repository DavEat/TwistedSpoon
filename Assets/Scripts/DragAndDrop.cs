using UnityEngine;
using System.Collections;

public class DragAndDrop : MonoBehaviour {

    private Transform currentDragElement = null;

    [SerializeField]
    private float heightDraElem = 1;

    [SerializeField]
    public LayerMask layer;

	void Update ()
    {
        #if (UNITY_EDITOR || UNITY_STANDALONE_WIN)
            if (Input.GetMouseButtonUp(0))
            {
                if(currentDragElement != null)
                {
                    currentDragElement.gameObject.layer = 0;
                    currentDragElement = null;
                }               
            }
            else if (Input.GetMouseButton(0))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                CheckClick(ray);
            }
        #elif (UNITY_ANDROID || UNITY_IPHONE || UNITY_WP8)
            
        #endif
    }

    private void CheckClick(Ray ray)
    {
        RaycastHit hit;
        Debug.DrawRay(ray.origin, ray.direction * 100, Color.blue);
        if (Physics.Raycast(ray, out hit, 100, layer))
        {
            if (currentDragElement == null)
            {
                if (hit.transform.parent != null && hit.transform.parent.CompareTag("Weight"))
                {
                    currentDragElement = hit.transform.parent;
                    currentDragElement.gameObject.layer = 11;
                }
            }
            else
            {
                currentDragElement.position = hit.point + new Vector3(0, heightDraElem, 0);
            }
        }
    }
}
