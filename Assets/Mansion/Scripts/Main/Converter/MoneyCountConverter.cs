using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class MoneyCountConverter : MonoBehaviour {
	public static string Convert (decimal moneyCount) {
		//整数にする
		moneyCount = Math.Floor (moneyCount);

		string moneyCountString = System.Convert.ToString (moneyCount);

		//まず、リストにコピーする
		List<char> list = new List<char> ();
		for (int i = 0; i < moneyCountString.Length; i++) {
			char c = moneyCountString [i];
			list.Add (c);
		}


		int count = 1;
		//４けたでカンマ区切りにする
		for (int i = list.Count - 1; i >= 0; i--) {
			if (count % 4 == 0) {
				list.Insert (i, ',');
				Debug.Log ("insert " + i);
				Debug.Log ("count = " + count);
			}
			count++;
		}

		//先頭がカンマになっている場合は削除
		if(list[0] == ','){
			list.RemoveAt (0);
		}

		char[] unitArray = { '万', '億', '兆', '京' };
		char[] moneyCharArray = new char[list.Count];
		int unitIndex = 0;
		for (int i = list.Count - 1; i >= 0; i--) {
			char c = list [i];
			if(c == ','){
				c = unitArray [unitIndex];
				unitIndex++;
			}
			moneyCharArray [i] = c;
		}

		moneyCountString = new string (moneyCharArray);
		moneyCountString = moneyCountString + "円";

		return moneyCountString;
	}
}
