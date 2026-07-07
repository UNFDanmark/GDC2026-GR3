using Unity.Mathematics;
using Unity.VisualScripting.Dependencies.NCalc;
using UnityEngine;

public class Heat : MonoBehaviour
{
    [Header("Settings")] 
    [SerializeField] float maxHeat = 100f;

    [SerializeField] float heatDrainPerSecondBase = 1f;

    [SerializeField] float currentHeat;

    public float CurrentHeat
    {
        get => currentHeat;
        private set => currentHeat = value;
    }

    [SerializeField] GameObject loseScreen;
    bool dead;
    [Header("Public variables")]
    [Tooltip("Adds its value directly to heatDrainPerSecondBase, so if hDPSB is 1 and this modifier is 1, total hDPS will be 2")]
    public float HeatDrainModifier = 0f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentHeat = maxHeat;
    }
    
    public void AddToCurrentHeat(float heatValue)
    {
        currentHeat = currentHeat + math.clamp(heatValue, -maxHeat, maxHeat);

    }
    // Update is called once per frame
    void Update()
    {
        currentHeat = currentHeat - (heatDrainPerSecondBase + HeatDrainModifier) * Time.deltaTime;
        currentHeat = math.clamp(currentHeat, 0, maxHeat);
        if (currentHeat <= 0 && !dead)
        {
            dead = true;
            Time.timeScale = 0;
            loseScreen.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }
}
