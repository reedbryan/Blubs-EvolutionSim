using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Collections.Generic;

public class TestScript : MonoBehaviour
{
    int negativeTwos;
    int negativeOnes;
    int zeros;
    int ones;
    int twos;

    private void Start()
    {
        for (int i = 0; i < 1000; i++)
        {
            float rand = Random.Range(0.0f, 1.0f);
            //Debug.Log("index: " + i);
            //Debug.Log("number: " + rand);
            BinomualDistribution(rand);
        }
        Debug.Log("Negative ones: " + negativeOnes);
        Debug.Log("Negative twos: " + negativeTwos);
        Debug.Log("Zeros: " + zeros);
        Debug.Log("Ones: " + ones);
        Debug.Log("Twos: " + twos);
    }


    void BinomualDistribution(float rand)
    {
        if (rand >= 0 &&rand <= 0.05)
        {
            negativeTwos++;
            return;
        }
        if (rand >= 0.06 && rand <= 0.25)
        {
            negativeOnes++;
            return;
        }
        if (rand >= 0.26 && rand <= 0.74)
        {
            zeros++;
            return;
        }
        if (rand >= 0.75 && rand <= 0.94)
        {
            ones++;
            return;
        }
        if (rand >= 0.95 && rand <= 1)
        {
            twos++;
            return;
        }
    }
}
