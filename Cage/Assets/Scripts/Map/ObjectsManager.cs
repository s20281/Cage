using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ObjectsManager : MonoBehaviour
{
    public string key;

    private void Awake()
    {
        key = SceneManager.GetActiveScene().name + "." + this.name;
    }

    private void Start()
    {
        if (!PlayerPrefs.HasKey(key))
            PlayerPrefs.SetInt(key, 1);

        else if (PlayerPrefs.GetInt(key) == 0)
        {
            Destroy(this.gameObject);
        }
    }

    public void setOff()
    {
        PlayerPrefs.SetInt(key, 0);
    }

    public void setOn()
    {
        PlayerPrefs.SetInt(key, 1);
    }
}
