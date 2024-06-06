using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterController2D : MonoBehaviour
{
    #region Tanımlamalar

    public float moveSpeed = 5f;
    public float jumpForce = 10f;
    public Transform groundCheck;
    public LayerMask groundLayer;
    public float groundCheckRadius = 0.1f;

    public string horizontalAxis = "Horizontal";
    public string verticalAxis = "Vertical";

    private Rigidbody2D rb;
    private Animator animator;
    private bool facingRight = true;
    private float inputHorizontal;
    private float inputVertical;

    private GameObject[] Healths;

    public GameObject LosePanel;
    public GameObject WinPanel;
    
    public float finishTime; 
    private float timer;
    private bool timerRunning;

    public Text TimerText;

    #endregion

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        LosePanel.SetActive(false);
        WinPanel.SetActive(false);

        GameObject[] kalpObjects = GameObject.FindGameObjectsWithTag("Kalp");
        Healths = new GameObject[kalpObjects.Length];
        for (int i = 0; i < kalpObjects.Length; i++)
        {
            Healths[i] = kalpObjects[i];
        }
        
        timer = finishTime;
        timerRunning = true;
        
    }

    #region Movement

    void Update()
    {
        if (timerRunning)
        {
            TimerText.text = timer.ToString();
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                timerRunning = false;
                OnTimerFinish();
            }
        }

        inputHorizontal = SimpleInput.GetAxis(horizontalAxis);
        inputVertical = SimpleInput.GetAxis(verticalAxis);

        if (inputVertical > 0 && IsGrounded())
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            SoundManager.Instance.PlayEffectSound(SoundManager.Instance.JumpSound);
        }

        animator.SetFloat("Speed", Mathf.Abs(inputHorizontal));
        animator.SetBool("isGrounded", IsGrounded());
        animator.SetFloat("VerticalSpeed", rb.velocity.y);

        if (inputHorizontal > 0 && !facingRight)
        {
            Flip();
        }
        else if (inputHorizontal < 0 && facingRight)
        {
            Flip();
        }
    }

    void FixedUpdate()
    {
        rb.velocity = new Vector2(inputHorizontal * moveSpeed, rb.velocity.y);
    }

    bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
    }

    void Flip()
    {
        facingRight = !facingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    #endregion

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            animator.SetTrigger("Hit");
            SoundManager.Instance.PlayEffectSound(SoundManager.Instance.HitSound);
            DestroyHealthItem();
        }

        if (other.gameObject.CompareTag("BigEnemy"))
        {
            StartCoroutine(DestroyAllHealthAndRestart());
            SoundManager.Instance.PlayEffectSound(SoundManager.Instance.HitSound);
            animator.SetTrigger("Dead");
            ScoreCount.score = 0;
            SoundManager.Instance.PlayEffectSound(SoundManager.Instance.LoseSound);
            gameObject.SetActive((false));
        }
    }

    void DestroyHealthItem()
    {
        if (Healths.Length > 0)
        {
            for (int i = 0; i < Healths.Length; i++)
            {
                if (Healths[i] != null)
                {
                    Destroy(Healths[i]);
                    Healths[i] = null;
                    break;
                }
            }

            if (AllHealthItemsDestroyed())
            {
                ScoreCount.score = 0;
                LosePanel.SetActive(true);
                animator.SetTrigger("Dead");
                SoundManager.Instance.PlayEffectSound(SoundManager.Instance.LoseSound);
                gameObject.SetActive((false));
            }
        }
    }

    bool AllHealthItemsDestroyed()
    {
        
        foreach (GameObject health in Healths)
        {
            if (health != null)
            {
                return false;
            }
        }

        return true;
    }

    IEnumerator DestroyAllHealthAndRestart()
    {
        foreach (GameObject health in Healths)
        {
            if (health != null)
            {
                Destroy(health);
            }
        }

        yield return new WaitForSeconds(1f);
        LosePanel.SetActive(true);
        
    }

    void OnTimerFinish()
    {
       gameObject.SetActive((false));
        WinPanel.SetActive(true);
        SoundManager.Instance.PlayEffectSound(SoundManager.Instance.WinSound);
    }
}
