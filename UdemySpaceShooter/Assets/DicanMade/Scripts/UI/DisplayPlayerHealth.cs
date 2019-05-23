using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class DisplayPlayerHealth : MonoBehaviour
{
    TextMeshProUGUI healthText;
    float playerHealth;
    Player player;
    // Start is called before the first frame update
    void Start()
    {
        healthText = GetComponent<TextMeshProUGUI>();
        player = FindObjectOfType<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        if(player)
            healthText.text = player.GetPlayerHealth().ToString();
        else
        healthText.text = "0";
    }
}
