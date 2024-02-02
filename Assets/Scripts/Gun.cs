using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    private int currentBullet;
    private Contestants otherContestant;
    private Animator animator;
    private List<bool> isBullet;
    private int defaultBullet = 0;
    private static float _LiveBullets = 0f;
    private static float _BulletTotal = 0f;
    private int blanks = 0;

    public enum animationNumber
    {
        PlayerGrab = 0,
        PlayerShootSelf = 1,
        PlayerShootSelfBlank = 2,
        PlayerShootOther = 3,
        PlayerPutDown = 4
    }

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
    void Awake()
    {
        reloadGun();
        currentBullet = 0;
        animator = GetComponent<Animator>();
    }
    public void gunAnimation(animationNumber number)
    {
        if (number == animationNumber.PlayerGrab) // grab
        {
            animator.SetTrigger("playerGrabRevolver");
        }
        else if (number == animationNumber.PlayerShootSelf)
        {
            animator.SetTrigger("playerShootSelf");
        }
        else if (number == animationNumber.PlayerShootSelfBlank)
        {
            animator.SetTrigger("playerShootSelfBlank");
        }
        else if (number == animationNumber.PlayerShootOther)
        {
            animator.SetTrigger("playerShootOther");
        }
        else if (number == animationNumber.PlayerPutDown)
        {
            animator.SetTrigger("playerPutDown");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator shootOther(GameObject other, GameObject shooter) 
    {
        yield return new WaitForSeconds(2.5f);
        Player temporaryPlayer;

        otherContestant = other.GetComponent<Contestants>();
        BulletTotal--;
        //do if round is blah here

        if (isBullet[0] == true)
        {
            if (shooter.TryGetComponent(out temporaryPlayer))
            {
                gunAnimation(animationNumber.PlayerShootOther);

            }
            else
            {

            }
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
                if (shooter.TryGetComponent(out temporaryPlayer))
                {
                    yield return new WaitForSeconds(2.5f);
                    //gunAnimation(animationNumber.PlayerPutDown);

                }
                else
                {
                   
                }
                yield return new WaitForSeconds(2.5f);
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
                if (shooter.TryGetComponent(out temporaryPlayer))
                {
                    yield return new WaitForSeconds(2.5f);
                    //gunAnimation(animationNumber.PlayerPutDown);

                }
                else
                {
                    yield return new WaitForSeconds(2.5f);
                }
                yield return new WaitForSeconds(2.5f);
                otherContestant.nextTurn();
            }
        }

    }

    public IEnumerator shootSelf(GameObject self)
    {
        yield return new WaitForSeconds(2.5f);
        Player temporaryPlayer;
        Enemy temporaryEnemy;

        otherContestant = self.GetComponent<Contestants>();
        BulletTotal--;
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
            if (self.TryGetComponent(out temporaryPlayer))
            {
                gunAnimation(animationNumber.PlayerShootSelfBlank);
            }
            else
            {
                yield return new WaitForSeconds(2.5f);
            }
            yield return new WaitForSeconds(2.5f);
            otherContestant.nextTurn();
        }
        else
        {
            if (self.TryGetComponent(out temporaryPlayer))
            {
                gunAnimation(animationNumber.PlayerShootSelfBlank);
            }
            else
            {
                yield return new WaitForSeconds(2.5f);
            }
            isBullet.RemoveAt(0);
            print("Blank");
            if (self.TryGetComponent(out temporaryEnemy))
            {
                temporaryEnemy.enemyTurn();
            }
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
