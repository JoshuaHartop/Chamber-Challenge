using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour, IInteractible
{
    public int slot;
    public itemSpawning spawner;

    public void Start()
    {
        
    }

    public virtual void OnInteract() { }
    public void AssignSlot (int slott)
    {
        slot = slott;
    }


    void OnDestroy()
    {
        spawner = GameObject.FindAnyObjectByType<itemSpawning>();
        spawner.OnItemUsed(slot);
    }

}

