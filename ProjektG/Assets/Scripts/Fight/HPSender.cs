using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPSender : MonoBehaviour
{
    public Slider bar;
    int Hp;
    int MaxHp;

    void Update()
    {
        Hp= GameObject.FindGameObjectWithTag("Enemy").GetComponent<EnemyBehavior>().HP;
        MaxHp = GameObject.FindGameObjectWithTag("Enemy").GetComponent<EnemyBehavior>().MaxHP;
        bar.value = (float)Hp/(float)MaxHp;

        if(Hp <=0)
            Destroy(this.gameObject);
    }
}
