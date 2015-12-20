using UnityEngine;
using System.Collections;
using System.IO;
using UnityEngine.UI;
using System.Collections.Generic;

public class GameManager : MonoBehaviour {
	List<string> conditions = new List<string> ();
	List<List<string>>conditionList = new List<List<string>> ();
	string[] settings = new string[0];
	Text conditionEnText;
	Text conditionChText;
	Text cardNumberText;
	Text logText;
	void Start () {
		conditionChText = GameObject.Find ("ConditionCh").GetComponent<Text>();
		conditionEnText = GameObject.Find ("ConditionEn").GetComponent<Text>();
		cardNumberText = GameObject.Find ("CardNumber").GetComponent<Text>();
		logText = GameObject.Find ("LogText").GetComponent<Text>();

		LoadFile ();

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
		string fileName = "SettingFile.txt";
		string fileString = "";

		#if UNITY_EDITOR
		string path = Application.dataPath +"/StreamingAssets/"+fileName;
		StreamReader sw = File.OpenText (path);//new StreamReader(file);
		if (sw == null)
			return;
		fileString = sw.ReadToEnd();
		ReadFileString(fileString);
		
		#elif UNITY_ANDROID
		StartCoroutine ("LoadFileForAndroid");
 
		#endif


	}

	void RndCondition(){
		int rndid = Random.Range (1, conditionList.Count);
		if(cardNumberText!=null)
			cardNumberText.text = "編號:" + conditionList[rndid][0];
		if(conditionChText!=null)
			conditionChText.text = "條件:\r\n<color=yellow>" + conditionList[rndid][3]+"</color>";
		if(conditionEnText!=null)
			conditionEnText.text = "原文:\r\n<color=green>" + conditionList[rndid][2]+"</color>";
	}


	IEnumerator LoadFileForAndroid(){
		string fileName = "SettingFile.txt";
		string filePath = "jar:file://" + Application.dataPath + "!/assets/" + fileName;
		var www = new WWW(filePath);
		yield return www;
		if (!string.IsNullOrEmpty(www.error))
		{
			DebugLog ("Can't read");
		}
		ReadFileString (www.text);
		RndCondition ();
	}

	void ReadFileString(string fileString){
		string[] elementStrs = fileString.Split('\n');
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
			conditionList.Add(elements);
			
			if(elementStrs.Length-1>k)
				elementStr = elementStrs [++k];
			else
				elementStr = "";
		}
	}

	void DebugLog(string log){
		if(logText!=null)
			logText.text += "\r\n"+log;
		Debug.Log ("\r\n"+log);
	}

}
