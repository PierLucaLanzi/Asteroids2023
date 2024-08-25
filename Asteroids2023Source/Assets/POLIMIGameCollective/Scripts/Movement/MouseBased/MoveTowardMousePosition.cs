using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTowardMousePosition : MonoBehaviour
{
    private Vector3 destination;
    private float speed = 2f;

    private bool followingWithY = true;
    
    // Start is called before the first frame update
    void Start()
    {
	    destination = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
	    if (Input.GetMouseButtonDown(0))
	    {
		    Vector2 positionOnScreen = Camera.main.WorldToViewportPoint(transform.position);

		    Vector3 mousePosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0f);
		
		    Vector3 target = Camera.main.ScreenToWorldPoint(mousePosition);
		    target.z = 0f;

		    Vector3 difference = (target - transform.position) ;
		
		    float rotation = Mathf.Atan2(difference.y,difference.x) * Mathf.Rad2Deg - (followingWithY?90f:0f);

		    transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, rotation));

		    destination = target;
	    }
	    
	    if (Vector3.Distance(destination, transform.position) > 0.001f)
	    {
		    Vector3 direction = (destination - transform.position).normalized;
		    transform.position += speed * Time.deltaTime * direction;
	    }

    }
    
    float AngleBetweenTwoPoints(Vector3 a, Vector3 b)
    {
        return Mathf.Atan2(a.y - b.y, a.x - b.x) * Mathf.Rad2Deg;
    }

}
