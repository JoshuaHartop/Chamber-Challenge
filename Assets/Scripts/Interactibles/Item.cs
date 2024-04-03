using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class Item : MonoBehaviour, IInteractible
{
    private const float ANIM_BOUNCE_HEIGHT = .1f;
    private const float ANIM_BOUNCE_DURATION = 0.75f;

    private const float ANIM_ROTATE_DEGS = 180f;
    private const float ANIM_ROTATE_DURATION = 1.5f;

    private static bool s_initFlag;
    private static Vector3 _animatedPosition;
    private static Vector3 _animatedRotation;

    public int slot;
    private ItemSpawner _spawner;

    private Vector3 _initialPosition;

    protected virtual void Start()
    {
        _spawner = FindAnyObjectByType<ItemSpawner>();
        _initialPosition = transform.position;

        if (!s_initFlag)
        {
            s_initFlag = true;

            DOTween.To(() => _animatedPosition, (x) => _animatedPosition = x, _animatedPosition + Vector3.up * ANIM_BOUNCE_HEIGHT, ANIM_BOUNCE_DURATION)
                .SetEase(Ease.Linear)
                .SetLoops(-1, LoopType.Yoyo);

            DOTween.To(() => _animatedRotation, (x) => _animatedRotation = x, _animatedRotation + Vector3.up * ANIM_ROTATE_DEGS, ANIM_ROTATE_DURATION)
                .SetEase(Ease.Linear)
                .SetLoops(-1, LoopType.Incremental);
        }
    }

    protected virtual void Update()
    {
        transform.position = _initialPosition + _animatedPosition;
        transform.rotation = Quaternion.Euler(_animatedRotation);
    }

    public virtual void OnInteract() {}

    void OnDestroy()
    { 
        _spawner.OnItemUsed(slot);
    }
}