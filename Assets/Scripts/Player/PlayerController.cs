using Photon.Pun;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(PhotonView))]
public class PlayerController : MonoBehaviour
{
    public float speed = 1;
    public float jumpForce = 200;
    // Referencias a componentes
    private Rigidbody2D rig;
    private Animator anim;
    private PhotonView photonView;
    private SpriteRenderer spriteRenderer;

    // Variables para capturar input y estado
    private float horizontalInput;
    private bool jumpRequested;
    private bool lastFlipState;

    void Start()
    {
        photonView = GetComponent<PhotonView>();
        rig = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        MainClass.CustomEvents.OnRegistryPlayer.Invoke(gameObject.transform);

        if (photonView.IsMine)
        {
            Camera.main.transform.SetParent(transform);
            Camera.main.transform.position = transform.position + (Vector3.up) + transform.forward * -10;
        }
        else
        {
            // Desactivar simulación física local en jugadores remotos:
            // el PhotonRigidbody2DView sincroniza la posición y velocidad desde el dueño,
            // y si la física local está activa lucha contra los datos de red.
            rig.isKinematic = true;
        }
    }

    void Update() 
    {
        if (!photonView.IsMine) return;

        horizontalInput = Input.GetAxis("Horizontal");
        if (Input.GetButtonDown("Jump"))
        {
            jumpRequested = true;
        }
    }

    void FixedUpdate()
    {
        if (photonView.IsMine)
        {
            // Aplicamos movimiento
            rig.velocity = (transform.right * speed * horizontalInput) + (transform.up * rig.velocity.y);
            
            // Solo mandamos RPC si el estado de rotación realmente cambia (optimización de red)
            if (horizontalInput > 0.1f && lastFlipState != false)
            {
                lastFlipState = false;
                photonView.RPC("RotateSprite", RpcTarget.All, false);
            }
            else if(horizontalInput < -0.1f && lastFlipState != true) 
            {
                lastFlipState = true;
                photonView.RPC("RotateSprite", RpcTarget.All, true);
            }

            if (jumpRequested)
            {
                rig.AddForce(transform.up * jumpForce);
                jumpRequested = false;
            }

            anim.SetFloat(AnimationConst.VelocityX, Mathf.Abs(rig.velocity.x));
            anim.SetFloat(AnimationConst.VelocityY, rig.velocity.y);
        }
        else
        {
            // Para jugadores remotos: actualizamos el Animator con la velocidad
            // que llega sincronizada por PhotonRigidbody2DView.
            anim.SetFloat(AnimationConst.VelocityX, Mathf.Abs(rig.velocity.x));
            anim.SetFloat(AnimationConst.VelocityY, rig.velocity.y);
        }
    }

    [PunRPC]
    public void RotateSprite(bool rotate)
    {
        if (spriteRenderer == null) return;
        spriteRenderer.flipX = rotate;
        lastFlipState = rotate;
    }
}
