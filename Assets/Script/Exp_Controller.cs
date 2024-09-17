using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Exp_Controller : MonoBehaviour
{
    [SerializeField] private RandomPoolAudio _randomPoolAudioEXP;
    [SerializeField] private LevelUpManager _levelUpManagerxp;
    [SerializeField] private int _exp = 0;
    [SerializeField] private Pool _effectDie;
    [SerializeField] private GameObject _player;
    [SerializeField] private float _moveSpeed = 1f;
    private float stockMoveSpeed = 1f;
    [SerializeField] private float _zigzagMagnitude = 0.5f; // Magnitude of the zigzag movement
    private float _zigzagFrequency;  // Frequency of the zigzag movement
    public void Awake(){
        stockMoveSpeed = _moveSpeed;
    }
    private void OnEnable(){
        _moveSpeed = stockMoveSpeed;
        int twoWay = Random.Range(0, 2);
        if(twoWay == 0){
            _zigzagFrequency = Random.Range(4, 6);
        }else if(twoWay == 1){
            _zigzagFrequency = Random.Range(-4, -6);
        }
    }
    void Update (){
        if(_player == null) {
            _effectDie.GetPool(transform.position);
            gameObject.SetActive(false);
            return;
        };
        if(_player.GetComponent<PlayerController>().UpgradeManager.IsStopGame == true) return;
        _moveSpeed += 0.01f;
        Vector3 direction = _player.transform.position - transform.position;
        direction.Normalize();
        // Create a perpendicular vector for the zigzag motion
        Vector3 perpendicular = Vector3.Cross(direction, Vector3.forward).normalized;
        // Calculate the zigzag offset
        float zigzagOffset = Mathf.Sin(Time.time * _zigzagFrequency) * _zigzagMagnitude;
        // Add the zigzag offset to the direction
        Vector3 zigzagDirection = direction + perpendicular * zigzagOffset;
        zigzagDirection.Normalize();
        transform.position += zigzagDirection * _moveSpeed * Time.deltaTime;
        if(transform.position.x < _player.transform.position.x){
            transform.rotation = Quaternion.Euler(0, 180, 0);
        } else if(transform.position.x > _player.transform.position.x){
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(_player == null) return;
        if (other.gameObject.CompareTag("Player"))
        {
            StartCoroutine(WaitToDie());
        }else if (other.gameObject.CompareTag("slash"))
        {
            StartCoroutine(WaitToDie());
        }
    }   
    IEnumerator WaitToDie()
    {
        yield return new WaitForSeconds(0.05f);
        _randomPoolAudioEXP.RandomPool(transform.position);
        _levelUpManagerxp.AddExp(_exp);
        _effectDie.GetPool(transform.position);
        gameObject.SetActive(false);
    }
}
