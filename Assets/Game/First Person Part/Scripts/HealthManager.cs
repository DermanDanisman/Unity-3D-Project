using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthManager : MonoBehaviour
{
    [SerializeField] int health = 100;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GotHit(int damage)
    {
        health -= damage;
        SetHealth(health);
        print("HP: " + health);
    }

    void SetHealth(int newHealth)
    {
        health = newHealth;
        if (health <= 0)
            Destroy(gameObject);
    }
}
