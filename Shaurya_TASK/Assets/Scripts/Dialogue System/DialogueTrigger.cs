using System;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    [SerializeField] private string dialogue;
    private bool shown = false;
    
    private void OnTriggerEnter(Collider other)
    {
        if(shown) return;
        if (!other.CompareTag("Player")) return;
        
        DataReferences.Instance.dialogueSystem_UIManager.ShowDialogueUI(dialogue);
        shown = true;
    }
}
