using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RateDropItem : MonoBehaviour
{
    [SerializeField] private RandomPoolAudio _randomPoolAudio;
    [SerializeField] private ResourceManager _resourceManager;
    [SerializeField] private UpgradeStats _upgradeStats;
    [SerializeField] private float _rateBomb = 2f;
    [SerializeField] private float _rateSW = 5f;
    [SerializeField] private float _rateFood = 2f;
    [SerializeField] private float _rateGold = 10f;
    [SerializeField] private Pool _pool_Gold;
    [SerializeField] private Pool _pool_BIGGold;
    [SerializeField] private Pool _pool_BIGBIGGold;
    [SerializeField] private Pool _pool_TNT;
    [SerializeField] private Pool _pool_SW;
    [SerializeField] private Pool _pool_Food;
    public float GetRateBomb()
    {
        return (_rateBomb + _upgradeStats.UpgradeDropRateTNT);
    }
    public float GetRateSW()
    {
        return (_rateSW + _upgradeStats.UpgradeDropRateDash);
    }
    public float GetRateFood()
    {
        return (_rateFood + _upgradeStats.UpgradeDropRateFood);
    }
    public float GetRateGold()
    {
        return (_rateGold + _upgradeStats.UpgradeDropRateGold);
    }
    public void spawnItem(Vector3 position)
    {
        _resourceManager.AddDieCount();
        if(Random.Range(0.00f,100.00f) < GetRateBomb()){
            _pool_TNT.GetPool(position, new Vector3(0,0,0), new Vector3(1,1,1));
        }
         if(Random.Range(0.00f,100.00f) <= GetRateSW()){
            _pool_SW.GetPool(position, new Vector3(0,0,0), new Vector3(1,1,1));
        }
         if(Random.Range(0.00f,100.00f) < GetRateGold()){
            _pool_Gold.GetPool(position, new Vector3(0,0,0), new Vector3(1,1,1));
            _resourceManager.AddGold(20);
            _randomPoolAudio.RandomPool(position);
        }
         if(Random.Range(0.00f,100.00f) < GetRateFood()){
            _pool_Food.GetPool(position, new Vector3(0,0,0), new Vector3(1,1,1));
        }
    }
    public void spawnBigGold(Vector3 position)
    {
        _pool_BIGGold.GetPool(position, new Vector3(0,0,0), new Vector3(1,1,1));
        _resourceManager.AddGold(100);
        _randomPoolAudio.RandomPool(position);
    }
    public void spawnBigBigGold(Vector3 position)
    {
        _pool_BIGBIGGold.GetPool(position, new Vector3(0,0,0), new Vector3(1,1,1));
        _resourceManager.AddGold(300);
        _randomPoolAudio.RandomPool(position);
    }
    public void GetGoldPlayer(Vector3 position, float goldCount)
    {
        _pool_Gold.GetPool(position, new Vector3(0,0,0), new Vector3(1,1,1));
        _resourceManager.AddGold(goldCount);
    }
}
