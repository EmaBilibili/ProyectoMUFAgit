using UnityEngine;

public class PlayerCam : MonoBehaviour
{
    public float sensX;
    public float sensY;
    public float fov;
    public float viewDistance;
    static bool maniquiDetected = false;

    public Transform orientation;

    float xRotation;
    float yRotation;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * sensX;
        float mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * sensY;

        yRotation += mouseX;
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);
        orientation.rotation = Quaternion.Euler(0, yRotation, 0);

        RaycastHit hit;
        if (Physics.SphereCast(transform.position, 0.1f, orientation.forward, out hit, viewDistance))
        {
            if (hit.collider.CompareTag("Enemy"))
            {
                maniquiDetected = true;
                Debug.Log(maniquiDetected);
            }
            else
            {
                maniquiDetected = false;
                Debug.Log(maniquiDetected);
            }
        }
        
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawRay(transform.position, orientation.forward * viewDistance);

        Vector3 leftRayDirection = Quaternion.Euler(0, -fov / 2, 0) * orientation.forward;
        Vector3 rightRayDirection = Quaternion.Euler(0, fov / 2, 0) * orientation.forward;

        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, leftRayDirection * viewDistance);
        Gizmos.DrawRay(transform.position, rightRayDirection * viewDistance);
    }
}