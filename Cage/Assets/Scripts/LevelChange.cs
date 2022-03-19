using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelChange : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            collision.gameObject.SetActive(false);

            StaticClass.setEnemies(collision.GetComponent<EnemyInfo>().enemiesToLoad);

            GameEventSystemMap.Instance.SaveData();

            SceneManager.LoadScene("Combat");
        }
    }
}
