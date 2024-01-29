using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BlockName
{
    Grass,
    Slippery,
    Slow
}

public class BlockFloorID : MonoBehaviour
{
    [SerializeField] BlockName BlockIdentification;
    [SerializeField] AudioClip sonidoSouls;   
    [SerializeField] AudioSource audioSource;
    PlayerMove playerMoveScript;

    void Start()
    {
        if (GameObject.Find("Player") != null)
            playerMoveScript = GameObject.Find("Player").GetComponent<PlayerMove>();
    }

    void Update()
    {
        if (BlockIdentification != BlockName.Slow)
        {
            GetComponent<AudioSource>().Stop();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            switch (BlockIdentification)
            {
                case BlockName.Grass:
                    break;

                case BlockName.Slippery:
                    playerMoveScript.isCollidingWithSlippery = true;
                    break;

                case BlockName.Slow:
                    GetComponent<AudioSource>().PlayOneShot(sonidoSouls);
                    playerMoveScript.isCollidingWithSlow = true;
                    break;

                default:
                    break;
            }
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            switch (BlockIdentification)
            {
                case BlockName.Grass:
                    break;

                case BlockName.Slippery:
                    playerMoveScript.isCollidingWithSlippery = false;
                    break;

                case BlockName.Slow:
                    playerMoveScript.isCollidingWithSlow = false;
                    break;

                default:
                    break;
            }
        }
    }
}
