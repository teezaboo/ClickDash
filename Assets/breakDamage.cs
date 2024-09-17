using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class breakDamage : MonoBehaviour
{
    public UpgradeStats UpgradeStats;
    [SerializeField] private ResourceManager _resourceManager;
    [SerializeField] private float knockbackForce = 1;
    [SerializeField] private PlayerController _playerController;
    void OnEnable()
    {
        GetComponent<CapsuleCollider2D>().enabled = true;
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
                monsterScript.BoomDamage(_playerController.GetPlayerAttackDamage() * UpgradeStats.UpgradeDarkSword, true, transform.position, knockbackForce);
            }
        }
    }
    IEnumerator endAttack()
    {
        yield return new WaitForSeconds(0.01f);
        GetComponent<CapsuleCollider2D>().enabled = false;
    }
}