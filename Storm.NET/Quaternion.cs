using System;
using System.Globalization;

namespace StormDotNet
{
	// Token: 0x02000037 RID: 55
	public struct Quaternion
	{
		// Token: 0x060000CE RID: 206 RVA: 0x00003611 File Offset: 0x00001811
		public Quaternion(float x, float y, float z, float w)
		{
			this.X = x;
			this.Y = y;
			this.Z = z;
			this.W = w;
		}

		// Token: 0x060000CF RID: 207 RVA: 0x00003630 File Offset: 0x00001830
		public static bool operator ==(Quaternion value1, Quaternion value2)
		{
			return value1.X == value2.X && value1.Y == value2.Y && value1.Z == value2.Z && value1.W == value2.W;
		}

		// Token: 0x060000D0 RID: 208 RVA: 0x0000366C File Offset: 0x0000186C
		public override int GetHashCode()
		{
			return this.X.GetHashCode() + this.Y.GetHashCode() + this.Z.GetHashCode() + this.W.GetHashCode();
		}

		// Token: 0x060000D1 RID: 209 RVA: 0x0000369D File Offset: 0x0000189D
		public override bool Equals(object obj)
		{
			return obj is Quaternion && this == (Quaternion)obj;
		}

		// Token: 0x060000D2 RID: 210 RVA: 0x000036BC File Offset: 0x000018BC
		public override string ToString()
		{
			return string.Format(CultureInfo.CurrentCulture, "{{X:{0} Y:{1} Z:{2} W:{3}}}", new object[]
			{
				this.X.ToString(CultureInfo.CurrentCulture),
				this.Y.ToString(CultureInfo.CurrentCulture),
				this.Z.ToString(CultureInfo.CurrentCulture),
				this.W.ToString(CultureInfo.CurrentCulture)
			});
		}

		// Token: 0x04000046 RID: 70
		public float X;

		// Token: 0x04000047 RID: 71
		public float Y;

		// Token: 0x04000048 RID: 72
		public float Z;

		// Token: 0x04000049 RID: 73
		public float W;
	}
}
