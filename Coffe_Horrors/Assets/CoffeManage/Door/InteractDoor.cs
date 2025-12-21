using System.Threading;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEditor.Progress;
public class InteractDoor : MonoBehaviour
{
    private InputSystem _inputSystem;
    [SerializeField] private LayerMask _canDoorLayer;

    private Collider _doorCollider;

    void Start()
    {
        _inputSystem = new();
        _inputSystem.Enable();

        _inputSystem.Player.DoorInterakt.performed += Open_door;
    }

    // Update is called once per frame
    private void Open_door(InputAction.CallbackContext _)
    {
        if (!Physics.Raycast(transform.parent.position, transform.parent.forward, out RaycastHit hit, 5, _canDoorLayer)) return;
        _doorCollider = hit.collider;

        Animator _anim = _doorCollider.GetComponent<Animator>();
        _anim.SetBool("DoorOC", !_anim.GetBool("DoorOC"));



    }

    private void OnDestroy()
    {
        _inputSystem.Player.DoorInterakt.performed -= Open_door;
    }
}
