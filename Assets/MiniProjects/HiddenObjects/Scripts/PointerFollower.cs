using UnityEngine;

public class PointerFollower : MonoBehaviour
{
    [SerializeField, Range(0,10)] 
    private float _distanceToCamera = 1;

    [SerializeField] private Collider _playerCapsule;
    [SerializeField] private LayerMask _wallMask;
    
    private Camera _camera;

    private void Awake()
    {
        _camera = Camera.main;
    }

    private void Update()
    {
        if (_camera == null) return;
        
        Vector3 pointerPosition = Input.mousePosition;
        pointerPosition.z = _distanceToCamera;
        transform.position = _camera.ScreenToWorldPoint(pointerPosition);
        
        Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit, 1000f, _wallMask))
        {
            if (hit.collider.TryGetComponent(out PassageGate gate))
            {
                gate.Open(_playerCapsule);
            }
        }
    }
}
