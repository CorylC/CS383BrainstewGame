using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class PlayerStats : MonoBehaviour
{

    //bug fix: persist health across all scenes
    public static float persistHealth = -1f;
    public float health = 100f;
    public float maxHealth = 100f;
    public int baseAttackDmg = 10;
    //could add defense or speed 
    public Image healthBar;


    void Awake(){
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    void OnDestroy(){
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
    //reset health whenever MainMenu(0) or FirstLevel (1) loads
    void OnSceneLoaded(Scene scene, LoadSceneMode mode){
        if(scene.buildIndex == 0 || scene.buildIndex == 1){
            persistHealth = maxHealth;
            health = maxHealth;
            UpdateHealthBar();
        }
    }
    void Start()
    {
        if(persistHealth < 0f){
            health = maxHealth;
        }else{
            health = persistHealth;
        }
        UpdateHealthBar();
    }


    void Update()
    {
        UpdateHealthBar();
        persistHealth = health;

        if(health <= 0f)
        {
            //player die animation
            Die();
        }
    }

    void UpdateHealthBar()
    {
        if(healthBar != null)
        {
            healthBar.fillAmount = Mathf.Clamp(health / maxHealth,0,1);
        }
    }

    //any time another script calls this function, it can send in however much dmg it sends to playe
    //enemies will ref this class and function so player can take dmg from contact hits or projectiles
    public void TakeDamage(float damage)
    {
        bool bcMode = PlayerPrefs.GetInt("BCMode",0) == 1;
        if(!bcMode) //update damage to health when not in bc mode
        {
            health -= damage;
            UpdateHealthBar();

            if(health <= 0f)
            {
                Die();
            }
        }    
    }

    public void Heal(float amount)
    {
        health = Mathf.Min(health + amount,maxHealth);
        UpdateHealthBar();
    }

    void Die()
    {
        //player die logic and animation when added
        if(PointManager.instance != null){
            PointManager.instance.ResetPoints();
        }

        var animCtrl = GetComponent<PAnimatorController>();
    if (animCtrl != null)
        animCtrl.TriggerDeath();

        // Disable components that might interfere
        GetComponent<Move>().enabled = false;
        GetComponent<Jump>().enabled = false;
        GetComponent<CONTROLLER>().enabled = false;
        GetComponent<Rigidbody2D>().linearVelocity = Vector2.zero; // Stop movement

        // Wait for the full animation length
        Animator anim = GetComponent<Animator>();
        float deathClipLength = anim.GetCurrentAnimatorStateInfo(0).length;
        Invoke(nameof(TriggerGameOver), deathClipLength + 0.3f);
    }

    void TriggerGameOver()
    {
        //load the game over screen when player dies
        SceneManager.LoadScene(4);
    }

    public float GetBaseAttackDmg()
    {
        return baseAttackDmg;
    }
}
