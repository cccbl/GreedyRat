using UnityEngine;
using System.Collections;

//陷阱
public class LaserScript : MonoBehaviour {

	public Sprite laserOnSprite;    
	public Sprite laserOffSprite;
	
    //陷阱激光打开间隔
	public float interval = 1f;    
	public float rotationSpeed = 0.0f;
	
	private bool isLaserOn = true;    
	private float timeUntilNextToggle;


	// Use this for initialization
	void Start () {
		timeUntilNextToggle = interval;
	}
	
	// Update is called once per frame


	void FixedUpdate() {
		
		timeUntilNextToggle -= Time.fixedDeltaTime;
		
        //陷阱刷新间隔，设置陷阱on / off
		if (timeUntilNextToggle <= 0) {
			
			isLaserOn = !isLaserOn;
            GetComponent<Collider2D>().enabled = isLaserOn;
			
            //渲染激光
			SpriteRenderer spriteRenderer = ((SpriteRenderer)this.GetComponent<Renderer>());
            //true，那么激光为on状态
            if (isLaserOn) { 
                spriteRenderer.sprite = laserOnSprite;
            }
            else
            {
                spriteRenderer.sprite = laserOffSprite;
            }
            //重设刷新时间，
			timeUntilNextToggle = interval;
		}
		
		transform.RotateAround(transform.position, 
		                       Vector3.forward, 
		                       rotationSpeed * Time. fixedDeltaTime);
	}

}
