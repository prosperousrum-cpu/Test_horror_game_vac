using System.Threading;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEditor.Progress;

public class DoorOpenScript : MonoBehaviour
{
    private InputSystem _inputSystem;
    [SerializeField] private LayerMask _canDoorLayer;

    private Collider _doorCollider;
    private CancellationTokenSource _cancellationToken;
    private Rigidbody _currentRigidbodyObject;

    void Start()
    {
        _inputSystem = new();
        _inputSystem.Enable();

        //_inputSystem.Player.PickUp.performed += PickUp;       
        //_inputSystem.Player.PickUp.canceled += Drop;

        _inputSystem.Player.DoorInterakt.performed += Open_door;
        _inputSystem.Player.DoorInterakt.canceled += dont_touch_door;
    }

    private void Open_door(InputAction.CallbackContext _)
    {
        if (!Physics.Raycast(transform.parent.position, transform.parent.forward, out RaycastHit hit, 5, _canDoorLayer)) return;
        _doorCollider = hit.collider;




        _currentRigidbodyObject = _doorCollider.gameObject.GetComponent<Rigidbody>();
        transform.position = _doorCollider.transform.position;

        

        _cancellationToken = new();
        Follow();



    }

    private void dont_touch_door(bool isThrow = false)
    {
        if (_currentRigidbodyObject == null) return;

        _cancellationToken.Cancel();

        

        

        
        _currentRigidbodyObject = null;
    }

    private void dont_touch_door(InputAction.CallbackContext _) => dont_touch_door();

    private async void Follow()
    {
        while (!_cancellationToken.IsCancellationRequested)
        {
            _currentRigidbodyObject.linearVelocity = (transform.position - _currentRigidbodyObject.transform.position) * 30;
            await Task.Delay(20, _cancellationToken.Token);
        }
    }

    private void OnDestroy()
    {
        _inputSystem.Player.PickUp.performed -= Open_door;
    }
}
