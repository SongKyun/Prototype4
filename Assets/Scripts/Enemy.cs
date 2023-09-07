using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed;
    private Rigidbody enemyRb;
    private GameObject player;

    void Start()
    {
        enemyRb = GetComponent<Rigidbody>();
        player = GameObject.Find("Player");
    }

    void Update()
    {
        Vector3 lookDirection = (player.transform.position - transform.position).normalized;

        // �ð��� �带���� ���� �����Ѵ�. -> ���� ���� ����ȭ�� �Ͽ� �����ϰ� �ӵ��� �����Ѵ�.
        // ������ ���� ����ϰ� �ӵ��� ������� �Ѵ�.
        enemyRb.AddForce(lookDirection * speed);

        if(transform.position.y < -10)
        {
            Destroy(gameObject);
        }
    }
}
