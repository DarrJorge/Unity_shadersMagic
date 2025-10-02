using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    [SerializeField] private TriggerLevelZone _triggerLevelZone;
    [SerializeField] private GameObject[] zones;
    [SerializeField] private PointerFollower _pointerFollower;
    [SerializeField] private TMPCharTyping _tipMessage;
    
    private int _currentZoneIndex = -1;
    private Camera _camera;

    private float[] _cameraPositionsZ = new[] { -12.74f, 5.32f };

    private string _firstMessage = "Welcome! You are playing as Lovikko, and your mission is to rescue your friend, the wizard Lucius.";
    private string _twoMessage = "Before he vanished, your friend managed to leave you his artifact — the hidden item scanner. Use it well.";
    
    private void Start()
    {
        _camera = Camera.main;
        
        if (zones == null || zones.Length == 0)
        {
            Debug.LogError("No zones found");
            return;
        }

        _currentZoneIndex = 0;
        
        var pos = _camera.transform.position;
        _camera.transform.position = new Vector3(pos.x, pos.y, _cameraPositionsZ[_currentZoneIndex]);
        
        for (int i = 0; i < zones.Length; i++)
        {
            zones[i].SetActive(i == _currentZoneIndex);
        }
        
        _tipMessage.ShowText(_firstMessage);
        _tipMessage.ShowText(_twoMessage);
    }

    private void OnEnable()
    {
        _triggerLevelZone.OnCharacterEntered += OnEnterToZone;
    }

    private void OnDisable()
    {
        _triggerLevelZone.OnCharacterEntered -= OnEnterToZone;
    }
    
    private void OnEnterToZone()
    {
        if (_currentZoneIndex == -1) return;

        if (zones.Length <= (_currentZoneIndex + 1))
        {
            return;
        }
        zones[_currentZoneIndex].SetActive(false);
        zones[++_currentZoneIndex].SetActive(true);
        
        var pos = _camera.transform.position;
        _camera.transform.position = new Vector3(pos.x, pos.y, _cameraPositionsZ[_currentZoneIndex]);

        if (_pointerFollower != null)
        {
            _pointerFollower.gameObject.SetActive(false);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPaused = true;
#endif
        }
    }
}
