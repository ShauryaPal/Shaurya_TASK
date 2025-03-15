using TarodevController;
using UnityEngine;
using Random = UnityEngine.Random;

public class PlayerManager_Sounds : MonoBehaviour
{
    [SerializeField] private AudioSource walkingSound;
    [SerializeField] private AudioClip[] hurtSound;
    [SerializeField] private AudioClip[] attackSound;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    
    private Transform player;
    private Vector3 oldPlayerPosition;
    
    private void Start()
    {
        PlayerReferences.Instance.health.OnDamageTaken += () => DataReferences.Instance.soundManager.PlaySfx(hurtSound[Random.Range(0, hurtSound.Length)]);
        player = PlayerReferences.Instance.playerTransform;
    }
    

    private void Update()
    {
        var colliders = Physics2D.OverlapCircleAll(groundCheck.position, 0.1f, groundLayer);
        walkingSound.mute = !(colliders.Length > 1 && oldPlayerPosition != player.position && PlayerReferences.Instance.controller.FrameInput.x!=0);
        oldPlayerPosition = player.position;
    }
}
