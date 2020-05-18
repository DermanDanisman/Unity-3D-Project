using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public int score = 0;

    private void OnCollisionEnter(Collision other) 
    {
        if (other.gameObject.tag == "Collectible")
        {
            Destroy(other.gameObject);
            score++;
        }

        if (other.gameObject.tag == "FinishLine")
        {
            if (score == 4)
            {
                print("You have completed the level!");
            }
        }
    }
}
