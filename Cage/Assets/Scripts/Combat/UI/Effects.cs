using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Effects : MonoBehaviour
{
    Text effectText;
    private float timer = 0f;
    public float displayTime = 1f;

    private void Awake()
    {
        effectText = GetComponent<Text>();
    }
    // Start is called before the first frame update
    void Start()
    {
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        if(timer < 0)
        {
            gameObject.SetActive(false);
        }
    }

    public void displayEffect(string text, Color color)
    {
        this.effectText.text = text;
        this.effectText.color = color;
        gameObject.SetActive(true);
        timer = displayTime;
    }

}
