using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GridBasedMovement
{
    public class MovementRigidbodyController : MonoBehaviour
    {
        public bool moveDiagonally = false;
        public float speed = 5f;
        public LayerMask obstacle;
        
        // position where we want to move next
        public Transform movepoint = null;
        public float horizontalMovement;
        public float verticalMovement;

        private Rigidbody2D _rigidbody;
        
        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
            movepoint = GetComponentInChildren<Movepoint>().transform;
            movepoint.parent = null;
        }

        private void Update()
        {
            // _rigidbody.MovePosition(movepoint.position);
            transform.position = Vector3.MoveTowards(transform.position, movepoint.position, speed * Time.deltaTime);

            if (Vector3.Distance(transform.position, movepoint.position) <= 0.05f)
            {
                horizontalMovement = Input.GetAxisRaw("Horizontal");
                
                // if we don't want to move diagonally,
                // then if we already pressed to move horizontally, 
                // we set vertical movement to zero
                if (!moveDiagonally && (horizontalMovement > 0))
                {
                    verticalMovement = 0; 
                }
                else
                {
                    verticalMovement = Input.GetAxisRaw("Vertical");
                }
                
                if (Mathf.Abs(horizontalMovement) >= 1)
                {
                    Vector3 movement = new Vector3(horizontalMovement, 0, 0);
                    Vector3 destination = movepoint.position + movement;

                    if (!Physics2D.OverlapCircle(destination, .2f, obstacle))
                    {
                        movepoint.position = destination;
                    }
                }
                
                if (Mathf.Abs(verticalMovement) >= 1)
                {
                    Vector3 movement = new Vector3(0,verticalMovement, 0);
                    Vector3 destination = movepoint.position + movement;

                    if (!Physics2D.OverlapCircle(destination, .2f, obstacle))
                    {
                        movepoint.position = destination;
                    }
                }
            }                
        }
    }
}