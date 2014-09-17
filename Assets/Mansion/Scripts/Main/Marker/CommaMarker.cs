using UnityEngine;
using System.Collections;
using System;

public class CommaMarker {
	public static string MarkDecimalCount (decimal keepCount) {
		keepCount = Math.Floor (keepCount);
		return keepCount.ToString ("#,0");
	}

	public static string MarkGenerateSpeed (decimal generateSpeed) {
		// 小数点以下の数値を取得して整数にする
		decimal underCount = generateSpeed % 1;
		int underCountInt = (int)(underCount * 10);
		//小数点以下を切り捨て
		decimal topCount = Math.Floor (generateSpeed);
		string topCountString = topCount.ToString ("#,0");
		return topCountString + "." + underCountInt;
	}

	public static string MarkLongCount(long priceCount){
		return priceCount.ToString ("#,0");
	}

	public static string MarkIntCount(int count){
		return count.ToString ("#,0");
	}
}
