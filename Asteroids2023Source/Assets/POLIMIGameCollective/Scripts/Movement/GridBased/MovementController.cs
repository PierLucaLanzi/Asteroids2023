using System;
using UnityEngine;

namespace GridBasedMovement
{
    public class MovementController : MonoBehaviour
    {
        [SerializeField] private bool moveDiagonally = false;
        [SerializeField] private float speed = 5f;
        [SerializeField] private LayerMask obstacle;
        
        // position where we want to move next
        public Transform MovePosition { get; private set; } = null;
        
        private float _horizontalMovement;
        private float _verticalMovement;

        private void Awake()
        {
            MovePosition = GetComponentInChildren<Movepoint>().transform;
            MovePosition.parent = null;

        }

        private void Update()
        {
            transform.position = Vector3.MoveTowards(transform.position, MovePosition.position, speed * Time.deltaTime);
        
            if (Vector3.Distance(transform.position, MovePosition.position) <= 0.05f)
            {
                _horizontalMovement = Input.GetAxisRaw("Horizontal");
                
                // if we don't want to move diagonally,
                // then if we already pressed to move horizontally, 
                // we set vertical movement to zero
                if (!moveDiagonally && (_horizontalMovement > 0))
                {
                    _verticalMovement = 0; 
                }
                else
                {
                    _verticalMovement = Input.GetAxisRaw("Vertical");
                }
                
                if (Mathf.Abs(_horizontalMovement) >= 1 || Mathf.Abs(_verticalMovement) >= 1)
                {
                    Vector3 movement = new Vector3(_horizontalMovement, _verticalMovement, 0);
                    Vector3 destination = MovePosition.position + movement;
        
                    if (!Physics2D.OverlapCircle(destination, .2f, obstacle))
                    {
                        MovePosition.position = destination;
                    }
                }
                
            }                
        }

    }
}
