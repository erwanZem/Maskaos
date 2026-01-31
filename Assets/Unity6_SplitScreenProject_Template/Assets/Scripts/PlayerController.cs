using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float moveSpeed = 15f;
    [SerializeField] float maxJumpDuration = 10f;
    [SerializeField] float jumpHeigt = 20f;
    [SerializeField] float rotationSpeed = .2f;
    private float lastJumpTime = 0f;
    private Vector2 moveInput;
    private Vector2 lookInput;
    private Rigidbody m_rigidBody;
    private bool jump = false;
    //logique 
    private int hp = 20;
    private Color color = Color.red;
    protected int power = 2;
    
    private void Start()
    {
       m_rigidBody = GetComponent<Rigidbody>();
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
    
    /*public void OnAttack(Collider other)
    {
        if (other != null && other.isTrigger)
        {
            Guy guy = Guy.GetGuy(other.gameObject);
            if (!guy.IsSameColor(color))
            {
                
            }
        }
    }*/
    
    public void OnLook(InputValue input)
    {
        lookInput = input.Get<Vector2>();
    }

    private void punching(Guy guy)
    {
        if (guy.IsAlive())
        {
            guy.GetRigidbody().AddForce(guy.transform.forward * power * 100);
            guy.setDamage(power);
        }
    }
    void Update()
    {
        Vector3 move = new Vector3(moveInput.x, 0, moveInput.y);
        transform.Translate(move * moveSpeed * Time.deltaTime, Space.World);
        Vector3 direction = new Vector3(0,lookInput.x,0);
        Vector3 lookAtPos =  direction - transform.position;
        // lookAtPos.y = player.transform.position.y; // do not rotate the player around x
        Debug.Log(lookInput);
       // transform.Rotate(direction);
        transform.Rotate(direction * rotationSpeed);
        //Quaternion targetRotation = Quaternion.LookRotation(direction,Vector3.up);
        //transform.rotation = Quaternion.Lerp(transform.rotation,targetRotation , Time.deltaTime * rotationSpeed);
    }
    
}
