using System;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public abstract class BulletPattern : MonoBehaviour
{
    [SerializeField]
    protected List<float> volleyAngles;
    [SerializeField]
    protected List<float> volleyTimings;

    protected int releaseCounter;
    protected float timer;

    public abstract void ShootNextVolley();

    protected void Start()
    {
        releaseCounter = 0;
    }
}
