using System;
using System.Net;
using System.Runtime.InteropServices;

namespace StormDotNet
{
	// Token: 0x02000028 RID: 40
	public class IPEndPoint : IPEndPoint
	{
		// Token: 0x060000AE RID: 174 RVA: 0x000031B4 File Offset: 0x000013B4
		internal IPEndPoint(sbyte addressFamily, IntPtr addressData, ushort port)
			: base(IPAddress.IPv6None, (int)port)
		{
			using (StormEngine.SetProfilePoint(null))
			{
				byte[] array = null;
				if (addressFamily != 0)
				{
					if (addressFamily != 2)
					{
						if (addressFamily != 23)
						{
							throw new ArgumentException("The Internet Protocol (IP) address family must be IPv4 or IPv6.");
						}
						array = new byte[16];
						Marshal.Copy(addressData, array, 0, 16);
					}
					else
					{
						array = new byte[4];
						Marshal.Copy(addressData, array, 0, 4);
					}
				}
				if (array != null)
				{
					base.Address = new IPAddress(array);
				}
			}
		}
	}
}
