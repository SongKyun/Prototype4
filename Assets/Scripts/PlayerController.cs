using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed;
    public bool hasPowerup = false;
    public GameObject powerupIndicator;

    private float powerUpStrength = 15.0f;
    private Rigidbody playerrb;
    private GameObject focalPoint;

    void Start()
    {
        playerrb = GetComponent<Rigidbody>();
        focalPoint = GameObject.Find("Focal Point");
    }

    void Update()
    {
        float forwardInput = Input.GetAxis("Vertical");
        
        playerrb.AddForce(focalPoint.transform.forward * speed * forwardInput);

        // 인디케이터의 위치를 고정해서 붙여준다
        powerupIndicator.transform.position = transform.position + new Vector3(0, -0.5f, 0);
    }

    // 트리거 검출 시에 활용함
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Powerup"))
        {
            hasPowerup = true;
            powerupIndicator.gameObject.SetActive(true);
            Destroy(other.gameObject);
            StartCoroutine(PowerupCountdownRoutine());
        }
    }

    IEnumerator PowerupCountdownRoutine()
    {
        yield return new WaitForSeconds(4);
        hasPowerup = false;
        powerupIndicator.gameObject.SetActive(false);
    }

    // 물리 충돌 시에 활용함
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Enemy") && hasPowerup)
        {
            Rigidbody enemyRigidbody = collision.gameObject.GetComponent<Rigidbody>();
            Vector3 awayFromPlayer = collision.gameObject.transform.position - transform.position;

            enemyRigidbody.AddForce(awayFromPlayer * powerUpStrength, ForceMode.Impulse);
            Debug.Log("Collided with: " + collision.gameObject.name + "with powerup set to " + hasPowerup);
        }
    }
}
