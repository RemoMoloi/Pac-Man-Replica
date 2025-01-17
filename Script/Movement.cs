using UnityEngine;


[RequireComponent (typeof(Rigidbody2D))]
public class Movement : MonoBehaviour
{
    public float speed = 8.0f;

    public float speedMultiplier = 1.0f;

    public Vector2 initialDirection;

    public LayerMask obstacleLayer;

  public new Rigidbody2D rigidbody { get; private set; }

  public Vector2 direction {  get; private set; }
  
  public Vector2 nextDirection { get; private set; }

  public Vector3 startingPosition { get; private set; }

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        startingPosition = this.transform.position;
    }

    private void Start()
    {
       ResetState();
    }

    public void ResetState()
    {
        speedMultiplier = 1.0f;
        direction = this.initialDirection;
        nextDirection = Vector2.zero;
        this.transform.position = this.startingPosition;
        this.rigidbody.isKinematic = false;
        enabled = true;
    }

    private void Update()
    {
            if (this.nextDirection != Vector2.zero)
            {
                SetDirection(this.nextDirection); 
            }
    }



    private void FixedUpdate()
    {
        Vector2 position = this.rigidbody.position;
        Vector2 translation = this.direction * this.speed * this.speedMultiplier * Time.fixedDeltaTime;

        this.rigidbody.MovePosition(position + translation);
    }

    public void SetDirection(Vector2 direction, bool forced = false)
    {
        if (forced || !Occupied(direction))
        {
            this.direction = direction;
            nextDirection = Vector2.zero;
        }
        else
        {
            nextDirection = direction;
        }
    }

    public bool Occupied(Vector2 direction)
    {
        RaycastHit2D hit = Physics2D.BoxCast(this.transform.position, Vector2.one * 0.75f, 0.0f, direction, 1.5f, this.obstacleLayer);
        return hit.collider != null;
    }
}
