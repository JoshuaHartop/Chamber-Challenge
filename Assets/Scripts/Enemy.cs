using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Enemy : Contestants
{
    [SerializeField] GameObject player;
    private int enemyRNG = 0;
    // Start is called before the first frame update
    void Start()
    {
        HP = 3;
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void enemyTurn()
    {
        if (playerTurn == false)
        {
            print("enemy shooting");
            if (gun.LiveBullets / gun.BulletTotal > 0.5f)
            {
                print("enemy shoots you");
                gun.BulletTotal--;
                StartCoroutine(gun.shootOther(player, gameObject));
            }
            else if (gun.LiveBullets / gun.BulletTotal == 0.5f)
            {
                {
                    enemyRNG = UnityEngine.Random.Range(1, 1000);
                    if (enemyRNG % 2 == 0)
                    {
                        print("enemy shoots you");
                        gun.BulletTotal--;
                        StartCoroutine(gun.shootOther(player, gameObject));

                    }
                    else if (enemyRNG % 2 == 1)
                    {
                        print("enemy shoot self");
                        gun.BulletTotal--;
                        StartCoroutine(gun.shootSelf(gameObject));

                    }
                }
            }
            else if (gun.LiveBullets / gun.BulletTotal < 0.5f)
            {
                print("enemy shoot self");
                gun.BulletTotal--;
                StartCoroutine(gun.shootSelf(gameObject));
            }
            else
            {
                print("enemy shoots you");
                StartCoroutine(gun.shootOther(player, gameObject));
            }
        }
       
    }
}
