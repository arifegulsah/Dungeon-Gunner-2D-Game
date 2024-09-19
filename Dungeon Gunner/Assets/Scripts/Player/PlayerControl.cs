using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    [SerializeField] private Transform weaponShootTransform;


    private Player player;

    public void Awake()
    {
        player = GetComponent<Player>();
    }




    public void Update()
    {
        MovementInput();

        WeaponInput();
    }


    private void MovementInput()
    {
        player.idleEvent.CallIdleEvent();
    }

    private void WeaponInput()
    {
        Vector3 weaponDirection;
        float weaponAngleDegrees, playerAngleDegrees;
        AimDirection playerAimDirection;

        AimWeaponInput(out weaponDirection, out weaponAngleDegrees, out playerAngleDegrees, out playerAimDirection);
    }

    private void AimWeaponInput(out Vector3 weaponDirection, out float weaponAngleDegrees, out float playerAngleDegrees, out AimDirection playerAimDirection)
    {
        Vector3 mouseWorldPosition = HelperUtilities.GetMouseWorldPosition();
        weaponDirection = (mouseWorldPosition - weaponShootTransform.position);

        Vector3 playerDirection = (mouseWorldPosition - transform.position);

        weaponAngleDegrees = HelperUtilities.GetAngleFromVector(weaponDirection);
        playerAngleDegrees = HelperUtilities.GetAngleFromVector(playerDirection);

        playerAimDirection = HelperUtilities.GetAimDirection(playerAngleDegrees);

        player.aimWeaponEvent.CallAimWeaponEvent(playerAimDirection, playerAngleDegrees, weaponAngleDegrees, weaponDirection);
    }
}