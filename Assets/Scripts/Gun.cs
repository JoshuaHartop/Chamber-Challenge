using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    private int currentBullet;
    private Contestants otherContestant;
    private Queue<bool> isBullet;
    private int liveBullets = 0;
    private int blanks = 0;
    // Start is called before the first frame update
    void Start()
    {
       reloadGun();
        currentBullet = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void shootOther(GameObject other) 
    {
        otherContestant = other.GetComponent<Contestants>();
        //do if round is blah here
        if (isBullet.Dequeue() == true)
        {
            print("BOOM");
            otherContestant.takeDamage(1);
            if (isBullet.Count == 0)
            {
                reloadGun();
                otherContestant.playerTurnForce();
            }
            else
            {
                otherContestant.nextTurn();
            }

        }
        else
        {
            print("Blank");
            if (isBullet.Count == 0)
            {
                reloadGun();
                otherContestant.playerTurnForce();
            }
            else
            {
                otherContestant.nextTurn();
            }
        }
    }

    public void shootSelf(GameObject other)
    {
        otherContestant = other.GetComponent<Contestants>();
        //do if round is blah here
        if (isBullet.Dequeue() == true)
        {
            print("BOOM");
            otherContestant.takeDamage(1);
            if (isBullet.Count == 0)
            {
                reloadGun();
                otherContestant.playerTurnForce();
            }
            else
            {
                otherContestant.nextTurn();
            }

            
            

        }
        else
        {
            print("Blank");
            if (isBullet.Count == 0)
            {
                reloadGun();
                otherContestant.playerTurnForce();
            }
        }
    }

    public void reloadGun()
    {
        isBullet = new Queue<bool>(6);
        for (int i = 0; i < 6; i++)
        {
            currentBullet = UnityEngine.Random.Range(0, 100);
            if (currentBullet%6 == 0)
            {
                isBullet.Enqueue(true);
                liveBullets++;
            }
            else
            {
                isBullet.Enqueue(false);
                blanks++;
            }
        }
        print("loaded in " + liveBullets.ToString() + " bullets and " + blanks.ToString() + " blanks");
        liveBullets = 0;
        blanks = 0;
    }
}
