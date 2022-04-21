using UnityEngine;

public class WeaponSwitching : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform[] weapons;

    [Header("Keys")]
    [SerializeField] private KeyCode[] keys;

    private int selectedWeapon;

    private void Start()
    {
        SetWeapons();
        Select(selectedWeapon);
    }

    private void Update()
    {
        int previousSelectedWeapon = selectedWeapon;

        // Selecting weapons by scrolling up.
        if (Input.GetAxis("Mouse ScrollWheel") > 0f)
        {
            if (selectedWeapon >= transform.childCount - 1)
            {
                selectedWeapon = 0;
            }
            else
            {
                selectedWeapon++;
            }
        }

        // And the same for scrolling down.
        if (Input.GetAxis("Mouse ScrollWheel") < 0f)
        {
            if (selectedWeapon <= 0)
            {
                selectedWeapon = transform.childCount - 1;
            }
            else
            {
                selectedWeapon--;
            }
        }

        // Selecting weapons using the number keys.
        for (int i = 0; i < keys.Length; i++)
        {
            if (Input.GetKeyDown(keys[i]))
            {
                selectedWeapon = i;
            }
        }

        // Only bother to equip weapon if the new selected weapon is different from the current one.
        if (previousSelectedWeapon != selectedWeapon)
        {
            Select(selectedWeapon);
        }
    }

    private void SetWeapons()
    {
        weapons = new Transform[transform.childCount];

        for (int i = 0; i < transform.childCount; i++)
        {
            weapons[i] = transform.GetChild(i); // Get the transforms of all the weapons.
        }

        if (keys == null)
        {
            keys = new KeyCode[weapons.Length];
        }
    }

    private void Select(int weaponIndex)
    {
        for (int i = 0; i < weapons.Length; i++)
        {
            weapons[i].gameObject.SetActive(i == weaponIndex);
        }

        OnWeaponSelected();
    }

    private void OnWeaponSelected()
    {
        print("Weapon selected!");
    }


    /*
    public int selectedWeapon = 0;

    void Start()
    {
        SelectWeapon();
    }

    void Update()
    {
        int previousSelectedWeapon = selectedWeapon;

        

        // If the current weapon is not the same as the previous, change to that new weapon.
        if (previousSelectedWeapon != selectedWeapon)
        {
            SelectWeapon();
        }
    }

    // Activate the current weapon.
    void SelectWeapon(){
        int i = 0;
        foreach (Transform weapon in transform)
        {
            if(i == selectedWeapon)
            {
                weapon.gameObject.SetActive(true);
            }
            else
            {
                weapon.gameObject.SetActive(false);
            }
            i++;
        }*/
}

