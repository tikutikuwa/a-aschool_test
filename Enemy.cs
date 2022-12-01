using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    public int maxHp;//最大HP
    int currentHp;   //現在のHP

    public GameObject enemyDeathEffect;

    public Slider hpSlider;

    public Transform target;

    public GameObject bullet;

    // Start is called before the first frame update
    void Start()
    {
        hpSlider.maxValue = maxHp;
        currentHp = maxHp;
        UpdateHpSlider();

        InvokeRepeating(nameof(EnemyAttack), 2, 3);
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(target);
    }

    public void OnAttacked()//(プレイヤーに)攻撃された時
    {
        currentHp--;
        UpdateHpSlider();

        if (currentHp <= 0)
        {
            Destroy(gameObject);
            Instantiate(enemyDeathEffect, transform.position, Quaternion.identity);
        }
    }

    void UpdateHpSlider()
    {
        hpSlider.value = currentHp;
    }

    void EnemyAttack()
    {
        Instantiate(bullet, transform.position + transform.forward, transform.rotation);
    }
}
