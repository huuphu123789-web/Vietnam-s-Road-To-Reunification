    using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarScript : MonoBehaviour
{
    [SerializeField] Slider healthBar;
    [SerializeField] TextMeshProUGUI healthValueText,ammoText,grenadeText;

    [SerializeField] int maxHealth ;
    [SerializeField] int ammoCount ;
    [SerializeField] int grenadeCountUI ;
    void Start()
    {
        if (PlayerController.instance != null)
        {
            PlayerController.instance.playerHp = maxHealth;
            PlayerController.instance.ammo=ammoCount;
            PlayerController.instance.grenadeCount=grenadeCountUI;
           
        }
        else
        {
           
        }
    }

    // Update is called once per frame
    void Update()
    {
       
            healthValueText.text = PlayerController.instance.playerHp.ToString() + "/" + maxHealth.ToString();
            ammoText.text = PlayerController.instance.ammo.ToString() + "/" + ammoCount.ToString();
            grenadeText.text = PlayerController.instance.grenadeCount.ToString() + "/" + grenadeCountUI.ToString();
            healthBar.value = PlayerController.instance.playerHp;
            healthBar.maxValue = maxHealth;
        
    }
}
