using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Contestants : MonoBehaviour
{
    public static bool s_playerTurn = true;
    public static int shotsFired;

    public ItemSpawner item;

    [SerializeField]
    private Enemy _enemy;

    [SerializeField]
    protected Gun _gun;

    private int _hp;

    public int HP
    {
        get
        {
            return _hp; 
        }

        set 
        { 
            _hp = value;
        }
    }

    private void Awake()
    {
        s_playerTurn = true;
        shotsFired = 1;
    }

    public void TakeDamage(int dmg)
    {
        _hp -= dmg;
    }

    public void NextTurn()
    {
        shotsFired = 1;

        if (s_playerTurn)
        {
            s_playerTurn = false;

            _gun.PlayAnimation(GunAnimationType.EnemyGrab);
            _enemy.EnemyTurn();
        }
        else
        {
            _gun.PlayAnimation(GunAnimationType.PlayerGrab);
            s_playerTurn = true;
            shotsFired = 1;
            item.SpawnItem();
        }
    }

    public void PlayerTurnForce()
    {
        shotsFired = 1;
        s_playerTurn = true;
        _gun.PlayAnimation(GunAnimationType.PlayerGrab);
        item.SpawnItem();
    }
}
