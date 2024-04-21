using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChamberCheckItem : Item
{
    private Gun _gun;
    private HUD _hud;

    protected override void Start()
    {
        base.Start();

        _gun = FindObjectOfType<Gun>();
        _hud = FindObjectOfType<HUD>();
    }

    public override void OnInteract()
    {
        Debug.Log("Interacted with binos");

        Debug.Log("Chamber Count: " + _gun.GetChamber().Length);
        Debug.Log("Current Bullet: " + _gun.GetCurrentBullet());
        bool isLive = _gun.GetChamber()[_gun.GetCurrentBullet()];

        // if (isLive) Debug.Log("There is a live bullet in the chamber.");
        // else        Debug.Log("The chamber is empty.");

        if (isLive) _hud.ShowMessage("There is a <color=red>live bullet</color> in the chamber.", 2.5f);
        else        _hud.ShowMessage("The chamber is <color=green>empty</color>.", 2.5f);

        Destroy(gameObject);
    }
}
