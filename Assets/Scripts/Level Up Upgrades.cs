using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class LevelUpUpgrades : MonoBehaviour
{
    public string Upgrade1;
    public string Upgrade2;
    public string Upgrade3;

    public GameObject button1;
    public GameObject button2;
    public GameObject button3;

    public TextMeshPro label1;

    public GameObject player;
    public GameObject bullet;
    public GameObject gun;

    enum Upgrades
    {
        Health = 0,
        Damage = 1,
        FireRate = 2,
        MovementSpeed = 3,
        BulletSpeed = 4
    }

    public void GetUpgrades()
    {
        Time.timeScale = 0f;
        button1.SetActive(true);
        button2.SetActive(true);
        button3.SetActive(true);
        Cursor.visible = true;
        System.Enum.TryParse(Random.Range(0, 5).ToString(), out Upgrades upgrade1);
        Upgrade1 = upgrade1.ToString();
        System.Enum.TryParse(Random.Range(0, 5).ToString(), out Upgrades upgrade2);
        Upgrade2 = upgrade2.ToString();
        System.Enum.TryParse(Random.Range(0, 5).ToString(), out Upgrades upgrade3);
        Upgrade3 = upgrade3.ToString();
        button1.GetComponentInChildren<TextMeshProUGUI>().SetText(Upgrade1);
        button2.GetComponentInChildren<TextMeshProUGUI>().SetText(Upgrade2);
        button3.GetComponentInChildren<TextMeshProUGUI>().SetText(Upgrade3);
    }

    public void Button1Click()
    {
        Upgrade(button1);
    }
    public void Button2Click()
    {
        Upgrade(button2);
    }
    public void Button3Click()
    {
        Upgrade(button3);
    }

    public void Upgrade(GameObject buttonClicked)
    {
        var upgrade = buttonClicked.GetComponentInChildren<TextMeshProUGUI>().text;
        button1.SetActive(false);
        button2.SetActive(false);
        button3.SetActive(false);
        Debug.Log(upgrade);
        Debug.Log(Upgrades.Health.ToString());
        float upgradeValue = Random.Range(1, 10);
        if(upgrade == Upgrades.Health.ToString())
        {
            player.GetComponent<PlayerController>().MaxHP += upgradeValue;
            player.GetComponent<PlayerController>().CurrentHP += upgradeValue;
        }
        else if(upgrade == Upgrades.Damage.ToString())
        {
            bullet.GetComponent<BulletController>()._Damage += (int)Mathf.Floor(upgradeValue+1/2);
        }
        else if(upgrade == Upgrades.FireRate.ToString())
        {
            gun.GetComponent<WeaponController>()._timeBetweenShooting -= upgradeValue / 100;
        }
        else if(upgrade == Upgrades.MovementSpeed.ToString())
        {
            player.GetComponent<PlayerController>().MaxSpeedMove += upgradeValue / 5;
        }
        else if(upgrade == Upgrades.BulletSpeed.ToString())
        {
            gun.GetComponent<WeaponController>()._shootForce += upgradeValue / 100;
        }
        Time.timeScale = 1f;
        button1.GetComponentInChildren<TextMeshProUGUI>().SetText("");
        button2.GetComponentInChildren<TextMeshProUGUI>().SetText("");
        button3.GetComponentInChildren<TextMeshProUGUI>().SetText("");
    }
}
