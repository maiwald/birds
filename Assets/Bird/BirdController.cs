using UnityEngine;
using System.Linq;
using System.Collections;

public class BirdController : MonoBehaviour {

	public float moveSpeed = 10;
    public float turnSpeed = 10;
    public float scale = 0.5F;
	
    private Vector3 target;
    private float randomizedMoveSpeed;

	private GameObject obstacle;

	// Use this for initialization
	void Start () {
        transform.position = RandomScreenPoint();
        target = NextTargetPoint(transform.position);
        randomizedMoveSpeed = moveSpeed + Random.Range(-2, 2);

		obstacle = GameObject.Find ("Obstacle");
	}

	void OnTriggerEnter2D(Collider2D col) {
		Debug.Log (col.gameObject.name);
	}

    Vector3 RandomScreenPoint()
    {
        float x = Random.Range(0, Camera.main.pixelWidth);
        float y = Random.Range(0, Camera.main.pixelHeight);

        return Camera.main.ScreenToWorldPoint(new Vector3(x, y, 10));
    }

    Vector3 NextTargetPoint(Vector2 initial)
    {
        int offScreenSpace = 80;
        float x, y;

        initial = Camera.main.WorldToScreenPoint(initial);

        // find point on the other half of the screen
        if ((Camera.main.pixelWidth / 2) < initial.x) {
            x = -offScreenSpace;
        } else {
            x = Camera.main.pixelWidth + offScreenSpace;
        }

        // don't always change y point
        y = (Random.value > 0.5) ? initial.y : Random.Range(0, Camera.main.pixelHeight);

        return Camera.main.ScreenToWorldPoint(new Vector3(x, y, 10));
    }
	
	// Update is called once per frame
	void Update () {
		if (obstacle.renderer.bounds.Intersects(renderer.bounds)) {
			BirdAnimator animator = GetComponent("BirdAnimator") as BirdAnimator;
			animator.StartApproach();
		}

		if (Vector3.Distance(transform.position, target) < 0.2)
            target = NextTargetPoint(target);

        MoveToward(target);
	}

    void MoveToward(Vector3 moveToward)
    {
        Vector3 currentPosition = transform.position;
        Vector3 localScale = transform.localScale;
        Vector3 moveDirection = moveToward - currentPosition;

        moveDirection.z = 0;
        moveDirection.Normalize();

        Vector3 targetPosition = moveDirection * randomizedMoveSpeed + currentPosition;
        transform.position = Vector3.Lerp(currentPosition, targetPosition, Time.deltaTime);

        float targetAngle = Mathf.Atan2(moveDirection.y, moveDirection.x) * Mathf.Rad2Deg;
        transform.rotation =
          Quaternion.Slerp(transform.rotation,
                            Quaternion.Euler(0, 0, targetAngle),
                            turnSpeed * Time.deltaTime);

        int angle = (int)transform.rotation.eulerAngles.z;
        if (Enumerable.Range(90, 180).Contains(angle))
        {
            localScale.x = scale;
            localScale.y = -scale;
            localScale.z = -scale;
        }
        else
        {
            localScale.x = scale;
            localScale.y = scale;
            localScale.z = scale;
        }
        transform.localScale = localScale;
    }

}
