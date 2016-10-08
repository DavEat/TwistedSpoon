using UnityEngine;
using System.Collections;

public class DragAndDrop : MonoBehaviour
{
    [HideInInspector]
    public Transform currentDragElement = null;

    [SerializeField]
    private float heightDraElem = 1;

    [SerializeField]
    private LayerMask layerBoard;  //--Check if we are on the board--

    [SerializeField]
    private LayerMask layerCube;  //--Check if we are not on a orther undrag cube--

    private bool inventory;

    void Update()
    {
    #if (UNITY_EDITOR || UNITY_STANDALONE_WIN)
        if (Input.GetMouseButtonUp(0))  //---If release left click---
        {            
            if (currentDragElement != null)
            {
                Ray ray;
                if (inventory)
                    ray = new Ray(currentDragElement.localPosition, currentDragElement.rotation * Vector3.forward);
                else ray = new Ray(currentDragElement.localPosition, Vector3.down);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit, 100, layerCube))
                    if (hit.transform.CompareTag("Green"))
                    {
                        if (currentDragElement.GetComponent<Rigidbody>().useGravity)
                            currentDragElement.GetComponent<Rigidbody>().useGravity = false;

                        currentDragElement.parent = currentDragElement.GetComponent<WeightInfo>().GetParentInventory();
                        currentDragElement.localPosition = Vector3.zero;
                        currentDragElement.localEulerAngles = Vector3.zero;
                        currentDragElement.GetChild(0).gameObject.layer = 0;
                    }
                    else if (!IA.CheckSpawnPosition(currentDragElement.position, IA.BoardType.BoardType_Simple))
                    {
                        Debug.Log("red + alpha");
                        currentDragElement.GetChild(0).gameObject.layer = 12;
                    }
                    else
                    {
                        currentDragElement.GetComponent<Rigidbody>().useGravity = true;
                        currentDragElement.GetChild(0).gameObject.layer = 0;
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
        //Debug.DrawRay(ray.origin, ray.direction * 100, Color.blue, 5);
        if (Physics.Raycast(ray, out hit, 100, layerBoard))
        {
            if (currentDragElement == null)
            {
                if (/*hit.transform.parent != null &&*/ hit.transform.CompareTag("Weight"))
                {
                    currentDragElement = hit.transform;
                    Debug.Log("hit = " + hit.transform.name);
                    currentDragElement.parent = transform.parent;
                    currentDragElement.localEulerAngles = Vector3.zero;
                    currentDragElement.GetComponent<Rigidbody>().useGravity = false;
                    currentDragElement.GetComponent<Rigidbody>().isKinematic = false;
                    currentDragElement.GetChild(0).gameObject.layer = 11;
                }
            }
            else
            {
                if (currentDragElement.localEulerAngles != Vector3.zero)
                {
                    currentDragElement.localEulerAngles = Vector3.zero;
                    if (inventory)
                        inventory = false;
                }
                currentDragElement.position = hit.point + new Vector3(0, heightDraElem, 0);
            }
        }
    }
}
