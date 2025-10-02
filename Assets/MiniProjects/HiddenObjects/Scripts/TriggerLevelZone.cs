using System;
using UnityEngine;

[RequireComponent(typeof(TriggerObserver))]
public class TriggerLevelZone : MonoBehaviour
{
    public event Action OnCharacterEntered;

    private TriggerObserver _observer;
    
    private void OnEnable()
    {
        _observer = GetComponent<TriggerObserver>();
        _observer.OnEntered += HandleEnter;
    }

    private void OnDisable()
    {
        _observer.OnEntered -= HandleEnter;
    }

    private void HandleEnter(Collider other)
    {
        OnCharacterEntered?.Invoke();
    }
}
