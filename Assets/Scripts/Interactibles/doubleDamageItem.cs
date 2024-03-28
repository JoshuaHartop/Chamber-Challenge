using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class doubleDamageItem : Item
{
    [SerializeField] private Gun gun;
    // Start is called before the first frame update
    void Start()
    {
        gun = GameObject.FindObjectOfType<Gun>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public override void OnInteract()
    {
        gun.bulletDamage = 2;
        Destroy(gameObject);
    }
}
