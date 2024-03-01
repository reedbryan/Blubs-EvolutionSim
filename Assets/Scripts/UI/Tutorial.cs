using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tutorial : MonoBehaviour
{
    public Canvas canvas;
    public Text textObjectPrefab;
    public GameManagment gameManagment;
    public SideMenuUI sideMenu;

    public GameObject sideMenuTip;
    public GameObject movementTips;
    GameObject activeTip;

    float swapingTimerStart = 10; // in seconds
    float swapingTimer;
    int swapCount;
    bool showTips;

    public GameObject speciesSetUpInstructions1;
    public GameObject speciesSetUpInstructions2;
    bool intructions1Showing;

    private void Awake()
    {
        activeTip = movementTips;
        showTips = true;

        swapingTimer = swapingTimerStart;
        sideMenuTip.SetActive(false);
        movementTips.SetActive(false);

        intructions1Showing = true;
        speciesSetUpInstructions1.SetActive(true);
        speciesSetUpInstructions2.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        sideMenuTips();

        if (intructions1Showing)
        {
            speciesSetUpInstructions1.SetActive(true);
            speciesSetUpInstructions2.SetActive(false);
        }
        else
        {
            speciesSetUpInstructions1.SetActive(false);
            speciesSetUpInstructions2.SetActive(true);
        }
    }

    public void SpeciesSetUpInstructions()
    {
        if (intructions1Showing)
        {
            intructions1Showing = false;
        }
        else
        {
            intructions1Showing = true;
        }
    }

    void sideMenuTips()
    {
        if (showTips)
        {
            if (swapingTimer <= 0)
            {
                // Reset timer
                swapingTimer = swapingTimerStart;
                swapCount++;
                if (swapCount > 16)
                {
                    showTips = false;
                }

                // Swap active tip
                if (activeTip == movementTips)
                {
                    activeTip = sideMenuTip;
                }
                else if (activeTip == sideMenuTip)
                {
                    activeTip = movementTips;
                }
            }
            swapingTimer -= 1 * Time.deltaTime;

            if (gameManagment.gameStarted && !sideMenu.sideMenuShowing)
            {
                activeTip.SetActive(true);
            }
            else
            {
                activeTip.SetActive(false);
            }

            if (sideMenuTip != activeTip)
            {
                sideMenuTip.SetActive(false);
            }
            else
            {
                movementTips.SetActive(false);
            }
        }
        else
        {
            sideMenuTip.SetActive(false);
            movementTips.SetActive(false);
        }
    }




    // Experiments

    /*
    public void createPanel(string mainTextValues, int fontSize, Vector2 panelSize, Vector2 panelLocation)
    {
        GameObject newPanel = Instantiate(panelPrefab);
        newPanel.transform.parent = canvas.transform;
        // Replace any old panel with the new one
        Destroy(currentPanel);
        currentPanel = newPanel;

        RectTransform panelTransform = (RectTransform)newPanel.transform;

        panelTransform.position = panelLocation;

        mainTextGameOb = panelTransform.GetChild(1).gameObject;
        mainText = mainTextGameOb.GetComponent<Text>();

        mainText.text = mainTextValues;
        mainText.fontSize = fontSize;
    }
    */
}
