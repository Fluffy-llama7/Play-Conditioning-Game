using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbController : MonoBehaviour
{
    [SerializeField] private float damage = 3.0f;
    private CircleCollider2D orbCollider;
    private PhysicsMaterial2D orbPhysics;

    void Awake()
    {
        orbCollider = GetComponent<CircleCollider2D>();
        orbPhysics = orbCollider.sharedMaterial;

        SetOrb();
    }

    void SetOrb()
    {
        switch (GameManager.instance.GetVersion())
        {
            case 1:
                orbPhysics.bounciness = 0.8f;
                damage = 3.0f;
                break;
            case 2:
                orbPhysics.bounciness = 0.0f;
                damage = 6.0f;
                break;
            case 3:
                Destroy(this.gameObject);
                break;
        }
    }

    public float GetDamage()
    {
        return damage;
    }
}
