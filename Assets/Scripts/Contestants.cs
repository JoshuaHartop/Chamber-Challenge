using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Contestants : MonoBehaviour
{
    protected static bool s_playerTurn;

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
        if (s_playerTurn)
        {
            _gun.PlayAnimation(GunAnimationType.PlayerPutDown);
            s_playerTurn = false;
            _enemy.EnemyTurn();
        }
        else if (s_playerTurn == false)
        {
            s_playerTurn = true;
            _gun.PlayAnimation(GunAnimationType.PlayerGrab);
        }
    }

    public void PlayerTurnForce()
    {
        s_playerTurn = true;
        _gun.PlayAnimation(GunAnimationType.PlayerGrab);
    }
}
