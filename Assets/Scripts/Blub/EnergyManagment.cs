using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyManagment : MonoBehaviour
{
    BlubGenes genes;
    Movment movment;
    Counter counter;
    GameObject brain;

    public float energy;
    public float baseEnergy;
    [SerializeField] private float energyDepletionRate;
    [SerializeField] private float reproductionCost;
    [SerializeField] private float reproductionPoint;
    private float instantiationTimer = 2;
    [SerializeField]private bool instantiationTiming = false;

    // Start is called before the first frame update
    void Awake()
    {
        brain = GameObject.Find("Brain");

        genes = GetComponent<BlubGenes>();
        movment = GetComponent<Movment>();
        counter = brain.GetComponent<Counter>();
        energy = baseEnergy;
        instantiationTiming = false;
    }

    // Update is called once per frame
    void Update()
    {
        EnergyDeplete();
        EnergyCheck();
        if (instantiationTiming)
        {
            instantiationTimer -= 1 * Time.deltaTime;
            if (instantiationTimer <= 0)
            {
                Reproduce();
                instantiationTiming = false;
                instantiationTimer = 2;
            }
        }

        // Set stats to their gene levels
        energyDepletionRate = counter.baseEnergyDepletion * genes.energyDepletionGeneLevel;
        reproductionCost = counter.baseReproductionCost * genes.reproductionCostGeneLevel;
        reproductionPoint = counter.baseReproductionPoint * genes.reproductionPointGeneLevel;
    }

    public void GiveEnergy(float energyGiven)
    {
        energy += energyGiven;
    }

    void EnergyDeplete()
    {
        energy -= energyDepletionRate * Time.deltaTime;
    }

    void EnergyCheck()
    {
        if (energy <= 0)
        {
            Die();
        }
        if (energy >= reproductionPoint)
        {
            instantiationTiming = true;
        }
        else
        {
            instantiationTiming = false;
        }
    }

    void Reproduce()
    {
        Vector3 spawnPos = transform.position + new Vector3(Random.Range(-2, 2), 2, Random.Range(-2,2));
        energy -= reproductionCost;

        GameObject myChild;
        myChild = Instantiate(gameObject, spawnPos, transform.rotation);
        BlubGenes myChildGenes = myChild.GetComponent<BlubGenes>();
        EnergyManagment myChildEnergy = myChild.GetComponent<EnergyManagment>();

        // Pass down genes to child
        myChildGenes.startAsChild(genes.geneStats);
        myChildEnergy.baseEnergy = reproductionCost;

        // Add blub to count
        counter.AddBlub();
        counter.allEntities.Add(myChild);
    }

    public void Die()
    {
        //float[] geneStats = { genes.speedGeneLevel, genes.targetRangeGeneLevel, genes.sizeGeneLevel, genes.energyDepletionGeneLevel, genes.reproductionCostGeneLevel, genes.reproductionPointGeneLevel, genes.nourishmentGeneLevel };

        counter.LoseBlub();
        counter.UpdateAverages(genes.geneStats, false);
        Destroy(this.gameObject);
    }
}
