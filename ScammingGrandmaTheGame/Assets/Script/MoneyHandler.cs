using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class MoneyHandler : MonoBehaviour {
    public static int money;
    public GameObject moneyText;
    
    void Update() {
        UpdateMoney();
        if (money >= 50000) {
            SceneManager.LoadScene("GameWinScene");
        }
    }

    public void UpdateMoney(){
        Text moneyTextTemp = moneyText.GetComponent<Text>();
        moneyTextTemp.text = "$" + money;
    }
}