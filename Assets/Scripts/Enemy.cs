using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

/// <summary>
/// 敌人类
/// </summary>

public class Enemy : MonoBehaviour{
    // 属性值
    // 移动速度
    public float moveSpeed = 3;
    // 子弹发射角度
    private Vector3 bulletEulerAngles;
    // 子弹CD
    private float timeVal;
    private float timeValChangeDirection;
    private float v = -1;
    private float h;
    
    // 引用
    private SpriteRenderer sr;
    public Sprite[] tankSprites;// 坦克方向：上右下左 
    public GameObject bulletPrefab;
    public GameObject explosionPrefab;

    private void Awake(){
        sr = GetComponent<SpriteRenderer>();
    }

    private void Update(){
        
        // 攻击的CD
        if (timeVal >= 3) {
            Attack();
        }
        else {
            timeVal += Time.deltaTime;
        }
    }

    private void FixedUpdate(){
        Move();
    }

    /// <summary>
    /// 攻击方法
    /// </summary>
    private void Attack(){
        Instantiate(bulletPrefab, transform.position,
            Quaternion.Euler(transform.eulerAngles + bulletEulerAngles));
        timeVal = 0;
    }
    
    /// <summary>
    /// 坦克移动
    /// </summary>
    private void Move(){
        if (timeValChangeDirection >= 4) {
            int num = Random.Range(0, 8);
            if (num > 5) {
                v = -1;
                h = 0;
            }
            else if (num == 0) {
                v = 1;
                h = 0;
            }else if (num > 0 && num <= 2) {
                v = 0;
                h = -1;
            }else if (num > 2 && num <= 4) {
                v = 0;
                h = 1;
            }

            timeValChangeDirection = 0;
        }
        else {
            timeValChangeDirection += Time.fixedDeltaTime;
        }
        // 水平方向移动
        transform.Translate(Vector3.right * h * moveSpeed * Time.fixedDeltaTime,Space.World);
        if (h < 0) {
            sr.sprite = tankSprites[3];
            bulletEulerAngles = new Vector3(0, 0, 90);
        }
        else if(h > 0){
            sr.sprite = tankSprites[1];
            bulletEulerAngles = new Vector3(0, 0, -90);
        }

        if (h != 0) {
            return;
        }
        // 垂直方向移动
        transform.Translate(Vector3.up * v * moveSpeed * Time.fixedDeltaTime,Space.World);
        if (v < 0) {
            sr.sprite = tankSprites[2];
            bulletEulerAngles = new Vector3(0, 0, -180);
        }
        else if(v > 0){
            sr.sprite = tankSprites[0];
            bulletEulerAngles = new Vector3(0, 0, 0);
        }
    }

    /// <summary>
    /// 坦克死亡
    /// </summary>
    private void Die(){
        PlayerManager.Instance.playerScore++;
        // 产生爆炸特效
        Instantiate(explosionPrefab, transform.position, transform.rotation);
        // 死亡
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision){
        if (collision.gameObject.tag == "Enemy") {
            timeValChangeDirection = 4;
        }
    }
}
