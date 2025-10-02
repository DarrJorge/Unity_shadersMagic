using System;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider))]
public class TriggerObserver : MonoBehaviour
{
    [Header("Filter")] 
    [SerializeField] private LayerMask _layers = ~0;
    [SerializeField] private string _requiredTag = "";
    
    [Header("Unity Events")]
    public UnityEvent<Collider> OnEnterUnity;
    public UnityEvent<Collider> OnExitUnity;
    
    // C# events
    public event Action<Collider> OnEntered;
    public event Action<Collider> OnExited;

    private void Reset()
    {
        var col = GetComponent<Collider>();
        col.isTrigger = true;
    }

    private bool PassesFilter(Collider other)
    {
        if (((1 << other.gameObject.layer) & _layers) == 0)
            return false;

        if (!string.IsNullOrEmpty(_requiredTag) && !other.CompareTag(_requiredTag))
            return false;

        return true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!PassesFilter(other)) return;
        OnEntered?.Invoke(other);
        OnEnterUnity?.Invoke(other);
    }

    private void OnTriggerExit(Collider other)
    {
        if (!PassesFilter(other)) return;
        OnExited?.Invoke(other);
        OnExitUnity?.Invoke(other);
    }
}
