using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;
using System.IO;


public class XML : MonoBehaviour {
    private static XML instance;

    public static XML Instance { get { return instance; } }
    public string fileName;
    string filepath;
    public List<PackageItems> equipsLoaded;
	public GameObject[] _effects;
    // Use this for initialization
    private void Awake()
    {
        //filepath = Application.dataPath + @"/" + fileName + ".xml";
		filepath=Application.streamingAssetsPath+"/"+fileName+".xml";
        equipsLoaded = new List<PackageItems>();
        LoadXML();
        instance = this;
    }
    void Start () {
        
//		CreateXML();
    }
	
	// Update is called once per frame
	void Update () {
		
	}
    void CreateXML()
    {
        if (!File.Exists(filepath))
        {
            //创建xml实例
            XmlDocument xmlDoc = new XmlDocument();
            XmlDeclaration xmlDec = xmlDoc.CreateXmlDeclaration("1.0", "UTF-8", null);
            xmlDoc.AppendChild(xmlDec);
            //只有一个root
            XmlElement root = xmlDoc.CreateElement("道具列表");

            XmlElement xmlEquip = xmlDoc.CreateElement("装备");
            XmlElement name = xmlDoc.CreateElement("道具1");
            name.SetAttribute("id", "0");
            XmlElement price = xmlDoc.CreateElement("价格");
            price.InnerText = "100";

            xmlEquip.AppendChild(name);
            name.AppendChild(price);
            root.AppendChild(xmlEquip);


            xmlDoc.AppendChild(root);
            //把xml文件保存至本地
            xmlDoc.Save(filepath);
            Debug.Log("creatXml OK!");
        }
        

#if UNITY_EDITOR
        UnityEditor.AssetDatabase.Refresh();
#endif
    }
    void LoadXML()
    {
        equipsLoaded.Clear();

        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.Load(filepath);

        //        XmlElement root = xmlDoc.DocumentElement;
        //        Debug.Log("Root : " + root.Name);

        // XPath的开头是一个斜线代表这是绝对路径，可以选出所有路径符合这个模式的元素。
        XmlNode node = xmlDoc.SelectSingleNode("/道具列表/装备");
        XmlNodeList pNodeList = node.ChildNodes;
        foreach (var item in pNodeList)
        {
            XmlElement element = item as XmlElement;
            if (element != null)
            {
                PackageItems equip = new PackageItems();
                equip.type = int.Parse(element.GetAttribute("type"));
                equip.name = element.Name;
                //获取道具下的各个属性
                XmlNodeList List = element.ChildNodes;
                foreach (var info in List)
                {
                    XmlElement infoElm = info as XmlElement;
                    if (infoElm.Name=="冷却")
                        equip.coldTime = float.Parse(infoElm.InnerText);
                    else if (infoElm.Name == "买价")
                        equip.buyPrice = float.Parse(infoElm.InnerText);
                    else if (infoElm.Name == "卖价")
                        equip.sellPrice = float.Parse(infoElm.InnerText);
                    else if (infoElm.Name == "攻击距离")
                        equip.range = float.Parse(infoElm.InnerText);
                    else if (infoElm.Name == "伤害")
                        equip.damage = float.Parse(infoElm.InnerText);
                    else if (infoElm.Name == "冲击")
                        equip.hitback = float.Parse(infoElm.InnerText);
					else if (infoElm.Name == "健康值消耗")
                        equip.healthUse = float.Parse(infoElm.InnerText);
                    else if (infoElm.Name == "子弹特效名")
                        equip.bulletEffectName = infoElm.InnerText;
                    else if (infoElm.Name == "魔法特效名")
                        equip.magicEffectName = infoElm.InnerText;
                    else if (infoElm.Name == "击中特效名")
                        equip.hitEffectName = infoElm.InnerText;
                    else if (infoElm.Name == "后坐力")
                        equip.playerHitBack = float.Parse(infoElm.InnerText);
                    else if (infoElm.Name == "发射物飞行速度")
                        equip.bulletSpeed = float.Parse(infoElm.InnerText);
                    else if (infoElm.Name == "爆炸半径")
                        equip.exploadRadius = float.Parse(infoElm.InnerText);
                    else if (infoElm.Name == "爆炸伤害")
                        equip.explodeDamage = float.Parse(infoElm.InnerText);
                    else if (infoElm.Name == "准备时长")
                        equip.prepareTime = float.Parse(infoElm.InnerText);
                    else if (infoElm.Name == "是否可以穿墙")
                        equip.canCrossWall = int.Parse(infoElm.InnerText);

                }
                //equip.buyPrice = int.Parse(element.InnerText);
                equipsLoaded.Add(equip);
            }
        }
    }


    }
[System.Serializable]
public struct PackageItems
{
    public int type;
    public string name;
    public float coldTime;
    public float buyPrice;
    public float sellPrice;
    public float range;
    public float damage;
    public float hitback;
    public float healthUse;
    public string bulletEffectName;
    public string magicEffectName;
    public string hitEffectName;
    public float playerHitBack;
    public float bulletSpeed;
    public float exploadRadius;
    public float explodeDamage;
    public float prepareTime;
    public int canCrossWall;

}
