using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Shaker : MonoBehaviour
{
    [SerializeField]
    private float _strength = 1f;

    [SerializeField]
    private int _vibrato = 10;

    [SerializeField]
    private float _randomness = 90f;

    private void Start()
    {
        transform.DOShakePosition(10f, _strength, _vibrato, _randomness, false, false, ShakeRandomnessMode.Full)
            .SetLoops(-1, LoopType.Yoyo);
    }
}
