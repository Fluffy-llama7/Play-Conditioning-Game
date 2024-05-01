using NUnit.Framework;
using NUnit.Framework.Constraints;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject basicPrefab;
    [SerializeField]
    private GameObject tankPrefab;
    [SerializeField]
    private GameObject chargePrefab;
    [SerializeField]
    private GameObject bossPrefab;

    [SerializeField]
    private float basicInterval = 3.5f;
    [SerializeField]
    private float tankInterval = 4.5f;
    [SerializeField]
    private float chargeInterval = 5.5f;
    [SerializeField]
    private float bossInterval = 10.0f;


    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Spawn(basicInterval, basicPrefab));
        StartCoroutine(Spawn(tankInterval, tankPrefab));
        StartCoroutine(Spawn(chargeInterval, chargePrefab));
        StartCoroutine(Spawn(bossInterval, bossPrefab));
    }

    private IEnumerator Spawn(float interval, GameObject enemy)
    {
        yield return new WaitForSeconds(interval);
        // Testing parameters for location of enemy spawns
        GameObject newEnemy = Instantiate(enemy, new Vector3(Random.Range(-5f, 5f), Random.Range(-6f, 6f), 0), Quaternion.identity);
        StartCoroutine(Spawn(interval, enemy));
    }
}
