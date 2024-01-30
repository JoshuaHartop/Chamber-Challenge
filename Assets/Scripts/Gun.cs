using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    private int currentBullet;
    private Contestants otherContestant;
    private List<bool> isBullet;
    private int defaultBullet = 0;
    private static float _LiveBullets = 0f;
    private static float _BulletTotal = 0f;
    private int blanks = 0;

    public float BulletTotal
    {
        get { return _BulletTotal; }
        set { _BulletTotal = value; }
    }
    public float LiveBullets
    {
        get { return _LiveBullets; }
        set { _LiveBullets = value; }
    }
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
        BulletTotal--;
        //do if round is blah here
        if (isBullet[0] == true)
        {
            isBullet.RemoveAt(0);
            LiveBullets--;
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
            isBullet.RemoveAt(0);
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
        BulletTotal--;
        //do if round is blah here
        if (isBullet[0] == true)
        {
            isBullet.RemoveAt(0);
            LiveBullets--;
            print("BOOM");
            otherContestant.takeDamage(1);
            if (isBullet.Count == 0)
            {
                reloadGun();
                otherContestant.playerTurnForce();
            }
            otherContestant.nextTurn();
        }
        else
        {
            isBullet.RemoveAt(0);
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
        isBullet = new List<bool>(6);
        defaultBullet = UnityEngine.Random.Range(0, 5);
        _LiveBullets = 0;
        _BulletTotal = 6;
        blanks = 0;

        for (int i = 0; i < 6; i++)
        {
            currentBullet = UnityEngine.Random.Range(0, 100);
            if (currentBullet%6 == 0)
            {
                isBullet.Add(true);
                _LiveBullets++;
            }
            else
            {
                isBullet.Add(false);
                blanks++;
            }
        }
        if (isBullet[defaultBullet] == false)
        {
            _LiveBullets++;
            blanks--;
        }
        isBullet[defaultBullet] = true;
        print("loaded in " + _LiveBullets.ToString() + " bullets and " + blanks.ToString() + " blanks");

    }
}
