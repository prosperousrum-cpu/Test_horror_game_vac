using System.Threading;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEditor.Progress;

namespace Player
{
    public class PickUper : MonoBehaviour
    {
        [SerializeField] private float _followSpeed = 30;
        [SerializeField] private float _trowForce = 7;
        [SerializeField] private float _pickUpDistance = 5;
        [SerializeField] private LayerMask _canPickUpLayer;
        [SerializeField] private LayerMask _canInteractLayer;
        [SerializeField] private Collider _playerCollider;
        [SerializeField] private Transform slot;

        public bool _HoldingObject = false;
        
        private InputSystem _inputSystem;
        private CancellationTokenSource _cancellationToken;
        
        private Rigidbody _currentRigidbodyObject;
        private Collider _currentColliderObject;

        private void Start()
        {
            _inputSystem = new();
            _inputSystem.Enable();
            
            //_inputSystem.Player.PickUp.performed += PickUp;       
            //_inputSystem.Player.PickUp.canceled += Drop;

            _inputSystem.Player.Trow.performed += Throw;

            _inputSystem.Player.Intaractive.performed += PickUp;
            _inputSystem.Player.Intaractive.canceled -= Drop;

            _inputSystem.Player.Intaractive.performed += Interact;

            
        }
        private void Interact(InputAction.CallbackContext _)
        {
            if (!Physics.Raycast(transform.parent.position, transform.parent.forward, out RaycastHit hit, _pickUpDistance, _canInteractLayer)) return;
            hit.collider.gameObject.GetComponent<Usage>().Use();


        }

        
        private void PickUp(InputAction.CallbackContext _)
        {
            


            if (!Physics.Raycast(transform.parent.position, transform.parent.forward, out RaycastHit hit, _pickUpDistance, _canPickUpLayer)) return;

            


            _currentColliderObject = hit.collider;
            _currentRigidbodyObject = _currentColliderObject.gameObject.GetComponent<Rigidbody>();
            _currentColliderObject.gameObject.GetComponent<Collider>().enabled = false;
            Physics.IgnoreCollision(_playerCollider, _currentColliderObject);


            _currentRigidbodyObject.useGravity = false;
            _currentRigidbodyObject.collisionDetectionMode = CollisionDetectionMode.Continuous;


            //_HoldingObject = true;

            _cancellationToken = new();
            Follow();
        }

        private void Drop(bool isThrow = false)
        {
            if (_currentRigidbodyObject == null) return;
            
            _cancellationToken.Cancel();
            
            _currentRigidbodyObject.linearVelocity = Vector3.zero;
            _currentRigidbodyObject.collisionDetectionMode = CollisionDetectionMode.Discrete;
            _currentRigidbodyObject.useGravity = true;
            //_currentColliderObject.transform.SetParent(null);
            //_currentColliderObject.gameObject.GetComponent<BoxCollider>().enabled = true;
            _currentColliderObject.gameObject.GetComponent<Collider>().enabled = true;

            if (isThrow) _currentRigidbodyObject.AddForce(transform.parent.forward * _trowForce, ForceMode.Impulse);

            _HoldingObject = false;

            Physics.IgnoreCollision(_playerCollider, _currentColliderObject, false);
            _currentRigidbodyObject = null;
        }

        private async void Follow()
        {
            while (!_cancellationToken.IsCancellationRequested)
            {
                _currentRigidbodyObject.linearVelocity = (transform.position - _currentRigidbodyObject.transform.position) * _followSpeed;
    
                await Task.Delay(20, _cancellationToken.Token);

            }
        }

        private void Drop(InputAction.CallbackContext _) => Drop();
        private void Throw(InputAction.CallbackContext _) => Drop(true);

        private void OnDestroy()
        {
            _inputSystem.Player.PickUp.performed -= PickUp;
            _inputSystem.Player.PickUp.canceled -= Drop;
            _inputSystem.Player.Trow.performed -= Throw;
        }
    }
}