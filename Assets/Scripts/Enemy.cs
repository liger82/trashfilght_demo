using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private GameObject coin;
    [SerializeField]
    private float moveSpeed = 10f;
    private float minY = -7f;
    
    [SerializeField]
    // 체력
    private float hp = 1f;

    public void setMoveSpeed(float moveSpeed){
        this.moveSpeed = moveSpeed;
    }
    // Update is called once per frame
    void Update()
    {
        transform.position += Vector3.down * moveSpeed * Time.deltaTime;
        if (transform.position.y < minY){
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        // 충돌하면 여기로 들어옴
        // Weapon tag인지 확인
        if (other.gameObject.tag == "Weapon"){
            Weapon weapon = other.gameObject.GetComponent<Weapon>();
            hp -= weapon.damage;
            if (hp <= 0){
                if (gameObject.tag == "Boss"){
                    GameManager.instance.SetGameOver();
                }
                // 적의 체력을 소진시켰으면 없앰.
                Destroy(gameObject);
                // 코인 생성
                Instantiate(coin, transform.position, Quaternion.identity);

            }
            // 미사일도 지우기
            Destroy(other.gameObject);
        }
    }
}
