using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

//reqiere component aracýlýðý ile bu scriptin component olarak eklendiði her bir gameobject,
//bahsi geçen typeof(...) kýsmýndaki ... componentlerine otomatik olarak sahip olur.
//unity arayüzü içerisinde bunu görebiliriz.
#region GEREKLI BILESENLER - REQUIRED COMPONENTS
[RequireComponent(typeof (Health))]
[RequireComponent(typeof(SortingGroup))] //rendering eklenmeli namespace
[RequireComponent(typeof (SpriteRenderer))]
[RequireComponent(typeof (Animator))]
[RequireComponent(typeof (BoxCollider2D))]
[RequireComponent(typeof (PolygonCollider2D))] //mermileri yiyip yemediðini anlamak için kullanacak oldugumuz component
[RequireComponent(typeof (Rigidbody2D))]
[RequireComponent(typeof(IdleEvent))]
[RequireComponent(typeof(AimWeaponEvent))]


[DisallowMultipleComponent]
#endregion GEREKLI BILESENLER - REQUIRED COMPONENTS

public class Player : MonoBehaviour
{
    [HideInInspector] public PlayerDetailsSO playerDetails;
    [HideInInspector] public Health health;
    [HideInInspector] public IdleEvent idleEvent;
    [HideInInspector] public AimWeaponEvent aimWeaponEvent;
    [HideInInspector] public SpriteRenderer spriteRenderer;
    [HideInInspector] public Animator animator;

    private void Awake()
    {
        health = GetComponent<Health>();

        idleEvent = GetComponent<IdleEvent>();
        aimWeaponEvent = GetComponent<AimWeaponEvent>();

        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    public void Initialise(PlayerDetailsSO playerDetails)
    {
        this.playerDetails = playerDetails;

        SetPlayerHealth();
    }

    private void SetPlayerHealth()
    {
        health.SetStartingHealth(playerDetails.playerHealthAmount);
    }
}
