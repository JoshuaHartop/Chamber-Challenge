using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Contestants
{
    [SerializeField]
    private GameObject _dealer;

    private void Start()
    {
        HP = 3;
        _gun.PlayAnimation(GunAnimationType.PlayerGrab);
    }

    private void Update()
    {
        if (s_playerTurn == true && !CursorManager.CursorEnabled)
        {
            if (Input.GetMouseButtonDown(0) && shotsFired == 1)
            {
                print("player shooting enemy");
                shotsFired = 0;
                StartCoroutine(_gun.ShootOther(_dealer, gameObject));
            }
            else if (Input.GetMouseButtonDown(1) && shotsFired == 1)
            {
                _gun.PlayAnimation(GunAnimationType.PlayerShootSelf);
                print("player shooting self");
                shotsFired = 0;
                StartCoroutine(_gun.ShootSelf(gameObject));
            }
        }
    }
}
