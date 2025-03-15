using UnityEngine;

public class Void : MonoBehaviour
{
    private void Update()
    {
        if (DataReferences.Instance.playerTransform.position.y < -25)
        {
            PlayerReferences.Instance.health.DamagePlayer(100);
        }
    }
}
