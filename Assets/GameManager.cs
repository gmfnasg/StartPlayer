using UnityEngine;
using System.Collections;
using System.IO;
using UnityEngine.UI;
using System.Collections.Generic;

public class GameManager : MonoBehaviour {
	List<string> conditions = new List<string> ();
	List<List<string>>conditionList = new List<List<string>> ();
	string[] settings = new string[0];
	Text conditionText;
	Text cardNumberText;
	void Start () {
		LoadFile ();
		conditionText = GameObject.Find ("Condition").GetComponent<Text>();
		cardNumberText = GameObject.Find ("CardNumber").GetComponent<Text>();
		Button rndButton = GameObject.Find ("Button").GetComponent<Button> ();
		rndButton.onClick.AddListener (()=>{
			RndCondition();
		});
		RndCondition ();
	}
	
	// Update is called once per frame
	void Update () {
	}
	
	void LoadFile(){
		string path = "jar:file://" + Application.dataPath + "!/assets" + "/SettingFile.txt";//Application.dataPath + "/SettingFile.txt";
		//WWW wWA=new WWW(path);  

		FileStream file = new FileStream (path, FileMode.Open);
		if (file == null)
			return;
		StreamReader sw = new StreamReader(file);
		if (sw == null)
			return;

		string [] elementStrs = sw.ReadToEnd().Split('\n');
		int k = 0;
		string elementStr = elementStrs [k];
		while (!string.IsNullOrEmpty(elementStr)) {
			settings = elementStr.Split('\t');
			string log = "";
			List<string> elements = new List<string>();
			for(int i=0;i<settings.Length;i++)
			{
				log += " ("+settings[i]+") ";
				elements.Add(settings[i]);
			}
			elementStr = elementStrs [++k];
			conditionList.Add(elements);
			Debug.Log (log);
		}

	}

	void RndCondition(){
		int rndid = Random.Range (0, conditionList.Count);
		if(cardNumberText!=null)
			cardNumberText.text = "編號:" + conditionList[rndid][0];
		if(conditionText!=null)
			conditionText.text = "條件:\r\n<color=yellow>" + conditionList[rndid][3]+"</color>";
	}
}
