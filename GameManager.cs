using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Button foodButton;
    public Button sleepButton;
    public Button workButton;
    public Button funButton;
    public Button healthButton;

    public Button restart;

    public Text timeText;
    public Text healthText, foodText, stressText, debtText, sleepText;
    //public Text cheatBox;

    bool dead;

    int gameTime;
    int fed;
    int rested;
    int stress;
    int money;
    int health;
    int overfed;
    // Start is called before the first frame update
    void Start()
    {
        gameTime = 0;
        fed = 10;
        rested = 10;
        stress = 0;
        money = 5;
        health = 10;
        overfed = 0;

        foodButton.onClick.AddListener(eat);
        sleepButton.onClick.AddListener(sleep);
        workButton.onClick.AddListener(work);
        funButton.onClick.AddListener(fun);
        healthButton.onClick.AddListener(doctor);
        restart.onClick.AddListener(restartGame);

        updateUI();
    }

    void restartGame()
    {
        gameTime = 0;
        fed = 10;
        rested = 10;
        stress = 0;
        money = 5;
        health = 10;
        overfed = 0;

        foodButton.enabled = true;
        healthButton.enabled = true;
        foodButton.enabled = true;
        sleepButton.enabled = true;
        workButton.enabled = true;
        funButton.enabled = true;
        healthButton.enabled = true;
        dead = false;
        updateUI();
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    void progress(int time)
    {
        for (int i = 0; i < time; i++)
        {
            Debug.Log("test " + i);
            gameTime++;
            rested--;
            fed--;
            if (overfed > 0)
            {
                fed++;
                overfed -= 2;
            }
            money--;
            if (money > 15)
            {
                money++;
            }

            if (money > 20)
            {
                money += money - 20;
            }

            if (money < 0)
            {
                money += money / 10;
                stress -= money / 2;
            }
            calcHealth();
        }

        
        updateUI();

    }

    void calcHealth()
    {
        if (stress > 5)
        {
            health--;
        }
        if (stress > 10)
        {
            health--;
        }
        if (stress > 15)
        {
            health -= stress - 13;
        }
        if (fed < 5)
        {
            health--;
        }
        if (fed < 0)
        {
            health += fed;
        }
        if (rested < 0)
        {
            health += fed*2;
        }
        if (health < 0)
        {
            healthText.text = "You are dying...";
        }
        if (health < -5)
        {
            death();
        }
        health++;
    }


    void death()
    {
        dead = true;
        
        foodButton.enabled = false;
        healthButton.enabled = false;
        foodButton.enabled = false;
        sleepButton.enabled = false;
        workButton.enabled = false;
        funButton.enabled = false;
        healthButton.enabled = false;

    }

    void fun()
    {
        stress -= 4;
        money--;
        if (money > 10)
        {
            stress -= 4;
            money--;
        }
        progress(2);
        health++;
    }

    void doctor()
    {
        money -= 3;
        health += 6;
        progress(1);
        if (money > 10)
        {
            money--;
            health += 5;
        }
    }

    void work()
    {
        stress += 2;
        money += 3;
        progress(1);
    }

    void sleep ()
    {
        rested += 4;
        progress(1);
        health++;
    }

    void eat()
    {
        fed += 3;
        progress(1);
        if (fed > 10)
        {
            overfed = fed - 10;
            fed = 10;
        }
    }

    void updateUI()
    {
        sleepText.text = "You are well rested.";
        if (rested < 10)
        {
            sleepText.text = "You are rested.";
        }
        if (rested < 5)
        {
            sleepText.text = "You are tired.";
        }
        if (rested < 0)
        {
            sleepText.text = "You are exhausted.";
        }


        healthText.text = "You are healthy.";
        if (health >= 10)
        {
            healthText.text = "You are very healthy.";
        }
        if (health < 5)
        {
            healthText.text = "You feel a little sick.";
        }

        stressText.text = "You are feeling relaxed.";
        if (stress > 5)
        {
            stressText.text = "You are feeling stressed.";
        }
        if (stress > 10)
        {
            stressText.text = "You are feeling very stressed.";
        }

        foodText.text = "You are well fed.";
        if (fed < 5)
        {
            foodText.text = "You are hungry.";
        }
        if (fed < 0)
        {
            foodText.text = "You are starving.";
        }

        if (money > 10)
        {
            debtText.text = "You are financially well off.";
        }
        if (money < 10)
        {
            debtText.text = "You are poor.";
        }
        if (money < 0)
        {
            debtText.text = "You are in debt.";
        }
        if (money < -10)
        {
            debtText.text = "You are in crippling debt.";
        }
        //cheatBox.text =
        //    "gameTime: " + gameTime +
        //"\nfed: " + fed +
        //"\nrested: " + rested +
        //"\nstress: " + stress +
        //"\nmoney: " + money +
        //"\nhealth: " + health;

        if (dead)
        {
            healthText.text = "You are dead.";
            stressText.text = "You are dead.";
            foodText.text = "You are dead.";
            sleepText.text = "You are dead.";
        }

        timeText.text = "Weeks: " + gameTime / 2;
    }
}
