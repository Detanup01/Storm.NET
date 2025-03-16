using System;

namespace StormDotNet.Echo.Object
{
	// Token: 0x020000EC RID: 236
	public struct NetObjectMigrationMode
	{
		// Token: 0x17000062 RID: 98
		// (get) Token: 0x06000496 RID: 1174 RVA: 0x0000D76F File Offset: 0x0000B96F
		public StringId GiveMasterRoleMode
		{
			get
			{
				return this.giveMasterRoleMode;
			}
		}

		// Token: 0x17000063 RID: 99
		// (get) Token: 0x06000497 RID: 1175 RVA: 0x0000D777 File Offset: 0x0000B977
		public StringId AcquireMasterRoleMode
		{
			get
			{
				return this.acquireMasterRoleMode;
			}
		}

		// Token: 0x17000064 RID: 100
		// (get) Token: 0x06000498 RID: 1176 RVA: 0x0000D77F File Offset: 0x0000B97F
		public StringId ReplaceGoneMasterMode
		{
			get
			{
				return this.replaceGoneMasterMode;
			}
		}

		// Token: 0x17000065 RID: 101
		// (get) Token: 0x06000499 RID: 1177 RVA: 0x0000D787 File Offset: 0x0000B987
		public StringId ResolveMasterConflictMode
		{
			get
			{
				return this.resolveMasterConflictMode;
			}
		}

		// Token: 0x0600049A RID: 1178 RVA: 0x0000D78F File Offset: 0x0000B98F
		public NetObjectMigrationMode SetDefault()
		{
			this.giveMasterRoleMode = NetObjectMigrationMode.Disable;
			this.acquireMasterRoleMode = NetObjectMigrationMode.Disable;
			this.replaceGoneMasterMode = NetObjectMigrationMode.Disable;
			this.resolveMasterConflictMode = NetObjectMigrationMode.Disable;
			return this;
		}

		// Token: 0x040002AC RID: 684
		public static readonly StringId Active = new StringId("ObjectMigrationMode.Active");

		// Token: 0x040002AD RID: 685
		public static readonly StringId Disable = new StringId("ObjectMigrationMode.Disable");

		// Token: 0x040002AE RID: 686
		public static readonly StringId SessionHostOnly = new StringId("ObjectMigrationMode.SessionHostOnly");

		// Token: 0x040002AF RID: 687
		private StringId giveMasterRoleMode;

		// Token: 0x040002B0 RID: 688
		private StringId acquireMasterRoleMode;

		// Token: 0x040002B1 RID: 689
		private StringId replaceGoneMasterMode;

		// Token: 0x040002B2 RID: 690
		private StringId resolveMasterConflictMode;
	}
}
