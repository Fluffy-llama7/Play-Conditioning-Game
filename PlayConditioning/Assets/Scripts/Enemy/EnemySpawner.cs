using NUnit.Framework.Constraints;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject basicPrefab;
    [SerializeField]
    private GameObject tankPrefab;

    [SerializeField]
    private float basicInterval = 3.5f;
    [SerializeField]
    private float tankInterval = 4.5f;


    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(spawnEnemy(basicInterval, basicPrefab));
        StartCoroutine(spawnEnemy(tankInterval, tankPrefab));
    }

    private IEnumerator spawnEnemy(float interval, GameObject enemy)
    {
        yield return new WaitForSeconds(interval);
        // Testing parameters for location of enemy spawns
        GameObject newEnemy = Instantiate(enemy, new Vector3(Random.Range(-5f, 5f), Random.Range(-6f, 6f), 0), Quaternion.identity);
        StartCoroutine(spawnEnemy(interval, enemy));
    }
}
