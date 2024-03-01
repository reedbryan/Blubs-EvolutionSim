using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Counter : MonoBehaviour
{
    /// <summary>
    /// The average level of each blub stat
    /// </summary>
    public float avSpeed;
    public float avEnergyDepletion;
    public float avrReproductionCost;
    public float avReproductionPoint;
    public float avSize;
    public float avTargetRange;
    public float avNourishment;

    public float blubCount;
    public float foodCount;

    /// <summary>
    /// The base stats for all blubs as they must be kept track of in the brain or they will get altered during reproduction
    /// This happens because both the ___GeneLevel and the setSpeed or radius will be copied and will be grow bigger than it is supposed to.
    /// </summary>
    public float baseSpeed;
    public float baseTargetRngRadius;
    public float baseSize;
    public float baseEnergyDepletion;
    public float baseNourishment;
    public float baseReproductionPoint;
    public float baseReproductionCost;

    // List of all entities in the sim
    public List<GameObject> allEntities;

    // Is called whenever a new blub is spawned from BlubGenes script
    public void UpdateAverages(float[] geneStats, bool newBlub)
    {
        avSpeed = ProcessAverages(avSpeed, geneStats[0], newBlub);
        avEnergyDepletion = ProcessAverages(avEnergyDepletion, geneStats[3], newBlub);
        avReproductionPoint = ProcessAverages(avReproductionPoint, geneStats[5], newBlub);
        avrReproductionCost = ProcessAverages(avrReproductionCost, geneStats[4], newBlub);
        avSize = ProcessAverages(avSize, geneStats[2], newBlub);
        avTargetRange = ProcessAverages(avTargetRange, geneStats[1], newBlub);
        avNourishment = ProcessAverages(avNourishment, geneStats[6], newBlub);
    }

    public float ProcessAverages(float av, float newStat, bool newBlub)
    {
        float wtPrev = (blubCount - 1) / blubCount;
        float wtNew = 1 - wtPrev;
        float wtPrev2 = blubCount / (blubCount - 1);
        float wtNew2 = 1 / (blubCount - 1);

        if (newBlub)
        {
            av = av * wtPrev + newStat * wtNew;
            return av;
        }
        else
        {
            if (blubCount <= 0)
            {
                av = 0;
                return av;
            }
            else
            {
                av = av * wtPrev2 - newStat * wtNew2;
                return av;
            }
        }
    }

    public void AddBlub()
    {
        blubCount++;
    }
    public void LoseBlub()
    {
        blubCount--;
    }

    public void AddFood()
    {
        foodCount++;
    }
    public void LoseFood()
    {
        foodCount--;
    }
}
