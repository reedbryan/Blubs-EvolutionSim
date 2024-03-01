using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodSpawing : MonoBehaviour
{
    public GameObject food;
    public Counter counter;
    public GameManagment gameManagment;
    public EnvironmentSetUpUI environment;

    public int amountSpawnedOverTime;
    public float SpawnTimerStart;
    public float SpawnTimer;
    [SerializeField] private float spawningRng;

    public bool initialSpawnReady;

    // Start is called before the first frame update
    void Start()
    {
        SpawnTimer = SpawnTimerStart;
        initialSpawnReady = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (gameManagment.inSpeciesSetUp || gameManagment.inEnvironmentSetUp)
        {
            //Dont run script
        }
        else
        {
            if (initialSpawnReady)
            {
                InitialSpawn();
                initialSpawnReady = false;
            }
            SpawnOverTime();
        }

        //Set to half the the full diamiter of the base environment size -1 so no food on the walls
        spawningRng = (environment.environmentSize.x / 2) - 1;
    }

    void InitialSpawn()
    {
        for (int i = 0; i < environment.initialFoodSpawned; i++)
        {
            SpawnFood();
        }
    }

    void SpawnOverTime()
    {
        SpawnTimer -= 1 * Time.deltaTime;
        if (SpawnTimer <= 0)
        {
            for (int i = 0; i < environment.gradualFoodSpawned; i++)
            {
                SpawnFood();
                SpawnTimer = SpawnTimerStart;
            }
        }
    }

    void SpawnFood()
    {
        GameObject newFood = Instantiate(food, new Vector3(Random.Range(spawningRng * -1, spawningRng), 1,
                                      Random.Range(spawningRng * -1, spawningRng)),
                  new Quaternion(0, 0, 0, 0));
        counter.AddFood();
        counter.allEntities.Add(newFood);
    }
}
