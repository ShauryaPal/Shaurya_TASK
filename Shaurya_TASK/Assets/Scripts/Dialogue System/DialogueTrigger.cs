using System;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    [SerializeField] private string dialogue;
    [SerializeField] private string hint;
    [SerializeField] private Transform cameraFocus;
    
    private bool shown = false;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(shown) return;
        if (!other.CompareTag("Player")) return;
    
        if(cameraFocus!=null)
            DataReferences.Instance.cinemachineVirtualCamera.Follow = cameraFocus;
        
        DataReferences.Instance.dialogueSystem_UIManager.ShowDialogueUI(dialogue, hint);
        shown = true;
    }
}
