using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie : MonoBehaviour
{
    public float speed;
    public int positionOfPatrol;
    public Transform point;
    bool movingRight;

    Transform player;
    public float stoppingDistance;

    bool chill = false;
    bool angry = false;
    bool goBack = false;

    public float moveSpeed = 5.0f;
    public int scoreValue = 5;
    public Transform followTarget = null;
    public float followRange = 10.0f;
    public enum MovementModes { NoMovement, FollowTarget, Scroll };
    public MovementModes movementMode = MovementModes.FollowTarget;
    [SerializeField] private Vector3 scrollDirection = Vector3.right;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        if (movementMode == MovementModes.FollowTarget && followTarget == null)
        {
            if (GameManager.instance != null && GameManager.instance.player != null)
            {
                followTarget = GameManager.instance.player.transform;
            }
        }
    }
void Update()
    {
        if(Vector2.Distance(transform.position,point.position) < positionOfPatrol && angry == false)
        {
            chill = true;
        }
        if(Vector2.Distance(transform.position, player.position) < stoppingDistance)
        {
            
            angry = true;
            chill = false;
            goBack = false;
        }
        if (Vector2.Distance(transform.position, player.position) > stoppingDistance)
        {
            goBack = true;
            angry = false;
        }
        if (chill == true)
        {
            Chill();
        }
        else if (angry == true)
        {
            Angry();
            Quaternion rotationToTarget = GetDesiredRotation();
            transform.rotation = rotationToTarget;
        }
        else if (goBack == true)
        {
            GoBack();
        }
    } 
    void Chill()
    {
        if(transform.position.x > point.position.x + positionOfPatrol)
        {
            movingRight = false;
        }
        else if (transform.position.x < point.position.x - positionOfPatrol)
        {
            movingRight = true;
        }
        if (movingRight)
        {
            Vector3 movement = GetDesiredMovement();
            transform.position = new Vector2(transform.position.x + speed * Time.deltaTime, transform.position.y);
        }
        else
        {
            Vector3 movement = GetDesiredMovement();
            transform.position = new Vector2(transform.position.x - speed * Time.deltaTime, transform.position.y);
        }
    }
    void Angry()
    {
        transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
        speed = 9;
    }
    void GoBack()
    {
        transform.position = Vector2.MoveTowards(transform.position, point.position, speed * Time.deltaTime);
    }
    public void DoBeforeDestroy()
    {
        AddToScore();
        IncrementEnemiesDefeated();
    }

    private void AddToScore()
    {
        if (GameManager.instance != null && !GameManager.instance.gameIsOver)
        {
            GameManager.AddScore(scoreValue);
        }
    }

    private void IncrementEnemiesDefeated()
    {
        if (GameManager.instance != null && !GameManager.instance.gameIsOver)
        {
            GameManager.instance.IncrementEnemiesDefeated();
        }
    }

 
    private void MoveEnemy()
    {
        // Determine correct movement
        Vector3 movement = GetDesiredMovement();

        // Determine correct rotation
        Quaternion rotationToTarget = GetDesiredRotation();

        // Move and rotate the enemy
        transform.position = transform.position + movement;
        transform.rotation = rotationToTarget;
    }

    protected virtual Vector3 GetDesiredMovement()
    {
        Vector3 movement;
        switch (movementMode)
        {
            case MovementModes.FollowTarget:
                movement = GetFollowPlayerMovement();
                break;
            case MovementModes.Scroll:
                movement = GetScrollingMovement();
                break;
            default:
                movement = Vector3.zero;
                break;
        }
        return movement;
    }

    protected virtual Quaternion GetDesiredRotation()
    {
        Quaternion rotation;
        switch (movementMode)
        {
            case MovementModes.FollowTarget:
                rotation = GetFollowPlayerRotation();
                break;
            case MovementModes.Scroll:
                rotation = GetScrollingRotation();
                break;
            default:
                rotation = transform.rotation; ;
                break;
        }
        return rotation;
    }

    private Vector3 GetFollowPlayerMovement()
    {
        Vector3 moveDirection = (followTarget.position - transform.position).normalized;
        Vector3 movement = moveDirection * moveSpeed * Time.deltaTime;
        return movement;
    }

    private Quaternion GetFollowPlayerRotation()
    {
        float angle = Vector3.SignedAngle(Vector3.down, (followTarget.position - transform.position).normalized, Vector3.forward);
        Quaternion rotationToTarget = Quaternion.Euler(0, 0, angle);
        return rotationToTarget;
    }

    private Vector3 GetScrollingMovement()
    {
        scrollDirection = GetScrollDirection();
        Vector3 movement = scrollDirection * moveSpeed * Time.deltaTime;
        return movement;
    }

    private Quaternion GetScrollingRotation()
    {
        return Quaternion.identity;
    }

    private Vector3 GetScrollDirection()
    {
        Camera camera = Camera.main;
        if (camera != null)
        {
            Vector2 screenPosition = camera.WorldToScreenPoint(transform.position);
            Rect screenRect = camera.pixelRect;
            if (!screenRect.Contains(screenPosition))
            {
                return scrollDirection * -1;
            }
        }
        return scrollDirection;
    }
    
}
