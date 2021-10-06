using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private Text _coinText, _livesText;
    
    public void SetCoinsText(int coins) {
      _coinText.text = "Coins: " + coins.ToString();
    }

    public void SetLivesText(int lives) {
      _livesText.text = "Lives: " + lives.ToString();
    }

}
