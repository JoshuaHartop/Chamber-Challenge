using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Contestants
{
    [SerializeField] GameObject player;
    private int enemyRNG = 0;
    // Start is called before the first frame update
    void Start()
    {
        HP = 3;
        StartCoroutine("shootWait");
    }

    // Update is called once per frame
    void Update()
    { 
    }

    void enemyTurn()
    {
        if (playerTurn == false)
        {
            print("enemy shooting");
            enemyRNG = UnityEngine.Random.Range(1, 1000);
            if (enemyRNG % 2 == 0)
            {
                print("enemy shoots you");
                gun.shootOther(player);

            }
            else if (enemyRNG % 2 == 1)
            {
                print("enemy shoot self");
                gun.shootSelf(gameObject);

            }
            StartCoroutine("shootWait");
        }
       
    }

    IEnumerator shootWait()
    {
        while (true)
        {
            yield return new WaitForSeconds(3);
            enemyTurn();
        }

    }
}
