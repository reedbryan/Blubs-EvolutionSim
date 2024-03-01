using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodConsomtion : MonoBehaviour
{
    Movment Movment;
    EnergyManagment energyManagment;
    BlubGenes genes;
    Counter counter;

    // Start is called before the first frame update
    void Start()
    {
        Movment = GetComponent<Movment>();
        energyManagment = GetComponent<EnergyManagment>();
        genes = GetComponent<BlubGenes>();
        counter = GetComponent<Counter>();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void EatFood(GameObject Food)
    {
        FoodProperties foodProperties = Food.GetComponent<FoodProperties>();
        energyManagment.GiveEnergy(foodProperties.energyGiven);
    }

    public void EatBlub(GameObject Blub)
    {
        BlubBody blubBody = Blub.GetComponent<BlubBody>();
        energyManagment.GiveEnergy(blubBody.nourishment);
    }
}
