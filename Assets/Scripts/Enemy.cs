using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Enemy : Contestants
{
    [SerializeField]
    private GameObject _player;
    private int _enemyRNG = 0;

    // Start is called before the first frame update
    void Start()
    {
        HP = 3;
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void EnemyTurn()
    {
        if (s_playerTurn == false)
        {
            print("enemy shooting");
            if (_gun.LiveBullets / _gun.BulletTotal > 0.5f)
            {
                print("enemy shoots you");
                _gun.BulletTotal--;
                StartCoroutine(_gun.ShootOther(_player, gameObject));
            }
            else if (_gun.LiveBullets / _gun.BulletTotal == 0.5f)
            {
                _enemyRNG = UnityEngine.Random.Range(1, 1000);
                if (_enemyRNG % 2 == 0)
                {
                    print("enemy shoots you");
                    _gun.BulletTotal--;
                    StartCoroutine(_gun.ShootOther(_player, gameObject));
                }
                else if (_enemyRNG % 2 == 1)
                {
                    print("enemy shoot self");
                    _gun.BulletTotal--;
                    StartCoroutine(_gun.ShootSelf(gameObject));
                }
            }
            else if (_gun.LiveBullets / _gun.BulletTotal < 0.5f)
            {
                print("enemy shoot self");
                _gun.BulletTotal--;
                StartCoroutine(_gun.ShootSelf(gameObject));
            }
            else
            {
                print("enemy shoots you");
                StartCoroutine(_gun.ShootOther(_player, gameObject));
            }
        }
    }
}
