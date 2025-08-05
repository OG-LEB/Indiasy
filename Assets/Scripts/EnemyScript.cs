using System.Collections;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    [SerializeField] private int health = 3;
    [SerializeField] private bool isMoving;
    private Transform KotelTranform;
    [SerializeField] private float distanceToDamage;
    [SerializeField] private float MoveSpeed;
    [SerializeField] private float PushBackForce;
    private Rigidbody rb;
    private Transform playerTransform;
    [SerializeField] private float DamageAmount;
    [SerializeField] private float DamageCoolDown;
    private LevelScript levelScript;
    private bool damaging;

    [Space]
    [Header("Sound")]
    [SerializeField] private GameObject DeathSound;
    private AudioSource WalkSound;

    private void Start()
    {
        isMoving = true;
        rb = GetComponent<Rigidbody>();
        levelScript = LevelScript.GetInstance();
        WalkSound = GetComponent<AudioSource>();
    }

    //public void GetDamage(PlayerKatanaMeeleTrigger trigger) 
    //{
    //    health--;
    //    if (health <= 0)
    //    {
    //        Die();
    //    }
    //}
    public void GetDamage(int damageAmount)
    {
        health -= damageAmount;
        Vector3 direction = (transform.position - playerTransform.position).normalized;
        rb.AddForce(direction * PushBackForce + Vector3.up);
        if (health <= 0)
        {
            Die();
        }
    }
    private void Die()
    {
        Instantiate(DeathSound, transform.position, Quaternion.identity);
        EnemySpawner.GetInstance().RemoveEnemyFromList(this);
        Destroy(gameObject);
    }
    private void Update()
    {
        if (KotelTranform == null)
        {
            KotelTranform = LevelScript.GetInstance().GetKotelTransform();
        }
        if (playerTransform == null)
        {
            playerTransform = LevelScript.GetInstance().GetPlayerTransform();
        }
        Movement();
        if (isMoving)
        {
            if (!WalkSound.isPlaying)
            {
                Debug.Log("Playing enemy steps");
                WalkSound.Play();
            }
        }
        else
        {
            if (WalkSound.isPlaying)
            {

                Debug.Log("Stop enemy steps");
                WalkSound.Stop();
            }
        }
    }
    private void Movement()
    {
        if (isMoving)
        {
            transform.position = Vector3.MoveTowards(transform.position, KotelTranform.position, Time.deltaTime * MoveSpeed);
            if (Vector3.Distance(transform.position, KotelTranform.position) <= distanceToDamage)
            {
                isMoving = false;
                if (!damaging)
                {
                    damaging = true;
                    StartCoroutine(Damage());
                }
            }
        }
        else
        {
            if (Vector3.Distance(transform.position, KotelTranform.position) > distanceToDamage)
            {
                isMoving = true;
            }
        }
    }
    private IEnumerator Damage()
    {
        SoundController.GetInstance().PlayKotelDamage();
        levelScript.GetDamageFromBug(DamageAmount);
        yield return new WaitForSeconds(DamageCoolDown);
        StartCoroutine(Damage());
    }
}

