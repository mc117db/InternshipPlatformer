using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour {

    public GameObject EnemyPrefab;
    public float spawnRange = 20f;
    public float spawnFrequency = 20f;
	// Use this for initialization
	void Start () {
        if(EnemyPrefab)
        {
            InvokeRepeating("Spawn", spawnFrequency, spawnFrequency);
        }
    }
	void Spawn()
    {
        GameObjectUtil.Instantiate(EnemyPrefab, transform.position + Vector3.right * spawnRange * (-1 * Random.value * Random.Range(-1,1)));
    }
    // Update is called once per frame
    void Update () {
	
	}
}
