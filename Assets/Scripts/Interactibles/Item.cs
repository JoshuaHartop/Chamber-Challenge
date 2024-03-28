using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour, IInteractible
{
    public int slot;
    public itemSpawning spawner;


    public virtual void OnInteract() { }
    public void AssignSlot(int slott)
    {
        slot = slott;
    }

    public void Update()
    {
        if (spawner == null)
        {
            spawner = GameObject.FindAnyObjectByType<itemSpawning>();
        }
        
    }


    void OnDestroy()
    { 
        spawner.OnItemUsed(slot);
    }

}