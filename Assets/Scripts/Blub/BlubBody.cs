using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlubBody : MonoBehaviour
{
    CapsuleCollider targetRange;
    BlubGenes genes;
    Movment movment;
    Counter counter;
    GameObject brain;

    [SerializeField] private float baseRadius;
    [SerializeField] private float baseNourishment;
    public float nourishment;

    // Start is called before the first frame update
    void Start()
    {
        brain = GameObject.Find("Brain");

        counter = brain.GetComponent<Counter>();
        genes = GetComponent<BlubGenes>();
        targetRange = GetComponent<CapsuleCollider>();
        movment = GetComponent<Movment>();
    }

    // Update is called once per frame
    void Update()
    {
        // Set size levels
        transform.localScale = new Vector3(counter.baseSize, counter.baseSize, counter.baseSize) * genes.sizeGeneLevel;

        // Set nourishment levels
        nourishment = counter.baseNourishment * genes.nourishmentGeneLevel;
    }
}
