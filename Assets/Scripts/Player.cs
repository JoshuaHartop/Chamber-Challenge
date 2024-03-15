using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Contestants
{
    [SerializeField]
    private GameObject _dealer; 
    
    // Start is called before the first frame update
    void Start()
    {
        HP = 3;
        _gun.PlayAnimation(GunAnimationType.PlayerGrab);
    }

    // Update is called once per frame
    void Update()
    {
        if (s_playerTurn == true && !CursorManager.CursorEnabled)
        {
            if (Input.GetMouseButtonDown(0))
            {
                print("player shooting enemy");
                StartCoroutine(_gun.ShootOther(_dealer, gameObject));
            }
            else if (Input.GetMouseButtonDown(1))
            {
                _gun.PlayAnimation(GunAnimationType.PlayerShootSelf);
                print("player shooting self");
                StartCoroutine(_gun.ShootSelf(gameObject));
            }
        }
    }
}
