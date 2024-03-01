using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlubBehavior : MonoBehaviour
{
    GameObject brain;

    FoodConsomtion foodConsomtion;
    BlubGenes genes;
    Movment movment;
    Counter counter;

    public GameObject currentTarget;
    public GameObject currentDanger;

    // Raycasting
    public float radius;
    private Vector3 origin;
    private Vector3 direction;
    public LayerMask rayLayerMask;
    public GameObject otherGameOb;
    private float attentionTimerCurrent;
    private float attentionTimerStart = 1f;

    // Start is called before the first frame update
    void Start()
    {
        brain = GameObject.Find("Brain");

        foodConsomtion = GetComponent<FoodConsomtion>();
        genes = GetComponent<BlubGenes>();
        movment = GetComponent<Movment>();
        counter = brain.GetComponent<Counter>();
    }

    //Targeting- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - 


    // These are the two states a blub can be in when interacting with other GameObs
    // They are activated in the UPDATE FUNCTION of this script 

    /*
    /// <summary>
    /// The targeting state
    /// </summary>
    /// <param name="target"></param>
    public void Target(GameObject target)
    {
        //Assign new target
        if (!targeting)
        {
            currentTarget = target;
            targeting = true;
        }
        else
        {
            currentTarget = target;
        }
    }

    public void Avoid(GameObject danger)
    {
        // Targeting overrules avoiding
        if (!targeting)
        {
            currentDanger = danger;
            avoiding = true;
        }
    }
    */

    void Update()
    {
        // Setting the proper radius
        radius = counter.baseTargetRngRadius * genes.targetRangeGeneLevel;

        // If there is a current target
        if (currentTarget != null)
        {
            movment.GoToTarget();
        }
        else
        {
            // If there is no target but there is a danger
            if (currentDanger != null)
            {
                movment.RunFromDanger();
            }
            // If there is neither a target or a danger
            else
            {
                movment.RegMovment();
            }
        }
        // Cast rays every frame
        TargetRangeRays();
    }

    //Colliders- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - 

    /*
    //OnTrigger for targetRange collider
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Terrain"))
        {
            Debug.Log("Constant collision baby");
        }

        //if it comes into contact with another blub
        if (other.gameObject.CompareTag("Blub"))
        {
            BlubGenes otherBlub = other.gameObject.GetComponent<BlubGenes>();

            if (otherBlub.sizeGeneLevel * 1.5 <= genes.sizeGeneLevel)//Try to eat smaller blub
            {
                Target(other.gameObject);
            }
            else if (otherBlub.sizeGeneLevel >= genes.sizeGeneLevel * 1.5)//Run from bigger blub
            {
                Avoid(other.gameObject);
            }
        }
        //if it comes into contact with a food (it has an else so blubs will be priority)
        else if (other.gameObject.CompareTag("Food"))
        {
            other.gameObject.GetComponent<FoodProperties>();
            Target(other.gameObject);
        }
    }
    //OutTrigger for targetRange collider
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == currentTarget)
        {
            targeting = false;
        }
        if (other.gameObject == currentDanger)
        {
            avoiding = false;
        }
    }
    */


    //Checks for collision with other food and blub
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Food"))
        {
            foodConsomtion.EatFood(other.gameObject);
            Destroy(other.gameObject);
            counter.LoseFood();
        }
        if (other.gameObject.CompareTag("Blub"))
        {
            BlubGenes otherBlub = other.gameObject.GetComponent<BlubGenes>();

            if (otherBlub == null)
            {
                return;
            }
            else
            {
                if (otherBlub.sizeGeneLevel * 1.2 <= genes.sizeGeneLevel)//Try to eat smaller blub
                {
                    foodConsomtion.EatBlub(other.gameObject);
                    Destroy(other.gameObject);
                    counter.LoseBlub();
                }
            }
        }
    }

    // Raycasting - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -

    void TargetRangeRays()
    {
        origin = transform.position;

        Collider[] hitColliders = Physics.OverlapSphere(origin, radius);

        foreach (var item in hitColliders)
        {
            otherGameOb = item.gameObject;

            // if it comes into contact with another blub
            if (otherGameOb.CompareTag("Blub"))
            {
                BlubGenes otherBlub = otherGameOb.GetComponent<BlubGenes>();

                if (otherBlub.sizeGeneLevel * 1.2 <= genes.sizeGeneLevel)
                {
                    // Try to eat the other smaller blub
                    // If not already doing that
                    if (currentTarget == null)
                    {
                        currentTarget = otherGameOb;
                        ResetAttentionTimer();
                    }
                    // If already doing that
                    // Check if this new blub is more noutrishuse
                    else
                    {
                        if (currentTarget.CompareTag("Blub"))
                        {
                            BlubGenes currentTargetedBlub = currentTarget.GetComponent<BlubGenes>();
                            if (otherBlub.nourishmentGeneLevel >= currentTargetedBlub.nourishmentGeneLevel)
                            {
                                currentTarget = otherGameOb;
                                ResetAttentionTimer();
                            }
                        }
                    }
                }
                else if (otherBlub.sizeGeneLevel >= genes.sizeGeneLevel * 1.2)
                {
                    // Run from bigger blub
                    // If not already doing that
                    // If not already targeting somthing
                    if (currentDanger == null && currentTarget == null)
                    {
                        currentDanger = otherGameOb;
                        ResetAttentionTimer();
                    }
                }
            }

            // if it comes into contact with a food
            if (otherGameOb.CompareTag("Food"))
            {
                // Go for the food
                // if there is not already a target
                if (currentTarget == null)
                {
                    currentTarget = otherGameOb;
                    ResetAttentionTimer();
                }
            }
        }

        attentionTimerCurrent -= 1 * Time.deltaTime;
        if (attentionTimerCurrent <= 0)
        {
            currentTarget = null;
            currentDanger = null;

            attentionTimerCurrent = attentionTimerStart;
        }

        void ResetAttentionTimer()
        {
            // Reset attention timer as a GameOb of interest has been found
            attentionTimerCurrent = attentionTimerStart;
        }


        /*
        RaycastHit hit;
        if (Physics.SphereCast(origin, radius, Vector3.forward, out hit, 1, rayLayerMask, QueryTriggerInteraction.UseGlobal))
        {
            otherGameOb = hit.transform.gameObject;

            Debug.Log(otherGameOb);

            //if it comes into contact with another blub
            if (otherGameOb.CompareTag("Blub"))
            {
                BlubGenes otherBlub = otherGameOb.GetComponent<BlubGenes>();

                if (otherBlub.sizeGeneLevel * 1.5 <= genes.sizeGeneLevel)//Try to eat smaller blub
                {
                    if (currentTarget == null)
                    {
                        currentTarget = otherGameOb;
                        // Reset attention timer as a GameOb of interest has been found
                        attentionTimerCurrent = attentionTimerStart;
                    }
                }
                else if (otherBlub.sizeGeneLevel >= genes.sizeGeneLevel * 1.5)//Run from bigger blub
                {
                    if (currentDanger == null)
                    {
                        currentDanger = otherGameOb;
                        // Reset attention timer as a GameOb of interest has been found
                        attentionTimerCurrent = attentionTimerStart;
                    }
                }
            }
            else
            {
                // if it comes into contact with a food (it has an else so blubs will be priority)
                if (otherGameOb.CompareTag("Food"))
                {
                    if (currentTarget == null)
                    {
                        // Reset attention timer as a GameOb of interest has been found
                        attentionTimerCurrent = attentionTimerStart;

                        otherGameOb.GetComponent<FoodProperties>();
                        currentTarget = otherGameOb;
                    }
                }
            }
            // This is only called if no gameobjects of interest are found
            Debug.Log("Nothing interessting");
            if (attentionTimerCurrent <= 0)
            {
                currentDanger = null;
                currentTarget = null;

                attentionTimerCurrent = attentionTimerStart;
            }
            attentionTimerCurrent -= 1f * Time.deltaTime;
        }
        */
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(origin, radius);
    }
}