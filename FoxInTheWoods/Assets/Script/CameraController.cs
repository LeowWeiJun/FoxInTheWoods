using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    public Transform target;

    public float smoothSpeed = 0.125f;

    public Vector3 offset;

    public float rotateSpeed;
   
    public Transform pivot;

    public float maxViewAngle;
    public float minViewAngle;

    // Use this for initialization
    void Start () {
        offset = target.position - transform.position;
        //pivot.transform.position = target.position;
        pivot.transform.parent = null;

        //Cursor.lockState = CursorLockMode.Locked;
    }
	
	// Update is called once per frame
	void LateUpdate () {

        pivot.transform.position = target.position;

        float horizontal = Input.GetAxis("Mouse X") * rotateSpeed;
        pivot.Rotate(0, horizontal, 0);

        //float vertical = Input.GetAxis("Mouse Y") * rotateSpeed;
        //pivot.Rotate(-vertical, 0, 0);

        /*if(pivot.rotation.eulerAngles.x > maxViewAngle && pivot.rotation.eulerAngles.x < 180f)
        {
            pivot.rotation = Quaternion.Euler(maxViewAngle, 0, 0);
        }

        if (pivot.rotation.eulerAngles.x > 180f && pivot.rotation.eulerAngles.x < 360f + minViewAngle)
        {
            pivot.rotation = Quaternion.Euler(360f + minViewAngle, 0, 0);
        }*/

        float desiredYAngle = pivot.eulerAngles.y;
        //float desiredXAngle = pivot.eulerAngles.x;

        Quaternion rotation = Quaternion.Euler(0, desiredYAngle, 0);

        transform.position = target.position - (rotation * offset);
       // Vector3 smoothPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        //transform.position = smoothPosition;

        if(transform.position.y < target.transform.position.y)
        {
            transform.position = new Vector3(transform.position.x, target.transform.position.y, transform.position.z);
        }

        transform.LookAt(target);
    }
}
