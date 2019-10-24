using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager InstanceUI;

    [SerializeField] private float limitHealthBar;
    [SerializeField] private float limitBlueScreen;

    private LifeManager lifeScript;

    private GameObject player;

    public RawImage blueScreenOfDeath;
    public Slider sliderHealth;
    public Image fillSliderHealth;
    public Color fullLifeColor;
    public Color noLifeColor;

    public Color transparent;
    public Color blueScreenLayer;

    void Awake()
    {
        InstanceUI = this;
    }
    
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        lifeScript = player.GetComponent<LifeManager>();

        sliderHealth.maxValue = lifeScript.maxHealth;
        fillSliderHealth.color = fullLifeColor;
    }
    // Update is called once per frame
    void Update()
    {
        transform.position = player.transform.position - Vector3.up * 0.95f;

        sliderHealth.value = lifeScript.currentHealth;
        fillSliderHealth.color = Color.Lerp(noLifeColor, fullLifeColor, limitHealthBar * sliderHealth.value / sliderHealth.maxValue);

        blueScreenOfDeath.color = Color.Lerp(blueScreenLayer, transparent, limitBlueScreen * sliderHealth.value / sliderHealth.maxValue);
    }
}
