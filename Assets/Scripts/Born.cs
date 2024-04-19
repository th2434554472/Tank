using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

/// <summary>
/// 生成玩家和敌人
/// </summary>

public class Born : MonoBehaviour{

    public GameObject playerPrefab;
    public GameObject[] enemyPrefabList;
    public bool createPlayer;

    private void Start(){
        Invoke("BornTank",0.8f);
        Destroy(gameObject,0.8f);
    }

    private void BornTank(){
        if (createPlayer) {
            Instantiate(playerPrefab, transform.position, Quaternion.identity);
        }
        else {
            int num = Random.Range(0, 2);
            Instantiate(enemyPrefabList[num], transform.position, Quaternion.identity);
        }

        
    }
}
