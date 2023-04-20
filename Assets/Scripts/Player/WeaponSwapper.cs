using UnityEngine;
using System.Collections;

public class WeaponSwapper : MonoBehaviour
{
    public GameObject[] weapons;
    private int currentWeaponIndex;

    // Use this for initialization
    void Start()
    {
        // Set the first weapon as active
        currentWeaponIndex = 0;
        weapons[currentWeaponIndex].SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        // Check for input to switch weapons
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            // Deactivate the current weapon
            weapons[currentWeaponIndex].SetActive(false);

            // Increment the weapon index and loop back to the first weapon if necessary
            currentWeaponIndex++;
            if (currentWeaponIndex >= weapons.Length)
            {
                currentWeaponIndex = 0;
            }

            // Activate the new current weapon
            weapons[currentWeaponIndex].SetActive(true);
        }
    }
}