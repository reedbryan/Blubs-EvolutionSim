using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlubSpawning : MonoBehaviour
{
    public GameObject blub;
    public EnvironmentSetUpUI environmentSetUp;

    public GameManagment gameManagment;
    public EnvironmentSetUpUI environment;
    Counter counter;

    private float spawnTimer;
    public float spawnTimerStart;

    [SerializeField] private float spawningRng;

    public bool initialSpawnReady;

    // Start is called before the first frame update
    void Start()
    {
        initialSpawnReady = true;
        counter = GetComponent<Counter>();
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
            // Spawn blubs over time if it has a value of 0 it will simply spawn nothing
            SpawnOverTime();
        }

        //Set to a third the the full diamiter of the base environment size
        spawningRng = (environment.environmentSize.x / 3);
    }

    void InitialSpawn()
    {
        for (int i = 0; i < environmentSetUp.initialBlubsSpawned; i++)
        {
            SpawnBlub();
        }
    }

    void SpawnOverTime()
    {
        spawnTimer -= 1 * Time.deltaTime;
        if (spawnTimer <= 0)
        {
            for (int i = 0; i < environment.gradualBlubsSpawned; i++)
            {
                SpawnBlub();
                spawnTimer = spawnTimerStart;
            }
        }
    }

    void SpawnBlub()
    {
        counter.AddBlub();
        GameObject newBlub = Instantiate(blub, new Vector3(Random.Range(spawningRng * -1, spawningRng), 1.5f,
                  Random.Range(spawningRng * -1, spawningRng)),
                  new Quaternion(0, 0, 0, 0));

        BlubGenes newBlubGenes = newBlub.GetComponent<BlubGenes>();
        newBlubGenes.startAsNew();
        counter.allEntities.Add(newBlub);
    }
}
