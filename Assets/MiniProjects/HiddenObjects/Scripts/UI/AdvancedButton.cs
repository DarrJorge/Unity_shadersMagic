using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class AdvancedButton : Button
{
    [SerializeField] private RectTransform _icon;

    public void AddListener(UnityAction listener)
    {
        onClick.AddListener(listener);
        onClick.AddListener(RotateIcon);
    }

    public void RemoveListener(UnityAction listener)
    {
        onClick.RemoveListener(listener);
        onClick.RemoveListener(RotateIcon);
    }

    private void RotateIcon()
    {
        _icon.rotation = Quaternion.Euler(Vector3.zero);
    }
}
