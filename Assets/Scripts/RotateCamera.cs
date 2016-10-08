using UnityEngine;
using System.Collections;

public class RotateCamera : MonoBehaviour {

    public bool canRotate, firstStat;
    private Vector2 atClick ,firstpoint, secondpoint;
    private float xAngTemp, yAngTemp, xAngle, yAngle;

    void Update()
    {
        if (canRotate)
        {
            //Check count touches
            if (Input.GetMouseButton(0))
            {
                if (Input.GetMouseButtonDown(0))
                {
                    atClick = Input.mousePosition;
                }

                //Touch began, save position
                if (firstStat)
                {
                    firstStat = !firstStat;
                    firstpoint = Input.mousePosition;
                    xAngTemp = xAngle;
                    yAngTemp = yAngle;
                }
                //Move finger by screen
                else
                {
                    firstStat = !firstStat;
                    secondpoint = Input.mousePosition;
                    //Mainly, about rotate camera. For example, for Screen.width rotate on 180 degree
                    xAngle = xAngTemp + (secondpoint.x - firstpoint.x) * 180.0f / Screen.width;
                    yAngle = yAngTemp - (secondpoint.y - firstpoint.y) * 90.0f / Screen.height;
                    

                    /*if (atClick.x > Screen.width / 2)
                        xAngle *= -1;
                    if (atClick.y > Screen.width / 2)
                        yAngle *= -1;*/

                    //Rotate camera
                    this.transform.rotation = Quaternion.Euler(0.0f, xAngle + yAngle, 0.0f);
                }
            }
        }
    }
}
