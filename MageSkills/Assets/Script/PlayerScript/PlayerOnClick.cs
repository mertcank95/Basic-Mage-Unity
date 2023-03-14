using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerOnClick : MonoBehaviour
{
    [SerializeField] private float maxSpeed = 5f;
    public float trunSpeed = 15f;
    private float currentSpeed;
    private Vector3 playerMove = Vector3.zero;
    private bool canAttack, canMove;
    private Vector3 newMovePoint;
    private float playerToPointDistance;
    private Vector3 targetMovePoint = Vector3.zero;
    private Animator anim;
    private CharacterController controller;
    private bool finishedMovement;
    private void Awake()
    {
        anim = GetComponent<Animator>();
        currentSpeed = maxSpeed;
        controller = GetComponent<CharacterController>();
    }
    void Update()
    {
        CheckIfFinishedMovement();
        MovePlayer();
        controller.Move(playerMove);
    }

    void MovePlayer()
    {
        if (Input.GetMouseButtonDown(1))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                playerToPointDistance = Vector3.Distance(transform.position, hit.point);
                if (hit.collider.gameObject.layer == LayerMask.NameToLayer("Ground"))
                {
                    if (playerToPointDistance >= 1.0f)
                    {
                        canMove = true;
                        canAttack = false;
                        targetMovePoint = hit.point;

                    }
                }

            }

        }
        if (canMove)
        {
            anim.SetFloat("Speed", 1.0f);
            if (!canAttack)
            {
                newMovePoint = new Vector3(targetMovePoint.x, transform.position.y, targetMovePoint.z);
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(newMovePoint - transform.position), trunSpeed * Time.deltaTime);
            }
            playerMove = transform.forward * currentSpeed * Time.deltaTime;
            if (Vector3.Distance(transform.position, newMovePoint) <= 0.6f && !canAttack)
            {
                canMove = false;
                canAttack = false;
            }
        }
        else
        {
            playerMove.Set(0f, 0f, 0f);
            anim.SetFloat("Speed", 0f);

        }
    }

    void CheckIfFinishedMovement()
    {
        if (!finishedMovement)
        {
            if (!anim.IsInTransition(0) && !anim.GetCurrentAnimatorStateInfo(0).IsName("Idle") && anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.8f) 
            {
                finishedMovement = true;
            }
        }
        else
        {
            MovePlayer();
        }
    }
    public Vector3 TargetPosion
    {
        get
        {
            return targetMovePoint;
        }
        set
        {
            targetMovePoint = value;
        }
    }

    public bool FinishedMovent
    {
        get
        {
            return finishedMovement;
        }

        set
        {
            finishedMovement = value;
        }

    }

}
