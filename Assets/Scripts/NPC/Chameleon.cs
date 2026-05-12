using Photon.Pun;
using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(PhotonView))]
public class Chameleon : MonoBehaviour
{
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    private Rigidbody2D rb;
    private PhotonView view;
    private Transform target;
    private bool isInvisible;

    private float rayFrontDistance = 2f;
    private float rayBackDistance = 1f;

    private float patrolSpeed = 0.8f;   // Velocidad lenta para patrullar
    private float chaseSpeed = 2f;    // Velocidad rápida para perseguir
    
    private LayerMask playerLayer;
    private LayerMask groundLayer;
    
    private float maxFollowHeight = 1.5f;

    private float translucentAlpha = 0.15f;
    private float fadeSpeed = 4f;
    private float camouflageDelay = 5f; // Tiempo que tarda en volver a camuflarse
    private float lastDetectionTime = -100f; // Inicializado bajo para empezar transparente



    private void Awake()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        view = GetComponent<PhotonView>();

        playerLayer = LayerMask.GetMask(LayerUtils.Player);
        groundLayer = LayerMask.GetMask(LayerUtils.Ground);

        SetAlpha(translucentAlpha);
    }

    private void Update()
    {
        UpdateTransparency();

        if (PhotonNetwork.IsConnected && !PhotonNetwork.IsMasterClient) return;
        
        DetectPlayer();
        animator.SetFloat(AnimationConst.VelocityX, Mathf.Abs(rb.velocity.x));
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (TagsUtils.IsPlayer(collision.gameObject))
        {
            MainClass.GameManager.MinusScore();
            collision.gameObject
                .GetComponent<PlayerController>()
                ?.ApplyInvulnerability();
        }
    }

    private void UpdateTransparency()
    {
        float targetAlpha = GetTargetAlpha();

        SetAlpha(Mathf.MoveTowards(spriteRenderer.color.a, targetAlpha, fadeSpeed * Time.deltaTime));
    }

    private float GetTargetAlpha() => (isInvisible || target != null) ? 1f : translucentAlpha;

    private void SetAlpha(float alpha)
    {
        Color c = spriteRenderer.color;
        c.a = alpha;
        spriteRenderer.color = c;
    }

    private void FixedUpdate()
    {
        if (PhotonNetwork.IsConnected && !PhotonNetwork.IsMasterClient) return;

        if (target != null)
        {
            MoveToPlayer();
        }
        else
        {
            Patrol();
        }
    }

    private void Patrol()
    {
        Vector2 direction = GetFrontDirection();
        
        float wallCheckDist = 0.2f;
        RaycastHit2D wallHit = Physics2D.Raycast(transform.position, direction, wallCheckDist, groundLayer);
        Debug.DrawRay(transform.position, direction * wallCheckDist, Color.red);
        Vector2 edgeCheckPos = (Vector2) transform.position + (direction * 0.2f);
        RaycastHit2D edgeHit = Physics2D.Raycast(edgeCheckPos, Vector2.down, 1f, groundLayer);
        Debug.DrawRay(edgeCheckPos, Vector2.down * 1f, Color.blue);

        if (wallHit.collider != null || edgeHit.collider == null)
        {
            Vector3 pointOpposite = transform.position - (Vector3) direction;
            view.RPC("FlipX", RpcTarget.All, pointOpposite);
        }

        rb.velocity = new Vector2(direction.x * patrolSpeed, rb.velocity.y);
    }

    private void DetectPlayer()
    {
        Vector2 frontDirection = GetFrontDirection();
        Vector2 backDirection = -frontDirection;

        RaycastHit2D hitFront = Physics2D.Raycast(transform.position, frontDirection, rayFrontDistance, playerLayer);
        RaycastHit2D hitBack = Physics2D.Raycast(transform.position, backDirection, rayBackDistance, playerLayer);
        Debug.DrawRay(transform.position, frontDirection * rayFrontDistance, Color.green);
        Debug.DrawRay(transform.position, backDirection * rayBackDistance, Color.yellow);

        bool currentlySeeingPlayer = false;
        if (hitFront.collider != null)
        {
            target = hitFront.collider.transform;
            currentlySeeingPlayer = true;
            lastDetectionTime = Time.time;
        }
        else if (hitBack.collider != null)
        {
            target = hitBack.collider.transform;
            currentlySeeingPlayer = true;
            lastDetectionTime = Time.time;
            FlipX(target.position);
        }

        bool shouldBeVisible = currentlySeeingPlayer || (Time.time < lastDetectionTime + camouflageDelay);

        if (shouldBeVisible != isInvisible)
        {
            isInvisible = shouldBeVisible;
            if (PhotonNetwork.IsConnected)
                view.RPC("RPC_SetDetected", RpcTarget.Others, shouldBeVisible);
        }
    }

    private Vector2 GetFrontDirection() => spriteRenderer.flipX ? Vector2.right : Vector2.left;
    
    private void MoveToPlayer()
    {
        if (target == null) return;
        
        float diffX = Mathf.Abs(transform.position.x - target.position.x);
        if (diffX < 0.2f) 
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
            return;
        }

        float diffY = Mathf.Abs(transform.position.y - target.position.y);

        if (diffX > rayFrontDistance || diffY > maxFollowHeight)
        {
            target = null;
            return;
        }

        float directionX = (target.position.x > transform.position.x) ? 1f : -1f;
        rb.velocity = new Vector2(directionX * chaseSpeed, rb.velocity.y);
        FlipX(target.position);
    }

    [PunRPC]
    private void RPC_SetInvisible(bool state) => isInvisible = state;

    [PunRPC]
    public void FlipX(Vector3 target)
    {
        if (transform.position.x < target.x)
        {
            spriteRenderer.flipX = true;
        }
        else
        {
            spriteRenderer.flipX = false;
        }
    }
}