using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpeciesSetUpUI : MonoBehaviour
{
    public GameObject ThisMenu;
    public GameManagment gameManagment;

    // Gene Mut Per Sliders
    public Slider speedMutChanceSlider;
    public Slider sizeMutChanceSlider;
    public Slider targetRngMutChanceSlider;
    public Slider energyDepletionMutChanceSlider;
    public Slider nourishmentMutChanceSlider;
    public Slider reproductionPointMutChanceSlider;
    public Slider reprodictionCostMutChanceSlider;

    public float speedMutationPer;
    public float energyDepletionMutationPer;
    public float reproductionCostMutationPer;
    public float reproductionPointMutationPer;
    public float sizeMutationPer;
    public float targetRangeMutationPer;
    public float nourishmentMutationPer;

    // SandBox Mode
    public bool sandBoxMode;
    private float[] preSandBoxEvoPoints;

    // All same mode
    public bool allSameMode;
    public float allSameValue;

    [SerializeField] public float evoPoints;
    [SerializeField] private float startingEvoPoints;
    [SerializeField] private float[] sliderEvoPointsUsed;
    public Text TotalEvoPointsUI;

    // Start is called before the first frame update
    void Start()
    {
        speedMutationPer = 1;
        energyDepletionMutationPer = 1;
        reproductionCostMutationPer = 1;
        reproductionPointMutationPer = 1;
        sizeMutationPer = 1;
        targetRangeMutationPer = 1;
        nourishmentMutationPer = 1;

        ThisMenu.SetActive(true);

        preSandBoxEvoPoints = new float[7];
        sliderEvoPointsUsed = new float[7];
    }

    // Update is called once per frame
    void Update()
    {
        if (!gameManagment.inSpeciesSetUp)
        {
            ThisMenu.SetActive(false);
        }
        else
        {
            ThisMenu.SetActive(true);
        }
        UpdateEvoPoints();
    }

    public void UpdateSlider(Slider slider)
    {
        // INTRUCTIONS TO CP LINES BELLOW:
        // Get text component attached to the inputed Slider
        // Write specific text stating sliders current value and what it coralates to in the Sliders text component
        // Set in the gene in question's Chance of mutation stat to the value of the slider
        // Input the sliders value into an array to keep track of them all to mesure total evo points used or in case of sandbox mode activation

        if (slider.name == "SpeedMCSliderUI")
        {
            Text text = slider.GetComponentInChildren<Text>();
            text.text = "Speed Mutation Chance: " + slider.value + "%";
            speedMutationPer = slider.value;
            sliderEvoPointsUsed[0] = slider.value - 1;
        }
        if (slider.name == "SizeMCSliderUI")
        {
            Text text = slider.GetComponentInChildren<Text>();
            text.text = "Size Mutation Chance: " + slider.value + "%";
            sizeMutationPer = slider.value;
            sliderEvoPointsUsed[1] = slider.value - 1;
        }
        if (slider.name == "TargetRangeMCSliderUI")
        {
            Text text = slider.GetComponentInChildren<Text>();
            text.text = "TargetRange Mutation Chance: " + slider.value + "%";
            targetRangeMutationPer = slider.value;
            sliderEvoPointsUsed[2] = slider.value - 1;
        }
        if (slider.name == "NourishmentMCSliderUI")
        {
            Text text = slider.GetComponentInChildren<Text>();
            text.text = "Nourishment Mutation Chance: " + slider.value + "%";
            nourishmentMutationPer = slider.value;
            sliderEvoPointsUsed[3] = slider.value - 1;
        }
        if (slider.name == "EnergyDepletionMCSliderUI")
        {
            Text text = slider.GetComponentInChildren<Text>();
            text.text = "EnergyDepletion Mutation Chance: " + slider.value + "%";
            energyDepletionMutationPer = slider.value;
            sliderEvoPointsUsed[4] = slider.value - 1;
        }
        if (slider.name == "ReproductionCostMCSliderUI")
        {
            Text text = slider.GetComponentInChildren<Text>();
            text.text = "ReproductionCost Mutation Chance: " + slider.value + "%";
            reproductionCostMutationPer = slider.value;
            sliderEvoPointsUsed[5] = slider.value - 1;
        }
        if (slider.name == "ReproductionPointMCSliderUI")
        {
            Text text = slider.GetComponentInChildren<Text>();
            text.text = "ReproductionPoint Mutation Chance: " + slider.value + "%";
            reproductionPointMutationPer = slider.value;
            sliderEvoPointsUsed[6] = slider.value - 1;
        }

        // If allSameMode is activated
        if (allSameMode)
        {
            allSameValue = slider.value;

            speedMutChanceSlider.value = allSameValue;
            sizeMutChanceSlider.value = allSameValue;
            targetRngMutChanceSlider.value = allSameValue;
            nourishmentMutChanceSlider.value = allSameValue;
            energyDepletionMutChanceSlider.value = allSameValue;
            reprodictionCostMutChanceSlider.value = allSameValue;
            reproductionPointMutChanceSlider.value = allSameValue;
        }
    }

    void UpdateEvoPoints()
    {
        if (sandBoxMode)
        {
            // If SandBox mode is activated
            // Give player ∞ evo points
            evoPoints = 100000f;
            // Update evo points display text to "NO LIMIT"
            TotalEvoPointsUI.text = "Total Evolution Points: NO LIMIT";
        }
        else
        {
            // If SandBox mode is not activated
            // Take away slider values from evo points
            evoPoints = startingEvoPoints - sliderEvoPointsUsed[0]
                              - sliderEvoPointsUsed[1]
                              - sliderEvoPointsUsed[2]
                              - sliderEvoPointsUsed[3]
                              - sliderEvoPointsUsed[4]
                              - sliderEvoPointsUsed[5]
                              - sliderEvoPointsUsed[6];

            // Update evo points display text
            TotalEvoPointsUI.text = "Total Evolution Points: " + evoPoints;


            // old
            /*
            if (allSameMode)
            {
                evoPoints = startingEvoPoints - allSameValue * 7;
            }
            else
            {
                // If SandBox mode is not activated
                // Take away slider values from evo points
                evoPoints = startingEvoPoints - sliderEvoPointsUsed[0]
                                  - sliderEvoPointsUsed[1]
                                  - sliderEvoPointsUsed[2]
                                  - sliderEvoPointsUsed[3]
                                  - sliderEvoPointsUsed[4]
                                  - sliderEvoPointsUsed[5]
                                  - sliderEvoPointsUsed[6];

                // Update evo points display text
                TotalEvoPointsUI.text = "Total Evolution Points: " + evoPoints;
            }
            */
        }
    }


    /// <summary>
    /// This func is run whenever the Sand Box Mode check box is toggled
    /// "toggle" is the gameOb itself and the boolian that detects wether or not its checked is called "isOn"
    /// if isOn then Sand Box Mode is activated
    /// </summary>
    /// <param name="toggle"></param>
    public void SandBoxToggle(Toggle toggle)
    {
        // If box is checked
        if (toggle.isOn)
        {
            // Save current evo points for reference so when sand box mode is turned off slidered will revert to og values
            for (int i = 0; i < preSandBoxEvoPoints.Length; i++)
            {
                // +1 to sliderEvoPointsUsed to cancel out the -1 prev put on it
                preSandBoxEvoPoints[i] = sliderEvoPointsUsed[i] + 1;
            }

            // Turn on sand box mode
            sandBoxMode = true;
        }
        // If box is empty
        if (!toggle.isOn)
        {
            // Turn off sand box mode
            sandBoxMode = false;

            // Revert all sliders to what they were pre Sand Box Mode 
            speedMutChanceSlider.value = preSandBoxEvoPoints[0];
            sizeMutChanceSlider.value = preSandBoxEvoPoints[1];
            targetRngMutChanceSlider.value = preSandBoxEvoPoints[2];
            nourishmentMutChanceSlider.value = preSandBoxEvoPoints[3];
            energyDepletionMutChanceSlider.value = preSandBoxEvoPoints[4];
            reprodictionCostMutChanceSlider.value = preSandBoxEvoPoints[5];
            reproductionPointMutChanceSlider.value = preSandBoxEvoPoints[6];
        }
    }

    public void allSameToggle(Toggle toggle)
    {
        if (toggle.isOn)
        {
            allSameMode = true;
        }
        if (!toggle.isOn)
        {
            allSameMode = false;
        }
    }
}
