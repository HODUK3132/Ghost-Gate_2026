
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class TopDownPlayer : MonoBehaviour
{
    public float moveSpeed = 4f;
    public float frameTime = 0.15f;

    public Sprite[] downSprites;
    public Sprite[] upSprites;
    public Sprite[] leftSprites;
    public Sprite[] rightSprites;

    public DialogueBox dialogueBox;
    public string nextSceneName = "Stage2";

    private Vector2 moveInput;
    private Vector2 facingDir = Vector2.down;

    private Rigidbody2D rb;
    private SpriteRenderer sr;
    private Animator animator;

    private Sprite[] currentSprites;
    private int frameIndex = 0;
    private float frameTimer = 0f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();

        // 처음에는 아래쪽을 바라보도록 설정
        animator.SetFloat("LastX", 0f);
        animator.SetFloat("LastY", -1f);
    }

    void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();

        if (moveInput != Vector2.zero)
        {
            // 대각선 입력도 한 방향으로 정리
            if (Mathf.Abs(moveInput.x) > Mathf.Abs(moveInput.y))
            {
                facingDir = new Vector2(
                    Mathf.Sign(moveInput.x), 0f);
            }
            else
            {
                facingDir = new Vector2(
                    0f, Mathf.Sign(moveInput.y));
            }
        }
    }

    void Update()
    {
        // 대화 중에는 이동 금지
        if (dialogueBox != null && dialogueBox.isOpen)
        {
            moveInput = Vector2.zero;
        }

        // 마지막으로 바라본 방향 전달
        animator.SetFloat("LastX", facingDir.x);
        animator.SetFloat("LastY", facingDir.y);

        // 방향에 맞는 걷기 Sprite 선택
        if (facingDir.y > 0)
            currentSprites = upSprites;
        else if (facingDir.y < 0)
            currentSprites = downSprites;
        else if (facingDir.x < 0)
            currentSprites = leftSprites;
        else
            currentSprites = rightSprites;

        bool isMoving = moveInput != Vector2.zero;

        animator.SetFloat("Speed", isMoving ? 1f : 0f);

        if (isMoving)
        {
            // 걷는 동안 Animator가 Sprite를 변경하지 않도록 함
            animator.enabled = false;

            frameTimer -= Time.deltaTime;

            if (frameTimer <= 0f)
            {
                frameTimer = frameTime;
                frameIndex++;

                if (frameIndex >= currentSprites.Length)
                    frameIndex = 0;
            }

            sr.sprite = currentSprites[frameIndex];
        }
        else
        {
            // 정지하면 숨쉬기 애니메이션 재생
            frameIndex = 0;
            frameTimer = 0f;

            if (!animator.enabled)
            {
                animator.enabled = true;
            }
        }
    }

    void FixedUpdate()
    {
        Vector2 velocity = moveInput * moveSpeed;

        if (dialogueBox != null && dialogueBox.isOpen)
            velocity = Vector2.zero;

        rb.linearVelocity = velocity;
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Goal"))
        {
            SceneManager.LoadScene(nextSceneName);
        }
    }

}
