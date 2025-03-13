using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform playerTransform;
    [SerializeField] private float smoothSpeed = 0.125f;

    private Vector3 offset;

    private void Start()
    {
        offset = transform.position - playerTransform.position;
    }

    void LateUpdate()
    {
        Vector3 desiredPosition = playerTransform.position + offset;

        transform.position = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
    }
}