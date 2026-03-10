using Photon.Pun;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(SpriteRenderer))]
[RequireComponent(typeof(PhotonView))]
public class PlayerController : MonoBehaviour
{
    public float speed = 1;
    public float jumpForce = 200;
    private Rigidbody2D rig;
    private Animator anim;
    private PhotonView photonView;
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        photonView = GetComponent<PhotonView>();
        if (photonView.IsMine)
        {
            rig = GetComponent<Rigidbody2D>();
            anim = GetComponent<Animator>();
            spriteRenderer = GetComponent<SpriteRenderer>();
            Camera.main.transform.SetParent(transform);
            Camera.main.transform.position = transform.position + (Vector3.up) + transform.forward * - 10;
        }
    }

    void Update()
    {
        if (photonView.IsMine)
        {
            rig.velocity = (transform.right * speed * Input.GetAxis("Horizontal")) + (transform.up * rig.velocity.y);
            if (rig.velocity.x > 0.1f)
            {
                photonView.RPC("RotateSprite", RpcTarget.All, false);
            }
            else if(rig.velocity.x < 0.1f) 
            {
                photonView.RPC("RotateSprite", RpcTarget.All, true);
            }

            if (Input.GetButtonDown("Jump"))
            {
                rig.AddForce(transform.up * jumpForce);
            }

            anim.SetFloat(AnimationConst.VelocityX, Mathf.Abs(rig.velocity.x));
            anim.SetFloat(AnimationConst.VelocityY, rig.velocity.y);
        }
    }

    [PunRPC]
    public void RotateSprite(bool rotate)
    {
        spriteRenderer.flipX = rotate;
    }
}
