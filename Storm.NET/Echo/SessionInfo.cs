using System;
using System.Runtime.InteropServices;

namespace StormDotNet.Echo
{
	// Token: 0x020000BA RID: 186
	internal sealed class SessionInfo
	{
		// Token: 0x17000055 RID: 85
		// (get) Token: 0x060003B4 RID: 948 RVA: 0x0000B9BF File Offset: 0x00009BBF
		public unsafe uint SessionRefId
		{
			get
			{
				return *this.sessionRefId;
			}
		}

		// Token: 0x17000056 RID: 86
		// (get) Token: 0x060003B5 RID: 949 RVA: 0x0000B9C8 File Offset: 0x00009BC8
		public unsafe uint HostPeerId
		{
			get
			{
				return *this.hostPeerId;
			}
		}

		// Token: 0x17000057 RID: 87
		// (get) Token: 0x060003B6 RID: 950 RVA: 0x0000B9D1 File Offset: 0x00009BD1
		public unsafe uint LocalPeerId
		{
			get
			{
				return *this.localPeerId;
			}
		}

		// Token: 0x17000058 RID: 88
		// (get) Token: 0x060003B7 RID: 951 RVA: 0x0000B9DA File Offset: 0x00009BDA
		public GUID SessionGUID
		{
			get
			{
				return this.sessionGUID;
			}
		}

		// Token: 0x060003B8 RID: 952 RVA: 0x0000B9E4 File Offset: 0x00009BE4
		internal unsafe SessionInfo(IntPtr sessionInfo)
		{
			using (StormEngine.SetProfilePoint(null))
			{
				if (sessionInfo == IntPtr.Zero)
				{
					throw new ArgumentNullException("sessionInfo");
				}
				SessionInfo.Native* ptr = (SessionInfo.Native*)sessionInfo.ToPointer();
				this.sizeOfBool = ptr->sizeOfBool;
				this.sessionRefId = ptr->sessionRefId;
				this.localPeerId = ptr->localPeerId;
				this.hostPeerId = ptr->hostPeerId;
				this.isLeaving = ptr->isLeaving;
				this.isHostMigrationRunning = ptr->isHostMigrationRunning;
				this.sessionGUID = ((ptr->sessionGUIDLength > 0) ? new GUID(ptr->sessionGUID, ptr->sessionGUIDLength) : GUID.INVALID_GUID);
			}
		}

		// Token: 0x04000277 RID: 631
		private int sizeOfBool;

		// Token: 0x04000278 RID: 632
		private unsafe uint* sessionRefId;

		// Token: 0x04000279 RID: 633
		private unsafe uint* localPeerId;

		// Token: 0x0400027A RID: 634
		private unsafe uint* hostPeerId;

		// Token: 0x0400027B RID: 635
		private IntPtr isLeaving;

		// Token: 0x0400027C RID: 636
		private IntPtr isHostMigrationRunning;

		// Token: 0x0400027D RID: 637
		private GUID sessionGUID;

		// Token: 0x020000BB RID: 187
		[StructLayout(0, Pack = 1)]
		private struct Native
		{
			// Token: 0x0400027E RID: 638
			public int sizeOfBool;

			// Token: 0x0400027F RID: 639
			public unsafe uint* sessionRefId;

			// Token: 0x04000280 RID: 640
			public unsafe uint* localPeerId;

			// Token: 0x04000281 RID: 641
			public unsafe uint* hostPeerId;

			// Token: 0x04000282 RID: 642
			public IntPtr isLeaving;

			// Token: 0x04000283 RID: 643
			public IntPtr isHostMigrationRunning;

			// Token: 0x04000284 RID: 644
			public IntPtr sessionGUID;

			// Token: 0x04000285 RID: 645
			public byte sessionGUIDLength;
		}
	}
}
