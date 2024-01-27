using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Contestants : MonoBehaviour
{
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
            playerTurn = !playerTurn;
            //call enemy turn
        }
        else
        {
            playerTurn = true;
        }
    }

    public void playerTurnForce()
    {
        playerTurn = true;
    }
}
