using System;
using System.Collections;
using System.Collections.Generic;

public class Character : Health
{
    private int _characterLevel = 1;

    private PlayerComponents _components;

    public event Action CharacterInitialized;

    protected override void Start()
    {
        _components = GetComponent<PlayerComponents>();

        base.Start();

        CharacterInitialized?.Invoke();
    }

    public PlayerSaveData PlayerStruct => 
        new PlayerSaveData(SetItemData(_components.Inventory.Weapons), _characterLevel, _components.Inventory.Money);

    public void Initialize(int level)
    {
        _characterLevel = level;
    }

    protected override IEnumerator Dead()
    {
        yield return null;
    }

    private List<WeaponData> SetItemData(List<Weapon> weapons)
    {
        List<WeaponData> result = new ();

        for (int i = 0; i < weapons.Count; i++)
        {
            result.Add(weapons[i].WeaponData);
        }

        return result;
    }
}