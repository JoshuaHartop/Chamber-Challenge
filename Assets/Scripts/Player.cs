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
        gun.gunAnimation(Gun.animationNumber.PlayerGrab);
    }

    // Update is called once per frame
    void Update()
    {
        if (playerTurn == true)
        {
           
            if (Input.GetMouseButtonDown(0))
            {
                print("player shooting enemy");
                StartCoroutine(gun.shootOther(dealer, gameObject));

            }
            else if (Input.GetMouseButtonDown(1))
            {
                gun.gunAnimation(Gun.animationNumber.PlayerShootSelf);
                print("player shooting self");
                StartCoroutine(gun.shootSelf(gameObject));
            }
        }
    }
}
