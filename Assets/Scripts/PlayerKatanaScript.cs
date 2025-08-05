using System;
using System.Collections;
using UnityEngine;

public class PlayerKatanaScript : MonoBehaviour
{
    [SerializeField] private Transform Katana;
    [SerializeField] private float timer = 0;
    [SerializeField] private Vector3 offset;
    [SerializeField] private float bobAmount;
    [SerializeField] private float TimeToBob;
    private Rigidbody rb;
    [SerializeField] private float MoveMagnitude;

    private bool ReadyToRun = false;
    private bool ReadyToIdle = false;
    private bool Shooting = false;
    [SerializeField] private SpriteRenderer spriteRender;

    [Header("Sprites")]
    [SerializeField] private Sprite WalkSprite;
    [SerializeField] private Sprite ShootAttackSprite;

    [Header("Shooting logic")]
    [SerializeField] private GameObject Bullet;
    [SerializeField] private Transform playerCameraTransform;
    [SerializeField] private float MaxspreadAngle;
    [SerializeField] private float accuracy;
    [SerializeField] private float timeTillMaxSpread;
    [SerializeField] private Transform ShootPoint;
    //private int counter = 0;
    [SerializeField] private float shootingTimer = 0f;
    [SerializeField] private float shootingDelay;

    [Header("MeeleAtack")]
    [SerializeField] private Sprite[] MeeleAttackSprites;
    [SerializeField] private bool MeeleAtacking = false;
    [SerializeField] private float SecondsBetweenSpritesInMeeleAtack;
    [SerializeField] private PlayerKatanaMeeleTrigger KatanaTrigger;
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }
    private void Update()
    {
        MoveMagnitude = rb.velocity.magnitude;
        if (rb.velocity.magnitude > 0.1f)
        {
            if (timer < 1 && ReadyToRun)
            {
                timer += 0.01f;
                Katana.transform.localPosition = Vector3.Lerp(Katana.transform.localPosition, new Vector3(Mathf.Sin(Time.time * TimeToBob) * bobAmount, Mathf.Sin(Time.time * TimeToBob) * bobAmount, 0) + offset, timer);
            }
            else
            {
                float LocalTimer = Time.time * TimeToBob;
                ReadyToRun = false;
                ReadyToIdle = true;
                Katana.localPosition = new Vector3(Mathf.Sin(LocalTimer) * bobAmount, Mathf.Sin(LocalTimer) * bobAmount, 0) + offset;
                timer = 0;
            }
        }
        else
        {
            if (timer < 1 && ReadyToIdle)
            {
                timer += 0.01f;
                Katana.transform.localPosition = Vector3.Lerp(Katana.transform.localPosition, new Vector3(0, Mathf.Sin(Time.time * (TimeToBob / 2)) * (bobAmount / 2), 0) + offset, timer);
            }
            else
            {
                timer = Time.time * (TimeToBob / 2);
                ReadyToRun = true;
                ReadyToIdle = false;
                Katana.localPosition = new Vector3(0, Mathf.Sin(timer) * (bobAmount / 2), 0) + offset;
                timer = 0;
            }


        }

        if (Input.GetMouseButtonDown(0) && !MeeleAtacking && !Shooting)
        {

            Shooting = true;

            spriteRender.sprite = ShootAttackSprite;
        }
        if (Input.GetMouseButtonUp(0) && Shooting)
        {

            Shooting = false;
            spriteRender.sprite = WalkSprite;
        }

        if (Input.GetMouseButtonDown(1) && !Shooting && !MeeleAtacking)
        {
            StopAllCoroutines();
            MeeleAtacking = true;
            StartCoroutine(MeeleAtack());
        }

        ShootLoop();

        //MeeleRay debug
        //Debug.DrawRay(playerCameraTransform.forward, Vector3.forward, Color.blue, 2f);
        Debug.DrawRay(playerCameraTransform.position, playerCameraTransform.forward * 3f, Color.blue);
    }

    private void ShootLoop()
    {
        if (Shooting)
        {
            if (shootingTimer == 0f)
            {
                Shoot();
            }
            shootingTimer += Time.deltaTime;

            if (shootingTimer >= shootingDelay)
            {
                shootingTimer = 0f;
            }
        }
    }
    private void Shoot()
    {
        SoundController.GetInstance().PlayeShootSound();
        RaycastHit hit;
        Quaternion fireRotation = Quaternion.LookRotation(playerCameraTransform.forward);
        float currentSpread = Mathf.Lerp(0.0f, MaxspreadAngle, accuracy / timeTillMaxSpread);
        fireRotation = Quaternion.RotateTowards(fireRotation, UnityEngine.Random.rotation, UnityEngine.Random.Range(0.0f, currentSpread));

        if (Physics.Raycast(playerCameraTransform.position, fireRotation * Vector3.forward, out hit, Mathf.Infinity))
        {
            GameObject bullet = Instantiate(Bullet, ShootPoint.position, fireRotation);
            bullet.GetComponent<KatanaBullet>().hitPoint = hit.point;
            bullet.GetComponent<KatanaBullet>().MoveByRaycassHit();
        }
        else
        {
            GameObject bullet = Instantiate(Bullet, ShootPoint.position, fireRotation);
            bullet.GetComponent<KatanaBullet>().MoveWithoutRayCast();

        }
        //Audio
    }


    IEnumerator MeeleAtack() 
    {
        MeeleAtackRayCast();
        for (int i = 0; i < MeeleAttackSprites.Length; i++)
        {
            spriteRender.sprite = MeeleAttackSprites[i];
            yield return new WaitForSeconds(SecondsBetweenSpritesInMeeleAtack);
        }
        MeeleAtacking = false;
    }
    private void MeeleAtackRayCast() 
    {
        SoundController.GetInstance().PlayeMeeleAtack();

        //Debug.DrawRay(playerCameraTransform.position, playerCameraTransform.forward * 3, Color.cyan, 1);
        //Debug.DrawRay(playerCameraTransform.position, playerCameraTransform.forward * 3 + playerCameraTransform.right * 0.75f, Color.cyan,1);
        //Debug.DrawRay(playerCameraTransform.position, playerCameraTransform.forward * 3 + playerCameraTransform.right * 1.5f, Color.cyan,1);
        //Debug.DrawRay(playerCameraTransform.position, playerCameraTransform.forward * 3 + playerCameraTransform.right * 2.5f, Color.cyan,1);
        //Debug.DrawRay(playerCameraTransform.position, playerCameraTransform.forward * 3 + playerCameraTransform.right * -0.75f, Color.cyan,1);
        //Debug.DrawRay(playerCameraTransform.position, playerCameraTransform.forward * 3 + playerCameraTransform.right * -1.5f, Color.cyan,1);
        //Debug.DrawRay(playerCameraTransform.position, playerCameraTransform.forward * 3 + playerCameraTransform.right * -2.5f, Color.cyan,1);


        RaycastHit hit;

        if (Physics.Raycast(playerCameraTransform.position, playerCameraTransform.forward, out hit, 6f))
        {
            if (hit.transform.CompareTag("Enemy")) 
            {
                hit.transform.GetComponent<EnemyScript>().GetDamage(10);
            }
        }
        if (Physics.Raycast(playerCameraTransform.position, playerCameraTransform.forward + playerCameraTransform.right * 0.75f, out hit, 6f))
        {
            if (hit.transform.CompareTag("Enemy"))
            {
                hit.transform.GetComponent<EnemyScript>().GetDamage(10);
            }
        }
        if (Physics.Raycast(playerCameraTransform.position, playerCameraTransform.forward + playerCameraTransform.right * 1.5f, out hit, 6f))
        {
            if (hit.transform.CompareTag("Enemy"))
            {
                hit.transform.GetComponent<EnemyScript>().GetDamage(10);
            }
        }
        if (Physics.Raycast(playerCameraTransform.position, playerCameraTransform.forward + playerCameraTransform.right * 2.5f, out hit, 6f))
        {
            if (hit.transform.CompareTag("Enemy"))
            {
                hit.transform.GetComponent<EnemyScript>().GetDamage(10);
            }
        }
        if (Physics.Raycast(playerCameraTransform.position, playerCameraTransform.forward + playerCameraTransform.right * -0.75f, out hit, 6f))
        {
            if (hit.transform.CompareTag("Enemy"))
            {
                hit.transform.GetComponent<EnemyScript>().GetDamage(10);
            }
        }
        if (Physics.Raycast(playerCameraTransform.position, playerCameraTransform.forward + playerCameraTransform.right * -1.5f, out hit, 6f))
        {
            if (hit.transform.CompareTag("Enemy"))
            {
                hit.transform.GetComponent<EnemyScript>().GetDamage(10);
            }
        }
        if (Physics.Raycast(playerCameraTransform.position, playerCameraTransform.forward + playerCameraTransform.right * -2.5f, out hit, 6f))
        {
            if (hit.transform.CompareTag("Enemy"))
            {
                hit.transform.GetComponent<EnemyScript>().GetDamage(10);
            }
        }
    }
    public void ChooseMechanicWindowShootingFix() 
    {
        Shooting = false;
        spriteRender.sprite = WalkSprite;
    }
}
