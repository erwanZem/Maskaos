using UnityEngine;
using UnityEngine.InputSystem;

public class TestAnimationController : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    Animator animator;
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

    }


    public void OnAttack(InputValue input)
    {
        Debug.Log("ATTACK");
        animator.SetBool("IsFighting", true);
    }


}
