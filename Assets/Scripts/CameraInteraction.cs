using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class CameraInteraction : MonoBehaviour
{
    [SerializeField]
    private float _maxInteractDistance;
    
    private static bool _isLookingAtInteractible;
    
    public static bool IsPlayerLookingAtInteracatible
    {
        private set {
            _isLookingAtInteractible = value;
        }

        get {
            return _isLookingAtInteractible;
        }
    }

    private Camera _camera;

    private void Awake()
    {
        _camera = GetComponent<Camera>();
    }

    private void FixedUpdate()
    {
        RaycastHit hitInfo;
        if (Physics.Raycast(transform.position, transform.forward, out hitInfo, _maxInteractDistance))
        {
            IInteractible interactible = null;
            IsPlayerLookingAtInteracatible = hitInfo.collider.gameObject.TryGetComponent(out interactible);

            if (IsPlayerLookingAtInteracatible)
            {
                if (Input.GetButtonDown("Interact") && Contestants.s_playerTurn == true)
                {
                    interactible.OnInteract();
                }
            }
        }
    }
}
