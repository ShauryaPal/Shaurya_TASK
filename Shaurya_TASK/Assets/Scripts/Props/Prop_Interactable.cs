using UnityEngine;

public class Prop_Interactable : MonoBehaviour
{
    internal bool interacted { get; private set; }

    public void Interact()
    {
        if(interacted) return;
        Use();
    }
    
    protected virtual void Use()
    {
        interacted = true;
    }

    public void LoadSavedData(bool _interacted)
    {
        interacted = _interacted;
        OnLoadSavedData(_interacted);
    }

    protected virtual void OnLoadSavedData(bool _interacted)
    {
        
    }
}
