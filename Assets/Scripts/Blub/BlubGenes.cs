using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlubGenes : MonoBehaviour
{
    Counter counter;
    SpeciesSetUpUI setUp;
    EnergyManagment energy;
    public GameObject brain;

    /// <summary>
    /// These floats are what influences the base stat of the blub (in most case with multiplication)
    /// When changed they will have direct effects to in game actions of the blub in question
    /// <summary>
    public float speedGeneLevel;
    public float targetRangeGeneLevel;
    public float sizeGeneLevel;
    public float energyDepletionGeneLevel;
    public float reproductionCostGeneLevel;
    public float reproductionPointGeneLevel;
    public float nourishmentGeneLevel;

    public float[] geneStats;
    // Checks that all genes are > 0 to they can update the stats
    bool allGenesSet;
    int genesReady;

    private void Update()
    {
        // Sets gensStats array to the current vallues of genestats (whether they are above or below 0)
        float[] temp = { speedGeneLevel, targetRangeGeneLevel, sizeGeneLevel, energyDepletionGeneLevel, reproductionCostGeneLevel, reproductionPointGeneLevel, nourishmentGeneLevel };
        geneStats = temp;
    }

    private void Start()
    {
        /*
        speedGeneLevel = 1f;
        targetRangeGeneLevel = 1f;
        sizeGeneLevel = 1f;
        energyDepletionGeneLevel = 1f;
        reproductionCostGeneLevel = 1f;
        reproductionPointGeneLevel = 1f;
        nourishmentGeneLevel = 1f;
        */
    }

    public void startAsChild(float[] parentGenes)
    {
        brain = GameObject.Find("Brain");
        counter = brain.GetComponent<Counter>();
        setUp = brain.GetComponent<SpeciesSetUpUI>();
        energy = GetComponent<EnergyManagment>();

        speedGeneLevel = parentGenes[0];
        targetRangeGeneLevel = parentGenes[1];
        sizeGeneLevel = parentGenes[2];
        energyDepletionGeneLevel = parentGenes[3];
        reproductionCostGeneLevel = parentGenes[4];
        reproductionPointGeneLevel = parentGenes[5];
        nourishmentGeneLevel = parentGenes[6];

        genesReady = 0;
        Mutation(setUp.nourishmentMutationPer, "nourishment");
        Mutation(setUp.speedMutationPer, "speed");
        Mutation(setUp.energyDepletionMutationPer, "energyDepletion");
        Mutation(setUp.reproductionCostMutationPer, "reproductionCost");
        Mutation(setUp.reproductionPointMutationPer, "reproductionPoint");
        Mutation(setUp.sizeMutationPer, "size");
        Mutation(setUp.targetRangeMutationPer, "targetRange");
    }

    public void startAsNew()
    {
        brain = GameObject.Find("Brain");
        counter = brain.GetComponent<Counter>();
        setUp = brain.GetComponent<SpeciesSetUpUI>();
        energy = GetComponent<EnergyManagment>();

        speedGeneLevel = 1f;
        targetRangeGeneLevel = 1f;
        sizeGeneLevel = 1f;
        energyDepletionGeneLevel = 1f;
        reproductionCostGeneLevel = 1f;
        reproductionPointGeneLevel = 1f;
        nourishmentGeneLevel = 1f;

        genesReady = 0;
        Mutation(setUp.nourishmentMutationPer, "nourishment");
        Mutation(setUp.speedMutationPer, "speed");
        Mutation(setUp.energyDepletionMutationPer, "energyDepletion");
        Mutation(setUp.reproductionCostMutationPer, "reproductionCost");
        Mutation(setUp.reproductionPointMutationPer, "reproductionPoint");
        Mutation(setUp.sizeMutationPer, "size");
        Mutation(setUp.targetRangeMutationPer, "targetRange");
    }

    void UpdateStats()
    {
        float[] temp = { speedGeneLevel, targetRangeGeneLevel, sizeGeneLevel, energyDepletionGeneLevel, reproductionCostGeneLevel, reproductionPointGeneLevel, nourishmentGeneLevel };
        geneStats = temp;
        counter.UpdateAverages(geneStats, true);
    }

    void Mutation(float mutationChance, string mutationType)
    {
        // Roll the dice for mutation
        // "Chance" is the randomized number that becomes persentage chance that a mutation will ocure
        // If that random number is lower than the givin number belonging to each mutation a mutation ill ocure
        float Chance = Random.Range(0, 100);
        if (Chance <= mutationChance)
        {
            Mutate(mutationType);
        }
        else
        {
            // The genesReady int counts the genes that have been through the mutation methodes and will update the stats when it is = to 7 (or the current number of genes)
            // It should be ++ here so it will count the genes that done get chosen to mutate
            // The rest will be dealt with at the end of the Mutate methode
            genesReady++;
            // Check if genes have all been dealt with 
            if (genesReady >= 7)
            {
                UpdateStats();
            }
        }
    }

    void Mutate(string currentMutation)
    {
        // Create new temporary array containing all mutations as strings
        string[] mutationID = new string[7];
        // Assigne all mutations to array values
        mutationID[0] = "speed";
        mutationID[1] = "targetRange";
        mutationID[2] = "size";
        mutationID[3] = "energyDepletion";
        mutationID[4] = "nourishment";
        mutationID[5] = "reproductionCost";
        mutationID[6] = "reproductionPoint";

        // Create new temporary array containing all current gene levels
        float[] currentGeneLevel = new float[7];
        // Assigne all mutations to array values
        currentGeneLevel[0] = speedGeneLevel;
        currentGeneLevel[1] = targetRangeGeneLevel;
        currentGeneLevel[2] = sizeGeneLevel;
        currentGeneLevel[3] = energyDepletionGeneLevel;
        currentGeneLevel[4] = nourishmentGeneLevel;
        currentGeneLevel[5] = reproductionCostGeneLevel;
        currentGeneLevel[6] = reproductionPointGeneLevel;

        // This int is used to relate the two arrays above in the foreach loop
        int loopCount = 0;

        foreach (var item in mutationID)
        {
            if (item == currentMutation)
            {
                // When correct mutation is selected apply the changes to geneLevel
                currentGeneLevel[loopCount] += Random.Range(-0.6f, 0.6f);
            }
            // if currentMutation is not selected slightly decrease all other muation values
            currentGeneLevel[loopCount] -= 0.05f;
            // Next statment is for the values that need to go up to be less effective
            if (item == "nourishment" || item == "reproductionCost" || item == "reproductionPoint" || item == "energyDepletion")
            {
                currentGeneLevel[loopCount] += 0.05f;
            }
            // Check for an overmutation
            if (CheckForOverMutation(currentGeneLevel[loopCount]))
            {
                energy.Die();
            }
            // Must be last so index 0 is counted
            loopCount++;
        }

        // Apply the new array values to the gene levels
        speedGeneLevel = currentGeneLevel[0];
        targetRangeGeneLevel = currentGeneLevel[1];
        sizeGeneLevel = currentGeneLevel[2];
        energyDepletionGeneLevel = currentGeneLevel[3];
        nourishmentGeneLevel = currentGeneLevel[4];
        reproductionCostGeneLevel = currentGeneLevel[5];
        reproductionPointGeneLevel = currentGeneLevel[6];

        // The genesReady int counts the genes that have been through the mutation methodes and will update the stats when it is = to 7 (or the current number of genes)
        // It should be ++ here so it will count the genes that have been chosen to mutate
        genesReady++;
        // Check if genes have all been dealt with 
        if (genesReady >= 7)
        {
            UpdateStats();
        }
    }

    /*
    void Mutate(string mutationType)
    {
        if (mutationType == "speed")
        {
            speedGene += Random.Range(0.1f, 0.5f);//Increases Speed Gene
            energyDepletionGene += Random.Range(0f, 0.3f);//Decrease energy depletion stat as a couter for speed increase
            reproductionCostGene += Random.Range(0f, 0.2f);
            Debug.Log("Speed Mutated");
            counter.totEnhancedMutations++;
        }

        if (mutationType == "energyDepletion")
        {
            //this if-else must be run on an mutation that could lead to a gene going below 0
            bool isOverMutated = CheckForOverMutation(energyDepletionGene);
            bool isOverMutated2 = CheckForOverMutation(speedGene);
            bool isOverMutated3 = CheckForOverMutation(sizeGene);
            if (isOverMutated || isOverMutated2 || isOverMutated3)
            {
                energy.Die();
            }
            else
            {
                energyDepletionGene -= Random.Range(0.1f, 0.5f);//Increase ED
                speedGene -= Random.Range(0f, 0.3f);//Decreases Speed
                sizeGene -= Random.Range(0f, 0.2f);
                Debug.Log("energyDepletion Mutated");
                counter.totNonEnhancedMutations++;
            }
        }

        if (mutationType == "reproductionCost")
        {
            bool isOverMutated = CheckForOverMutation(reproductionCostGene);
            bool isOverMutated2 = CheckForOverMutation(sizeGene);
            if (isOverMutated || isOverMutated2)
            {
                energy.Die();
            }
            else
            {
                reproductionCostGene -= Random.Range(0.1f, 0.5f);
                sizeGene -= Random.Range(0f, 0.2f);
                reproductionPointGene += Random.Range(0f, 0.3f);
                Debug.Log("reproductionCost Mutated");
                counter.totNonEnhancedMutations++;
            }
        }

        if (mutationType == "reproductionPoint")
        {
            bool isOverMutated = CheckForOverMutation(reproductionPointGene);
            bool isOverMutated2 = CheckForOverMutation(speedGene);
            if (isOverMutated || isOverMutated2)
            {
                energy.Die();
            }
            else
            {
                reproductionPointGene -= Random.Range(0.1f, 0.5f);
                speedGene -= Random.Range(0, 0.2f);
                reproductionCostGene += Random.Range(0f, 0.3f);
                Debug.Log("reproductionPoint Mutated");
                counter.totNonEnhancedMutations++;
            }
        }

        if (mutationType == "targetRange")
        {
            bool isOverMutated = CheckForOverMutation(speedGene);
            if (isOverMutated)
            {
                energy.Die();
            }
            else
            {
                targetRangeGene += Random.Range(0.1f, 0.5f);
                sizeGene += Random.Range(0f, 0.3f);
                speedGene -= Random.Range(0f, 0.1f);
                reproductionPointGene += Random.Range(0f, 0.1f);
                Debug.Log("targetRange Mutated");
                counter.totNonEnhancedMutations++;
            }
        }

        if (mutationType == "size")
        {
            bool isOverMutated = CheckForOverMutation(speedGene);
            if (isOverMutated)
            {
                energy.Die();
            }
            else
            {
                sizeGene += Random.Range(0.1f, 1f);
                energyDepletionGene += Random.Range(0.1f, 0.7f);
                speedGene -= Random.Range(0.1f, 0.3f);
                nourishmentGene += Random.Range(0.1f, 0.5f);
                Debug.Log("size Mutated");
                counter.totNonEnhancedMutations++;  
            }
        }

        if (mutationType == "nourishment")
        {
            nourishmentGene += Random.Range(0.1f, 0.5f);
            Debug.Log("nourishment Mutated");
            counter.totNonEnhancedMutations++;
        }
    }
    */


    bool CheckForOverMutation(float mutation)
    {
        if (mutation <= 0.1)
        {
            return true;
        }
        else
        {
            return false;
        } 
    }
}
