using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    public class PlayerController : MonoBehaviour
    {
        [Header("Player moving velocity")]
        public float velocity = 1f;

        public LayerMask obstacleLayer; 
        
        private Rigidbody2D _rb;

        private Transform _tr;

        private bool _moving = false;

        private void Awake()
        {
            InitComponents();
        }

        private void InitComponents()
        {
            _rb = GetComponent<Rigidbody2D>();
            _tr = GetComponent<Transform>();
        }

    bool CanMove(Vector2 direction, float deltaPosition)
    {
        RaycastHit2D hit = Physics2D.BoxCast(_tr.position,
            Vector2.one * 0.95f, 0f, direction, deltaPosition,
            obstacleLayer);

        //Vector2 destination = (Vector2) _tr.position + direction;
        //if (!Physics2D.OverlapCircle(destination, .2f, obstacleLayer))
        //{
        //    return true;
        //}
        //else
        //{
        //    Debug.Log("HIT A " + hit.collider.gameObject.name);
        //    return false;
        //}

        if (hit.collider != null)
        {
            Debug.Log("HIT A " + hit.collider.gameObject.name);
            return false;
        }
        else
        {
            return true;
        }
    }

    void Update()
    {
        if (_moving)
            return;

        float horizontalMovement = Input.GetAxisRaw("Horizontal");
        float verticalMovement = Input.GetAxisRaw("Vertical");

        if (horizontalMovement != 0)
            verticalMovement = 0;

        float deltaPosition = 0f;
        Vector2 direction = Vector2.zero;
        Vector3 destination = Vector3.zero;

        if (horizontalMovement != 0)
        {
            direction = Vector2.right * Mathf.Sign(horizontalMovement);

            destination = _tr.position + (Vector3) direction;

            print("DIREZIONE " + direction.ToString());
            print("DESTINAZIONE " + destination.ToString());
            print("CAN MOVE " + CanMove(direction * Mathf.Sign(horizontalMovement), 1f));

        }
        else if (verticalMovement != 0)
        {
            direction = Vector2.up * Mathf.Sign(verticalMovement);

            destination = _tr.position + (Vector3)direction;

            print("DIREZIONE " + direction.ToString());
            print("DESTINAZIONE " + destination.ToString());
            print("CAN MOVE " + CanMove(direction * Mathf.Sign(verticalMovement), 1f));

        }
        else
        {
            direction = Vector2.zero;
            deltaPosition = 0f;
        }


        if (direction != Vector2.zero && CanMove(direction, 1f))
        {
            StartCoroutine(SmoothMove((Vector3)destination, .2f));
        }
    }

    IEnumerator SmoothMove(Vector3 target, float time)
    {
        float inverseTime = 1 / time;
            
        _moving = true;

        float distanceFromTarget =
            (_tr.position - target).sqrMagnitude;

        while (distanceFromTarget > float.Epsilon)
        {
            Vector3 next_position =
                Vector3.MoveTowards(_tr.position,
                    target,
                    Time.deltaTime * inverseTime);
            _tr.position = next_position;
            distanceFromTarget =
                (_tr.position - target).sqrMagnitude;
            yield return null;
        }

        _tr.position = target;
        _moving = false;
        yield return null;
    }        
}
    
