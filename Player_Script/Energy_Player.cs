using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Energy_Player : MonoBehaviour
{
    [SerializeField, Header("Energyアイコン")]
    private GameObject energyIconPrefab; // アイコンのプレハブ
    [SerializeField, Header("Emptyアイコン")]
    private GameObject emptyIconPrefab; // アイコンのプレハブ
    private List<GameObject> energyIcons = new List<GameObject>(); // HPアイコンのリスト\
    private int _startEnergy;
    private int _maxEnergy;
    private PlayerBase _playerMovement;
    // Start is called before the first frame update
    void Start()
    {
        _playerMovement=FindObjectOfType<PlayerBase>();
        _startEnergy=_playerMovement._energy;
        _maxEnergy=_playerMovement._maxEnergy;
        CreateEnergyIcons(_startEnergy);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ConsumeEnergy(int amount){
        _playerMovement._energy-=amount;
        UpdateHPIcons(_playerMovement._energy);
    }
    public void HealEnergy(int amount)
    {
        _playerMovement._energy += amount;
        UpdateHPIcons(_playerMovement._energy);
    }
    // HPアイコンを更新するメソッド
    private void UpdateHPIcons(int currentEnergy)
    {
        for (int i = 0; i < _maxEnergy; i++)
        {
            energyIcons[i].SetActive(i < currentEnergy);
        }
    }
    void CreateEnergyIcons(int energy)
    {
        for (int i = 0; i < _maxEnergy; i++)
        {
            
            GameObject energyObj = Instantiate(energyIconPrefab, transform);
            energyIcons.Add(energyObj);
            if(i>=_startEnergy){
                energyIcons[i].SetActive(false);
            }
        }
    }
}
