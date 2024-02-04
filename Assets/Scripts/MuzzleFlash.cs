using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MuzzleFlash : MonoBehaviour
{
    [SerializeField]
    private float _aliveTime = 0.2f;

    [SerializeField]
    private AnimationCurve _intensityCurve;

    [SerializeField]
    private Light _light;

    [SerializeField]
    private MeshRenderer _renderer;
    
    private float _spawnTime;

    private float _startIntensity;

    private void Start()
    {
        _spawnTime = Time.time;
        _startIntensity = _light.intensity;
    }

    private void Update()
    {
        float curveValue = _intensityCurve.Evaluate((Time.time - _spawnTime) / _aliveTime);

        _light.intensity = curveValue * _startIntensity;

        _renderer.material.color = new Color(
            _renderer.material.color.r,
            _renderer.material.color.g,
            _renderer.material.color.b,
            curveValue
        );


        if (Time.time > _spawnTime + _aliveTime)
        {
            Destroy(gameObject);
        }
    }
}
