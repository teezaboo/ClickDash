using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bomb_Controller : MonoBehaviour
{
    public bool IsBoombBody = false;
    [SerializeField] private ResourceManager _resourceManager;
    [SerializeField] private float knockbackForce = 1;
    [SerializeField] private PlayerController _playerController;
    void OnEnable()
    {
        GetComponent<CircleCollider2D>().enabled = true;
        StartCoroutine(endAttack());
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("monster"))
        {
            _resourceManager.AddScore(10);
            monsterController monsterScript = other.gameObject.GetComponent<monsterController>();
            if (monsterScript != null)
            {
                if(IsBoombBody == true){
                    monsterScript.BoomDamage(_playerController.GetPlayerAttackDamage() * _playerController.UpgradeStats.UpgradeExplodingMonstersDeath, true, transform.position, knockbackForce);
                }else{
                    monsterScript.BoomDamage(_playerController.GetPlayerAttackDamage() * 4, true, transform.position, knockbackForce);
                }
            }
        }
    }
    IEnumerator endAttack()
    {
        yield return new WaitForSeconds(0.05f);
        GetComponent<CircleCollider2D>().enabled = false;
    }
}
