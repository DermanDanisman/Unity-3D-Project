using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public int score;
    float timer;

    private void Start() 
    {
        score = 0;
        timer = 0;
    }

    private void Update() 
    {
        timer += Time.deltaTime;
    }
    private void OnCollisionEnter(Collision other) 
    {
        if (other.gameObject.tag == "Collectible")
        {
            Destroy(other.gameObject);
            GameObject.Find("CollectiblesText").GetComponent<Text>().text = "You have collected an item!";
            timer = 0;

            if (timer >= 0)
            {
                GameObject.Find("CollectiblesText").GetComponent<Text>().text = "";
            }
            GameObject.Find("ScoreText").GetComponent<Text>().text = score++.ToString();

        }

        if (other.gameObject.tag == "FinishLine")
        {
            if (score == 4)
            {
                print("You have completed the level! Your score is: " + score);
                GameObject.Find("FinishingText").GetComponent<Text>().text = "Well Done! You have completed this level!";
            }
            else
            {
                print("You have to collect all the items!");
            }
        }
    }
}
