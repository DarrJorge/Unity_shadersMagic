using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TMPCharTyping : MonoBehaviour
{
    [SerializeField] private float _charsPerSecond = 30f;
    [SerializeField] private TextMeshProUGUI _displayText;
    [SerializeField] private float _delayBetweenMessages = 0.5f;
    
    private Coroutine _typingCoroutine;
    private Queue<string> _messages = new Queue<string>();
    private bool _isTyping = false;
    
    public void ShowText(string message)
    {
        _messages.Enqueue(message);

        if (!_isTyping)
        {
            _typingCoroutine = StartCoroutine(ProcessQueue());
        }
    }

    private IEnumerator ProcessQueue()
    {
        _isTyping = true;

        while (_messages.Count > 0)
        {
            string nextMessage = _messages.Dequeue();
            yield return StartCoroutine(TypeText(nextMessage));
        }
    }

    private IEnumerator TypeText(string message)
    {
        _displayText.SetText(string.Empty);
        float delay = 1f / _charsPerSecond;

        foreach (char c in message)
        {
            _displayText.SetText(_displayText.text + c);
            yield return new WaitForSeconds(delay);
        }

        yield return new WaitForSeconds(_delayBetweenMessages);
    }
}
