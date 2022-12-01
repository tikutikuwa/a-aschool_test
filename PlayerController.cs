using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    Camera cam;
    public Transform cameraView;

    public Rigidbody rbody;
    public float moveSpeed;
    public float jumpPower;

    public bool isGrounded;

    public GameObject hitEffect, enemyHitEffect;

    public int maxHp;//最大HP
    int currentHp;   //現在のHP

    public Slider playerHpSlider;


    void Start()
    {
        cam = Camera.main;
        Cursor.lockState = CursorLockMode.Locked;

        playerHpSlider.maxValue = maxHp;
        currentHp = maxHp;
        UpdatePlayerHpSlider();
    }

    void Update()
    {
        Move();

        if (Input.GetMouseButtonDown(0))
        {
            Shoot();
        }
    }

    private void LateUpdate()
    {
        // マウス移動で左右を向く
        transform.Rotate(0, Input.GetAxis("Mouse X"), 0);
        // マウス移動で上下を向く
        cameraView.Rotate(-Input.GetAxis("Mouse Y"), 0, 0);

        // カメラの角度を制限する
        float camAngle = cameraView.eulerAngles.x;
        if (camAngle > 30 && camAngle < 330)
        {
            cameraView.Rotate(Input.GetAxis("Mouse Y"), 0, 0);
        }
        cam.transform.position = cameraView.position;
        cam.transform.rotation = cameraView.rotation;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Ground")
        {
            isGrounded = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Ground")
        {
            isGrounded = false;
        }
    }

    void Move()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        Vector3 movement = (transform.right * h + transform.forward * v).normalized * moveSpeed;

        if (Input.GetKeyDown("space") && isGrounded)
        {
            movement.y = jumpPower;
        }
        else
        {
            movement.y = rbody.velocity.y;
        }

        rbody.velocity = movement;
    }

    void Shoot()
    {
        Ray ray = cam.ViewportPointToRay(new Vector2(0.5f, 0.5f));//メインカメラから、ゲーム画面中心に向かう光線を作成

        if (Physics.Raycast(ray, out RaycastHit hit))//光線が何かに当たったら
        {
            if (hit.transform.tag == "Enemy")
            {
                Instantiate(enemyHitEffect, hit.point, Quaternion.identity);

                hit.transform.GetComponent<Enemy>().OnAttacked();
            }
            else
            {
                Instantiate(hitEffect, hit.point, Quaternion.identity);
            }
        }
    }

    void UpdatePlayerHpSlider()
    {
        playerHpSlider.value = currentHp;
    }

    public void OnAttacked()//(敵に)攻撃された時
    {
        currentHp--;
        UpdatePlayerHpSlider();
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            OnAttacked();
        }
    }
}
