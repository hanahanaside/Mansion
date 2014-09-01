using UnityEngine;
using System.Collections;

public class SecomData {

	public long Count{ get; set; }

	public long MacxCount{ get; set; }

	public string Description{
		get{
			return "待機中に泥棒を自動的に撃退";
		}
	}

	public long Price{
		get{
			long price = 100L;
			for(int i = 0;i < Count;i++){
				price *= 10;
			}
			return price;
		}
	}
}
