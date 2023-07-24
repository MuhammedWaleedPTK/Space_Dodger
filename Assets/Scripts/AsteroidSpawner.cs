using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class AsteroidSpawner : MonoBehaviour
{
   
    public float spawnInterval = 2f;
    public float asteroidSpeed = 5f;
    public float spawnYOffset = 3f;

    private float screenHeight;

    int count = 1;


    public static AsteroidSpawner instance;

    private List<GameObject> pooledObjects = new List<GameObject>();
    private int amountToPool = 20;
    [SerializeField] private GameObject[] Asteroids;



    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    void Start()
    {
        for (int i = 0; i < amountToPool; i++)
        {
            GameObject obj = Instantiate(Asteroids[Random.Range(0,3)]);
            obj.SetActive(false);
            obj.transform.SetParent(transform);
            pooledObjects.Add(obj);
        }
        screenHeight = Camera.main.orthographicSize;

        InvokeRepeating("SpawnAsteroid", 0f, spawnInterval);
    }
    public GameObject GetPooledObject()
    {
        for (int i = 0; i < pooledObjects.Count; i++)
        {
            if (!pooledObjects[i].activeInHierarchy)
            {
                return pooledObjects[i];
            }
        }
        return null;
    }








    void SpawnAsteroid()
    {
        float randomYPosition = Random.Range(-screenHeight , screenHeight );

        Vector3 spawnPosition = new Vector3(screenHeight + spawnYOffset, randomYPosition, 0f);
        GameObject obj=GetPooledObject();
        if(obj != null)
        {
            obj.SetActive(true);
           
            count++;
            obj.transform.position=spawnPosition;
        }
        Rigidbody2D asteroidRigidbody = obj.GetComponent<Rigidbody2D>();
        asteroidRigidbody.velocity = Vector2.left * asteroidSpeed;


    }
}
