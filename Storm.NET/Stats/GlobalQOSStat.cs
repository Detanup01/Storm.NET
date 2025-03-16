using System;
using System.IO;
using System.Runtime.Serialization;

namespace StormDotNet.Stats
{
	// Token: 0x020000FA RID: 250
	public struct GlobalQOSStat
	{
		// Token: 0x060004A7 RID: 1191 RVA: 0x0000DDF0 File Offset: 0x0000BFF0
		internal GlobalQOSStat(BinaryReader reader)
		{
			if (2494012508U != reader.ReadUInt32())
			{
				throw new SerializationException("Invalid magic number for a GlobalQOSStat.");
			}
			this.minRTT = reader.ReadUInt32();
			this.currentMinRTT = reader.ReadUInt32();
			this.minRTTDelay = reader.ReadUInt32();
			this.currentRecvBufferSize = reader.ReadUInt32();
			this.maxRecvBufferSize = reader.ReadUInt32();
		}

		// Token: 0x04000307 RID: 775
		private uint minRTT;

		// Token: 0x04000308 RID: 776
		private uint currentMinRTT;

		// Token: 0x04000309 RID: 777
		private uint minRTTDelay;

		// Token: 0x0400030A RID: 778
		private uint currentRecvBufferSize;

		// Token: 0x0400030B RID: 779
		private uint maxRecvBufferSize;
	}
}
