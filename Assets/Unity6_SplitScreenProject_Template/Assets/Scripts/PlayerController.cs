using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float moveSpeed = 15f;
    [SerializeField] float maxJumpDuration = 10f;
    [SerializeField] float jumpHeigt = 20f;
    [SerializeField] float rotationSpeed = 1f;
    [SerializeField] AudioClip hayaa;
    [SerializeField] TextMeshProUGUI scoreLabel;
    [SerializeField] TextMeshProUGUI gameoverLabel;
    private float lastJumpTime = 0f;
    private Vector2 moveInput;
    private Vector2 lookInput;
    private Rigidbody m_rigidBody;
    private bool jump = false;
    //logique 
    private int hp = 20;
    private Color color = Color.red;
    protected int power = 2;
    private Animator animator;
    private GameManager gameManager;
    private AudioSource audioSource;
    private int team;
    static int nb_teams = -1;


    private void Start()
    {
        gameoverLabel.enabled = false;

        nb_teams++;
        team = nb_teams;

        switch (team % 4)
        {
            case 0:
                SetColor(Color.green);
                gameoverLabel.color = (Color32)(Color.green);
                scoreLabel.color = (Color32)(Color.green);
                break;
            case 1:
                SetColor(Color.red);
                gameoverLabel.color = (Color32)(Color.red);
                scoreLabel.color = (Color32)(Color.red);
                break;
            case 2:
                SetColor(Color.blue);
                gameoverLabel.color = (Color32)(Color.blue);
                scoreLabel.color = (Color32)(Color.blue);
                break;
            case 3:
                SetColor(Color.cyan);
                gameoverLabel.color = (Color32)(Color.cyan);
                scoreLabel.color = (Color32)(Color.cyan);
                break;
        }

        gameManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
        m_rigidBody = GetComponent<Rigidbody>();
        animator = GetComponentInChildren<Animator>();
        audioSource = GetComponent<AudioSource>();


        gameManager.RegisterGameOverFunction(GameOver);
    }

    public bool GameOver(int winningTeam)
    {
        Debug.Log("winning team " + winningTeam);
        Debug.Log("team " + team);
        gameoverLabel.enabled = true;
        if (winningTeam == team)
        {
            gameoverLabel.text = "YOU WIN!";
        }
        return true;
    }


    public void OnMove(InputValue input)
    {
        moveInput = input.Get<Vector2>();
    }

    public void OnJump(InputValue input)
    {
        //a bouger dans un update
        m_rigidBody.AddForce(transform.up * jumpHeigt,ForceMode.Impulse);
    }
        
    public void OnLook(InputValue input)
    {
        lookInput = input.Get<Vector2>();
    }

    public void OnSpawnGuys(InputValue input)
    {
        GameObject.FindGameObjectWithTag("GameController").GetComponent<SpawnCrowd>().SpawnGuys(10);
    }

    void Update()
    {
        Vector3 localForward = transform.worldToLocalMatrix.MultiplyVector(transform.right);
        Vector3 localRight = transform.worldToLocalMatrix.MultiplyVector(transform.forward * -1);

        transform.Translate(localForward * moveInput.y * moveSpeed * Time.deltaTime);
        transform.Translate(localRight * moveInput.x * moveSpeed * Time.deltaTime);


        Vector3 direction = new Vector3(0,lookInput.x,0);
        transform.Rotate(direction * rotationSpeed);

        scoreLabel.text = "Score : " + gameManager.GetScoreForTeeam(team);
    }

    public void OnAttack(InputValue input)
    {
        animator.SetTrigger("Punch");
        Collider[] ennemies = Physics.OverlapCapsule(transform.position, transform.position + transform.right * 10, 3);
        ennemies = ennemies.Where(c => Guy.GetGuy(c.gameObject) != null).ToArray();

        foreach (Collider c in ennemies)
        {
            Guy guy = Guy.GetGuy(c.gameObject);

            guy.GetRigidBody().AddForce(transform.right * 100, ForceMode.Impulse);
            audioSource?.PlayOneShot(hayaa);
            if (guy.ApplyDamage(power))
            {
                gameManager.PlayerKilled(this, guy);
            }


        }
    }

    public void SetColor(Color color)
    {
        this.color = color;
        MeshRenderer[] renderers = GetComponentsInChildren<MeshRenderer>();
        foreach (MeshRenderer renderer in renderers)
        {
            renderer.material.color = color;
        }
    }
    public Color GetColor()
    {
        return color;
    }


}
