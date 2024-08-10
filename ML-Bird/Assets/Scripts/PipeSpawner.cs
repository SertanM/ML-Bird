using UnityEngine;

public class PipeSpawner : MonoBehaviour
{
    [SerializeField] private GameObject pipePrefab;
    void OnEnable(){
        InvokeRepeating(nameof(SpawnPipe), 0f, 3f);
    }

    void SpawnPipe(){
        Instantiate(pipePrefab, new Vector3(12, Random.Range(-2f, 2f) ,0), Quaternion.identity);
    }
}
