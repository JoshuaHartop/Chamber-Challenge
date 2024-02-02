using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Contestants : MonoBehaviour
{
    [SerializeField] Enemy enemy;
    [SerializeField] protected Gun gun;
    protected static bool playerTurn;
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
        playerTurn = true;
        

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void takeDamage(int dmg)
    {
        _hp -= dmg;
    }

    public void nextTurn()
    {
        if (playerTurn)
        {
            gun.gunAnimation(Gun.animationNumber.PlayerPutDown);
            playerTurn = false;
            enemy.enemyTurn();
            
        }
        else if (playerTurn == false)
        {
            playerTurn = true;
            gun.gunAnimation(Gun.animationNumber.PlayerGrab);
        }
    }

    public void playerTurnForce()
    {
        playerTurn = true;
        gun.gunAnimation(Gun.animationNumber.PlayerGrab);
    }
}
