using UnityEngine;
using UnityEngine.AI;

public class CharacterMovement : MonoBehaviour
{
    [SerializeField] private float speed = 3.5f;
    [SerializeField] private float _rotarionSpeed = 3.5f;

    private NavMeshAgent _agent;
    private Vector3 _movement;

    public bool IsRunning { get; private set; }

    public float CurrentSpeed => _movement.magnitude;

    private void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        _movement = new Vector3(horizontal, 0, vertical) * speed * Time.deltaTime;
        _agent.Move(_movement);

        if (_movement.magnitude > 0)
        {
            IsRunning = true;
            Quaternion targetRotation = Quaternion.LookRotation(_movement);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, _rotarionSpeed * Time.deltaTime);
        }
        else
        {
            IsRunning = false;
        }
    }
}