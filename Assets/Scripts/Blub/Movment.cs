using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movment : MonoBehaviour
{
    BlubGenes genes;
    BlubBehavior behavior;
    Rigidbody rb;
    EnvironmentSetUpUI environment;
    GameObject brain;
    Counter counter;

    [SerializeField] private float RotationSpeed;
    [SerializeField] private float setSpeed; // This is what will be used in translate methode to make this thang move
    [SerializeField] private bool onWall;

    // Random movment
    [SerializeField] private Vector3 randMovmentPoint; // the range in witch point targets can be created for the blub to move twords during regMovment (also just environment size)
    public float directionChangeTimer; // The base time of the timer (can be changed in inspector)
    private float directionTimerCurrent;
    private float directionTimerStart; // Timer for changing the next point the blub will go to

    // Start is called before the first frame update
    void Start()
    {
        brain = GameObject.Find("Brain");

        counter = brain.GetComponent<Counter>();
        rb = GetComponent<Rigidbody>();
        genes = GetComponent<BlubGenes>();
        behavior = GetComponent<BlubBehavior>();
        environment = brain.GetComponent<EnvironmentSetUpUI>();

        onWall = false;

        directionTimerStart = directionChangeTimer;
    }

    private void Update()
    {
        // Set the speed of this blub with its genes
        setSpeed = counter.baseSpeed * genes.speedGeneLevel;
    }

    private void LateUpdate()
    {
        //Keeps blub rotation stable
        transform.localEulerAngles = new Vector3(0,transform.localEulerAngles.y , 0);
    }

    // Called in the (not fixed) update methode of blubBehavior when there is nothing interesting in targeting range
    public void RegMovment()
    {
        /*
        transform.Translate(transform.forward * setSpeed * Time.deltaTime);
        int turnDisision = Random.Range(1, 4);
        if (turnDisision == 1)//Right
        {
            for (int i = 0; i < Random.Range(2, 7); i++)
            {
                if (onWall)
                {
                    transform.Rotate(0, 180, 0);
                    onWall = false;
                }
                transform.Rotate(0, 0.2f, 0);
            }
        }
        else if (turnDisision == 2)//left
        {
            for (int i = 0; i < Random.Range(2, 7); i++)
            {
                if (onWall)
                {
                    transform.Rotate(0, 180, 0);
                    onWall = false;
                }
                transform.Rotate(0, -0.2f, 0);
            }
        }
        */


        // /*
        // Constant forward movment so and rotation changes so will the direction of movment
        transform.Translate(Vector3.forward * setSpeed * Time.deltaTime);
        // Look at current point
        transform.LookAt(randMovmentPoint);
        // Run the timer for the next point change
        directionTimerCurrent -= 1 * Time.deltaTime;

        // If Timer is up create a new target for blub to point at
        // or
        // If blub reaches the point its moving twords
        if (directionTimerCurrent <= 0 || transform.position == randMovmentPoint)
        {
            // Get new random point location
            // They all must be divided by 2.1 because the size of the environment about half of the actual coordanites
            // For ex the base environment is 100 * 100 but if you go to the furthest wall you will only be at 50 x or z
            float xRandPoint = Random.Range(environment.environmentSize.x * -1, environment.environmentSize.x) / 2.1f; //2.1f
            float yRandPoint = 0;
            float zRandPoint = Random.Range(environment.environmentSize.z * -1, environment.environmentSize.z) / 2.1f; //2.1f

            randMovmentPoint = new Vector3(xRandPoint, yRandPoint, zRandPoint);

            // Restet timer
            directionTimerCurrent = directionTimerStart;
            // Change time till next point change by a random number between -30% and +30% of orginal number
            float changeOfTimer = Random.Range((directionTimerStart * 0.60f) * -1f, directionTimerStart * 0.60f);
            directionTimerStart += changeOfTimer;
            // Check if the current timer is less than a third of the original
            if (directionTimerStart <= directionChangeTimer / 3)
            {
                if (changeOfTimer <= 0)
                {
                    directionTimerStart -= changeOfTimer;
                }
            }
            // Check if the current timer is more than three times the original
            else if (directionTimerStart >= directionChangeTimer * 3)
            {
                directionTimerStart += changeOfTimer;
            }
        }
        // */
    }

    public void GoToTarget()
    {
        if (behavior.currentTarget == null)
        {
            return;
        }
        else
        {
            Vector3 targetPos = new Vector3(behavior.currentTarget.transform.position.x, transform.position.y, behavior.currentTarget.transform.position.z);
            
            transform.LookAt(targetPos);
            transform.Translate(Vector3.forward * setSpeed * Time.deltaTime);
        }
    }

    public void RunFromDanger()
    {
        if (behavior.currentDanger == null)
        {
            return;
        }
        else
        {
            transform.LookAt(behavior.currentDanger.transform.position);
            transform.Translate(Vector3.back * setSpeed * Time.deltaTime);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            onWall = true;
        }
    }
}
