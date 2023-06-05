using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class EnemyController : MonoBehaviour
{
    public bool canMove;
    public float speed=2.0f;
    public int vertical;
    public float changeTime = 3.0f,canMoveTime=5.0f,lanuchTime=5.0f;
    Animator animator;

    Rigidbody2D rigidbody2d;
    float timer,canMoveTimer;
    int direction = 1;

    public bool broken = true;
    public ParticleSystem smokeEffect;
    public int blood=0,level=1;
    public GameObject BloodTip;
    public GameObject projectilePrefab;
    Vector2 lookDirection = new Vector2(1, 0);

    // 在第一次帧更新之前调用 Start
    void Start()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
        timer = changeTime;
        animator = GetComponent<Animator>();
        blood = level * 5;
        canMove =true;
        if(level==2)    this.GetComponent<AIPath>().enabled = false;
        BloodTip.SetActive(false);
        Text txt = BloodTip.GetComponentInChildren<Text>();
        txt.text = blood.ToString() + "/" + (level * 5).ToString();
    }
    void Update()
    {
        if(canMove==true)
        {
            if (!broken)
            {
                return;
            }
            timer -= Time.deltaTime;
            

            if (timer < 0)
            {
                direction = (Random.Range(0, 2) * 2) - 1;
                vertical = Random.Range(0, 2);
                changeTime = Random.Range(1f, 3f);
                timer = changeTime;
            }
            if(level == 3)
            {
                int hori = 0;
                int verti = 0;
                //机器人方向
                if (vertical==0)
                {
                    hori = direction;
                    verti = 0;
                }
                else
                {
                    hori = 0;
                    verti = direction;
                }
                
                Vector2 move = new Vector2(hori, verti);

                if (!Mathf.Approximately(move.x, 0.0f) || !Mathf.Approximately(move.y, 0.0f))
                {
                    lookDirection.Set(move.x, move.y);
                    lookDirection.Normalize();
                }
                lanuchTime -= Time.deltaTime;
                if(lanuchTime <= 0)
                {
                    Debug.Log("敌人发射");
                    Launch();
                    lanuchTime = 5.0f;
                }
                
            }
        }
        else
        {
            canMoveTimer -= Time.deltaTime;
            if (canMoveTimer < 0)
            {
                canMove = true;
                //rigidbody2d.simulated = true;
            }
        }

    }
     void FixedUpdate()
    {
        if (canMove==true)
        {
            if (!broken)
            {
                return;
            }

            Vector2 position = rigidbody2d.position;
            if (vertical != 0)
            {
                position.y = position.y + Time.deltaTime * speed * direction;
                animator.SetFloat("Move X", 0);
                animator.SetFloat("Move Y", direction);
            }
            else
            {
                position.x = position.x + Time.deltaTime * speed * direction;
                animator.SetFloat("Move X", direction);
                animator.SetFloat("Move Y", 0);
            }

            rigidbody2d.MovePosition(position);
        }
        
    }
    void OnCollisionEnter2D(Collision2D other)
    {
        RubyController player = other.gameObject.GetComponent<RubyController>();

        if (player != null)
        {
            player.ChangeHealth(-1);
        }
        if (other.gameObject.CompareTag("Obstacle")|| other.gameObject.GetComponent<Tilemap>()) // 如果碰到的是障碍物
        {
            //Debug.Log("1111");
            direction = -direction;
            //vertical = Random.Range(0, 2);
            changeTime = Random.Range(1f, 3f);
            timer = changeTime;
        }
    }
    public void Fix()
    {
        broken = false;
        rigidbody2d.simulated = false;
        animator.SetTrigger("Fixed");
        smokeEffect.Stop();
        UIHealthBar.instance.UpdateEnemyCount(1);//更新敌人数
        BuffManager.instance.randombuff();
    }
    public void ChangeBlood(int amount,int Level)
    {
        if (blood-amount <=0)
        {
            Fix();
            if(level>1)
            {
                this.GetComponent<AIPath>().enabled = false;
                this.GetComponent<EnemyController>().enabled = true;
            }

        }
        else
        {
            blood -= amount;
            Text txt = BloodTip.GetComponentInChildren<Text>();
            txt.text = blood.ToString() + "/" + (level * 5).ToString();
            if (Level == 3)
            {
                canMove = false;
                canMoveTimer = canMoveTime;
            }
            if(level==2&&blood<5&&canMove)
            {
                this.GetComponent<AIPath>().enabled = true;
                this.GetComponent<EnemyController>().enabled = false;
                animator.SetTrigger("Fixed");
            }
   
        }

    }
    void Launch()
    {
        GameObject projectileObject = Instantiate(projectilePrefab, rigidbody2d.position + Vector2.up * 0.5f, Quaternion.identity);
        EnemyBullet projectile = projectileObject.GetComponent<EnemyBullet>();
        projectile.Launch(lookDirection, 300);
    }
}
