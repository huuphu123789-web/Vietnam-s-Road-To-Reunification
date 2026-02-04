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
        if (PlayController.instance != null)
        {
            PlayController.instance.playerHp = maxHealth;
            PlayController.instance.ammo=ammoCount;
            PlayController.instance.grenadeCount=grenadeCountUI;
           
        }
        else
        {
           
        }
    }

    // Update is called once per frame
    void Update()
    {
       
            healthValueText.text = PlayController.instance.playerHp.ToString() + "/" + maxHealth.ToString();
            ammoText.text = PlayController.instance.ammo.ToString() + "/" + ammoCount.ToString();
            grenadeText.text = PlayController.instance.grenadeCount.ToString() + "/" + grenadeCountUI.ToString();
            healthBar.value = PlayController.instance.playerHp;
            healthBar.maxValue = maxHealth;
        
    }
}
