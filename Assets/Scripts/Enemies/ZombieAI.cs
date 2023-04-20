using UnityEngine;

public class ZombieAI : MonoBehaviour
{
    public float detectionRange = 10f; // The range at which the zombie can detect the player
    public float attackRange = 2f; // The range at which the zombie can attack the player
    public float moveSpeed = 3f; // The speed at which the zombie moves
    public float turnSpeed = 5f; // The speed at which the zombie turns towards the player

    private Transform target; // The player's transform
    private bool isChasing = false; // Whether the zombie is currently chasing the player

    // Start is called before the first frame update
    void Start()
    {
        // Find the player's transform
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        // Check if the player is within detection range
        float distanceToTarget = Vector3.Distance(transform.position, target.position);
        if (distanceToTarget < detectionRange)
        {
            // Set the zombie to chase the player
            isChasing = true;
        }
        else
        {
            // Stop chasing if the player is out of range
            isChasing = false;
        }

        if (isChasing)
        {
            // Turn towards the player
            Vector3 direction = (target.position - transform.position).normalized;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            Quaternion targetRotation = Quaternion.Euler(new Vector3(0, 0, angle));
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * turnSpeed);

            // Move towards the player if not within attack range
            if (distanceToTarget > attackRange)
            {
                transform.Translate(direction * moveSpeed * Time.deltaTime);
            }
        }
    }
}