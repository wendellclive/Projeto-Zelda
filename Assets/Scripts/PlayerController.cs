using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    [Header("Config Player")]
    public float movementSpeed = 3f;
    private Vector3 direction;
    private CharacterController controller;
    private Animator animator;
    private bool isWalk;
    private float horizontal;
    private float vertical;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Inputs();
        MoveCharacter();
        UpdateAnimator();

    }

    #region MEUS MÉTODOS

    // MÉTODO RESPONSAVEL PELAS ENTRADAS DE COMANDO DO USUÁRIO
    void Inputs()
    {

        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");

        if (Input.GetButtonDown("Fire1"))
        {
            animator.SetTrigger("Attack");
        };

        direction = new Vector3(horizontal, 0f, vertical).normalized;

    }

    // MÉTODO RESPONSAVEL POR MOVER O PERSONAGEM
    void MoveCharacter()
    {
        if (direction.magnitude > 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, targetAngle, 0);
            isWalk = true;
        }
        else          // if(direction.magnitude <= 0.1f) 
        {
            isWalk = false;
        }

        controller.Move(direction * movementSpeed * Time.deltaTime);

    }

    // MÉTODO RESPONSAVEL EM ATUALIZAR OS ANIMATOR
    void UpdateAnimator()
    {
        animator.SetBool("isWalk", isWalk);
    }

    #endregion

}
