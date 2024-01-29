using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Contestants
{
    [SerializeField] GameObject dealer; 
    
    // Start is called before the first frame update
    void Start()
    {
        HP = 3;
    }

    // Update is called once per frame
    void Update()
    {
        if (playerTurn == true)
        {
            if (Input.GetMouseButtonDown(0))
            {
                print("player shooting enemy");
                gun.shootOther(dealer);
               
            }
            else if (Input.GetMouseButtonDown(1))
            {
                print("player shooting self");
                gun.shootSelf(gameObject);
            }
        }
    }
}
