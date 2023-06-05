using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class RubyController : MonoBehaviour
{
    public int maxHealth = 5;
    public float timeInvincible = 2.0f;
    public int currentHealth;
    public int maxenergy = 100, curenergy = 0;
    float energyTimer = 2.0f;
    public bool isInvincible;
    public float invincibleTimer;

    Rigidbody2D rigidbody2d;
    float horizontal;

    float vertical;
    public float speed=3f;
    public int health { get { return currentHealth; } }
    public int bulletcount { get { return curBulletCount; } }
    public int BulletLevel=1;
    float skillleft = 5.0f;//剩余技能时间
    Animator animator;
    Vector2 lookDirection = new Vector2(1, 0);

    public GameObject projectilePrefab;
    public GameObject projectilePrefab2;
    public GameObject projectilePrefab3;
    AudioSource audioSource;
    public AudioClip launchClip;
    public AudioClip behitClip;
    public int curBulletCount=0, maxBulletCount=99;//子弹数量
    public int curEnemyCount=0, maxEnemyCount = 5;//当前已修复机器人数
    float  lefttime = 300,launchTime=0f;//游戏倒计时，攻击倒计时
    public GameObject BulletTip;
    public float Lefttime { get { return lefttime; } }

    public static RubyController instance { get; private set; }

    // 在第一次帧更新之前调用 Start
    void Start()
    {
        instance = this;
        Time.timeScale = 1f;
        rigidbody2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        currentHealth = maxHealth;
        curBulletCount = maxEnemyCount*10;
        curenergy = 0;

        audioSource = GetComponent<AudioSource>();
        UIHealthBar.instance.UpdateBulletCount(curBulletCount, maxBulletCount);//更新子弹数
        UIHealthBar.instance.UpdateEnemyCount(curEnemyCount, maxEnemyCount);//更新敌人数
        Ring.instance.UpdateEnergy(curenergy, maxenergy);
        BulletTip.SetActive(false);
    }

    // 每帧调用一次 Update
    void Update()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");
        Vector2 move = new Vector2(horizontal, vertical);

        if (!Mathf.Approximately(move.x, 0.0f) || !Mathf.Approximately(move.y, 0.0f))
        {
            lookDirection.Set(move.x, move.y);
            lookDirection.Normalize();
        }

        animator.SetFloat("Look X", lookDirection.x);
        animator.SetFloat("Look Y", lookDirection.y);
        animator.SetFloat("Speed", move.magnitude);
        if (isInvincible)
        {
            invincibleTimer -= Time.deltaTime;
            if (invincibleTimer < 0)
                isInvincible = false;
        }
        //====================================角色攻击==========================================
        if (Input.GetKeyDown(KeyCode.J)&&launchTime<=0)
        {
            if(curBulletCount>0)
            {
                launchTime = 0.7f;
                ChangeBulletCount(-1);
                Launch();
                PlaySound(launchClip);
            }
            else
            {
                BulletTip.SetActive(true);
                StartCoroutine(StopTip(1f));
            }
           
        }
        if(Input.GetKeyDown(KeyCode.K)&& curenergy > 90)
        {
            curenergy -= 90;
            Ring.instance.UpdateEnergy(curenergy, maxenergy);
            BulletLevel = 2;

        }
        if (Input.GetKeyDown(KeyCode.L) && curenergy > 40)
        {
            Ring.instance.UpdateEnergy(curenergy, maxenergy);
            curenergy -= 40;
            BulletLevel = 3;

        }
        if (Input.GetKeyDown(KeyCode.H) && curenergy > 20)
        {
            Ring.instance.UpdateEnergy(curenergy, maxenergy);
            curenergy -= 20;
            BuffManager.instance.randombuff();
        }

        if (Input.GetKeyDown(KeyCode.X))
        {
            RaycastHit2D hit = Physics2D.Raycast(rigidbody2d.position + Vector2.up * 0.2f, lookDirection, 1.5f, LayerMask.GetMask("NPC"));
            if (hit.collider != null)
            {
                NonPlayerCharacter character = hit.collider.GetComponent<NonPlayerCharacter>();
                if (character != null)
                {
                    character.DisplayDialog();
                }
            }
        }
       

    }

    void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            speed = 8;
        }
        else
        {
            speed = 3;
        }
        Vector2 position = rigidbody2d.position;
        position.x = position.x + speed * horizontal * Time.deltaTime;
        position.y = position.y + speed * vertical * Time.deltaTime;
        UpdateTime();
        rigidbody2d.MovePosition(position);
    }

    public void ChangeHealth(int amount)
    {
        if (amount < 0)
        {
            animator.SetTrigger("Hit");
            if (isInvincible)
                return;
            PlaySound(behitClip);
            isInvincible = true;
            invincibleTimer = timeInvincible;
        }

        currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth);
        UIHealthBar.instance.SetValue(currentHealth / (float)maxHealth);
    }
    void Launch()
    {
        if (BulletLevel==1)
        {
            GameObject projectileObject = Instantiate(projectilePrefab, rigidbody2d.position + Vector2.up * 0.5f, Quaternion.identity);
            Projectile projectile = projectileObject.GetComponent<Projectile>();
            projectile.Launch(lookDirection, 300, 1);
            animator.SetTrigger("Launch");
        }
        else if(BulletLevel == 2)
        {
            GameObject projectileObject = Instantiate(projectilePrefab2, rigidbody2d.position + Vector2.up * 0.5f, Quaternion.identity);
            Projectile projectile = projectileObject.GetComponent<Projectile>();
            projectile.Launch(lookDirection, 300,2);
            animator.SetTrigger("Launch");
        }
        else if (BulletLevel == 3)
        {
            GameObject projectileObject = Instantiate(projectilePrefab3, rigidbody2d.position + Vector2.up * 0.5f, Quaternion.identity);
            Projectile projectile = projectileObject.GetComponent<Projectile>();
            projectile.Launch(lookDirection, 300,3);
            animator.SetTrigger("Launch");
        }
        else if (BulletLevel == 4)
        {
            GameObject projectileObject = Instantiate(projectilePrefab, rigidbody2d.position + Vector2.up * 0.5f, Quaternion.identity);
            Projectile projectile = projectileObject.GetComponent<Projectile>();
            projectile.Launch(lookDirection, 300, 4);
            animator.SetTrigger("Launch");
        }
    }
    public void PlaySound(AudioClip clip)
    {
        audioSource.PlayOneShot(clip);
    }
    public void ChangeBulletCount(int amount)
    {
        curBulletCount = Mathf.Clamp(curBulletCount + amount, 0, maxBulletCount);
        UIHealthBar.instance.UpdateBulletCount(curBulletCount, maxBulletCount);
    }

    public void UpdateTime()
    {
        lefttime -= Time.deltaTime;
        UIHealthBar.instance.UpdateLeftTime((int)lefttime);
        energyTimer -= Time.deltaTime;
        if(energyTimer<0)
        {
            ChangeEnergy(4);
            energyTimer = 2.0f;
        }
        if(BulletLevel!=1)
        {
            skillleft-=Time.deltaTime;
            if(skillleft<0)
            {
                skillleft = 5.0f;
                BulletLevel=1;
            }
        }
        if(launchTime>0)
        {
            launchTime-=Time.deltaTime;
        }
    }

    public void ChangeEnergy(int amount)
    {
        if (curenergy + amount > 100)
            curenergy = 100;
        else curenergy += amount;
        Ring.instance.UpdateEnergy(curenergy, maxenergy);

    }
    private IEnumerator StopTip(float duration)
    {
        yield return new WaitForSeconds(duration);
        BulletTip.SetActive(false);
    }
}
