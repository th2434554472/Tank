using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

/// <summary>
/// 地图创造器
/// </summary>


public class MapCreator : MonoBehaviour{

    // 初始化地图所需物体的数组
    // 0老家 1墙 2障碍 3出生效果 4河流 5草 6空气墙
    public GameObject[] item;
    // 已经有东西的位置列表
    private List<Vector3> itemPositionList = new List<Vector3>();

    private void Awake(){
        InitMap();
    }

    /// <summary>
    /// 初始化地图
    /// </summary>
    private void InitMap(){
        // 实例老家
        CreateItem(item[0], new Vector3(0, -8, 0), Quaternion.identity);
        // 用墙把老家围起来
        CreateItem(item[1], new Vector3(-1, -8, 0), Quaternion.identity);
        CreateItem(item[1], new Vector3(1, -8, 0), Quaternion.identity);
        for (int i = -1; i < 2; i++) {
            CreateItem(item[1], new Vector3(i, -7, 0), Quaternion.identity);
        }
        // 实例化外围墙
        for (int i = -11; i < 12; i++) {
            CreateItem(item[6],new Vector3(i,9,0),Quaternion.identity);
        }
        for (int i = -11; i < 12; i++) {
            CreateItem(item[6],new Vector3(i,-9,0),Quaternion.identity);
        }
        for (int i = -8; i < 9; i++) {
            CreateItem(item[6],new Vector3(-11,i,0),Quaternion.identity);
        }
        for (int i = -8; i < 9; i++) {
            CreateItem(item[6],new Vector3(11,i,0),Quaternion.identity);
        }
        
        // 初始化玩家
        GameObject player = Instantiate(item[3], new Vector3(-2, -8, 0), Quaternion.identity);
        player.GetComponent<Born>().createPlayer = true;
        
        // 产生敌人
        CreateItem(item[3],new Vector3(-10,8,0),quaternion.identity);
        CreateItem(item[3],new Vector3(0,8,0),quaternion.identity);
        CreateItem(item[3],new Vector3(10,8,0),quaternion.identity);
        InvokeRepeating("CreateEnemy",4,5);
        // 实例化地图
        // 随机实例化墙
        for (int i = 0; i < 20; i++) {
            CreateItem(item[1],CreateRandomPosition(),Quaternion.identity);
        }
        // 随机实例化障碍
        for (int i = 0; i < 20; i++) {
            CreateItem(item[2],CreateRandomPosition(),Quaternion.identity);
        }
        // 随机实例化河流
        for (int i = 0; i < 10; i++) {
            CreateItem(item[4],CreateRandomPosition(),Quaternion.identity);
        }
        // 随机实例化草
        for (int i = 0; i < 20; i++) {
            CreateItem(item[5],CreateRandomPosition(),Quaternion.identity);
        }
    }

    private void CreateItem(GameObject createGameObject, Vector3 createPosition, Quaternion createRotation){
        GameObject itemGo = Instantiate(createGameObject, createPosition, createRotation);
        itemGo.transform.SetParent(gameObject.transform);
        itemPositionList.Add(createPosition);
    }
    
    // 产生随机位置的方法
    private Vector3 CreateRandomPosition(){
        // 不生成x=-10,10的两列，y=-8,8的两行
        while (true) {
            Vector3 createPosition = new Vector3(Random.Range(-9, 10), Random.Range(-7, 8), 0);
            if (!HasThePosition(createPosition)) {
                return createPosition;
            }
        }
    }

    /// <summary>
    /// 判断位置列表中是否包含这个位置
    /// </summary>
    /// <param name="createPos"></param>
    /// <returns></returns>
    private bool HasThePosition(Vector3 createPos){
        for (int i = 0; i < itemPositionList.Count; i++) {
            if (createPos == itemPositionList[i]) {
                return true;
            }
        }

        return false;
    }

    /// <summary>
    /// 产生敌人的方法
    /// </summary>
    private void CreateEnemy(){
        int num = Random.Range(0, 3);
        Vector3 enemyPos = new Vector3();
        if (num == 0) {
            enemyPos = new Vector3(-10, 8, 0);
        }else if (num == 1) {
            enemyPos = new Vector3(0, 8, 0);
        }
        else {
            enemyPos = new Vector3(10, 8, 0);
        }
        CreateItem(item[3],enemyPos,Quaternion.identity);
    }
}
