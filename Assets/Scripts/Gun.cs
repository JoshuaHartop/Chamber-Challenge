using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GunAnimationType
{
    PlayerGrab,
    PlayerShootSelf,
    PlayerShootSelfBlank,
    PlayerShootOther,
    PlayerShootOtherBullet,
    PlayerPutDown
    
}

public class Gun : MonoBehaviour
{
    private static float s_liveBullets = 0f;
    private static float s_bulletTotal = 0f;

    private Contestants _otherContestant;
    private Animator _animator;
    private List<bool> _isBullet;
    private int _currentBullet;
    private int _defaultBullet = 0;
    private int _blanks = 0;

    public float BulletTotal
    {
        get { return s_bulletTotal; }
        set { s_bulletTotal = value; }
    }
    public float LiveBullets
    {
        get { return s_liveBullets; }
        set { s_liveBullets = value; }
    }

    // Start is called before the first frame update
    void Awake()
    {
        ReloadGun();
        _currentBullet = 0;
        _animator = GetComponent<Animator>();
    }

    public void PlayAnimation(GunAnimationType anim)
    {
        switch (anim)
        {
            case GunAnimationType.PlayerGrab:
                _animator.SetTrigger("playerGrabRevolver");
                break;
            case GunAnimationType.PlayerShootSelf:
                _animator.SetTrigger("playerShootSelf");
                break;
            case GunAnimationType.PlayerShootSelfBlank:
                _animator.SetTrigger("playerShootSelfBlank");
                break;
            case GunAnimationType.PlayerShootOther:
                _animator.SetTrigger("playerShootOther");
                break;
            case GunAnimationType.PlayerShootOtherBullet:
                _animator.SetTrigger("playerShootOtherBullet");
                break;
            case GunAnimationType.PlayerPutDown:
                _animator.SetTrigger("playerPutDown");
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator ShootOther(GameObject other, GameObject shooter) 
    {
        yield return new WaitForSeconds(2.5f);
        _otherContestant = other.GetComponent<Contestants>();
        BulletTotal--;
        //do if round is blah here

        if (_isBullet[0] == true)
        {
            print("BOOM");
            if (shooter.TryGetComponent<Player>(out _))
            {
                PlayAnimation(GunAnimationType.PlayerShootOtherBullet);
                yield return new WaitForSeconds(2.5f);
            }

            _isBullet.RemoveAt(0);
            LiveBullets--;
            _otherContestant.TakeDamage(1);
            if (_isBullet.Count == 0)
            {
                ReloadGun();
                _otherContestant.PlayerTurnForce();
            }
            else
            {
                if (shooter.TryGetComponent<Player>(out _))
                {
                    PlayAnimation(GunAnimationType.PlayerPutDown);
                }

                yield return new WaitForSeconds(1f);
                _otherContestant.NextTurn();
            }
        }
        else
        {
            print("Blank");
            _isBullet.RemoveAt(0);
            if (_isBullet.Count == 0)
            {
                ReloadGun();
                _otherContestant.PlayerTurnForce();
            }
            else
            {
                if (shooter.TryGetComponent<Player>(out _))
                {
                    PlayAnimation(GunAnimationType.PlayerShootOther);
                    yield return new WaitForSeconds(2.5f);
                    PlayAnimation(GunAnimationType.PlayerPutDown);
                }
                yield return new WaitForSeconds(1f);
                _otherContestant.NextTurn();
            }
        }
    }

    public IEnumerator ShootSelf(GameObject self)
    {
        yield return new WaitForSeconds(2.5f);
        Player temporaryPlayer;

        _otherContestant = self.GetComponent<Contestants>();
        BulletTotal--;
        if (_isBullet[0] == true)
        {
            _isBullet.RemoveAt(0);
            LiveBullets--;
            print("BOOM");
            _otherContestant.TakeDamage(1);
            if (_isBullet.Count == 0)
            {
                ReloadGun();
                _otherContestant.PlayerTurnForce();
            }
            if (self.TryGetComponent<Player>(out _))
            {
                PlayAnimation(GunAnimationType.PlayerShootSelfBlank); //for when it is bullet new anim here
                yield return new WaitForSeconds(2.5f);
                PlayAnimation(GunAnimationType.PlayerPutDown);
            }
            else
            {
                yield return new WaitForSeconds(2.5f);
            }
            yield return new WaitForSeconds(2.5f);
            _otherContestant.NextTurn();
        }
        else
        {
            if (self.TryGetComponent(out temporaryPlayer))
            {
                PlayAnimation(GunAnimationType.PlayerShootSelfBlank);
            }
            else
            {
                yield return new WaitForSeconds(2.5f);
            }
            _isBullet.RemoveAt(0);

            print("Blank");

            Enemy temporaryEnemy;
            if (self.TryGetComponent(out temporaryEnemy))
            {
                temporaryEnemy.EnemyTurn();
            }

            if (_isBullet.Count == 0)
            {
                ReloadGun();
                _otherContestant.PlayerTurnForce();
            }
        }
    }

    public void ReloadGun()
    {
        _isBullet = new List<bool>(6);
        _defaultBullet = UnityEngine.Random.Range(0, 5);
        s_liveBullets = 0;
        s_bulletTotal = 6;
        _blanks = 0;

        for (int i = 0; i < 6; i++)
        {
            _currentBullet = UnityEngine.Random.Range(0, 100);
            if (_currentBullet%6 == 0)
            {
                _isBullet.Add(true);
                s_liveBullets++;
            }
            else
            {
                _isBullet.Add(false);
                _blanks++;
            }
        }
        if (_isBullet[_defaultBullet] == false)
        {
            s_liveBullets++;
            _blanks--;
        }
        _isBullet[_defaultBullet] = true;
        print("loaded in " + s_liveBullets.ToString() + " bullets and " + _blanks.ToString() + " blanks");
    }
}
