using UnityEngine;
using System.Collections;
using System.Text;

public class DamageConverter {
	public static string Convert (string damage) {
		int length = damage.Length;
		char[] damageArray = damage.ToCharArray ();
		string unit = "";
		string convertedDamage = "";
		if (length >= 13) {
			unit = "兆円";
			convertedDamage = CreateConvertedDamage (damageArray, length - 12);
			return convertedDamage + unit;
		}
		if (length >= 9) {
			unit = "億円";
			convertedDamage = CreateConvertedDamage (damageArray, length - 8);
			return convertedDamage + unit;
		}
		if (length >= 5) {
			unit = "万円";
			convertedDamage = CreateConvertedDamage (damageArray, length - 4);
			return convertedDamage + unit;
		}
		unit = "円";
		return damage + unit;
	}

	private static string CreateConvertedDamage (char[] damageArray, int count) {
		StringBuilder sb = new StringBuilder ();
		for (int i = 0; i < count; i++) {
			sb.Append (damageArray [i]);
		}
		return sb.ToString ();
	}
}
