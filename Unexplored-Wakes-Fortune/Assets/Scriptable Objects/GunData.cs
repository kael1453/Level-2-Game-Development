using UnityEngine;

[CreateAssetMenu(fileName="Gun", menuName="Weapon/Gun")]
public class GunData : ScriptableObject
{
    [Header("Info")]
    public new string name;
    public GameObject bulletPrefab;

    [Header("Shooting")]
    public bool isAutomatic;
    public float damage;
    public float maxDistance;

    [Header("Reloading")]
    public int currentAmmo;
    public int magazineSize;
    public float fireRate;
    public float reloadTime;

    [Header("Physics")]
    public float impactForce;
    [HideInInspector] public bool reloading;
}