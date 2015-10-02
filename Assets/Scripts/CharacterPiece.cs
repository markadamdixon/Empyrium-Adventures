﻿using UnityEngine;
using System.Collections;


//unit is responsible for graphical representation of the character and animations on unit prefab
public class CharacterPiece : MonoBehaviour
{
    public bool isAttackStart = false;
    public bool isAttackFinish = false;
    Vector3 fixedPosition = new Vector3(0f, 0f, 0f);
    public int init = 0;

    //Animator[] ani;
    float timer = 0;


    public GameObject damageNumber;

    void Start()
    {
        //InvokeRepeating("ShowDamage", 2f, 2f);
    }

    // Update is called once per frame
    void Update()
    {

        if(isAttackStart)
        {
            if(!isAttackFinish)
            {
                //ani[0].SetTrigger("Hit");
                isAttackFinish = true;
            }
            //			if (isEnemy)obj.transform.position += new Vector3(0.04f,0f,0f);
            //				else obj.transform.position += new Vector3(-0.04f,0f,0f);
            //		    if ((obj.transform.position - fixedPosition).magnitude >0.66f)
            //			{
            //				isAttackStart = false;
            //				isAttackFinish = true;
            //			}

        }
        if(isAttackFinish)
        {
            timer += Time.deltaTime;
            if(timer > 1f)
            {
                isAttackFinish = false;
                isAttackStart = false;
                timer = 0f;
            }
            //			if (isEnemy) obj.transform.position += new Vector3(-0.04f,0f,0f);
            //				else obj.transform.position += new Vector3(0.04f,0f,0f);
            //			if ((obj.transform.position - fixedPosition).magnitude <= 0.03f)
            //			{
            //				obj.transform.position = fixedPosition;
            //				isAttackFinish = false;
            //			}
        }
    }

    public void ShowDamage()
    {

        int _damage = RollDamage();
        fixedPosition = gameObject.GetComponent<Transform>().transform.position;


        int[] digit = new int[3];
        bool isHeal = false;

        if(_damage < 0)
        {
            isHeal = true;
            _damage *= -1;
        }

        for(int i = 0; i < 3; i++)
        {
            digit[i] = _damage % 10;
            _damage = (int)_damage / 10;
        }//end loop

        GameObject damageNumberClone;
        DamageNumber dn;
        if(digit[2] != 0)
        {
            damageNumberClone = (GameObject)Instantiate(damageNumber, Vector3.one, Quaternion.identity);
            dn = (DamageNumber)damageNumberClone.GetComponent<DamageNumber>();
            dn.Create(digit[0], fixedPosition + new Vector3(0.6f, 0f, 0f), isHeal, 0.4f);
            damageNumberClone = (GameObject)Instantiate(damageNumber, Vector3.one, Quaternion.identity);
            dn = (DamageNumber)damageNumberClone.GetComponent<DamageNumber>();
            dn.Create(digit[1], fixedPosition, isHeal, 0.2f);
            damageNumberClone = (GameObject)Instantiate(damageNumber, Vector3.one, Quaternion.identity);
            dn = (DamageNumber)damageNumberClone.GetComponent<DamageNumber>();
            dn.Create(digit[2], fixedPosition + new Vector3(-0.6f, 0f, 0f), isHeal, 0f);
        }
        if(digit[2] == 0 && digit[1] != 0)
        {
            damageNumberClone = (GameObject)Instantiate(damageNumber, Vector3.one, Quaternion.identity);
            dn = (DamageNumber)damageNumberClone.GetComponent<DamageNumber>();
            dn.Create(digit[0], fixedPosition + new Vector3(0.3f, 0f, 0f), isHeal, 0.2f);
            damageNumberClone = (GameObject)Instantiate(damageNumber, Vector3.one, Quaternion.identity);
            dn = (DamageNumber)damageNumberClone.GetComponent<DamageNumber>();
            dn.Create(digit[1], fixedPosition + new Vector3(-0.3f, 0f, 0f), isHeal, 0f);
        }
        if(digit[2] == 0 && digit[1] == 0)
        {
            damageNumberClone = (GameObject)Instantiate(damageNumber, Vector3.one, Quaternion.identity);
            dn = (DamageNumber)damageNumberClone.GetComponent<DamageNumber>();
            dn.Create(digit[0], fixedPosition, isHeal, 0f);
        }
    }

    public int RollDamage()
    {

        return (int)Random.Range(-100, 999);
    }


}