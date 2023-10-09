using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{   
    // unity에서 통제할 수 있게 함
    [SerializeField]
    private float moveSpeed;

    [SerializeField]
    private GameObject[] weapons;
    private int weaponIndex = 0;

    [SerializeField]
    private Transform shootTransform;

    [SerializeField]
    private float shootInterval = 0.05f;
    private float lastShotTime = 0f;

    // Update is called once per frame
    void Update()
    {   
        // // 키보드 입력 받는 것(좌우)
        // float horizontalInput = Input.GetAxisRaw("Horizontal");
        // // 키보드 입력(상하)
        // float verticalInput = Input.GetAxisRaw("Vertical");
        // Vector3 moveto = new Vector3(horizontalInput, verticalInput, 0f);
        // transform.position += moveto * moveSpeed * Time.deltaTime;
        
        // 키보드로 제어
        // Vector3 moveto = new Vector3(moveSpeed * Time.deltaTime, 0f, 0f);
        // if (Input.GetKey(KeyCode.LeftArrow)){
        //     transform.position -= moveto;
        // } else if (Input.GetKey(KeyCode.RightArrow)){
        //     transform.position += moveto;
        // }

        // 마우스로 제어
        // 싱크 맞추기
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        // 최소, 최대값 설정
        float toX = Mathf.Clamp(mousePos.x, -2.35f, 2.35f);
        transform.position = new Vector3(toX, transform.position.y, transform.position.z);
        if (GameManager.instance.isGameOver == false){
            Shoot();
        }
    }
    
    void Shoot(){
        if (Time.time - lastShotTime > shootInterval){
            Instantiate(weapons[weaponIndex], shootTransform.position, Quaternion.identity);
            lastShotTime = Time.time;
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.tag=="Enemy"|| other.gameObject.tag == "Boss"){
            // 부딪히면 게임 오버
            GameManager.instance.SetGameOver();
            // 부딪히면 끝~ 객체 지움
            Destroy(gameObject);
        } else if (other.gameObject.tag=="Coin"){
            // 코인 증가
            GameManager.instance.IncreaseCoin();
            Destroy(other.gameObject);
        }
    }

    public void Upgrade(){
        weaponIndex += 1;
        if (weaponIndex >= weapons.Length){
            weaponIndex = weapons.Length - 1;
        }
    }
}
