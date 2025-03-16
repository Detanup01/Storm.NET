using System;
using System.Collections.Concurrent;

namespace StormDotNet
{
	// Token: 0x02000025 RID: 37
	public class DynamicTypeRegistry
	{
		// Token: 0x0600009E RID: 158 RVA: 0x00002F61 File Offset: 0x00001161
		public static bool TryGet(uint dynamicTypeId, out Type dynamicType)
		{
			return DynamicTypeRegistry.registeredDynamicTypes.TryGetValue(dynamicTypeId, out dynamicType);
		}

		// Token: 0x0600009F RID: 159 RVA: 0x00002F6F File Offset: 0x0000116F
		public static void TryAdd(uint dynamicTypeId, Type dynamicType)
		{
			DynamicTypeRegistry.registeredDynamicTypes.TryAdd(dynamicTypeId, dynamicType);
		}

		// Token: 0x04000021 RID: 33
		private static readonly ConcurrentDictionary<uint, Type> registeredDynamicTypes = new ConcurrentDictionary<uint, Type>();
	}
}
