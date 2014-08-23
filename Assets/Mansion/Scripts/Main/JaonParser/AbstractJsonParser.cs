using UnityEngine;
using System.Collections;

public abstract class AbstractJsonParser {

	protected class StatusDataKies{
		public const string TOTAL_GENERATE_COUNT = "totalGenerateCount";
		public const string MAX_KEEP_COUNT = "maxKeepCount";
		public const string TOTAL_PIT_GENERATE_COUNT = "totalPitGenerateCount";
		public const string TOTAL_TAP_PIT_COUNT = "totalTapPitCount";
		public const string TOTAL_CAME_ENEMY_COUNT = "totalCameEnemyCount";
		public const string TOTAL_ATACK_ENEMY_COUNT = "totalAtackEnemyCount";
		public const string TOTAL_USED_SECOM_COUNT = "totalUsedSecomCount";
		public const string TOTAL_DAMEGED_COUNT = "totalDamegedCount";
		public const string FIRST_GENERATE_DATE = "firstGenerateDate";
	}

	protected class SecomDataKies{
		public const string SECOM_COUNT = "secomCount";
		public const string SECOM_MAX_COUNT = "secomMaxCount";
	}
}
