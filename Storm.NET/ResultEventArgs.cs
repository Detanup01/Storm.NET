using System;

namespace StormDotNet
{
	// Token: 0x02000024 RID: 36
	public class ResultEventArgs : EventArgs
	{
		// Token: 0x17000010 RID: 16
		// (get) Token: 0x0600009B RID: 155 RVA: 0x00002F41 File Offset: 0x00001141
		// (set) Token: 0x0600009C RID: 156 RVA: 0x00002F49 File Offset: 0x00001149
		public EResult Result { get; private set; }

		// Token: 0x0600009D RID: 157 RVA: 0x00002F52 File Offset: 0x00001152
		public ResultEventArgs(EResult result)
		{
			this.Result = result;
		}
	}
}
