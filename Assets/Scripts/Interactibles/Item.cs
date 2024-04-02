using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour, IInteractible
{
    public int slot;
    int upDown = 1; // 1 is up 0 is down
    private ItemSpawner _spawner;

    protected virtual void Start()
    {
        _spawner = FindAnyObjectByType<ItemSpawner>();
        Debug.Log(_spawner);
    }

    protected virtual void Update()
    {
        bounceItem();
    }

    protected virtual void bounceItem()
    {
        
        this.transform.Rotate(0,20 * Time.deltaTime,0);
        if (this.transform.position.y > 1.1f)
        {
            upDown = 0;
        }
        else if (this.transform.position.y < 1f) // crayfish make this the item height instead of 0.1 so it touches the pad then goes up
        {
            upDown = 1;
        }
        if (upDown == 1)
        {
            transform.position = transform.position + new Vector3(0, 0.02f * Time.deltaTime, 0);
        }
        else if (upDown == 0)
        {
            transform.position = transform.position + new Vector3(0, -0.02f * Time.deltaTime, 0);
        }
        
    }




    public virtual void OnInteract() {}

    void OnDestroy()
    { 
        _spawner.OnItemUsed(slot);
    }

}