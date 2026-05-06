using System;
using System.Linq;
using UnityEngine;
using Photon.Pun;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(SpriteRenderer))]
public class Bat : MonoBehaviourPun
{
    //private Transform[] players;
    private float distancia;

    [NonSerialized] public Vector3 PointInital;
    private Animator animator;
    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        PointInital = transform.position;
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        //TODO NO encuentra a los jugadores, revisar
        /*players = GameObject.FindGameObjectsWithTag(TagsUtils.Player)
            .Select(p => p.transform)
            .ToArray();*/
    }

    
    private void Update()
    {
        if (!PhotonNetwork.IsMasterClient) return;
        
        distancia = GetDistancePlayerNearby();
        animator.SetFloat("Distancia", distancia);
    }

    [PunRPC]
    public void FlipX(Vector3 objetivo)
    {
        if (transform.position.x < objetivo.x)
        {
            spriteRenderer.flipX = true;
        }
        else
        {
            spriteRenderer.flipX = false;
        }
    }

    private float GetDistancePlayerNearby()
    {
        Transform playerNearby = GetPlayerNearby();

        if (playerNearby == null)
        {
            return float.MaxValue;
        }

        return Vector2.Distance(transform.position, playerNearby.position);
    }

    public Transform GetPlayerNearby()     {
        if (GameManager.Players == null || GameManager.Players.Count == 0)
        {
            return null;
        }
        return GameManager.Players.OrderBy(j => Vector2.Distance(transform.position, j.position))
            .FirstOrDefault();
    }
}
