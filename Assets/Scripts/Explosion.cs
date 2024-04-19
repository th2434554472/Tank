using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 爆炸特效类
/// </summary>

public class Explosion : MonoBehaviour{
    private void Start(){
        Destroy(gameObject,0.167f);
    }
}
