using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchImage : MonoBehaviour {

    [SerializeField]
    GameObject imagem;
    public float tempo;

	// Use this for initialization
	void Start () {
        tempo = 0;
	}
	
	// Update is called once per frame
	void Update () {

        tempo += Time.deltaTime;

        if (tempo >= 10)
        {
            imagem.SetActive(false);
        }

	}

}
