using System;
using System.Runtime.InteropServices;

namespace StormDotNet
{
	// Token: 0x0200000B RID: 11
	public abstract class BaseData
	{
		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000030 RID: 48
		public abstract DataType DataType { get; }

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000031 RID: 49 RVA: 0x0000251A File Offset: 0x0000071A
		protected DataContainer DataContainer
		{
			get
			{
				return this.dataContainer;
			}
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000032 RID: 50 RVA: 0x00002522 File Offset: 0x00000722
		protected byte Index
		{
			get
			{
				return this.index;
			}
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000033 RID: 51
		protected internal abstract string[] ExtraParams { get; }

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000034 RID: 52
		protected internal abstract string DefaultValue { get; }

		// Token: 0x06000035 RID: 53 RVA: 0x0000252A File Offset: 0x0000072A
		protected internal BaseData(DataContainer dataContainer)
		{
			if (dataContainer == null)
			{
				throw new ArgumentNullException("dataContainer");
			}
			this.dataContainer = dataContainer;
			this.index = 0;
		}

		// Token: 0x06000036 RID: 54 RVA: 0x0000254E File Offset: 0x0000074E
		protected void Register()
		{
			this.dataContainer.RegisterAttribute(this);
		}

		// Token: 0x04000006 RID: 6
		private DataContainer dataContainer;

		// Token: 0x04000007 RID: 7
		internal byte index;

		// Token: 0x0200000C RID: 12
		internal static class Native
		{
			// Token: 0x06000037 RID: 55
			[DllImport("Storm", CharSet = CharSet.Ansi, EntryPoint = "DataContainer_SetBuffer")]
			public static extern void SetBuffer(IntPtr dataContainer, byte index, IntPtr buffer, int length);
		}
	}
}
