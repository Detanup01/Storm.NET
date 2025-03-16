using System;
using System.IO;
using System.Runtime.Serialization;

namespace StormDotNet.Stats
{
	// Token: 0x020000F9 RID: 249
	public struct QOSStat
	{
		// Token: 0x060004A6 RID: 1190 RVA: 0x0000DDB2 File Offset: 0x0000BFB2
		internal QOSStat(BinaryReader reader)
		{
			if (2377063388U != reader.ReadUInt32())
			{
				throw new SerializationException("Invalid magic number for a QOSStat.");
			}
			this.bestRTTTime = reader.ReadUInt32();
			this.currentRTTTime = reader.ReadUInt32();
			this.packetLossRatio = reader.ReadSingle();
		}

		// Token: 0x04000304 RID: 772
		private uint bestRTTTime;

		// Token: 0x04000305 RID: 773
		private uint currentRTTTime;

		// Token: 0x04000306 RID: 774
		private float packetLossRatio;
	}
}
