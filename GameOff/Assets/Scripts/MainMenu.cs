using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

   public void Jogar()
    {
        int jogar = Random.Range(0, 3);
        if (jogar == 1)
        {
            SceneManager.LoadScene("Runner");
        }
        else
        {
            SceneManager.LoadScene("Runner 1");
        }
    }

    public void Creditos()
    {
        SceneManager.LoadScene("Creditos");
    }
}
