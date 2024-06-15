using UnityEngine;

public class GroundTile : MonoBehaviour
{
    GroundSpawner groundSpawner;
    [SerializeField] GameObject ObstaclePrefab;
    [SerializeField] GameObject LongObstaclePrefab;
    [SerializeField] GameObject CoinPrefab;
    [SerializeField] GameObject tracks;

    // Start is called before the first frame update
    void Start()
    {
        tracks = GameObject.FindGameObjectWithTag("Tracks");
        groundSpawner = GameObject.FindObjectOfType<GroundSpawner>();
        SpawnCoins();
    }

    private void OnTriggerExit(Collider collision)
    {
        groundSpawner.SpawnTile(true);
        Destroy(gameObject, 2);
    }

    public void SpawnObstacle()
    {
        //Spawn Long obstacle or Normal obstacle
        if (Random.Range(0,10) == 5) {
            Instantiate(LongObstaclePrefab, transform.GetChild(3).transform.position, Quaternion.identity, transform);
        }
        else {
            //Choose a random point to spawn obstacle
            int obstacleSpawnIndex = Random.Range(2, 5);
            Transform spawnPoint = transform.GetChild(obstacleSpawnIndex).transform;
            //Spawn the obstacle at that position
            Instantiate(ObstaclePrefab, spawnPoint.position, Quaternion.identity, transform);
        }
    }

    void SpawnCoins()
    {
        int coinToSpawn = 5;
        for (int i = 0; i < coinToSpawn; i++) {
            GameObject temp = Instantiate(CoinPrefab, transform);
            temp.transform.position = GetRandomPointInCollider(GetComponent<Collider>());
        }
    }

    Vector3 GetRandomPointInCollider (Collider collider)
    {
        int trackNo = Random.Range(0, 3);
        float PosX = tracks.transform.GetChild(trackNo).position.x;
        Vector3 point = new Vector3 (PosX, 1, Random.Range(collider.bounds.min.z, collider.bounds.max.z));
        return point;
    }
}
