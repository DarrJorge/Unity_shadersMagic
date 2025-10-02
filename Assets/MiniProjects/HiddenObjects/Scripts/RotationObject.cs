using System;
using UnityEngine;

public class RotationObject : MonoBehaviour
{
    public event Action<bool> OnTriggerEntered;
    private void OnTriggerEnter(Collider other)
    {
        OnTriggerEntered?.Invoke(true);
    }

    private void OnTriggerExit(Collider other)
    {
        OnTriggerEntered?.Invoke(false);
    }
}
