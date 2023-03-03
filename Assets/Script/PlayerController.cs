using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    Animator anim;
    public UnityEvent onDieCallback = new UnityEvent();
    /// <summary>�����Ă���A�C�e���̃��X�g</summary>
    List<ItemBase> _itemList = new List<ItemBase>();
    public int life = 100;
    public bool invaded;//�ǂ���������̈�̔���
    public Slider hpBar;

    void Start()
    {
        anim = GetComponent<Animator>();

        if (hpBar != null)
        {
            hpBar.value = life;
        }
    }
    private void Update()
    {
        if (_itemList.Count > 0)
        {
            // ���X�g�̐擪�ɂ���A�C�e�����g���āA�j������
            ItemBase item = _itemList[0];
            _itemList.RemoveAt(0);
            item.Activate();
            Destroy(item.gameObject);
        }
    }

    public void GetItem(ItemBase item)
    {
        _itemList.Add(item);
    }

    public void Damage(int damage)
    {
        if (life <= 0) return;

        life -= damage;
        if (hpBar != null)
        {
            hpBar.value = life;
        }
        if (life <= 0)
        {
            OnDie();
        }
    }
    void OnDie()
    {
        anim.SetBool("Die", true);
        onDieCallback.Invoke();
    }
    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.name == "CautionArea")
        {
            invaded = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name == "CautionArea")
        {
            invaded = false;
        }
    }
}