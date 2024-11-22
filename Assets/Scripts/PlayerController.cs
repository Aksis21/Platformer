using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed;
    public float accelerationSpeed;
    public float maxSpeed;
    float currentVelocity;
    FacingDirection last = FacingDirection.right;

    Rigidbody2D rb;

    public enum FacingDirection
    {
        left, right
    }

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        // The input from the player needs to be determined and
        // then passed in the to the MovementUpdate which should
        // manage the actual movement of the character.
        Vector2 playerInput = new Vector2();

        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
            playerInput.x = -1;
        else if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
            playerInput.x = 1;
        else playerInput.x = 0;

        MovementUpdate(playerInput);
    }

    private void MovementUpdate(Vector2 playerInput)
    {
        if (playerInput.x == 0)
            currentVelocity = moveSpeed;
        if (playerInput.x > 0)
        {
            currentVelocity *= accelerationSpeed;
            if (currentVelocity > maxSpeed) currentVelocity = maxSpeed;
            rb.AddForce(new Vector2(currentVelocity * Time.deltaTime, 0));
        }
        if (playerInput.x < 0)
        {
            currentVelocity *= accelerationSpeed;
            if (currentVelocity > maxSpeed) currentVelocity = maxSpeed;
            rb.AddForce(new Vector2(currentVelocity * -1 * Time.deltaTime, 0));
        }
    }

    public bool IsWalking()
    {
        if (rb.totalForce.x != 0) return true;
        return false;
    }
    public bool IsGrounded()
    {
        return false;
    }

    public FacingDirection GetFacingDirection()
    {
        if (rb.totalForce.x < 0)
        {
            last = FacingDirection.left;
            return FacingDirection.left;
        }

        if (rb.totalForce.x > 0)
        {
            last = FacingDirection.right;
            return FacingDirection.right;
        }

        else return last;
    }
}
