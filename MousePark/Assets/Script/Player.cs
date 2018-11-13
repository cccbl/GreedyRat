using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
   
    //火箭飞行上声速度
    public float jetpackForce = 25f;
    //人物向前移动速度
    public float forwardMovementSpeed = 4f;

    //落地
    private bool grounded=false;
    private bool canMove = true;
   //加速
	float fly=0;
	bool  canFly=false;

    int score = 0;
    public Text scoreText;
    public GameObject gameoverUI;

    public AudioClip coinCollectSound;
    public AudioClip deadSound;
    Animator animator;

    void Start()
    {
        animator=GetComponent<Animator>();
        gameoverUI.gameObject.SetActive(false);
		//重复调用  0秒后调用 0.6s一次　
		InvokeRepeating("downFly",0,0.45f);

    }

    private void FixedUpdate()
    {
        move();
        RestartGame();
    }
		
    void move() {
		
		fly += Time.deltaTime;
        if (canMove)
        {
			//加速
			if(Input.GetKeyDown(KeyCode.H)&&fly>0.3){
				forwardMovementSpeed +=10;
				canFly = true;
				fly = 0;
			}
			if (Input.GetKey(KeyCode.F))
            {
                GetComponent<Rigidbody2D>().AddForce(new Vector2(0, jetpackForce));
            }
            //向前
            Vector2 newVelocity = GetComponent<Rigidbody2D>().velocity;
            newVelocity.x = forwardMovementSpeed;
            GetComponent<Rigidbody2D>().velocity = newVelocity;
        }
    }

	void downFly()
	{
		//减速
		if (canFly) {
			forwardMovementSpeed -=10;
			canFly = false;
		}
	}

    void RestartGame() {

        //当玩家死亡时

            //重新开始
		if (grounded&&Input.GetMouseButtonDown(0)||Input.GetKeyDown(KeyCode.J))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }


    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Coins"))
        {
            score++;
            scoreText.text = "SCORE:" + score;
            Destroy(collision.gameObject);
            AudioSource.PlayClipAtPoint(coinCollectSound, transform.position);
        }
        else if (collision.CompareTag("obstacle"))
        {
                grounded = true;
                canMove = false;

                gameoverUI.SetActive(true);
                animator.SetBool("grounded",true);
                AudioSource.PlayClipAtPoint(deadSound, transform.position);

        }
    }






}
