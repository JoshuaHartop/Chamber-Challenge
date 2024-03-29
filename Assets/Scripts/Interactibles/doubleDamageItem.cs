using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleDamageItem : Item
{
    [SerializeField]
    private Gun _gun;

    protected override void Start()
    {
        base.Start();

        _gun = FindObjectOfType<Gun>();
    }

    public override void OnInteract()
    {
        _gun.bulletDamage = 2;
        Destroy(gameObject);
    }
}