using UnityEngine;
using System.Collections;

public class CameraScript : MonoBehaviour {

    public Transform lookatObj;

    public GameObject player;
    public GameObject mainCamera;
    public GameObject cameraCollisionBox;
    private Vector3 offset;

    // Use this for initialization
    void Start () {
    }

    public float Sensitivity = 5.0f;

    private float yaw = 0.0f;
    private float pitch = 0.0f;

    // Update is called once per frame
    void Update () {
        if (Screen.lockCursor) {
            if (Sensitivity * Input.GetAxis("Mouse Y") > 0 && pitch > -60)
            {
                pitch -= Sensitivity * Input.GetAxis("Mouse Y");
                if (pitch < -60) pitch = -60;
            }

            if (Sensitivity * Input.GetAxis("Mouse Y") < 0 && pitch < 60)
            {
                pitch -= Sensitivity * Input.GetAxis("Mouse Y");
                if (pitch > 60) pitch = 60;
            }
            yaw += Sensitivity * Input.GetAxis("Mouse X");
            transform.eulerAngles = new Vector3(pitch, yaw, 0.0f);

            float horizontal = Input.GetAxis("Mouse X") * Sensitivity;
            player.transform.Rotate(0, horizontal, 0);

            //lookatObj.transform.position = player.transform.position + mainCamera.transform.forward *5 +new Vector3(0,15,0);
            Vector3 newPosition = lookatObj.transform.position - mainCamera.transform.forward * 10 * 2;
            cameraCollisionBox.transform.position = newPosition;
        }
            
    }
}
