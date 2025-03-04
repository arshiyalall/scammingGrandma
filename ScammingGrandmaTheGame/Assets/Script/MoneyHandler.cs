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
            StartCoroutine(takeItAllIn());
        }
    }

    public void UpdateMoney(){
        Text moneyTextTemp = moneyText.GetComponent<Text>();
        moneyTextTemp.text = "$" + money;
    }

    IEnumerator takeItAllIn() {
        yield return new WaitForSeconds(1.5f);
        GameHandler.dayNumber--;
        SceneManager.LoadScene("GameWinScene");
    }
}