using UnityEngine;
using System.Collections;

public class DragAndDrop : MonoBehaviour {

    private Transform currentDragElement = null;

    [SerializeField]
    private float heightDraElem = 1;

    [SerializeField]
    private LayerMask layerBoard;  //--Check if we are on the board--

    [SerializeField]
    private LayerMask layerCube;  //--Check if we are not on a orther undrag cube--

    void Update ()
    {
        #if (UNITY_EDITOR || UNITY_STANDALONE_WIN)
            if (Input.GetMouseButtonUp(0))  //---If release left click---
            {
                if(currentDragElement != null)
                {
                    Ray ray = new Ray(currentDragElement.localPosition, Vector3.down);
                    RaycastHit hit;
                    if (Physics.Raycast(ray, out hit, 100, layerCube))
                    {
                        if (hit.transform.CompareTag("Weight"))
                        {
                            Debug.Log("red + alpha");
                            currentDragElement.gameObject.layer = 12;
                        }                           
                        else
                        {
                            currentDragElement.GetComponent<Rigidbody>().useGravity = true;
                            currentDragElement.gameObject.layer = 02;
                        }
                    }                        
                    currentDragElement = null;
                }               
            }
            else if (Input.GetMouseButtonDown(0))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                CheckClick(ray);
            }
            else if (Input.GetMouseButton(0))
            {
                if (currentDragElement != null)
                {
                    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                    CheckClick(ray);
                }                
            }
        #elif (UNITY_ANDROID || UNITY_IPHONE || UNITY_WP8)
            
        #endif
    }

    private void CheckClick(Ray ray)
    {
        RaycastHit hit;
        Debug.DrawRay(ray.origin, ray.direction * 100, Color.blue);
        if (Physics.Raycast(ray, out hit, 100, layerBoard))
        {
            Debug.Log("hit : " + hit.transform.name);
            if (currentDragElement == null)
            {
                if (/*hit.transform.parent != null &&*/ hit.transform.CompareTag("Weight"))
                {
                    currentDragElement = hit.transform;
                    currentDragElement.parent = transform.parent;
                    currentDragElement.localEulerAngles = Vector3.zero;
                    currentDragElement.GetComponent<Rigidbody>().useGravity = false;
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
