using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class TNT_Controller : MonoBehaviour
{
    
    [SerializeField] private UpgradeManager UpgradeManager;
    [SerializeField] private TextMeshProUGUI _textDealy;
    public Pool _boombEffectPool;
    private float _delay;
    private int Sec = 0;
    [SerializeField] private int minSec = 0;
    [SerializeField] private float _delayTime = 8f;
    private bool isStopGame = false;
    private Coroutine _coroutine;
    private float _floatDelay;
    private void Awake(){
        _delay = _delayTime;
    }
    private void Update(){
        if(UpgradeManager.IsStopGame == true){
            StopCoroutine(_coroutine);
            isStopGame = true;
            return;
        }
        if(isStopGame == true){
            StopCoroutine(_coroutine);
            if (minSec > 0)
            {
                _coroutine = StartCoroutine(DelayBoomb(_delay + ((60/minSec)/100)));
            }
            isStopGame = false;
        }
        if(_floatDelay > 0){
            _floatDelay -= 1f/60f;
        }else if(_floatDelay <= 0)
        {
            _floatDelay = 0;
            GetComponent<CircleCollider2D>().enabled = true;
        }
        minSec++;
        if(minSec == 60)
        {
            Sec++;
            _delay -= 1;
            minSec = 0;
            _textDealy.text = _delay.ToString();
        }
    }
    private void OnEnable(){
        GetComponent<CircleCollider2D>().enabled = false;
        _floatDelay = 0.3f;
        _delay = _delayTime;
        _textDealy.text = _delay.ToString();
        _coroutine = StartCoroutine(DelayBoomb(_delayTime));
    }
    IEnumerator DelayBoomb(float delay)
    {
        yield return new WaitForSeconds(delay);
        _boombEffectPool.GetPool(new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z), new Vector3(0, 0, 0), new Vector3(1,1,1));
        transform.parent.transform.parent.gameObject.SetActive(false);
        Sec = 0;
    }
}
