using System;

namespace StormDotNet
{
	// Token: 0x0200000D RID: 13
	public abstract class Data<T> : BaseData
	{
		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000038 RID: 56
		// (set) Token: 0x06000039 RID: 57
		public abstract T Value { get; set; }

		// Token: 0x0600003A RID: 58 RVA: 0x0000255D File Offset: 0x0000075D
		protected internal Data(DataContainer dataContainer)
			: base(dataContainer)
		{
		}

		// Token: 0x0600003B RID: 59
		protected internal abstract void UpdateValue(T value);
	}
}
