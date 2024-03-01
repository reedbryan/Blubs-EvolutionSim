using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnvironmentSetUpUI : MonoBehaviour
{
    public GameObject ThisMenu;
    public GameObject environment;
    public Slider EnvironmentSizeSlider;
    public GameManagment gameManagment;

    public int initialBlubsSpawned;
    public int initialFoodSpawned;
    public int gradualBlubsSpawned;
    public int gradualFoodSpawned;

    public Vector3 environmentSize;
    private Vector3 baseEnvironmentSize;

    public bool visited;

    // Start is called before the first frame update
    void Start()
    {
        // Set environement values to there defaults
        initialBlubsSpawned = 10;
        initialFoodSpawned = 25;
        gradualBlubsSpawned = 0;
        gradualFoodSpawned = 3;

        //Base size of environment (will prob make smaller in the future)
        baseEnvironmentSize = new Vector3(100, 1, 100);
        environmentSize = baseEnvironmentSize;
    }

    // Update is called once per frame
    void Update()
    {
        if (!gameManagment.inEnvironmentSetUp)
        {
            ThisMenu.SetActive(false);
        }
        else
        {
            ThisMenu.SetActive(true);
        }
    }

    public void GradualFoodSpawn(InputField inputField)
    {
        gradualFoodSpawned = int.Parse(inputField.text);
    }

    public void InitialFoodSpawn(InputField inputField)
    {
        initialFoodSpawned = int.Parse(inputField.text);
    }

    public void GradualBlubSpawn(InputField inputField)
    {
        gradualBlubsSpawned = int.Parse(inputField.text);
    }

    public void InitialBlubSpawn(InputField inputField)
    {
        initialBlubsSpawned = int.Parse(inputField.text);
    }

    public void EnvironmentSize(Slider slider)//Multiplying the scale of the transform
    {
        Text text = slider.GetComponentInChildren<Text>();

        environment.transform.localScale = new Vector3(slider.value, 1, slider.value);
        Vector3 x = environment.transform.localScale;
        environmentSize = Vector3.Scale(baseEnvironmentSize, x);

        text.text = "Environment Size: " + environmentSize + " by " + environmentSize;
    }
}
