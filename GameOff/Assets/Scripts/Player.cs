using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Player : MonoBehaviour {

    public float speed;
    public float minSpeed = 1f;
    public float maxSpeed = 2f;

    [SerializeField]
    GameObject Vida_1;
    [SerializeField]
    GameObject Vida_2;

    public int horizVel = 0;
    public int laneNum = 0;
    public int pontos;
    public int vidas = 3;

    public bool Swimming = false;
    private bool isSwipping = false;

    public KeyCode l;
    public KeyCode r;


    Animator anim;

    private Rigidbody rb;

    public Text txtScore;

    private Vector2 startingTouch;
    public Vector3 verticalTargetPosition;

    // Use this for initialization
    void Start() {

        rb = GetComponent<Rigidbody>();
        anim = GetComponentInChildren<Animator>();
        speed = minSpeed;

    }

    // Update is called once per frame
    void Update() {

        txtScore.text = "Score: " + pontos.ToString();

        //Movimentação do personagem pelas setas direcionais
        if (Input.GetKeyDown(l) && (laneNum >= 0))
        {
            horizVel = -2;
            transform.Translate(-1, 0, 0);
            laneNum -= 1;
        }
        else if (Input.GetKeyDown(r) && (laneNum < 1))
        {
            horizVel = 2;
            transform.Translate(+1, 0, 0);
            laneNum += 1;
        }
        
        //Movimentação do personagem pela tela do celular
        if (Input.touchCount == 1)
        {
            if (isSwipping)
            {
                Vector2 diff = Input.GetTouch(0).position - startingTouch;
                diff = new Vector2(diff.x / Screen.width, diff.y / Screen.width);
                if (diff.magnitude > 0.01f)
                {
                    if (Mathf.Abs(diff.y) > Mathf.Abs(diff.x))
                    {
                        if (diff.y < 0)
                        {

                        }
                        else
                        {
                            //Jump();
                        }
                    }
                    else
                    {
                        if (diff.x < 0 && laneNum >= 0)
                        {
                            horizVel = -2;
                            transform.Translate(-1, 0, 0);
                            laneNum -= 1;
                            // ChangeLane(-1);
                        }
                        else if(diff.x > 0 && laneNum < 1)
                        {
                            horizVel = 2;
                            transform.Translate(+1, 0, 0);
                            laneNum += 1;
                            // ChangeLane(1);

                        }
                    }

                    isSwipping = false;
                }

            }

            if (Input.GetTouch(0).phase == TouchPhase.Began)
            {
                startingTouch = Input.GetTouch(0).position;
                isSwipping = true;
            }
            else if (Input.GetTouch(0).phase == TouchPhase.Ended)
            {
                isSwipping = false;
            }

        }


    }

   /* void ChangeLane(int direction)
    {
        int targetLane = horizVel + direction;
        if (targetLane < 0 || targetLane > 2)
            return;

        horizVel = targetLane;
        verticalTargetPosition = new Vector3((horizVel - 1), 0, 0);
    }
    */
    private void FixedUpdate()
    {
        Vector3 movement = new Vector3(0, 0, speed);
        rb.AddForce(movement);


        if (speed < maxSpeed)
        {
            speed += Time.deltaTime;
        }
        else if(speed >= maxSpeed)
        {
            speed = maxSpeed;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Swimming"))
        {
            anim.SetBool("Swimming", true);
            Swimming = true;
        }

        if (other.CompareTag("Obstacle") && vidas == 1)
        {
            SceneManager.LoadScene("GameOver");
        }
        else if(other.CompareTag("Obstacle") && vidas == 3)
        {
            Destroy(Vida_1.gameObject);
            vidas = 2;
        }
        else if (other.CompareTag("Obstacle") && vidas == 2)
        {
            Destroy(Vida_2.gameObject);
            vidas = 1;
        }

        if (other.CompareTag("Coin"))
        {
            Destroy(other.gameObject);
            pontos += 10;
            GetComponent<AudioSource>().PlayOneShot(GetComponent<AudioSource>().clip);
        }
    }

}
