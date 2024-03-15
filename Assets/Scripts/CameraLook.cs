using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraLook : MonoBehaviour
{
    [SerializeField]
    private float _lookDelta = 1f;

    [SerializeField]
    private float _minHeadAngle = -90f;

    [SerializeField]
    private float _maxHeadAngle = 90f;

    private Vector3 _rotation;

    [SerializeField]
    private Transform _headObject;

    [SerializeField]
    private Transform _bodyObject;

    private Vector2 _mouseInputAxis;

    private void Start()
    {
        // Ensure the initial rotations are preserved
        // so the player is facing the intended direction
        _rotation = new Vector3(
            _headObject.eulerAngles.x,
            _bodyObject.eulerAngles.y,
            0f
        );
    }

    private void Update()
    {
        UpdateInput();
        UpdateLook();
    }

    private void UpdateInput()
    {
        if (!CursorManager.CursorEnabled)
        {
            _mouseInputAxis = new Vector2(
                Input.GetAxisRaw("Mouse X"),
                Input.GetAxisRaw("Mouse Y")
            ) * _lookDelta;
        }
        else
        {
            _mouseInputAxis = Vector2.zero;
        }
    }

    private void UpdateLook()
    {
        // Update rotation angles
        _rotation.x -= _mouseInputAxis.y;
        _rotation.x = Mathf.Clamp(_rotation.x, _minHeadAngle, _maxHeadAngle);

        _rotation.y += _mouseInputAxis.x;

        // Apply updated rotation to body and head independently
        _bodyObject.rotation = Quaternion.Euler(_bodyObject.eulerAngles.x, _rotation.y, _bodyObject.eulerAngles.z);
        _headObject.rotation = Quaternion.Euler(_rotation.x, _headObject.eulerAngles.y, _headObject.eulerAngles.z);
    }
}
