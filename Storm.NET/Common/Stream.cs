using System;

namespace StormDotNet.Common
{
	// Token: 0x020000FB RID: 251
	public class Stream : IDisposable
	{
		// Token: 0x060004A8 RID: 1192 RVA: 0x0000DE51 File Offset: 0x0000C051
		internal Stream(IntPtr handle)
		{
			this.handle = handle;
			this.disposed = false;
		}

		// Token: 0x060004A9 RID: 1193 RVA: 0x0000DE68 File Offset: 0x0000C068
		~Stream()
		{
			this.Dispose();
		}

		// Token: 0x060004AA RID: 1194 RVA: 0x0000DE94 File Offset: 0x0000C094
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x060004AB RID: 1195 RVA: 0x0000DEA3 File Offset: 0x0000C0A3
		protected virtual void Dispose(bool disposing)
		{
			if (!this.disposed)
			{
				if (this.handle != IntPtr.Zero)
				{
					this.handle = IntPtr.Zero;
				}
				this.disposed = true;
			}
		}

		// Token: 0x0400030C RID: 780
		private IntPtr handle;

		// Token: 0x0400030D RID: 781
		private bool disposed;
	}
}
