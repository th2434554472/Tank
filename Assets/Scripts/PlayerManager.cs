using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// 玩家管理器
/// </summary>

public class PlayerManager : MonoBehaviour{
    // 玩家属性
    // 玩家生命值
    public int lifeValue = 3;
    // 玩家得分
    public int playerScore = 0;
    // 玩家死亡
    public bool isDead;
    // 游戏失败
    public bool isDefeat;

    // 引用
    public GameObject born;
    public TMP_Text playerScoreText;
    public TMP_Text playerLifeValueText;
    public GameObject isDefeatUI;
    
    private static PlayerManager instance;
    public static PlayerManager Instance{
        get => instance;
        set => instance = value;
    }

    private void Awake(){
        instance = this;
    }

    private void Update(){
        if (isDefeat) {
            isDefeatUI.SetActive(true);
            Invoke("ReturnToMainMenu",3);
            return;
        }
        if (isDead) {
            Recover();
        }

        playerScoreText.text = playerScore.ToString();
        playerLifeValueText.text = lifeValue.ToString();
    }

    /// <summary>
    /// 玩家死亡
    /// </summary>
    private void Recover(){
        if (isDefeat) {
            isDefeatUI.SetActive(true);
            return;
        }
        if (lifeValue <= 0) {
            // 游戏失败，返回主界面
            isDefeat = true;
            Invoke("ReturnToMainMenu",3);
        }
        else {
            lifeValue--;
            GameObject player = Instantiate(born, new Vector3(-2, -8, 0), Quaternion.identity);
            player.GetComponent<Born>().createPlayer = true;
            isDead = false;
        }
    }

    /// <summary>
    /// 返回主界面
    /// </summary>
    private void ReturnToMainMenu(){
        SceneManager.LoadScene(0);
    }
}
