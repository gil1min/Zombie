using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SunManager : MonoBehaviour
{
    public static SunManager Instance { get; private set; }
    
    [SerializeField]
    private int sunAmount = 500;
    public int SunAmount
    {
        get { return sunAmount; }
    }

    public TextMeshProUGUI sunAmountText;
    private Vector3 sunPointTextPosition;

    public float produceTime;
    public float produceTimer;
    public GameObject sunPrefab;

    private bool isStartProduce = false;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        UpdateSunAmounntText();
        CalculatePointSunPointTextPosition();
        // StartProduce();
    }

    private void Update()
    {
        if (isStartProduce)
        {
            ProduceSun();
        }
        
    }

    public void StartProduce()
    {
        isStartProduce = true;
    }

    public void StopProduce()
    {
        isStartProduce = false;
    }

    void ProduceSun()
    {
        produceTimer += Time.deltaTime;
        if (produceTimer > produceTime)
        {
            produceTimer = 0;
            Vector3 position = new Vector3(Random.Range(-5f, 6.5f), 6.2f, -1f);
            GameObject go = GameObject.Instantiate(sunPrefab, position, Quaternion.identity);

            position.y = Random.Range(-3f, 3f);
            go.GetComponent<Sun>().LinearTo(position);
            
        }
    }

    public void UpdateSunAmounntText()
    {
        sunAmountText.text = sunAmount.ToString();
    }

    public void SubSun(int amount)
    {
        sunAmount -= amount;
        UpdateSunAmounntText();
    }

    public void AddSun(int amount)
    {
        sunAmount += amount;
        UpdateSunAmounntText();
    }

    public Vector3 GetSunPointTextPosition()
    {
        return sunPointTextPosition;
    }

    private void CalculatePointSunPointTextPosition()
    {
        Vector3 position = Camera.main.ScreenToWorldPoint(sunAmountText.transform.position);
        position.z = 0;
        sunPointTextPosition = position;
    }


}
