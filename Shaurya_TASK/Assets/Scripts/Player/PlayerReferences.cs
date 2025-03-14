using TarodevController;
using UnityEngine;

public class PlayerReferences : MonoBehaviour
{
    public static PlayerReferences Instance;

    public PlayerAnimator animator;
    public PlayerManager_Inventory inventory;
    public PlayerManager_UI ui;
    public PlayerManager_ItemInteracter itemInteracter;
    public PlayerManager_Health health;
    
    private void Awake()
    {
        Instance = this;
    }
}
