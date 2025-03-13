using UnityEngine;

public class ThirdPersonCameraController : MonoBehaviour
{
    [SerializeField] private Transform playerTransform;
    [SerializeField] private float rotationSpeed = 100f;

    void Update()
    {
        if (Input.GetMouseButton(1))
        {
            float horizontalRotation = Input.GetAxis("Mouse X");
            float verticalRotation = Input.GetAxis("Mouse Y");

            playerTransform.Rotate(0, horizontalRotation * rotationSpeed * Time.deltaTime, 0);

            float angle = playerTransform.eulerAngles.x - verticalRotation * rotationSpeed * Time.deltaTime;
            angle = Mathf.Clamp(angle, -45f, 45f);
            transform.localEulerAngles = new Vector3(angle, 0, 0);
        }
    }
}