using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerSkillCast : MonoBehaviour
{
    Animator anim;
    PlayerOnClick playerOnClick;
    [Header("Mana Settings")]
    [SerializeField] private float totalMana = 100;
    [SerializeField] private float manaRegenSpeed = 2f;
    [SerializeField] private Image manaBar;

    [Header("Cooldown Icons")]
    [SerializeField] private Image[] coolDownIcon;

    [Header("Out Of Mana Icons")]
    [SerializeField] private Image[] outOfManaIcons;
    [Header("Mana Amounts")]
    [SerializeField] private float skill1ManaAmount = 5f;
    [SerializeField] private float skill2ManaAmount = 10f;
    [SerializeField] private float skill3ManaAmount = 20f;
    [SerializeField] private float skill4ManaAmount = 5f;
    [SerializeField] private float skill5ManaAmount = 20f;
    [SerializeField] private float skill6ManaAmount = 20f;

    int[] fadeImages = new int[] { 0, 0, 0, 0, 0, 0 };
    List<float> manaAmountList = new List<float>();
    bool faded = false;
    [HideInInspector] public List<float> coolDownTimes = new List<float>();
    
    private void Awake()
    {
        playerOnClick = GetComponent<PlayerOnClick>();
        anim = GetComponent<Animator>();
    }

    void Start()
    {
        //coolDown
        coolDownTimes.Add(1f);
        coolDownTimes.Add(0.2f);
        coolDownTimes.Add(0.1f);
        coolDownTimes.Add(0.75f);
        coolDownTimes.Add(0.05f);
        coolDownTimes.Add(0.03f);
        //Mana
        manaAmountList.Add(skill1ManaAmount);
        manaAmountList.Add(skill2ManaAmount);
        manaAmountList.Add(skill3ManaAmount);
        manaAmountList.Add(skill4ManaAmount);
        manaAmountList.Add(skill5ManaAmount);
        manaAmountList.Add(skill6ManaAmount);

    }
    void Update()
    {
        if (totalMana < 100f)
        {
            totalMana += Time.deltaTime * manaRegenSpeed;
            manaBar.fillAmount = totalMana / 100;
        }
        CheckMana();
        CheckToFade();
        
        if (anim.IsInTransition(0) && anim.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
        {
            TrunThePlayer();
        }

        if (anim.GetInteger("Attack") == 0)
        {
            playerOnClick.FinishedMovent = false;
            if (!anim.IsInTransition(0) && anim.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
            {
                playerOnClick.FinishedMovent = true;
            }

        }

        if (!anim.IsInTransition(0) && anim.GetCurrentAnimatorStateInfo(0).IsName("Idle") && playerOnClick.FinishedMovent)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1) && totalMana >= skill1ManaAmount)
            {
                
                playerOnClick.TargetPosion = transform.position;
                if (fadeImages[0] != 1)
                {
                    anim.SetInteger("Attack", 1);
                    totalMana -= skill1ManaAmount;
                    fadeImages[0] = 1;
                }

            }
            else if (Input.GetKeyDown(KeyCode.Alpha2) && totalMana >= skill2ManaAmount)
            {
                playerOnClick.TargetPosion = transform.position;
                if (fadeImages[1] != 1)
                {
                    anim.SetInteger("Attack", 2);
                    totalMana -= skill2ManaAmount;
                    fadeImages[1] = 1;
                }
                    
            }
            else if (Input.GetKeyDown(KeyCode.Alpha3) && totalMana >= skill3ManaAmount)
            {
                playerOnClick.TargetPosion = transform.position;
                if (fadeImages[2] != 1)
                {
                    anim.SetInteger("Attack", 3);
                    totalMana -= skill3ManaAmount;
                    fadeImages[2] = 1;
                }
                    
            }
            else if (Input.GetKeyDown(KeyCode.Alpha4) && totalMana >= skill4ManaAmount)
            {
                playerOnClick.TargetPosion = transform.position;
                if (fadeImages[3] != 1)
                {
                    anim.SetInteger("Attack", 4);
                    totalMana -= skill4ManaAmount;
                    fadeImages[3] = 1;
                }
            }
            else if (Input.GetKeyDown(KeyCode.Alpha5) && totalMana >= skill5ManaAmount)
            {
                playerOnClick.TargetPosion = transform.position;
                if (fadeImages[4] != 1)
                {
                    anim.SetInteger("Attack", 5);
                    totalMana -= skill5ManaAmount;
                    fadeImages[4] = 1;
                }
            }
            else if (Input.GetKeyDown(KeyCode.Alpha6) && totalMana >= skill6ManaAmount)
            {
                playerOnClick.TargetPosion = transform.position;
                if (fadeImages[5] != 1)
                {
                    anim.SetInteger("Attack", 6);
                    totalMana -= skill6ManaAmount;
                    fadeImages[5] = 1;
                }
            }
            else
            {
                anim.SetInteger("Attack", 0);
            }
        }//anim
    }



    void TrunThePlayer()
    {
        Vector3 targetPos = Vector2.zero;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            targetPos = new Vector3(hit.point.x, transform.position.y, hit.point.z);
        }
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(targetPos - transform.position),
            playerOnClick.trunSpeed * Time.deltaTime);
    }

    void CheckMana()
    {
        for (int i = 0; i < outOfManaIcons.Length; i++)
        {
            if (totalMana < manaAmountList[i])
            {
                outOfManaIcons[i].gameObject.SetActive(true);
            }
            else
            {
                outOfManaIcons[i].gameObject.SetActive(false);
            }
        }
    }
    void CheckToFade()
    {
        for (int i = 0; i < fadeImages.Length; i++)
        {
            if (fadeImages[i] == 1)
            {
                if (FadeAndWait(coolDownIcon[i], coolDownTimes[i]))
                {
                    fadeImages[i] = 0;
                }
            }
        }

    }
    bool FadeAndWait(Image fadeImage, float fadeTime)
    {
        faded = false;
        if (fadeImage == null)
        {
            return faded;
        }
        if (!fadeImage.gameObject.activeInHierarchy)
        {
            fadeImage.gameObject.SetActive(true);
            fadeImage.fillAmount = 1f;
        }
        fadeImage.fillAmount -= fadeTime * Time.deltaTime;
        if (fadeImage.fillAmount <= 0)
        {
            fadeImage.gameObject.SetActive(false);
            faded = true;
        }
        return faded;

    }

}
