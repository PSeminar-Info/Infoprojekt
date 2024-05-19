using System.Collections;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.AI;

public class BearHitController : MonoBehaviour
{

    private float _despawnTime = 0.1f;
    private float _lastActionTime;

    void Start()
    {
        _lastActionTime = Time.time;
    }

    void Update()
    {
        if (Time.time - _lastActionTime > _despawnTime)
        {
            Destroy(gameObject);
        }
    }
}
