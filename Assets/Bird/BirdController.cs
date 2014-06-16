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
	private bool approaching;
	private int approachCounter;
	private bool approachIsRight;

	// Use this for initialization
	void Start () {
        transform.position = RandomScreenPoint();
		target = OppositeSideTargetPoint(transform.position);
        randomizedMoveSpeed = moveSpeed + Random.Range(-2, 2);

		obstacle = GameObject.Find ("Obstacle");
	}

	// Update is called once per frame
	void Update () {

		Bounds obstacleBounds = obstacle.renderer.bounds;
		// change this to change obstacle distance condition for start of approach
		float expansion = Random.Range (3, 7);
		obstacleBounds.Expand (expansion);

		Main main = Camera.main.GetComponent ("Main") as Main;

		if (main.circling && obstacle.renderer.enabled && obstacleBounds.Intersects (renderer.bounds)) {
			StartApproach ();
		}

		if (approaching) {
			Vector3 newPosition = transform.position;
			Vector3 localScale = transform.localScale;
			float scaleFactor = 1.05f;

			approachCounter += 1;

			if (approachIsRight) {
				newPosition.x = newPosition.x + ApproachDistance (approachCounter);
			} else {
				newPosition.x = newPosition.x - ApproachDistance (approachCounter);
			}

			localScale.x *= scaleFactor;
			localScale.y *= scaleFactor;
			localScale.z *= scaleFactor;

			transform.position = newPosition;
			transform.localScale = localScale;

			if (!renderer.isVisible || localScale.x > 1) {
				StartFlying ();
			}
		} else {
			if (main.IsIdle()) {
				MoveToward (NestPosition());
				main.waitForApproach();
			} else {
				if (Vector3.Distance (transform.position, target) < 0.2) {
					target = OppositeSideTargetPoint (target);
				}
		
				MoveToward (target);
			}
		}
	}

	void StartApproach() {
		if (!approaching) {
			BirdAnimator animator = GetComponent("BirdAnimator") as BirdAnimator;
			animator.StartApproach();

			Vector3 newPosition = Camera.main.WorldToScreenPoint(transform.position);
			newPosition.z = 9;
			transform.position = Camera.main.ScreenToWorldPoint(newPosition);

			approaching = true;
			approachCounter = 0;
			approachIsRight = IsRightOfObstacle();
		}
	}
	
	void StartFlying() {
		BirdAnimator animator = GetComponent("BirdAnimator") as BirdAnimator;
		animator.StartFlying();

		transform.position = OppositeSideTargetPoint (RandomScreenPoint ());
		transform.localScale = new Vector3(scale, scale, scale);

		approaching = false;
	}

    private Vector3 RandomScreenPoint()
    {
        float x = Random.Range(0, Camera.main.pixelWidth);
        float y = Random.Range(0, Camera.main.pixelHeight);

		Vector3 result = Camera.main.ScreenToWorldPoint (new Vector3 (x, y, 10));
		result.z = 0;

        return result;
    }

	private Vector3 OppositeSideTargetPoint(Vector3 initial)
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

		Vector3 result = Camera.main.ScreenToWorldPoint (new Vector3 (x, y, 10));
		result.z = 0;
		
		return result;
	}
	

	float ApproachDistance(int counter) {
		float val = Mathf.Pow (counter * 0.1f - 0.5f, 2) - 1f;
		return val * 0.1f;
	}


	bool IsRightOfObstacle() {
		return obstacle.transform.position.x < transform.position.x;
	}

    void MoveToward(Vector3 moveToward)
    {
        Vector3 currentPosition = transform.position;
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
			transform.localScale = new Vector3(scale, -scale, -scale);
        }
        else
        {
			transform.localScale = new Vector3(scale, scale, scale);
        }
    }

	Vector3 NestPosition() {
		Vector3 result = Camera.main.ScreenToWorldPoint (new Vector3 (
			Camera.main.pixelWidth * 0.8f,
			- Camera.main.pixelWidth * 0.1f,
			0));

		result.z = 0;
		return result;
	}

}
