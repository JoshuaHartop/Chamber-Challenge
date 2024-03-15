using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeItem : MonoBehaviour, IInteractible
{
    // Gets called automatically when the player interacts with this script/item!
    public void OnInteract()
    {
        Debug.Log("Used /THE CUBE/ !!!");

        Destroy(gameObject);
    }
}
