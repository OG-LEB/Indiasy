using System.Collections;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Jobs;

public class KatanaBullet : MonoBehaviour
{
    public Vector3 hitPoint;
    [SerializeField] private int Speed;
    [SerializeField] private GameObject JukeHitObj;
    [SerializeField] private GameObject otherObjHitObj;

    private void Start()
    {
    }

    private void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.CompareTag("Enemy"))
        {
            Instantiate(JukeHitObj, hitPoint, Quaternion.identity);
            col.gameObject.GetComponent<EnemyScript>().GetDamage(1);
        }
        else
        {
            Instantiate(otherObjHitObj, hitPoint, Quaternion.identity);
           //Debug.Log("Hit other collider");
        }
        Destroy(this.gameObject);
    }

    public void MoveByRaycassHit() 
    {
        this.GetComponent<Rigidbody>().AddForce((hitPoint - this.transform.position).normalized * Speed);
    }
    public void MoveWithoutRayCast() 
    {
        this.GetComponent<Rigidbody>().AddForce((this.transform.forward).normalized * Speed);
        StartCoroutine(DeadByTime());
    }

    IEnumerator DeadByTime() 
    {
        yield return new WaitForSeconds(5f);
        Destroy(this.gameObject);
    }
}
