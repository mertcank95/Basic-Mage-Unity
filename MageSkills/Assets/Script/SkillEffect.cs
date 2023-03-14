using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillEffect : MonoBehaviour
{
  
    [Header("Skill Effects")]
    public GameObject expolision;
    public GameObject zoneMagic;
    public GameObject earthShatter;
    public GameObject iceFireEffeckt;
    public GameObject electricEffeckt;
    public GameObject fireBuffEffect;
    [Header("Skill Transforms")]
    public Transform electricTransform;
    public Transform earthShatterTransform;
    [Header("Sound")]
    public AudioClip iceSound;
    public AudioClip zoneSound;
    public AudioClip earthSound;
    public AudioClip electricSound;
    public AudioClip fireSound;

    void ElectricSkill()
    {
        Instantiate(electricEffeckt, electricTransform.position, electricTransform.rotation);
        AudioSource.PlayClipAtPoint(electricSound, electricTransform.position);
    }
    void ZoneMagic()
    {
        Instantiate(zoneMagic, transform.position, Quaternion.identity);
        AudioSource.PlayClipAtPoint(zoneSound, transform.position);

    }

    void EarthShatter()
    {
        Instantiate(earthShatter, earthShatterTransform.position, transform.rotation);
        AudioSource.PlayClipAtPoint(earthSound, earthShatterTransform.position);

    }

    void FireAttack()
    {
        CreateSkill(expolision, fireSound);
    }

    void IceFire()
    {
        CreateSkill(iceFireEffeckt,iceSound);
    }

    void FireBuff()
    {
        GameObject buff = Instantiate(fireBuffEffect, earthShatterTransform.position, transform.rotation);
        buff.gameObject.transform.SetParent(transform);
    }
    void CreateSkill(GameObject skillEffect,AudioClip sound)
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider.gameObject.layer == LayerMask.NameToLayer("Ground"))
            {
                Vector3 lastPos = hit.point;
                Instantiate(skillEffect, lastPos, Quaternion.identity);
                AudioSource.PlayClipAtPoint(sound, lastPos);


            }
        }
    }

}
