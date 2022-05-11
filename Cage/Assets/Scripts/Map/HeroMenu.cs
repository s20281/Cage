using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeroMenu : MonoBehaviour
{
    public GameObject heroPanel;
    public Hero hero;

    public Text maxHealhText;
    public Text speedText;
    public Text strengthText;
    public Text dodgeText;
    public Text accuracyText;

    public Text levelText;
    public Text expText;

    public Image icon;

    public GameObject levelUpButtons;
    public GameObject levelUpText;
    public GameObject levelUpPoints;
    public GameObject exp;

    private void Start()
    {
        heroPanel.SetActive(false);
        GameEventSystemMap.Instance.OnHeroSelect += changeHero;
    }


    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.T))
        {
            bool active = heroPanel.activeSelf;

            if(!active)
            {
                updateUI();
            }

            heroPanel.SetActive(!active);
        }
    }

    public void increaseHealth()
    {
        hero.maxHealth += 2;
        hero.levelUpPoints--;
        updateUI();
    }

    public void increaseSpeed()
    {
        hero.speed += 1;
        hero.levelUpPoints--;
        updateUI();
    }

    public void increaseStrength()
    {
        hero.strength += 1;
        hero.levelUpPoints--;
        updateUI();
    }

    public void increaseDodge()
    {
        hero.dodge += 1;
        hero.levelUpPoints--;
        updateUI();
    }

    public void increaseAccuracy()
    {
        hero.accuracy += 1;
        hero.levelUpPoints--;
        updateUI();
    }

    private void updateUI()
    {
        icon.sprite = hero.icon;
        maxHealhText.text = hero.maxHealth.ToString();
        speedText.text = hero.speed.ToString();
        strengthText.text = hero.strength.ToString();
        dodgeText.text = hero.dodge.ToString();
        accuracyText.text = hero.accuracy.ToString();
        exp.GetComponent<Text>().text = hero.exp.ToString() + " / " + Hero.expRequiredForLvl[hero.level + 1];
        levelText.text = hero.level.ToString();

        if(hero.levelUpPoints > 0)
        {
            levelUpButtons.SetActive(true);
            levelUpText.SetActive(true);
            levelUpPoints.SetActive(true);

            if (hero.levelUpPoints == 1)
                levelUpPoints.GetComponent<Text>().text = "1 point";
            else
                levelUpPoints.GetComponent<Text>().text = hero.levelUpPoints + " points";
        }
        else
        {
            levelUpButtons.SetActive(false);
            levelUpText.SetActive(false);
            levelUpPoints.SetActive(false);
        }
        gameObject.GetComponent<HeroInventory>().activeHero = hero;
        gameObject.GetComponent<HeroInventory>().ReloadItems();
    }

    private void changeHero(Hero hero)
    {
        this.hero = hero;
        updateUI();
    }
}
