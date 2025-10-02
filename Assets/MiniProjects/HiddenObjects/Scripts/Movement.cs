using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] private float _moveSpeed = 8f;
    [SerializeField] private float _rotateSpeed = 10f;

    public void SetPosition(Vector3 position)
    {
        transform.position = position;
    }
    
    public void Move(Vector3 direction)
    {
        transform.position += direction * _moveSpeed * Time.deltaTime;
    }

    public void Rotate(Vector3 direction)
    {
        Quaternion rotation = Quaternion.LookRotation(direction, Vector3.up);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * _rotateSpeed);
    }
}
