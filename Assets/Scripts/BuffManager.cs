using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class BuffManager : MonoBehaviour
{
    //Player相关Manager类的都做成单例模式
    public static BuffManager instance { get; private set; }
    public IBuff[] buffs;
    public int buffQuantity;

    //buff执行效果的委托类
    public delegate void BuffCall();
    public BuffCall buffcall;

    // 记录协程的引用
    private Dictionary<int, Coroutine> coroutineRefs = new Dictionary<int, Coroutine>();

    private BuffIconManager iconManager;

    void Awake()
    {
        instance = this;
    }
    private void Update()
    {
        /*if (buffcall != null)
        {
            buffcall();
        }*/
    }

    // Start is called before the first frame update
    void Start()
    {
        buffs = new IBuff[3];
        //buffcall = delegate () { };
        buffQuantity = 0;
        iconManager = GameObject.FindObjectOfType<BuffIconManager>();
    }


    //添加Buff
    public bool AddBuff(IBuff buff)
    {
        //寻找数组中的空位，添加buff
        for (int i = 0; i < buffs.Length; i++)
        {
            if (buffs[i] == null)
            {
                buffs[i] = buff;
                buffs[i].BuffStart();
                //buffcall += buffs[i].BuffEffect;    //添加响应函数
                Coroutine coroutineRef = StartCoroutine(StopBuff(i, 15));
                coroutineRefs[i] = coroutineRef;  // 存储协程引用
                buffQuantity++;

                buffs[i].icon = iconManager.Icon[buffs[i].id];
                iconManager.ShowBuffIcon(buffs[i].icon, 15, i, buffs[i].id);
                return true;
            }
        }
        return false;
    }
    private IEnumerator StopBuff(int index, float duration)
    {
        yield return new WaitForSeconds(duration);
        coroutineRefs.Remove(index);  // 协程结束后从字典中移除引用
        RemoveBuff(index);
    }


    //删除指定位置的Buff
    public bool RemoveBuff(int index)
    {
        //如果已经为空则不用进行
        if (buffs[index] == null)
        {
            return false;
        }
        buffs[index].BuffEnd();
        //buffcall -= buffs[index].BuffEffect;	
        buffQuantity--;
        buffs[index] = null;
        return true;
    }
    public void randombuff()
    {
      
        int idx = Random.Range(0, 5);
        bool flag = true;
        while (flag)
        {
            flag = false;
            for (int i = 0; i < buffs.Length; i++)
            {
                if (buffs[i] != null && buffs[i].id == idx)
                {
                    idx = (idx + 1) % 5;
                    flag = true;
                    break;
                }
            }
        }

        if (idx == 0)
        {
            this.AddBuff(new AttackBuff());
        }
        else if (idx == 1)
        {
            this.AddBuff(new InvincibleBuff());
        }
        else if (idx == 2)
        {
            this.AddBuff(new LifeRecoverBuff());
        }
        else if (idx == 3)
        {
            this.AddBuff(new EnergyRecoverBuff());
        }
        else if (idx == 4)
        {
            this.AddBuff(new BulletRecoverBuff());
        }
        
        
    }
}



public class IBuff
{
    public int buffLevel,id;
    public Image buffIcon;
    public Sprite icon;
    //初始化，获取对应BuffData,输入至IBuff子类
    public IBuff()
    {
        buffLevel = 0;
    }
    //触发类buff单次发生
    public virtual void BuffEffect() { }
    //状态类buff开始
    public virtual void BuffStart() { }
    //状态类buff结束
    public virtual void BuffEnd() { }


}


public class AttackBuff : IBuff
{
    private Coroutine AttackCoroutine;
    private const float RecoverInterval = 15f;
    private const int RecoverAmount = 1;
    private float elapsedRecoverTime = 0f;
    private int recoveredAmount = 0;

    public override void BuffStart()
    {
        this.id = 0;
        AttackCoroutine = BuffManager.instance.StartCoroutine(Attack());
    }

    public override void BuffEnd()
    {
        Debug.Log("BuffEnd:" + this.id.ToString());
        if (AttackCoroutine != null)
        {
            BuffManager.instance.StopCoroutine(AttackCoroutine);
            AttackCoroutine = null;
        }
        elapsedRecoverTime = 0f;
        recoveredAmount = 0;
    }

    public override void BuffEffect()
    {
    }

    private IEnumerator Attack()
    {
        while (recoveredAmount < 1)
        {
            elapsedRecoverTime += Time.deltaTime;
            RubyController player = GameObject.FindGameObjectWithTag("Player").GetComponent<RubyController>();
            player.BulletLevel = 4;
            if (elapsedRecoverTime >= RecoverInterval)
            {
                // 更新计数器
                recoveredAmount++;
                elapsedRecoverTime = 0f;
            }
            yield return null;
        }
    }
}

public class InvincibleBuff : IBuff
{
    private Coroutine InvincibleCoroutine;
    private const float RecoverInterval = 15f;
    private const int RecoverAmount = 1;
    private float elapsedRecoverTime = 0f;
    private int recoveredAmount = 0;

    public override void BuffStart()
    {
        this.id = 1;
        InvincibleCoroutine = BuffManager.instance.StartCoroutine(Invincible());
    }

    public override void BuffEnd()
    {
        Debug.Log("BuffEnd:" + this.id.ToString());
        if (InvincibleCoroutine != null)
        {
            BuffManager.instance.StopCoroutine(InvincibleCoroutine);
            InvincibleCoroutine = null;
        }
        elapsedRecoverTime = 0f;
        recoveredAmount = 0;
        RubyController player = GameObject.FindGameObjectWithTag("Player").GetComponent<RubyController>();
        player.isInvincible = false;
        player.invincibleTimer = 1;
    }

    public override void BuffEffect()
    {
    }

    private IEnumerator Invincible()
    {
        while (recoveredAmount < 1)
        {
            elapsedRecoverTime += Time.deltaTime;
            RubyController player = GameObject.FindGameObjectWithTag("Player").GetComponent<RubyController>();
            player.isInvincible = true;
            player.invincibleTimer = 1000;
            if (elapsedRecoverTime >= RecoverInterval)
            {
                // 更新计数器
                recoveredAmount++;
                elapsedRecoverTime = 0f;
            }
            yield return null;
        }
    }
}

public class LifeRecoverBuff : IBuff
{
    private Coroutine lifeRecoverCoroutine;
    private const float RecoverInterval = 5f;
    private const int RecoverAmount = 1;
    private float elapsedRecoverTime = 0f;
    private int recoveredAmount = 0;

    public override void BuffStart()
    {
        this.id = 2;
        lifeRecoverCoroutine = BuffManager.instance.StartCoroutine(RecoverLife());
    }

    public override void BuffEnd()
    {
        Debug.Log("BuffEnd:" + this.id.ToString());
        if (lifeRecoverCoroutine != null)
        {
            BuffManager.instance.StopCoroutine(lifeRecoverCoroutine);
            lifeRecoverCoroutine = null;
        }
        elapsedRecoverTime = 0f;
        recoveredAmount = 0;
    }

    public override void BuffEffect()
    {
    }

    private IEnumerator RecoverLife()
    {
        while (recoveredAmount < 3)
        {
            elapsedRecoverTime += Time.deltaTime;
            if (elapsedRecoverTime >= RecoverInterval)
            {
                // 恢复生命值
                RubyController player = GameObject.FindGameObjectWithTag("Player").GetComponent<RubyController>();
                player.ChangeHealth(RecoverAmount);

                // 更新计数器
                recoveredAmount++;
                elapsedRecoverTime = 0f;
            }
            yield return null;
        }
    }
}

public class EnergyRecoverBuff : IBuff
{
    private Coroutine EnergyRecoverCoroutine;
    private const float RecoverInterval = 3f;
    private const int RecoverAmount = 5;
    private float elapsedRecoverTime = 0f;
    private int recoveredAmount = 0;

    public override void BuffStart()
    {
        this.id = 3;
        EnergyRecoverCoroutine = BuffManager.instance.StartCoroutine(RecoverEnergy());
    }

    public override void BuffEnd()
    {
        Debug.Log("BuffEnd:" + this.id.ToString());
        if (EnergyRecoverCoroutine != null)
        {
            BuffManager.instance.StopCoroutine(EnergyRecoverCoroutine);
            EnergyRecoverCoroutine = null;
        }
        elapsedRecoverTime = 0f;
        recoveredAmount = 0;
    }

    public override void BuffEffect()
    {
    }

    private IEnumerator RecoverEnergy()
    {
        while (recoveredAmount < 5)
        {
            elapsedRecoverTime += Time.deltaTime;
            if (elapsedRecoverTime >= RecoverInterval)
            {
                RubyController player = GameObject.FindGameObjectWithTag("Player").GetComponent<RubyController>();
                player.ChangeEnergy(RecoverAmount);

                // 更新计数器
                recoveredAmount++;
                elapsedRecoverTime = 0f;
            }
            yield return null;
        }
    }
}

public class BulletRecoverBuff : IBuff
{
    private Coroutine BulletRecoverCoroutine;
    private const float RecoverInterval = 3f;
    private const int RecoverAmount = 6;
    private float elapsedRecoverTime = 0f;
    private int recoveredAmount = 0;

    public override void BuffStart()
    {
        this.id = 4;
        BulletRecoverCoroutine = BuffManager.instance.StartCoroutine(RecoverBullet());
    }

    public override void BuffEnd()
    {
        Debug.Log("BuffEnd:" + this.id.ToString());
        if (BulletRecoverCoroutine != null)
        {
            BuffManager.instance.StopCoroutine(BulletRecoverCoroutine);
            BulletRecoverCoroutine = null;
        }
        elapsedRecoverTime = 0f;
        recoveredAmount = 0;
    }

    public override void BuffEffect()
    {
    }

    private IEnumerator RecoverBullet()
    {
        while (recoveredAmount < 5)
        {
            elapsedRecoverTime += Time.deltaTime;
            if (elapsedRecoverTime >= RecoverInterval)
            {
                RubyController player = GameObject.FindGameObjectWithTag("Player").GetComponent<RubyController>();
                player.ChangeBulletCount(RecoverAmount);

                // 更新计数器
                recoveredAmount++;
                elapsedRecoverTime = 0f;
            }
            yield return null;
        }
    }
}

