
using System.Collections.Generic;
using UnityEngine;

public class PatternGun : Gun
{
    [SerializeField]
    List<BulletPattern> bulletPatterns;
    [SerializeField]
    List<int> patternOrder;
    public override void Shoot()
    {
        
    }
}
