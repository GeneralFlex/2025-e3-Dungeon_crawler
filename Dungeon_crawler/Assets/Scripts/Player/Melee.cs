using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Melee : MonoBehaviour
{
    [SerializeField] private int damage = 1;
    private PlayerInput playerInput;
    private InputAction attackAction;

    private void Start()
    {
        playerInput = GetComponent<PlayerInput>();
        attackAction = playerInput.actions["Attack"];
    }
    public void OnAttack()
    {
        Debug.Log("attack");

        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        Vector2 direction = (mousePos - transform.position).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        GameObject meleeHitbox = new GameObject("MeleeHitbox");
        meleeHitbox.transform.position = transform.position + (Vector3)(direction * 1f);
        meleeHitbox.transform.rotation = Quaternion.Euler(0, 0, angle);

        BoxCollider2D collider = meleeHitbox.AddComponent<BoxCollider2D>();
        collider.isTrigger = true;
        collider.size = new Vector2(1f, 1f);

        Damage damageComponent = meleeHitbox.AddComponent<Damage>();
        damageComponent.SetDamage(damage);

        Destroy(meleeHitbox, 0.1f);
    }

}
