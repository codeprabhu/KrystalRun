using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    private Vector2 movement;
    private Rigidbody2D rb;
    private Animator animator;
    private const string horizontal = "Horizontal";
    private const string vertical = "Vertical";
    private const string lastHorizontal = "LastHorizontal";
    private const string lastVertical = "LastVertical";
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        movement.Set(InputManager.Movement.x, InputManager.Movement.y);
        rb.linearVelocity = movement* moveSpeed;
        animator.SetFloat( horizontal, movement.x);
        animator.SetFloat( vertical, movement.y);

        if( movement != Vector2.zero)
        {
            animator.SetFloat(lastHorizontal, movement.x);
            animator.SetFloat(lastVertical,movement.y);
        }
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("FireTile"))
        {
        // Game over: Load the End Credits scene
            SceneManager.LoadScene("endcreds");
        }
    }

}
