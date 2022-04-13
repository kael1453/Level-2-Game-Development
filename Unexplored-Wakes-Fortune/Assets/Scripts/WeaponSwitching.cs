using UnityEngine;

public class WeaponSwitching : MonoBehaviour
{
    public int selectedWeapon = 0;

    void Start()
    {
        SelectWeapon();
    }

    void Update()
    {
        int previousSelectedWeapon = selectedWeapon;

        // Selecting weapons by scrolling up.
        if(Input.GetAxis("Mouse ScrollWheel") > 0f)
        {
            if(selectedWeapon >= transform.childCount - 1)
            {
                selectedWeapon = 0;
            }
            else
            {
                selectedWeapon++;
            }
        }

        // And the same for scrolling down.
        if(Input.GetAxis("Mouse ScrollWheel") < 0f)
        {
            if(selectedWeapon <= 0)
            {
                selectedWeapon = transform.childCount - 1;
            }
            else
            {
                selectedWeapon--;
            }
        }

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
        }
    }
}
