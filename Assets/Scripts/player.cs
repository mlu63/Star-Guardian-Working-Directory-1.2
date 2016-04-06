using UnityEngine;
using System.Collections;

public class player : MonoBehaviour
{
	private bool facingRight = false;
	public float speed;
    public float fuel;
    public float maxFuel = 30;
    public int score = 0;
    public GUIText scoreText;
    public GUIText highScoreText;
    public Sprite goodStar;
    public Sprite badStar;
    public ParticleSystem goodParticle;
    public ParticleSystem badParticle;

    private float lastUpdate = 1;
    private float timeOfLevel = 60;

    public Rect fuelRect;
    public Texture2D fuelTexture;

    public AudioClip badStarAudio;
    public AudioClip goodStarAudio;
    private AudioSource source;

    void Start()
    {
        source = GetComponent<AudioSource>();
        fuel = maxFuel;

        fuelRect = new Rect(Screen.width / 4.63F, Screen.height/ 1.2F, Screen.width / 22, Screen.height / 30);
        fuelTexture = new Texture2D(1, 1);
        fuelTexture.SetPixel(0, 0, Color.red);
        fuelTexture.Apply();

		//Set high score to 0 on first run
        if (!PlayerPrefs.HasKey("highScore"))
        {
        	PlayerPrefs.SetInt("highScore", 0);
        }
        else
        {
			highScoreText.text = "High Score: " + PlayerPrefs.GetInt("highScore");
        }
    }

    // Update is called once per frame
    void Update ()
	{
		if (Input.GetKey(KeyCode.RightArrow) && Time.timeScale != 0.0f)
		{
			if (!facingRight)
			{
				flip();
			}
            transform.Translate(Vector2.right * 4f * Time.deltaTime);
            if(fuel > 0)
            {
                fuel = fuel - .01F;
            }
        }
		else if (Input.GetKey(KeyCode.LeftArrow) && Time.timeScale != 0.0f)
		{
			if (facingRight)
			{
				flip();
			}
            transform.Translate(Vector2.left * 4f * Time.deltaTime);
            if (fuel > 0)
            {
                fuel = fuel - .01F;
            }
        }

        if (transform.position.x <= -3.0f)
        {
            transform.position = new Vector2(-3.0f, transform.position.y);
        }
        else if (transform.position.x >= 3.0f)
        {
            transform.position = new Vector2(3.0f, transform.position.y);
        }

        if (fuel > 0)
        {
            if ((Time.time - lastUpdate) >= .3f)
            {
                fuel = fuel - .1F;
                lastUpdate = Time.time;
            }
        }

        //Stop game if you have 0 fuel!
        if(fuel <= 0)
        {
            Time.timeScale = 0;
        }

        //Game stops after 60 seconds
        if ((Time.time - 60f) >= 0)
        {
            Time.timeScale = 0;
        }
    }


//Change direction player sprite is facing
void flip()
	{
		facingRight = !facingRight;

		Vector2 scale = transform.localScale;
		scale.x *= -1;
		transform.localScale = scale;
	}

    void OnCollisionEnter2D(Collision2D col)
    {
        //If the player hits the star
        if (col.gameObject.name == "Star(Clone)")
        {
            //Destroy star
            Destroy(col.gameObject);

            //Instantiate particle effect


            //If the star is a good star
            if (col.gameObject.GetComponent<SpriteRenderer>().sprite ==  goodStar)
            {
				ParticleSystem particles = Instantiate(goodParticle, col.transform.position, Quaternion.identity) as ParticleSystem;

                source.PlayOneShot(goodStarAudio, 5);
                //Add 1 to score
                score = score + 1;
                //Set score to reflect new score
                scoreText.text = "Score: " + score;

				//Update high score if achieved
                if (score > PlayerPrefs.GetInt("highScore"))
                {
                	PlayerPrefs.SetInt("highScore", score);
                	highScoreText.text = "High Score: " + PlayerPrefs.GetInt("highScore");
                }

                //If the player is not at max fuel, add to his fuel
                if(fuel < (maxFuel-1))
                {
                    fuel++;
                }
                else if(fuel > (maxFuel - 1) && fuel < maxFuel)
                {
                    fuel = maxFuel;
                }
            }
            //If the player hits a bad star
            if (col.gameObject.GetComponent<SpriteRenderer>().sprite == badStar)
            {
				ParticleSystem particles = Instantiate(badParticle, col.transform.position, Quaternion.identity) as ParticleSystem;

                source.PlayOneShot(badStarAudio, 5);
                //Take away 1 fuel from user if there is 1 fuel left, else if there is less then 1 left
                //set fuel to 0 to avoid the bar going to far down
                if (fuel >= 1)
                {
                    fuel--;
                }
                else if(fuel > 0 && fuel < 1)
                {
                    fuel = 0;
                }
            }
        }
    }

    //Drawing the fuelgauge
    void OnGUI()
    {
        float ratio = -(fuel / maxFuel);
        float rectHeight = ratio * Screen.height / 2;
        fuelRect.height = rectHeight;
        GUI.DrawTexture(fuelRect, fuelTexture);
    }
}
