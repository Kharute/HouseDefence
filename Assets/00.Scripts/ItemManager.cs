using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PotionEffect { Heal, ATK_Up, DEF_Up, SPD_Up }
public enum EquipType { Weapon, Head, Body, Shoes }

public class ItemManager : MonoBehaviour
{
    public InventoryObject gameInventory;

    protected string itemID;       //������ID
    protected string _name;
    protected int _price;
    public string ItemID { get { return itemID; } set { itemID = ItemID; } }
    public string Name { get { return _name; } set { _name = Name; } }

    public int Price { get { return _price; } set { _price = Price; } }

    class Potion : ItemManager
    {
        //getPotion(ID) �� �Է��ϸ�, �ش� ID�� �ִ� Potion�� Player�� Inventory�� �ְ�) ����� Dic���� ã�Ƽ�
        // itemID�� �������
        public PotionEffect _pEffect;
        private int _value;

        public int Value { get { return _value; } set { _value = Value; } }

        public Potion(string id, string name, int value, PotionEffect pEffect, int price)
        {
            itemID = id;
            _name = name;
            _value = value;
            _pEffect = pEffect;
            _price = price;
        }
    }

    class EquipItem : ItemManager
    {
        private int objectID;     //���� ID
        private EquipType _type;
        public Stat ItemStat { get; set; }

        public int ObjectID { get { return objectID; } set { objectID = ObjectID; } }

        //���⿡ ��� DIC�� ���� �� ���
        /*public int WeaponAtk { get { return weaponAtk; } set { weaponAtk = WeaponAtk; } }
        public int WeaponDef { get { return weaponDef; } set { weaponDef = WeaponDef; } }*/

        //������� �Ź� ������ ������ 1�� �÷���ߵ�.
        public EquipItem(string itemid, string wName, EquipType type, Stat stat, int price)
        {
            itemID = itemid;
            _name = wName;
            _type = type;
            ItemStat = stat;
            _price = price;
        }
    }

    class ItemTable
    {
        public Dictionary<string, Potion> _potions;
        public Dictionary<string, EquipItem> _equips;

        public ItemTable()
        {
            // ��ĳ���� �Ǵϱ� ��ĥ ��.
            _potions = new Dictionary<string, Potion>
            {
                { "P_0001", new Potion("P_0001", "���� ����", 10, PotionEffect.Heal, 10) },
                { "P_0002", new Potion("P_0002", "��� ����", 10, PotionEffect.Heal, 20) },
                { "P_0003", new Potion("P_0003", "���� ���� ����", 50, PotionEffect.Heal, 50) },
                { "P_0004", new Potion("P_0004", "���� ��� ����", 50, PotionEffect.Heal, 100) },
                { "P_0005", new Potion("P_0005", "������ ����", 3, PotionEffect.ATK_Up, 20) },
                { "P_0006", new Potion("P_0006", "����� ����", 3, PotionEffect.DEF_Up, 20)},
                { "P_0007", new Potion("P_0007", "�ӵ��� ����", 3, PotionEffect.SPD_Up, 20)},
            };
            _equips = new Dictionary<string, EquipItem>
            {
                { "W_0001", new EquipItem("W_0001","������", EquipType.Weapon, new Stat(0, 1, 0, 0), 0)}         ,
                { "W_0002", new EquipItem("W_0002","û����", EquipType.Weapon, new Stat(0, 10, 0, 0), 10)}        ,
                { "W_0003", new EquipItem("W_0003","ö��", EquipType.Weapon, new Stat(0, 20, 0, 0), 20)}          ,
                { "W_0004", new EquipItem("W_0004","������", EquipType.Weapon, new Stat(0, 100, 0, 0), 1000)}       ,
                { "H_0001", new EquipItem("H_0001","�׸�", EquipType.Head, new Stat(0, 0, 5, 0), 0) }            ,
                { "H_0002", new EquipItem("H_0002","���", EquipType.Head, new Stat(0, 0, 10, 0), 20)}            ,
                { "H_0003", new EquipItem("H_0003","����", EquipType.Head, new Stat(0, 0, 20, -5), 100)}           ,
                { "B_0001", new EquipItem("B_0001","��������", EquipType.Body, new Stat(0, 0, 5, 0), 0)}         ,
                { "B_0002", new EquipItem("B_0002","ö��簩��", EquipType.Body, new Stat(0, 0, 100, -10), 100)}   ,
                { "S_0001", new EquipItem("S_0001","¤��", EquipType.Shoes, new Stat(0, 0, 1, 5), 0)}            ,
                { "S_0002", new EquipItem("S_0002","�Ƶ�ٽ�", EquipType.Shoes, new Stat(0, 0, 5, 20), 300)}
            };
        }
    }
}
public class Stat
{
    private int max_hp;
    private int atk;
    private int def;
    private int spd;
    public int MAX_HP { get { return max_hp; } set { max_hp = MAX_HP; } }
    public int ATK { get { return atk; } set { atk = ATK; } }
    public int DEF { get { return def; } set { def = DEF; } }
    public int SPD { get { return spd; } set { spd = SPD; } }
    public Stat(int _mHP, int _atk, int _def, int _spd)
    {
        max_hp = _mHP;
        atk = _atk;
        def = _def;
        spd = _spd;
    }
}