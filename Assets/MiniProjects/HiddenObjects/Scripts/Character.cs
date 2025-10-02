using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Movement), typeof(Rigidbody))]
public class Character : MonoBehaviour
{
    [SerializeField] private Movement _hiddenMoveComponent;
    [SerializeField] private float _moveSpeed = 5f;
    [SerializeField] private float _rotSpeed = 10f;

    private Rigidbody _rb;
    private Vector3 _direction;
    
    private Movement _ownMovement;
    private void Start()
    {
        _ownMovement = GetComponent<Movement>();
        _rb = GetComponent<Rigidbody>();
        
        UpdatePosAndRotationForHidden();
    }
    
    private void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal"); // A/D, Left/Right
        float vertical = Input.GetAxisRaw("Vertical");     // W/S, Up/Down
        
        _direction = new Vector3(horizontal, 0f, vertical).normalized;
        
    }

    private void FixedUpdate()
    {
        if (_direction.sqrMagnitude > 0.001f)
        {
            Vector3 targetPos = _rb.position + _direction * _moveSpeed * Time.fixedDeltaTime;
            _rb.MovePosition(targetPos);
            UpdatePosAndRotationForHidden();

            Quaternion targetRot = Quaternion.LookRotation(_direction, Vector3.up);
            _rb.MoveRotation(Quaternion.Slerp(transform.rotation, targetRot,_rotSpeed  * Time.fixedDeltaTime));
            
            if (_hiddenMoveComponent != null)
            {
                _hiddenMoveComponent.Rotate(_direction);
            }
        }
    }

    private void Move(Vector3 direction)
    {
        _ownMovement.Move(direction);
        UpdatePosAndRotationForHidden();
    }

    private void Rotate(Vector3 direction)
    {
        _ownMovement.Rotate(direction);
        
        if (_hiddenMoveComponent != null)
        {
            _hiddenMoveComponent.Rotate(direction);
        }
    }

    private void UpdatePosAndRotationForHidden()
    {
        if (_hiddenMoveComponent != null)
        {
            _hiddenMoveComponent.SetPosition(transform.position);
        }
    }
}
