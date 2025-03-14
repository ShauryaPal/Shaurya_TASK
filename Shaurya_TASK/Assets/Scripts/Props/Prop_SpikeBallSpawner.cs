using UnityEngine;

public class Prop_SpikeBallSpawner : MonoBehaviour
{
    [SerializeField] private Rigidbody2D spikeBallPrefab;
    [SerializeField] private Transform spikeBallSpawnPoint;
    [SerializeField] private float minForceMagnitude;
    [SerializeField] private float maxForceMagnitude;
    [SerializeField] private float spawnInterval = 4f;

    private void Start()
    {
        InvokeRepeating(nameof(SpawnSpikeBall), 0f,spawnInterval);
    }

    private void SpawnSpikeBall()
    {
        Instantiate(spikeBallPrefab, spikeBallSpawnPoint.position, Quaternion.identity).GetComponent<Rigidbody2D>().AddForce(spikeBallSpawnPoint.forward * Random.Range(minForceMagnitude, maxForceMagnitude) * 1000, ForceMode2D.Force);
    }

}
