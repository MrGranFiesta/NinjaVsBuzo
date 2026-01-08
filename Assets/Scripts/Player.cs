using Photon.Pun;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(SpriteRenderer))]
public class Player : MonoBehaviour
{
    public float speed = 1;
    public float jumpForce = 200;
    private Rigidbody2D rig;
    private Animator anim;

    void Start()
    {
        if (GetComponent<PhotonView>().IsMine)
        {
            rig = GetComponent<Rigidbody2D>();
            anim = GetComponent<Animator>();
            Debug.Log($"Pos {transform.position + (Vector3.up) + transform.forward * -10}");
            Camera.main.transform.SetParent(transform);
            Camera.main.transform.position = transform.position + (Vector3.up) + transform.forward * - 10;
        }
    }

    void Update()
    {
        if (GetComponent<PhotonView>().IsMine)
        {
            rig.velocity = (transform.right * speed * Input.GetAxis("Horizontal")) + (transform.up * rig.velocity.y);
            Debug.Log("Pasamos por aqui");
            if (rig.velocity.x > 0.1f && GetComponent<SpriteRenderer>().flipX)
            {
                GetComponent<PhotonView>().RPC("RotateSprite", RpcTarget.All, false);
            }
            else if(rig.velocity.x < 0.1f && GetComponent<SpriteRenderer>().flipX) 
            {
                GetComponent<PhotonView>().RPC("RotateSprite", RpcTarget.All, true);
            }

            if (Input.GetButtonDown("Jump"))
            {
                rig.AddForce(transform.up * jumpForce);
            }

            anim.SetFloat("velocityX", Mathf.Abs(rig.velocity.x));
            anim.SetFloat("velocityY", rig.velocity.y);
        }
    }

    [PunRPC]
    public void RotateSprite(bool rotate)
    {
        GetComponent<SpriteRenderer>().flipX = rotate;
    }
}
