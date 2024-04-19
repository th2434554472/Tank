using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 
/// </summary>

public class Heart : MonoBehaviour{
    private SpriteRenderer sr;
    public Sprite brokenSprite;
    public GameObject explosionPrefab;
    public AudioClip dieAudio;

    private void Start(){
        sr = GetComponent<SpriteRenderer>();
    }

    public void Die(){
        sr.sprite = brokenSprite;
        Instantiate(explosionPrefab, transform.position, transform.rotation);
        PlayerManager.Instance.isDefeat = true;
        AudioSource.PlayClipAtPoint(dieAudio, transform.position);
    }
}
