using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Contestants : MonoBehaviour
{
    public static bool s_playerTurn = true;
    public static int shotsFired;
    public itemSpawning item;
    

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

    // Start is called before the first frame update
    void Awake()
    {
        s_playerTurn = true;
        shotsFired = 1;
        
    }

    // Update is called once per frame
    void Update()
    {
        
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
        else if (s_playerTurn == false)
        {
            _gun.PlayAnimation(GunAnimationType.PlayerGrab);
            s_playerTurn = true;
            shotsFired = 1;
            item.spawnItem();

        }
    }

    public void PlayerTurnForce()
    {
        shotsFired = 1;
        s_playerTurn = true;
        _gun.PlayAnimation(GunAnimationType.PlayerGrab);
        item.spawnItem();
    }
}
