using System;
using UnityEngine;
using UnityEngine.Events;

public class FallTrigger : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public UnityEvent OnPinFall = new();
    public bool isPinFallen = false;

    private void OnTriggerEnter(Collider triggeredObject){
        if (triggeredObject.CompareTag("Ground") && !isPinFallen){
            isPinFallen = true;
            OnPinFall?.Invoke();
            Debug.Log($"{gameObject.name} is fallen");
        }
    }
}
