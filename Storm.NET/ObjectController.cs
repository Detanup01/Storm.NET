using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using StormDotNet.Echo.Object;

namespace StormDotNet
{
	// Token: 0x02000040 RID: 64
	public static class ObjectController
	{
		// Token: 0x060000FA RID: 250 RVA: 0x00003BD0 File Offset: 0x00001DD0
		internal static void Initialize()
		{
			List<GCHandle> list = ObjectController.gcHandles;
			lock (list)
			{
				ObjectController.Native.ObjectCreatedHandler objectCreatedHandler = new ObjectController.Native.ObjectCreatedHandler(ObjectController.ObjectCreatedHandler);
				GCHandle gchandle = GCHandle.Alloc(objectCreatedHandler, GCHandleType.Normal);
				ObjectController.gcHandles.Add(gchandle);
				ObjectController.Native.ObjectDeletedHandler objectDeletedHandler = new ObjectController.Native.ObjectDeletedHandler(ObjectController.ObjectDeletedHandler);
				gchandle = GCHandle.Alloc(objectDeletedHandler, GCHandleType.Normal);
				ObjectController.gcHandles.Add(gchandle);
				ObjectController.Native.Initialize(objectCreatedHandler, objectDeletedHandler);
			}
		}

		// Token: 0x060000FB RID: 251 RVA: 0x00003C50 File Offset: 0x00001E50
		internal static void Uninitialize()
		{
			List<GCHandle> list = ObjectController.gcHandles;
			lock (list)
			{
				foreach (GCHandle gchandle in ObjectController.gcHandles)
				{
					gchandle.Free();
				}
				ObjectController.gcHandles.Clear();
			}
			Dictionary<uint, Tuple<Type, Type>> dictionary = ObjectController.registeredObjectTypes;
			lock (dictionary)
			{
				ObjectController.registeredObjectTypes.Clear();
			}
			Dictionary<uint, Type> dictionary2 = ObjectController.registeredObjectMementos;
			lock (dictionary2)
			{
				ObjectController.registeredObjectMementos.Clear();
			}
			dictionary2 = ObjectController.registeredObjectMessages;
			lock (dictionary2)
			{
				ObjectController.registeredObjectMessages.Clear();
			}
			ObjectController.OnObjectCreated = null;
			ObjectController.OnObjectDeleted = null;
		}

		// Token: 0x060000FC RID: 252 RVA: 0x00003D80 File Offset: 0x00001F80
		internal static bool TryGetObjectType(uint netObjectTypeName, out Type netObjectType)
		{
			Tuple<Type, Type> tuple = null;
			bool flag = false;
			Dictionary<uint, Tuple<Type, Type>> dictionary = ObjectController.registeredObjectTypes;
			lock (dictionary)
			{
				flag = ObjectController.registeredObjectTypes.TryGetValue(netObjectTypeName, out tuple);
			}
			netObjectType = (flag ? tuple.Item1 : null);
			return flag;
		}

		// Token: 0x060000FD RID: 253 RVA: 0x00003DDC File Offset: 0x00001FDC
		internal static bool TryGetHandlerType(uint netObjectTypeName, out Type netObjectHandlerType)
		{
			Tuple<Type, Type> tuple = null;
			bool flag = false;
			Dictionary<uint, Tuple<Type, Type>> dictionary = ObjectController.registeredObjectTypes;
			lock (dictionary)
			{
				flag = ObjectController.registeredObjectTypes.TryGetValue(netObjectTypeName, out tuple);
			}
			netObjectHandlerType = (flag ? tuple.Item2 : null);
			return flag;
		}

		// Token: 0x060000FE RID: 254 RVA: 0x00003E38 File Offset: 0x00002038
		internal static bool TryGetMementoType(uint netProtocolEntryId, out Type netProtocolEntryType)
		{
			Dictionary<uint, Type> dictionary = ObjectController.registeredObjectMementos;
			bool flag2;
			lock (dictionary)
			{
				flag2 = ObjectController.registeredObjectMementos.TryGetValue(netProtocolEntryId, out netProtocolEntryType);
			}
			return flag2;
		}

		// Token: 0x060000FF RID: 255 RVA: 0x00003E80 File Offset: 0x00002080
		internal static bool TryGetMessageType(uint netProtocolEntryId, out Type netProtocolEntryType)
		{
			Dictionary<uint, Type> dictionary = ObjectController.registeredObjectMessages;
			bool flag2;
			lock (dictionary)
			{
				flag2 = ObjectController.registeredObjectMessages.TryGetValue(netProtocolEntryId, out netProtocolEntryType);
			}
			return flag2;
		}

		// Token: 0x06000100 RID: 256 RVA: 0x00003EC8 File Offset: 0x000020C8
		[MonoPInvokeCallback(typeof(ObjectController.Native.ObjectCreatedHandler))]
		private static void ObjectCreatedHandler(uint sessionRefId, uint objectRefId, IntPtr objectHandler, byte objectRole, int result)
		{
			using (StormEngine.SetProfilePoint(null))
			{
				if (ObjectController.OnObjectCreated != null)
				{
					if (objectHandler == IntPtr.Zero)
					{
						throw new ArgumentNullException("objectHandler");
					}
					NetObjectHandler netObjectHandler = GCHandle.FromIntPtr(objectHandler).Target as NetObjectHandler;
					if (netObjectHandler == null)
					{
						throw new ArgumentException("The object handler is not of base type NetObjectHandler.", "NetObjectHandler");
					}
					if (netObjectHandler.Descriptor == null)
					{
						throw new ArgumentException("The objectDescriptor is not of base type NetObjectDescriptor.", "ObjectDescriptorHandle");
					}
					netObjectHandler.ObjectRefId = objectRefId;
					ObjectController.OnObjectCreated(sessionRefId, netObjectHandler, (NetObjectRole)objectRole, new EResult((EResult.ECode)result, "", "", 0));
				}
			}
		}

		// Token: 0x06000101 RID: 257 RVA: 0x00003F7C File Offset: 0x0000217C
		[MonoPInvokeCallback(typeof(ObjectController.Native.ObjectDeletedHandler))]
		private static void ObjectDeletedHandler(uint sessionRefId, IntPtr objectDescriptorHandle, byte objectRole, int result)
		{
			using (StormEngine.SetProfilePoint(null))
			{
				if (ObjectController.OnObjectDeleted != null)
				{
					if (objectDescriptorHandle == IntPtr.Zero)
					{
						throw new ArgumentNullException("objectDescriptorHandle");
					}
					NetObjectDescriptor netObjectDescriptor = GCHandle.FromIntPtr(objectDescriptorHandle).Target as NetObjectDescriptor;
					if (netObjectDescriptor == null)
					{
						throw new ArgumentException("The objectDescriptor is not of base type NetObjectDescriptor.", "ObjectDescriptorHandle");
					}
					ObjectController.OnObjectDeleted(sessionRefId, netObjectDescriptor, (NetObjectRole)objectRole, new EResult((EResult.ECode)result, "", "", 0));
				}
			}
		}

		// Token: 0x04000057 RID: 87
		public static NetObject.ObjectCreatedHandler OnObjectCreated = null;

		// Token: 0x04000058 RID: 88
		public static NetObject.ObjectDeletedHandler OnObjectDeleted = null;

		// Token: 0x04000059 RID: 89
		private static readonly List<GCHandle> gcHandles = new List<GCHandle>();

		// Token: 0x0400005A RID: 90
		private static readonly Dictionary<uint, Tuple<Type, Type>> registeredObjectTypes = new Dictionary<uint, Tuple<Type, Type>>();

		// Token: 0x0400005B RID: 91
		private static readonly Dictionary<uint, Type> registeredObjectMementos = new Dictionary<uint, Type>();

		// Token: 0x0400005C RID: 92
		private static readonly Dictionary<uint, Type> registeredObjectMessages = new Dictionary<uint, Type>();

		// Token: 0x02000041 RID: 65
		private static class Native
		{
			// Token: 0x06000103 RID: 259
			[DllImport("Storm", CharSet = CharSet.Ansi, EntryPoint = "ObjectController_Initialize")]
			public static extern void Initialize(ObjectController.Native.ObjectCreatedHandler onObjectCreated, ObjectController.Native.ObjectDeletedHandler onObjectDeleted);

			// Token: 0x02000042 RID: 66
			// (Invoke) Token: 0x06000105 RID: 261
			[MonoUnmanagedFunctionPointer(CallingConvention.StdCall)]
			public delegate void ObjectCreatedHandler(uint sessionRefId, uint objectRefId, IntPtr objectHandler, byte objectRole, int result);

			// Token: 0x02000043 RID: 67
			// (Invoke) Token: 0x06000109 RID: 265
			[MonoUnmanagedFunctionPointer(CallingConvention.StdCall)]
			public delegate void ObjectDeletedHandler(uint sessionRefId, IntPtr objectDescriptorHandle, byte objectRole, int result);
		}
	}
}
