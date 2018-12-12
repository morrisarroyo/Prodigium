using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameUI : MonoBehaviour {

    public Text healthPotsNum;
    public Image armour;
    public Image weapon;

    public Sprite clothArmour;
    public Sprite plateArmour;

    public Sprite sword;
    public Sprite staff;

    public void UpdateWeaponImage(bool swrd)
    {
        weapon.enabled = true;
        if (swrd)
            weapon.sprite = sword;
        else
            weapon.sprite = staff;
    }

    public void UpdateArmourImage(bool physical)
    {
        armour.enabled = true;
        if (physical)
            armour.sprite = plateArmour;
        else
            armour.sprite = clothArmour;
    }

    public void UpdateHealthPots(int num)
    {
        healthPotsNum.text = "" + num;
    }
}
