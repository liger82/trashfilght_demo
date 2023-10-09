using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{   
    // 유니티 툴에서 드래그앤드랍으로 넣어줌
    [SerializeField]
    // enemy 여러 개라서 배열로 만듦
    private GameObject[] enemies;
    [SerializeField]
    private GameObject boss;
    private float[] arrPosX = {-2.2f, -1.1f, 0f, 1.1f, 2.2f};

    [SerializeField]
    private float spawnInterval = 1.5f;

    // Start is called before the first frame update
    void Start()
    {
        StartEnemyRoutine();
    }

    void StartEnemyRoutine(){
        StartCoroutine("EnemyRoutine");
    }

    public void StopEnemyRoutine(){
        StopCoroutine("EnemyRoutine");
    }
    IEnumerator EnemyRoutine() {
        yield return new WaitForSeconds(3f);
        
        float moveSpeed = 5f;
        // 처음엔 쉽게 0번째 인덱스의 적만 나오다가
        // 특정 단계를 넘으면 
        int spawnCount = 0;
        int enemyIndex = 0;
        while (true){
            foreach (float posX in arrPosX){
                SpawnEnemy(posX, enemyIndex, moveSpeed);
            }
            spawnCount++;
            // 10씩 증가할 때마다
            if (spawnCount % 10==0){
                enemyIndex++;
                moveSpeed+=2;

            }
            if (enemyIndex >= enemies.Length){
                // 보스 등장
                SpawnBoss();
                // 짜바리들은 약하게 바꾸기
                enemyIndex = 0;
                moveSpeed = 5f;
            }
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    void SpawnEnemy(float posX, int index, float moveSpeed){
        Vector3 spawnPos = new Vector3(posX, transform.position.y, transform.position.z);
        // 20% 확률로 한단계 높은 적을 만든다.
        if (Random.Range(0, 5)==0){
            index++;
        }
        // 아무리 커져도 인덱스는 그 한계가 있음
        if (index >= enemies.Length){
            index = enemies.Length -1;
        }
        GameObject enemyObject = Instantiate(enemies[index], spawnPos, Quaternion.identity); 
        // Enemy 내의 접근 허용 가능한 변수에 접근 가능
        Enemy enemy = enemyObject.GetComponent<Enemy>();
        // moveSpeed 적용
        enemy.setMoveSpeed(moveSpeed);
    }
    
    void SpawnBoss(){
        Instantiate(boss, transform.position, Quaternion.identity);
    }
}
