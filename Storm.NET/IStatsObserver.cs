using System;
using StormDotNet.Echo;
using StormDotNet.Stats;

namespace StormDotNet
{
	// Token: 0x02000068 RID: 104
	public interface IStatsObserver
	{
		// Token: 0x060001E2 RID: 482
		void OnDetailedStatistic(ref FrameStat frameStat);

		// Token: 0x060001E3 RID: 483
		void OnIntervalStatistic(ref FrameStat frameStat);

		// Token: 0x060001E4 RID: 484
		void OnGlobalQOSStatistic(ref GlobalQOSStat bandwidthRegulatorStat);

		// Token: 0x060001E5 RID: 485
		void OnQOSStatistic(PeerDescriptor peerDescriptor, ref QOSStat bandwidthRegulatorStat);
	}
}
