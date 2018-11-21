using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {
	private bool hasSpawned;
	private Transform target;
	private float nextSpawn;

	public GameObject[] enemiesToSpawn;
	public bool limitSpawning;
	public int spawnRate;
	public int minSpawnAmount;
	public int maxSpawnAmount;
	public float spawnRange;
	public float detectionRange;

	void Start(){
		target = GameObject.FindGameObjectWithTag ("Player").transform;
	}

	void Update(){
		float distance = Vector3.Distance (transform.position, target.position);
		if (distance <= detectionRange && !hasSpawned) {
			if (Time.time > nextSpawn ) {
				nextSpawn = Time.time + spawnRate;
				SpawnEnemies ();
			}
		}
	}

	void SpawnEnemies(){
		if (limitSpawning) {
			hasSpawned = true;
		}
		int spawnAmount = Random.Range (minSpawnAmount, maxSpawnAmount+1);
		for (int i = 0; i < spawnAmount; i++) {
			float xSpawnPos = transform.position.x + Random.Range (-spawnRange, spawnRange);
			float zSpawnPos = transform.position.z + Random.Range (-spawnRange, spawnRange);

			Vector3 spawnPoint = new Vector3 (xSpawnPos, 0, zSpawnPos);
			GameObject newEnemy = (GameObject)Instantiate (enemiesToSpawn[Random.Range(0,enemiesToSpawn.Length)], spawnPoint, Quaternion.identity);
		}
	}
    /*
	void OnDrawGizmos(){
		Handles.color = Color.blue;
		Handles.DrawWireArc (transform.position+new Vector3(0,0.2f,0), transform.up, transform.right, 360, detectionRange);
		Handles.color = Color.green;
		Handles.DrawWireArc (transform.position+new Vector3(0,0.2f,0), transform.up, transform.right, 360, spawnRange);
	}
    */
}
