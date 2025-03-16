using System;

namespace StormDotNet
{
	// Token: 0x02000002 RID: 2
	public sealed class ApplicationDescriptor
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
		// (set) Token: 0x06000002 RID: 2 RVA: 0x00002058 File Offset: 0x00000258
		public string ApplicationId
		{
			get
			{
				return this.applicationId;
			}
			set
			{
				this.applicationId = value;
			}
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000003 RID: 3 RVA: 0x00002061 File Offset: 0x00000261
		// (set) Token: 0x06000004 RID: 4 RVA: 0x00002069 File Offset: 0x00000269
		public string CompatibilityId
		{
			get
			{
				return this.compatibilityId;
			}
			set
			{
				this.compatibilityId = value;
			}
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000005 RID: 5 RVA: 0x00002072 File Offset: 0x00000272
		public string PersistentDataPath
		{
			get
			{
				return this.persistentDataPath;
			}
		}

		// Token: 0x06000006 RID: 6 RVA: 0x0000207A File Offset: 0x0000027A
		public ApplicationDescriptor()
		{
			this.applicationId = "ffffffff-ffff-ffff-ffff-ffffffffffff";
			this.compatibilityId = "ffffffff-ffff-ffff-ffff-ffffffffffff";
			this.persistentDataPath = string.Empty;
		}

		// Token: 0x04000001 RID: 1
		private string applicationId;

		// Token: 0x04000002 RID: 2
		private string compatibilityId;

		// Token: 0x04000003 RID: 3
		private string persistentDataPath;
	}
}
