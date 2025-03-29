using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private float _smoothSpeed = 0.125f;
    [SerializeField] private PlayerSpawner _spawner;

    private Transform _playerTransform;
    private Vector3 _offset;

    private void Awake()
    {
        _spawner.PlayerSpawned += GetLinkPlayer;
    }

    private void OnDestroy()
    {
        _spawner.PlayerSpawned -= GetLinkPlayer;
    }

    private void LateUpdate()
    {
        if (_playerTransform != null)
        {
            Vector3 desiredPosition = _playerTransform.position + _offset;
            transform.position = Vector3.Lerp(transform.position, desiredPosition, _smoothSpeed);
        }
    }

    private void GetLinkPlayer(Character player)
    {
        _playerTransform = player.transform;
        _offset = transform.position - _playerTransform.position;
    }
}