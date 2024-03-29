using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour, IInteractible
{
    public int slot;

    private ItemSpawner _spawner;

    protected virtual void Start()
    {
        _spawner = FindAnyObjectByType<ItemSpawner>();
        Debug.Log(_spawner);
    }

    public virtual void OnInteract() {}

    void OnDestroy()
    { 
        _spawner.OnItemUsed(slot);
    }

}