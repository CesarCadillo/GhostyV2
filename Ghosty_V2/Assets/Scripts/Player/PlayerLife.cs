using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;

public class PlayerLife : MonoBehaviour
{
    [SerializeField] private GameObject deathFeedback;
    public int maxLife;
    public int currentLife;
    [SerializeField] public bool invulnerableState;
    public Transform spawn;
    ScoreCounter scoreScript;

    //deathAnimationVariables
    [SerializeField] private float secondsToGameOver;
    [SerializeField] private float GameOverNow;

    void Start()
    {
        invulnerableState = false;
        spawn = GameObject.Find("Spawn").GetComponent<Transform>();
        scoreScript = GameObject.Find("ScoreManager").GetComponent<ScoreCounter>();
    }

    void Update()
    {
        if (currentLife <= 0)
        {
            Death();
        }

        ChangeMaxLife();
    }

    public void ChangeLife(int life)
    {
        currentLife += life;

        if (currentLife > maxLife)
        {
            currentLife = maxLife;
        }
               

    }

    void ChangeMaxLife()
    {
        if (maxLife <= 5)
        {
            maxLife = 5;
        }
    }

    private void Death()
    {
        
        PlayerShoot playerShootScript = GetComponent<PlayerShoot>();
        PlayerMove playerMoveScript = GetComponent<PlayerMove>();
        BoxCollider playerCollider = GetComponent<BoxCollider>();
        Rigidbody playerRB = GetComponent<Rigidbody>();
        MeshRenderer playerMeshRenderer = GetComponent<MeshRenderer>();

        playerRB.velocity = Vector3.zero;
        playerRB.useGravity = false;
        playerShootScript.enabled = false;
        playerMoveScript.enabled = false;
        playerCollider.enabled = false;
        playerMeshRenderer.enabled = false;
        
        secondsToGameOver += Time.deltaTime;
        deathFeedback.SetActive(true);
        scoreScript.SaveHighScore();

        if (secondsToGameOver >= GameOverNow)
        {
            SceneManager.LoadScene("GameOver");
            Destroy(gameObject);
        }
        
    }

    protected virtual void OnCollisionEnter(Collision collision)
    {
        if (invulnerableState == false && collision.gameObject.CompareTag("Enemy"))
        {
            ChangeLife(-1);
        }
    }

    protected virtual void OnTriggerEnter(Collider other)
    {
        if (invulnerableState == false && other.gameObject.CompareTag("EnemyBullet"))
        {
            BulletBehaviourA enemyBullet = other.gameObject.GetComponent<BulletBehaviourA>();
            int damageRecieved = enemyBullet.Damage;
            ChangeLife(-damageRecieved);
        }

        if (other.gameObject.CompareTag("DeadZone") && currentLife >= 3)
        {
            transform.position = spawn.position;
            ChangeLife(-2);
        }
        else if (other.gameObject.CompareTag("DeadZone"))
        {
            ChangeLife(-2);
        }
    }



}
