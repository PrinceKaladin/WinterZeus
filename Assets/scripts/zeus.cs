using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class zeus : MonoBehaviour
{
    public Text score;
    int scorecount = 0;
    bool can = true;
    public AudioSource x;
    public AudioSource correct;
    public AudioSource error;
    public Sprite ready;
    int lives = 3;
    public Sprite bgsprite3;
    public Sprite bgsprite2;
    public Sprite bgsprite1;
    public SpriteRenderer bg;
    
    public Sprite notready;
    private void Awake()
    {
        PlayerPrefs.SetInt("score", 0);
        if (!PlayerPrefs.HasKey("bestscore")) {
            PlayerPrefs.SetInt("bestscore", 0);
        }

    }
    private void Update()
    {
        zeusclick();
    }
    public void zeusclick() {
        if (can)
        {
          
            if (Input.GetMouseButtonDown(0))
            {
                StartCoroutine(reset());
                x.Play();
                Vector2 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                RaycastHit2D hit = Physics2D.Raycast(pos, Vector2.zero);

                if (hit.collider != null)
                {

                    if (hit.collider.tag == "lose")
                    {
                        win();
                        Destroy(hit.collider.gameObject);
                    }
                    if (hit.collider.tag == "win")
                    {
                        lose();
                        Destroy(hit.collider.gameObject);
                    }
                }
                
            }
        }

    }
    private void OnTriggerEnter2D(Collider2D hit)
    {
        
        Destroy(hit.gameObject);
        if (hit.gameObject.tag == "win")
        {
            win();
        }
        if (hit.gameObject.tag == "lose")
        {
            end();
        }
    }
    void win() {
        correct.Play();
        scorecount+=5;
        score.text = "Score: "+ scorecount.ToString();
        if (scorecount > PlayerPrefs.GetInt("bestscore")) {
            PlayerPrefs.SetInt("bestscore", scorecount);

        }
    }
    void lose() {
        error.Play();
        scorecount -= 5;
        score.text = "Score: " + scorecount.ToString();
    }
    void end() {

        if (lives == 3)
        {
            bg.sprite = bgsprite2;


            lives = 2;
        }
        else if (lives == 2)
        {
            bg.sprite = bgsprite1;


            lives = 1;
        }
        else if (lives == 1)
        {
            PlayerPrefs.SetInt("last", scorecount);
            SceneManager.LoadScene(3);
        }
        }

    IEnumerator reset(){
        can = false;
        this.gameObject.GetComponent<SpriteRenderer>().sprite = notready;
        yield return new WaitForSeconds(0.5f);
        can = true;
        this.gameObject.GetComponent<SpriteRenderer>().sprite = ready;
    }
}
