using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 
/// </summary>

public class Player : MonoBehaviour{

    // 属性值
    // 移动速度
    public float moveSpeed = 3;
    // 子弹发射角度
    private Vector3 bulletEulerAngles;
    // 子弹CD
    private float timeVal;
    // 无敌状态
    private bool isDefended = true;
    // 无敌时间
    private float defendTimeVal = 3;
    
    // 引用
    private SpriteRenderer sr;
    public Sprite[] tankSprites;// 坦克方向：上右下左 
    private Animator animator;
    
    public GameObject bulletPrefab;
    public GameObject explosionPrefab;
    public GameObject defendEffectPrefab;
    public AudioSource moveAudio;
    public AudioClip[] tankAudio;

    private void Awake(){
        sr = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    private void Update(){
        
        // 是否处于无敌状态
        if (isDefended) {
            defendEffectPrefab.SetActive(true);
            defendTimeVal -= Time.deltaTime;
            if (defendTimeVal <= 0) {
                isDefended = false;
                defendEffectPrefab.SetActive(false);
            }
        }
    }

    private void FixedUpdate(){
        if (PlayerManager.Instance.isDefeat) {
            return;
        }
        Move();
        // 攻击的CD
        if (timeVal > 0.4f) {
            Attack();
        }
        else {
            timeVal += Time.fixedDeltaTime;
        }
    }

    /// <summary>
    /// 坦克攻击
    /// </summary>
    private void Attack(){
        if (Input.GetKeyDown(KeyCode.Space)) {
            // 子弹产生的角度：当前坦克的角度+子弹应该旋转的角度
            Instantiate(bulletPrefab, transform.position, 
                Quaternion.Euler(transform.eulerAngles + bulletEulerAngles));
        }
    }
    
    /// <summary>
    /// 坦克移动
    /// </summary>
    private void Move(){
        float h = Input.GetAxisRaw("Horizontal");
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
        
        if (Mathf.Abs(h) > 0.05f) {
            moveAudio.clip = tankAudio[1];
            if (!moveAudio.isPlaying) {
                moveAudio.Play();
            }
        }

        if (h != 0) {
            return;
        }
        float v = Input.GetAxisRaw("Vertical");
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

        if (Mathf.Abs(v) > 0.05f) {
            moveAudio.clip = tankAudio[1];
            if (!moveAudio.isPlaying) {
                moveAudio.Play();
            }
        }
        else {
            moveAudio.clip = tankAudio[0];
            if (!moveAudio.isPlaying) {
                moveAudio.Play();
            }
        }
    }

    /// <summary>
    /// 坦克死亡
    /// </summary>
    private void Die(){
        if (isDefended) {
            return;
        }

        PlayerManager.Instance.isDead = true;
        // 产生爆炸特效
        Instantiate(explosionPrefab, transform.position, transform.rotation);
        // 死亡
        Destroy(gameObject);
    }
}
