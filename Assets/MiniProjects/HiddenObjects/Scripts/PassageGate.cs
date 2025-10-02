using System;
using UnityEngine;

[RequireComponent(typeof(TriggerObserver))]
public class PassageGate : MonoBehaviour
{
    [SerializeField] private Collider _passageCollider;
    
    private bool _isPlayerInside;
    
    private TriggerObserver _observer;
    private Collider _playerCollider; 
    
    private void Awake()
    {
        _observer = GetComponent<TriggerObserver>();
        _observer.OnEntered += HandleEnter;
        _observer.OnExited += HandleExit;
    }

    private void OnDestroy()
    {
        _observer.OnEntered -= HandleEnter;
        _observer.OnExited -= HandleExit;
    }

    private void Reset()
    {
        if (_passageCollider == null) _passageCollider = GetComponent<Collider>();
    }
    
    private void HandleEnter(Collider other)
    {
        _isPlayerInside = true;
        _playerCollider = other;
    }

    private void HandleExit(Collider other)
    {
        _isPlayerInside = false;

        if (_playerCollider != null)
        {
            Physics.IgnoreCollision(_playerCollider, _passageCollider, false);
            _playerCollider = null;            
        }
    }

    public void Open(Collider detectCollider)
    {
        if (detectCollider == null || !_isPlayerInside) return;
        
        Physics.IgnoreCollision(detectCollider, _passageCollider, true);
    }
}