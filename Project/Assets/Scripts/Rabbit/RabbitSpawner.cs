using UnityEngine;
using UnityEngine.AI;

public class RabbitSpawner : MonoBehaviour
{


    [SerializeField] private GameObject m_RabbitPrefab; //ウサギのプレハブ
    [SerializeField] private int rabbitCount = 30; //出現数
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        for(int i = 0; i < rabbitCount; i++)
        {
            SpawnRabbit();
        }
    }

   void SpawnRabbit()
    {
        Vector3 randomPos = new Vector3(Random.Range(-30f, 20f), 0f, Random.Range(-30f, 20f));
        NavMeshHit hit;
        if (NavMesh.SamplePosition(randomPos, out hit, 10.0f, NavMesh.AllAreas))
        {
            Instantiate(m_RabbitPrefab, hit.position, Quaternion.identity);
        }
        else
        {
            Debug.LogWarning("NavMesh 上に Rabbit をスポーンできませんでした。");
        }

    }
}
