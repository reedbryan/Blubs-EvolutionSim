using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SideMenuUI : MonoBehaviour
{
    //Scripts
    public BlubGenes genes;
    public Counter counter;
    public GameManagment gameManagment;

    //GameObjects
    public GameObject sideMenu;
    public Text speedAvUI;
    public Text sizeAvUI;
    public Text nourishmentAvUI;
    public Text reproductionCostAvUI;
    public Text reproductionPointAvUI;
    public Text targetRangeAvUI;
    public Text energyDepletionAvUI;
    public Text totalBlubCountUI;
    public Text totalFoodCountUI;

    public Slider timeScaleSliderUI;

    //Vars
    public bool sideMenuShowing;

    private void Start()
    {
        CloseSideMenu();
        sideMenuShowing = false;

        timeScaleSliderUI.value = 1f;

        counter = GetComponent<Counter>();
    }

    // Update is called once per frame
    void Update()
    {
        if (gameManagment.inSpeciesSetUp || gameManagment.inEnvironmentSetUp)
        {
            //Don't look for input
        }
        else
        {
            CheckForUI();
            UpdateSideBar();
        }
    }

    void CheckForUI()
    { 
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (sideMenuShowing)
            {
                CloseSideMenu();
            }
            else
            {
                ShowSideMenu();
            }
        }
    }

    //SideMenu Show/UnShow
    void CloseSideMenu()
    {
        sideMenu.SetActive(false);
        sideMenuShowing = false;
    }
    void ShowSideMenu()
    {
        sideMenu.SetActive(true);
        sideMenuShowing = true;
    }

    //SideMenu Contenents- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - 

    /// <summary>
    /// Shows the average gene values for players population of blubs
    /// Called from update method
    /// </summary>
    void UpdateSideBar()
    {
        speedAvUI.text = "Average Speed: " + counter.avSpeed;
        sizeAvUI.text = "Average Size: " + counter.avSize;
        targetRangeAvUI.text = "Average Targeting Range: " + counter.avTargetRange;
        reproductionCostAvUI.text = "Average Reproduction Cost: " + counter.avrReproductionCost;
        reproductionPointAvUI.text = "Average Reproduciton Point: " + counter.avReproductionPoint;
        nourishmentAvUI.text = "Average Nourishment: " + counter.avNourishment;
        energyDepletionAvUI.text = "Average Energy Depletion: " + counter.avEnergyDepletion;
        totalBlubCountUI.text = "Total Blub Count: " + counter.blubCount;
        totalFoodCountUI.text = "Total Food Count: " + counter.foodCount;
    }

    /// <summary>
    /// Called when the time multiplier buttons are pressed/changed in game
    /// Will derectly effect Time.timeScale (found in gameManagment script)
    /// </summary>
    public void timeScaleSlider(Slider slider)
    {
        gameManagment.currentTimeSpeed = slider.value;
        Text sliderText = slider.GetComponentInChildren<Text>();
        sliderText.text = "Time Scale: " + slider.value;
    }
}