using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;
    
    [SerializeField]
    private TextMeshProUGUI text;

    [SerializeField]
    private GameObject gameOverPanel;
    [SerializeField]
    private GameObject bgm;


    private int coin = 0;

    [HideInInspector]
    public bool isGameOver = false;

    // singleton pattern
    void Awake() {
        if (instance == null){
            instance = this;
        }
    }

    public void IncreaseCoin(){
        coin += 1;
        // ScorePanel에 등록하기
        text.SetText(coin.ToString()); 

        if (coin % 30 == 0){
            Player player = FindObjectOfType<Player>();
            if (player != null){
                player.Upgrade();
            }
        }
    }
    public void SetGameOver(){
        isGameOver = true;
        // 적을 더 이상 만들지 않게 하기
        EnemySpawner enemySpawner = FindObjectOfType<EnemySpawner>();
        if (enemySpawner != null){
            enemySpawner.StopEnemyRoutine();
        }
        // 특정 시간 뒤에 해당 함수를 호출
        Invoke("ShowGameOverPanel", 1f);
        
    }

    void ShowGameOverPanel(){
        // GameOverPanel 활성화
        gameOverPanel.SetActive(true);
        bgm.SetActive(false);

    }

    public void PlayAgain(){
        // 새롭게 SampleScene을 불러옴
        SceneManager.LoadScene("SampleScene");
    }
}
