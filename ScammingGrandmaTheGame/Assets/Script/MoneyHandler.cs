using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class MoneyHandler : MonoBehaviour {
    public static int money;
    public GameObject moneyText;

    void Awake() {
        money = 0;
    }
    
    void Update() {
        UpdateMoney();
    }

    public void UpdateMoney(){
        Text moneyTextTemp = moneyText.GetComponent<Text>();
        moneyTextTemp.text = "$" + money;
    }
}