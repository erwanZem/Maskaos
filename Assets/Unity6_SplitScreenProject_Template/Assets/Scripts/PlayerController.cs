using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float moveSpeed = 15f;
    [SerializeField] float maxJumpDuration = 10f;
    [SerializeField] float jumpHeigt = 20f;
    [SerializeField] float rotationSpeed = 1f;
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
    
    private void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
        m_rigidBody = GetComponent<Rigidbody>();
        animator = GetComponentInChildren<Animator>();
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

            if (guy.ApplyDamage(power))
            {
                gameManager.PlayerKilled(this, guy);
            }


        }
    }

    public Color GetColor()
    {
        return color;
    }

}
