using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagment : MonoBehaviour
{
    public bool inSpeciesSetUp;
    public bool inEnvironmentSetUp;
    SpeciesSetUpUI speciesSetUp;
    EnvironmentSetUpUI environmentSetUp;
    BlubSpawning blubSpawning;
    FoodSpawing foodSpawing;
    Counter counter;
    public bool gameStarted;

    public float currentTimeSpeed;

    private void Awake()
    {
        speciesSetUp = GetComponent<SpeciesSetUpUI>();
        counter = GetComponent<Counter>();
        environmentSetUp = GetComponent<EnvironmentSetUpUI>();
        blubSpawning = GetComponent<BlubSpawning>();
        foodSpawing = GetComponent<FoodSpawing>();

        currentTimeSpeed = 1f;

        gameStarted = false;
        inSpeciesSetUp = true;
        inEnvironmentSetUp = false;
    }

    /// <summary>
    /// This update method is very important as it controles the current time.timescale of the game.
    /// </summary>
    void Update()
    {
        Time.timeScale = currentTimeSpeed;

        // Check for end game request
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            EndGame();
        }
    }

    public void ToEnvironmentSetUp()
    {
        inSpeciesSetUp = false;
        inEnvironmentSetUp = true;
    }

    public void ToSpeciesSetUp()
    {
        inEnvironmentSetUp = false;
        inSpeciesSetUp = true;
    }

    public void StartGame()
    {
        if (speciesSetUp.evoPoints >= 0)
        {
            inSpeciesSetUp = false;
            inEnvironmentSetUp = false;
            gameStarted = true;
        }
    }

    public void EndGame()
    {
        if (gameStarted)
        {
            foreach (var item in counter.allEntities)
            {
                Destroy(item);
            }
            gameStarted = false;
            inSpeciesSetUp = true;

            // Reset default environment settings
            environmentSetUp.initialBlubsSpawned = 10;
            environmentSetUp.initialFoodSpawned = 25;
            environmentSetUp.gradualBlubsSpawned = 0;
            environmentSetUp.gradualFoodSpawned = 3;

            foodSpawing.initialSpawnReady = true;
            blubSpawning.initialSpawnReady = true;
        }
    }
}
