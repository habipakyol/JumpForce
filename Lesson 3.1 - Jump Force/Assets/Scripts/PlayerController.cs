using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Timeline;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public Rigidbody rb;

    public float jumpforce;
    public bool doubleJumpUsed = false;
    public float doubleJumpForce;
    public float gravityModifier;

    public bool isGrounded = true;
    public bool gameover;
    public bool doubleSpeed = false;

    public GameObject rg;

    public Animator playerAnim;

    public ParticleSystem explosionParticle;
    public ParticleSystem dirtParticle;

    public AudioClip jumpSound;
    public AudioClip crashSound;
    public AudioClip finishSound;
    private AudioSource playerAudio;
    public AudioSource mainCameraAudioSource;
    public AudioClip pointSound;

    GameManager gameManager;
   

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        Physics.gravity *= gravityModifier;
        playerAnim = GetComponent<Animator>();
        playerAudio = GetComponent<AudioSource>();
        //gameManager = GetComponent<GameManager>();
        gameManager = GameObject.FindObjectOfType<GameManager>();
        
    }

    // Update is called once per frame
    void Update()
    {
        JumpControl();
        hardSpeed();
       
    }

    private void OnCollisionEnter(Collision collision)
    {
        
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
            dirtParticle.Play();
        }
        else if (collision.gameObject.CompareTag("Obstacle"))
        {
            gameover = true;
            Debug.Log("Game Over!");
            playerAnim.SetBool("Death_b", true);
            playerAnim.SetInteger("DeathType_int", 1);
            explosionParticle.Play();
            dirtParticle.Stop();
            Destroy(collision.gameObject);
            playerAudio.PlayOneShot(crashSound, 1.0f);
            playerAudio.PlayOneShot(finishSound, 1.0f);
            mainCameraAudioSource.mute = true;
            rg.SetActive(true);
             
        }
        
        if (collision.gameObject.CompareTag("Coin"))
        {
            gameManager.AddScore(2);
            playerAudio.PlayOneShot(pointSound,1.0f);
            Destroy(collision.gameObject);
            
        }
    }

    public void JumpControl()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded && !gameover)
        {
            rb.AddForce(Vector3.up * jumpforce, ForceMode.Impulse);
            isGrounded = false;
            playerAnim.SetTrigger("Jump_trig");
            dirtParticle.Stop();
            playerAudio.PlayOneShot(jumpSound, 1.0f);
            doubleJumpUsed = false;
        }
        else if (Input.GetKeyDown(KeyCode.Space) && !isGrounded && !doubleJumpUsed)
        {
            doubleJumpUsed = true;
            rb.AddForce(Vector3.up * doubleJumpForce, ForceMode.Impulse);
            playerAnim.Play("Running_Jump", 3, 0f);
            playerAudio.PlayOneShot(jumpSound, 1.0f);
        }
    }



    public void hardSpeed()
    {
        if (Input.GetKey(KeyCode.LeftShift) && isGrounded && !gameover)
        {
            doubleSpeed = true;
            playerAnim.SetFloat("Speed_Multiplier", 2.0f);
        }
        else if (!isGrounded || gameover || Input.GetKeyUp(KeyCode.LeftShift))
        {
            doubleSpeed = false;
            playerAnim.SetFloat("Speed_Multiplier", 1.0f);
        }
    }
}
