using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XplotionObject : MonoBehaviour
{
    public Material redMaterial;
    public GameObject explosionParticles;
    public float explosionRadius = 5f;


    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine(ExplodeSequence());
        }
    }

    private void OnTriggerEnter(Collider other)
    {        
        if (other.gameObject.CompareTag("PlayerLightBullet") || other.gameObject.CompareTag("PlayerDarkBullet"))
        {
            StartCoroutine(ExplodeSequence());
        }
    }

    IEnumerator ExplodeSequence()
    {
        GetComponent<Renderer>().material = redMaterial;

        yield return new WaitForSeconds(1f);

        Instantiate(explosionParticles, transform.position, Quaternion.identity);

        ApplyDamage();

        Destroy(gameObject);
    }

    void ApplyDamage()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius);

        foreach (var collider in colliders)
        {
            if (collider.CompareTag("Player"))
            {
                collider.GetComponent<PlayerLife>().ChangeLife(-2);
            }
            else if (collider.CompareTag("Enemy"))
            {
                collider.GetComponent<EnemyLife>().ChangeLife(-5);
            }
        }
    }
}