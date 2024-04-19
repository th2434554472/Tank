using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 子弹类
/// </summary>

public class Bullet : MonoBehaviour{

    public float moveSpeed = 10;
    public bool isPlayerBullet;

    private void Update(){
        transform.Translate(transform.up * moveSpeed * Time.deltaTime,Space.World);
    }

    private void OnTriggerEnter2D(Collider2D collision){
        switch (collision.tag) {
            case "Tank":
                if (!isPlayerBullet) {
                    collision.SendMessage("Die");
                    Destroy(gameObject);
                }
                break;
            case "Heart":
                collision.SendMessage("Die");
                Destroy(gameObject);
                break;
            case "Enemy":
                if (isPlayerBullet) {
                    collision.SendMessage("Die");
                    Destroy(gameObject);
                }
                break;
            case "Wall":
                // 销毁子弹
                Destroy(collision.gameObject);
                // 销毁墙
                Destroy(gameObject);
                break;
            case "Barrier":
                if (isPlayerBullet) {
                    collision.SendMessage("PlayAudio");
                }
                Destroy(gameObject);
                break;
            default:
                break;
        }
    }
}
