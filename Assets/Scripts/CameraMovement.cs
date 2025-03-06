using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public float horSpeed = 20f;
    public float verSpeed = 20f;
    public float verUpperBound = 45f;
    public float verLowerBound = -45f;

    private float verRotation = 0f;
    private Camera cam;

    void Start() {
        cam = GetComponentInChildren<Camera>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update() {
        float horRotation = Input.GetAxis("Mouse X") * Time.deltaTime * horSpeed;
        transform.Rotate(0, horRotation, 0);

        verRotation -= Input.GetAxis("Mouse Y") * Time.deltaTime * verSpeed;
        verRotation = Mathf.Clamp(verRotation, verLowerBound, verUpperBound);
        cam.transform.localEulerAngles = new(verRotation, 0, 0);
    }
}
