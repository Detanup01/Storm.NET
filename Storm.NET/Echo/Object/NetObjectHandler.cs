using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.InteropServices;

namespace StormDotNet.Echo.Object
{
	// Token: 0x020000D4 RID: 212
	public class NetObjectHandler
	{
		// Token: 0x06000425 RID: 1061 RVA: 0x0000C8E4 File Offset: 0x0000AAE4
		internal static void Initialize()
		{
			List<GCHandle> list = NetObjectHandler.gcHandles;
			lock (list)
			{
				NetObjectHandler.Native.CreateObjectHandler createObjectHandler = new NetObjectHandler.Native.CreateObjectHandler(NetObjectHandler.CreateObjectHandler);
				GCHandle gchandle = GCHandle.Alloc(createObjectHandler, GCHandleType.Normal);
				NetObjectHandler.gcHandles.Add(gchandle);
				NetObjectHandler.Native.ReleaseObjectHandler releaseObjectHandler = new NetObjectHandler.Native.ReleaseObjectHandler(NetObjectHandler.ReleaseObjectHandler);
				gchandle = GCHandle.Alloc(releaseObjectHandler, GCHandleType.Normal);
				NetObjectHandler.gcHandles.Add(gchandle);
				NetObjectHandler.Native.SetObject setObject = new NetObjectHandler.Native.SetObject(NetObjectHandler.SetObject);
				gchandle = GCHandle.Alloc(setObject, GCHandleType.Normal);
				NetObjectHandler.gcHandles.Add(gchandle);
				NetObjectHandler.Native.GetDefaultReplicationMode getDefaultReplicationMode = new NetObjectHandler.Native.GetDefaultReplicationMode(NetObjectHandler.GetDefaultReplicationMode);
				gchandle = GCHandle.Alloc(getDefaultReplicationMode, GCHandleType.Normal);
				NetObjectHandler.gcHandles.Add(gchandle);
				NetObjectHandler.Native.GetDefaultMigrationMode getDefaultMigrationMode = new NetObjectHandler.Native.GetDefaultMigrationMode(NetObjectHandler.GetDefaultMigrationMode);
				gchandle = GCHandle.Alloc(getDefaultMigrationMode, GCHandleType.Normal);
				NetObjectHandler.gcHandles.Add(gchandle);
				NetObjectHandler.Native.OnWriteMementoCallback onWriteMementoCallback = new NetObjectHandler.Native.OnWriteMementoCallback(NetObjectHandler.OnWriteMemento);
				gchandle = GCHandle.Alloc(onWriteMementoCallback, GCHandleType.Normal);
				NetObjectHandler.gcHandles.Add(gchandle);
				NetObjectHandler.Native.OnReadMementoCallback onReadMementoCallback = new NetObjectHandler.Native.OnReadMementoCallback(NetObjectHandler.OnReadMemento);
				gchandle = GCHandle.Alloc(onReadMementoCallback, GCHandleType.Normal);
				NetObjectHandler.gcHandles.Add(gchandle);
				NetObjectHandler.Native.OnReplicaMessageCallback onReplicaMessageCallback = new NetObjectHandler.Native.OnReplicaMessageCallback(NetObjectHandler.OnReplicaMessage);
				gchandle = GCHandle.Alloc(onReplicaMessageCallback, GCHandleType.Normal);
				NetObjectHandler.gcHandles.Add(gchandle);
				NetObjectHandler.Native.OnMasterBroadcastCallback onMasterBroadcastCallback = new NetObjectHandler.Native.OnMasterBroadcastCallback(NetObjectHandler.OnMasterBroadcast);
				gchandle = GCHandle.Alloc(onMasterBroadcastCallback, GCHandleType.Normal);
				NetObjectHandler.gcHandles.Add(gchandle);
				NetObjectHandler.Native.OnMasterUnicastCallback onMasterUnicastCallback = new NetObjectHandler.Native.OnMasterUnicastCallback(NetObjectHandler.OnMasterUnicast);
				gchandle = GCHandle.Alloc(onMasterUnicastCallback, GCHandleType.Normal);
				NetObjectHandler.gcHandles.Add(gchandle);
				NetObjectHandler.Native.OnMementoTooBigCallback onMementoTooBigCallback = new NetObjectHandler.Native.OnMementoTooBigCallback(NetObjectHandler.OnMementoTooBig);
				gchandle = GCHandle.Alloc(onMementoTooBigCallback, GCHandleType.Normal);
				NetObjectHandler.gcHandles.Add(gchandle);
				NetObjectHandler.Native.OnMasterRoleAcquiredCallback onMasterRoleAcquiredCallback = new NetObjectHandler.Native.OnMasterRoleAcquiredCallback(NetObjectHandler.OnMasterRoleAcquired);
				gchandle = GCHandle.Alloc(onMasterRoleAcquiredCallback, GCHandleType.Normal);
				NetObjectHandler.gcHandles.Add(gchandle);
				NetObjectHandler.Native.OnReplicaResolvedCallback onReplicaResolvedCallback = new NetObjectHandler.Native.OnReplicaResolvedCallback(NetObjectHandler.OnReplicaResolved);
				gchandle = GCHandle.Alloc(onReplicaResolvedCallback, GCHandleType.Normal);
				NetObjectHandler.gcHandles.Add(gchandle);
				NetObjectHandler.Native.Initialize(createObjectHandler, releaseObjectHandler, setObject, getDefaultReplicationMode, getDefaultMigrationMode, onWriteMementoCallback, onReadMementoCallback, onMementoTooBigCallback, onMasterRoleAcquiredCallback, onReplicaResolvedCallback, onReplicaMessageCallback, onMasterBroadcastCallback, onMasterUnicastCallback);
			}
		}

		// Token: 0x06000426 RID: 1062 RVA: 0x0000CAFC File Offset: 0x0000ACFC
		internal static void Uninitialize()
		{
			List<GCHandle> list = NetObjectHandler.gcHandles;
			lock (list)
			{
				foreach (GCHandle gchandle in NetObjectHandler.gcHandles)
				{
					gchandle.Free();
				}
				NetObjectHandler.gcHandles.Clear();
			}
		}

		// Token: 0x1700005C RID: 92
		// (set) Token: 0x06000427 RID: 1063 RVA: 0x0000CB80 File Offset: 0x0000AD80
		public uint ObjectRefId
		{
			set
			{
				this.objectRefId = value;
			}
		}

		// Token: 0x1700005D RID: 93
		// (get) Token: 0x06000428 RID: 1064 RVA: 0x0000CB89 File Offset: 0x0000AD89
		public NetObjectDescriptor Descriptor
		{
			get
			{
				return this.objectDescriptor;
			}
		}

		// Token: 0x1700005E RID: 94
		// (get) Token: 0x06000429 RID: 1065 RVA: 0x0000CB94 File Offset: 0x0000AD94
		public virtual NetObjectReplicationMode DefaultReplicationMode
		{
			get
			{
				return default(NetObjectReplicationMode).SetDefault();
			}
		}

		// Token: 0x1700005F RID: 95
		// (get) Token: 0x0600042A RID: 1066 RVA: 0x0000CBB0 File Offset: 0x0000ADB0
		public virtual NetObjectMigrationMode DefaultMigrationMode
		{
			get
			{
				return default(NetObjectMigrationMode).SetDefault();
			}
		}

		// Token: 0x0600042B RID: 1067 RVA: 0x00002242 File Offset: 0x00000442
		protected virtual EResult OnWriteMemento(INetProperty netProperty)
		{
			return EResult.Create(EResult.ECode.OK, "");
		}

		// Token: 0x0600042C RID: 1068 RVA: 0x00002242 File Offset: 0x00000442
		protected virtual EResult OnReadMemento(INetProperty netProperty, uint sessionTime)
		{
			return EResult.Create(EResult.ECode.OK, "");
		}

		// Token: 0x0600042D RID: 1069 RVA: 0x00002242 File Offset: 0x00000442
		protected virtual EResult OnReplicaMessage(uint senderPeerId, INetProperty netProperty, uint sessionTime)
		{
			return EResult.Create(EResult.ECode.OK, "");
		}

		// Token: 0x0600042E RID: 1070 RVA: 0x00002242 File Offset: 0x00000442
		protected virtual EResult OnMasterBroadcast(uint senderPeerId, INetProperty netProperty, uint sessionTime)
		{
			return EResult.Create(EResult.ECode.OK, "");
		}

		// Token: 0x0600042F RID: 1071 RVA: 0x00002242 File Offset: 0x00000442
		protected virtual EResult OnMasterUnicast(uint senderPeerId, INetProperty netProperty, uint sessionTime)
		{
			return EResult.Create(EResult.ECode.OK, "");
		}

		// Token: 0x06000430 RID: 1072 RVA: 0x00002242 File Offset: 0x00000442
		protected virtual EResult OnMementoTooBig(INetProperty netProperty)
		{
			return EResult.Create(EResult.ECode.OK, "");
		}

		// Token: 0x06000431 RID: 1073 RVA: 0x00002242 File Offset: 0x00000442
		protected virtual EResult OnMasterRoleAcquired(uint previousPeerId)
		{
			return EResult.Create(EResult.ECode.OK, "");
		}

		// Token: 0x06000432 RID: 1074 RVA: 0x00002242 File Offset: 0x00000442
		protected virtual EResult OnReplicaResolved(uint peerId)
		{
			return EResult.Create(EResult.ECode.OK, "");
		}

		// Token: 0x06000433 RID: 1075 RVA: 0x0000CBCC File Offset: 0x0000ADCC
		[MonoPInvokeCallback(typeof(NetObjectHandler.Native.CreateObjectHandler))]
		private static IntPtr CreateObjectHandler(IntPtr objectHandler, uint netObjectTypeName, uint sessionRefId, uint objectRefId, IntPtr objectDescriptorHandle, byte objectRole)
		{
			if (objectHandler == IntPtr.Zero)
			{
				throw new ArgumentNullException("objectHandler");
			}
			if (objectDescriptorHandle == IntPtr.Zero)
			{
				throw new ArgumentNullException("objectDescriptorHandle");
			}
			Type type;
			if (!ObjectController.TryGetHandlerType(netObjectTypeName, out type))
			{
				throw new ArgumentException("The managed object type cannot be found for the specified object type's name. Make sure ObjectController.AddObjectType() was called properly.", "netObjectTypeName");
			}
			ConstructorInfo constructor = type.GetConstructor(new Type[]
			{
				typeof(IntPtr),
				typeof(uint),
				typeof(uint),
				typeof(NetObjectDescriptor),
				typeof(NetObjectRole)
			});
			if (constructor == null)
			{
				throw new MissingMethodException(string.Format("{0} does not implement a valid constructor.", type.FullName));
			}
			NetObjectDescriptor netObjectDescriptor = GCHandle.FromIntPtr(objectDescriptorHandle).Target as NetObjectDescriptor;
			if (netObjectDescriptor == null)
			{
				throw new ArgumentException("The objectDescriptor is not of base type NetObjectDescriptor.", "ObjectDescriptorHandle");
			}
			return GCHandle.ToIntPtr(GCHandle.Alloc(constructor.Invoke(new object[]
			{
				objectHandler,
				sessionRefId,
				objectRefId,
				netObjectDescriptor,
				(NetObjectRole)objectRole
			}), GCHandleType.Normal));
		}

		// Token: 0x06000434 RID: 1076 RVA: 0x0000CCF8 File Offset: 0x0000AEF8
		[MonoPInvokeCallback(typeof(NetObjectHandler.Native.ReleaseObjectHandler))]
		private static void ReleaseObjectHandler(IntPtr handle)
		{
			if (handle == IntPtr.Zero)
			{
				throw new ArgumentNullException("handle");
			}
			GCHandle gchandle = GCHandle.FromIntPtr(handle);
			NetObjectHandler netObjectHandler = gchandle.Target as NetObjectHandler;
			if (netObjectHandler == null)
			{
				throw new ArgumentException("The network object handler is not of base type NetObjectHandler.", "handle");
			}
			netObjectHandler.handle = IntPtr.Zero;
			netObjectHandler.netObject = null;
			gchandle.Free();
		}

		// Token: 0x06000435 RID: 1077 RVA: 0x0000CD5C File Offset: 0x0000AF5C
		[MonoPInvokeCallback(typeof(NetObjectHandler.Native.SetObject))]
		private static void SetObject(IntPtr objectHandlerHandle, IntPtr objectHandle)
		{
			if (objectHandlerHandle == IntPtr.Zero)
			{
				throw new ArgumentNullException("objectHandlerHandle");
			}
			if (objectHandle == IntPtr.Zero)
			{
				throw new ArgumentNullException("objectHandle");
			}
			GCHandle gchandle = GCHandle.FromIntPtr(objectHandlerHandle);
			GCHandle gchandle2 = GCHandle.FromIntPtr(objectHandle);
			NetObjectHandler netObjectHandler = gchandle.Target as NetObjectHandler;
			NetObject netObject = gchandle2.Target as NetObject;
			if (netObjectHandler == null)
			{
				throw new ArgumentException("The network object handler is not of base type NetObjectHandler.", "objectHandlerHandle");
			}
			if (netObject == null)
			{
				throw new ArgumentException("The network object is not of base type NetObject.", "objectHandle");
			}
			netObjectHandler.netObject = netObject;
		}

		// Token: 0x06000436 RID: 1078 RVA: 0x0000CDEC File Offset: 0x0000AFEC
		[MonoPInvokeCallback(typeof(NetObjectHandler.Native.GetDefaultReplicationMode))]
		private static void GetDefaultReplicationMode(IntPtr objectHandlerHandle, ref uint mode, ref uint sessionPeerId)
		{
			if (objectHandlerHandle == IntPtr.Zero)
			{
				throw new ArgumentNullException("objectHandlerHandle");
			}
			NetObjectHandler netObjectHandler = GCHandle.FromIntPtr(objectHandlerHandle).Target as NetObjectHandler;
			if (netObjectHandler == null)
			{
				throw new ArgumentException("The network object handler is not of base type NetObjectHandler.", "objectHandlerHandle");
			}
			NetObjectReplicationMode defaultReplicationMode = netObjectHandler.DefaultReplicationMode;
			mode = defaultReplicationMode.Mode.UniqueID;
			sessionPeerId = defaultReplicationMode.SessionPeerId;
		}

		// Token: 0x06000437 RID: 1079 RVA: 0x0000CE54 File Offset: 0x0000B054
		[MonoPInvokeCallback(typeof(NetObjectHandler.Native.GetDefaultMigrationMode))]
		private static void GetDefaultMigrationMode(IntPtr objectHandlerHandle, ref uint giveMasterRoleMode, ref uint acquireMasterRoleMode, ref uint replaceGoneMasterMode, ref uint resolveMasterConflictMode)
		{
			if (objectHandlerHandle == IntPtr.Zero)
			{
				throw new ArgumentNullException("objectHandlerHandle");
			}
			NetObjectHandler netObjectHandler = GCHandle.FromIntPtr(objectHandlerHandle).Target as NetObjectHandler;
			if (netObjectHandler == null)
			{
				throw new ArgumentException("The network object handler is not of base type NetObjectHandler.", "objectHandlerHandle");
			}
			NetObjectMigrationMode defaultMigrationMode = netObjectHandler.DefaultMigrationMode;
			giveMasterRoleMode = defaultMigrationMode.GiveMasterRoleMode;
			acquireMasterRoleMode = defaultMigrationMode.AcquireMasterRoleMode.UniqueID;
			replaceGoneMasterMode = defaultMigrationMode.ReplaceGoneMasterMode;
			resolveMasterConflictMode = defaultMigrationMode.ResolveMasterConflictMode;
		}

		// Token: 0x06000438 RID: 1080 RVA: 0x0000CEE0 File Offset: 0x0000B0E0
		[MonoPInvokeCallback(typeof(NetObjectHandler.Native.OnWriteMementoCallback))]
		private static int OnWriteMemento(IntPtr objectHandlerHandle, IntPtr objectMementoHandle)
		{
			if (objectHandlerHandle == IntPtr.Zero)
			{
				throw new ArgumentNullException("objectHandlerHandle");
			}
			if (objectMementoHandle == IntPtr.Zero)
			{
				throw new ArgumentNullException("objectMementoHandle");
			}
			GCHandle gchandle = GCHandle.FromIntPtr(objectHandlerHandle);
			GCHandle gchandle2 = GCHandle.FromIntPtr(objectMementoHandle);
			NetObjectHandler netObjectHandler = gchandle.Target as NetObjectHandler;
			INetProperty netProperty = gchandle2.Target as INetProperty;
			if (netObjectHandler == null)
			{
				throw new ArgumentException("The object handler is not of base type NetObjectHandler.", "objectHandlerHandle");
			}
			if (netProperty == null)
			{
				throw new ArgumentException("The object memento is not of base type INetProperty.", "objectMementoHandle");
			}
			return (int)netObjectHandler.OnWriteMemento(netProperty).GetErrorCode();
		}

		// Token: 0x06000439 RID: 1081 RVA: 0x0000CF7C File Offset: 0x0000B17C
		[MonoPInvokeCallback(typeof(NetObjectHandler.Native.OnReadMementoCallback))]
		private static int OnReadMemento(IntPtr objectHandlerHandle, IntPtr objectMementoHandle, uint sessionTime)
		{
			if (objectHandlerHandle == IntPtr.Zero)
			{
				throw new ArgumentNullException("objectHandlerHandle");
			}
			if (objectMementoHandle == IntPtr.Zero)
			{
				throw new ArgumentNullException("objectMementoHandle");
			}
			GCHandle gchandle = GCHandle.FromIntPtr(objectHandlerHandle);
			GCHandle gchandle2 = GCHandle.FromIntPtr(objectMementoHandle);
			NetObjectHandler netObjectHandler = gchandle.Target as NetObjectHandler;
			INetProperty netProperty = gchandle2.Target as INetProperty;
			if (netObjectHandler == null)
			{
				throw new ArgumentException("The object handler is not of base type NetObjectHandler.", "objectHandlerHandle");
			}
			if (netProperty == null)
			{
				throw new ArgumentException("The object memento is not of base type INetProperty.", "objectMementoHandle");
			}
			return (int)netObjectHandler.OnReadMemento(netProperty, sessionTime).GetErrorCode();
		}

		// Token: 0x0600043A RID: 1082 RVA: 0x0000D018 File Offset: 0x0000B218
		[MonoPInvokeCallback(typeof(NetObjectHandler.Native.OnReplicaMessageCallback))]
		private static int OnReplicaMessage(IntPtr objectHandlerHandle, uint senderPeerId, IntPtr objectMessageHandle, uint sessionTime)
		{
			if (objectHandlerHandle == IntPtr.Zero)
			{
				throw new ArgumentNullException("objectHandlerHandle");
			}
			if (objectMessageHandle == IntPtr.Zero)
			{
				throw new ArgumentNullException("objectMessageHandle");
			}
			GCHandle gchandle = GCHandle.FromIntPtr(objectHandlerHandle);
			GCHandle gchandle2 = GCHandle.FromIntPtr(objectMessageHandle);
			NetObjectHandler netObjectHandler = gchandle.Target as NetObjectHandler;
			INetProperty netProperty = gchandle2.Target as INetProperty;
			if (netObjectHandler == null)
			{
				throw new ArgumentException("The object handler is not of base type NetObjectHandler.", "objectHandlerHandle");
			}
			if (netProperty == null)
			{
				throw new ArgumentException("The object memento is not of base type INetProperty.", "objectMessageHandle");
			}
			return (int)netObjectHandler.OnReplicaMessage(senderPeerId, netProperty, sessionTime).GetErrorCode();
		}

		// Token: 0x0600043B RID: 1083 RVA: 0x0000D0B8 File Offset: 0x0000B2B8
		[MonoPInvokeCallback(typeof(NetObjectHandler.Native.OnMasterBroadcastCallback))]
		private static int OnMasterBroadcast(IntPtr objectHandlerHandle, uint senderPeerId, IntPtr objectMessageHandle, uint sessionTime)
		{
			if (objectHandlerHandle == IntPtr.Zero)
			{
				throw new ArgumentNullException("objectHandlerHandle");
			}
			if (objectMessageHandle == IntPtr.Zero)
			{
				throw new ArgumentNullException("objectMessageHandle");
			}
			GCHandle gchandle = GCHandle.FromIntPtr(objectHandlerHandle);
			GCHandle gchandle2 = GCHandle.FromIntPtr(objectMessageHandle);
			NetObjectHandler netObjectHandler = gchandle.Target as NetObjectHandler;
			INetProperty netProperty = gchandle2.Target as INetProperty;
			if (netObjectHandler == null)
			{
				throw new ArgumentException("The object handler is not of base type NetObjectHandler.", "objectHandlerHandle");
			}
			if (netProperty == null)
			{
				throw new ArgumentException("The object memento is not of base type INetProperty.", "objectMessageHandle");
			}
			return (int)netObjectHandler.OnMasterBroadcast(senderPeerId, netProperty, sessionTime).GetErrorCode();
		}

		// Token: 0x0600043C RID: 1084 RVA: 0x0000D158 File Offset: 0x0000B358
		[MonoPInvokeCallback(typeof(NetObjectHandler.Native.OnMasterUnicastCallback))]
		private static int OnMasterUnicast(IntPtr objectHandlerHandle, uint senderPeerId, IntPtr objectMessageHandle, uint sessionTime)
		{
			if (objectHandlerHandle == IntPtr.Zero)
			{
				throw new ArgumentNullException("objectHandlerHandle");
			}
			if (objectMessageHandle == IntPtr.Zero)
			{
				throw new ArgumentNullException("objectMessageHandle");
			}
			GCHandle gchandle = GCHandle.FromIntPtr(objectHandlerHandle);
			GCHandle gchandle2 = GCHandle.FromIntPtr(objectMessageHandle);
			NetObjectHandler netObjectHandler = gchandle.Target as NetObjectHandler;
			INetProperty netProperty = gchandle2.Target as INetProperty;
			if (netObjectHandler == null)
			{
				throw new ArgumentException("The object handler is not of base type NetObjectHandler.", "objectHandlerHandle");
			}
			if (netProperty == null)
			{
				throw new ArgumentException("The object memento is not of base type INetProperty.", "objectMessageHandle");
			}
			return (int)netObjectHandler.OnMasterUnicast(senderPeerId, netProperty, sessionTime).GetErrorCode();
		}

		// Token: 0x0600043D RID: 1085 RVA: 0x0000D1F8 File Offset: 0x0000B3F8
		[MonoPInvokeCallback(typeof(NetObjectHandler.Native.OnMementoTooBigCallback))]
		private static int OnMementoTooBig(IntPtr objectHandlerHandle, IntPtr objectMessageHandle)
		{
			if (objectHandlerHandle == IntPtr.Zero)
			{
				throw new ArgumentNullException("objectHandlerHandle");
			}
			if (objectMessageHandle == IntPtr.Zero)
			{
				throw new ArgumentNullException("objectMessageHandle");
			}
			GCHandle gchandle = GCHandle.FromIntPtr(objectHandlerHandle);
			GCHandle gchandle2 = GCHandle.FromIntPtr(objectMessageHandle);
			NetObjectHandler netObjectHandler = gchandle.Target as NetObjectHandler;
			INetProperty netProperty = gchandle2.Target as INetProperty;
			if (netObjectHandler == null)
			{
				throw new ArgumentException("The object handler is not of base type NetObjectHandler.", "objectHandlerHandle");
			}
			if (netProperty == null)
			{
				throw new ArgumentException("The object memento is not of base type INetProperty.", "objectMessageHandle");
			}
			return (int)netObjectHandler.OnMementoTooBig(netProperty).GetErrorCode();
		}

		// Token: 0x0600043E RID: 1086 RVA: 0x0000D294 File Offset: 0x0000B494
		[MonoPInvokeCallback(typeof(NetObjectHandler.Native.OnMasterRoleAcquiredCallback))]
		private static int OnMasterRoleAcquired(IntPtr objectHandlerHandle, uint previousPeerId)
		{
			if (objectHandlerHandle == IntPtr.Zero)
			{
				throw new ArgumentNullException("objectHandlerHandle");
			}
			NetObjectHandler netObjectHandler = GCHandle.FromIntPtr(objectHandlerHandle).Target as NetObjectHandler;
			if (netObjectHandler == null)
			{
				throw new ArgumentException("The object handler is not of base type NetObjectHandler.", "objectHandler");
			}
			return (int)netObjectHandler.OnMasterRoleAcquired(previousPeerId).GetErrorCode();
		}

		// Token: 0x0600043F RID: 1087 RVA: 0x0000D2EC File Offset: 0x0000B4EC
		[MonoPInvokeCallback(typeof(NetObjectHandler.Native.OnReplicaResolvedCallback))]
		private static int OnReplicaResolved(IntPtr objectHandlerHandle, uint peerId)
		{
			if (objectHandlerHandle == IntPtr.Zero)
			{
				throw new ArgumentNullException("objectHandlerHandle");
			}
			NetObjectHandler netObjectHandler = GCHandle.FromIntPtr(objectHandlerHandle).Target as NetObjectHandler;
			if (netObjectHandler == null)
			{
				throw new ArgumentException("The object handler is not of base type NetObjectHandler.", "objectHandler");
			}
			return (int)netObjectHandler.OnReplicaResolved(peerId).GetErrorCode();
		}

		// Token: 0x0400029F RID: 671
		private static readonly List<GCHandle> gcHandles = new List<GCHandle>();

		// Token: 0x040002A0 RID: 672
		private IntPtr handle;

		// Token: 0x040002A1 RID: 673
		private uint objectRefId;

		// Token: 0x040002A2 RID: 674
		private NetObjectDescriptor objectDescriptor;

		// Token: 0x040002A3 RID: 675
		private NetObject netObject;

		// Token: 0x020000D5 RID: 213
		private static class Native
		{
			// Token: 0x06000441 RID: 1089
			[DllImport("Storm", CharSet = CharSet.Ansi, EntryPoint = "NetObjectHandler_Initialize")]
			public static extern void Initialize(NetObjectHandler.Native.CreateObjectHandler createObjectHandler, NetObjectHandler.Native.ReleaseObjectHandler releaseObjectHandler, NetObjectHandler.Native.SetObject setObject, NetObjectHandler.Native.GetDefaultReplicationMode getDefaultReplicationMode, NetObjectHandler.Native.GetDefaultMigrationMode getDefaultMigrationMode, NetObjectHandler.Native.OnWriteMementoCallback onWriteMementoCallback, NetObjectHandler.Native.OnReadMementoCallback onReadMementoCallback, NetObjectHandler.Native.OnMementoTooBigCallback onMementoTooBigCallback, NetObjectHandler.Native.OnMasterRoleAcquiredCallback onMasterRoleAcquiredCallback, NetObjectHandler.Native.OnReplicaResolvedCallback onReplicaResolved, NetObjectHandler.Native.OnReplicaMessageCallback onReplicaMessageCallback, NetObjectHandler.Native.OnMasterBroadcastCallback onMasterBroadcastCallback, NetObjectHandler.Native.OnMasterUnicastCallback onMasterUnicastCallback);

			// Token: 0x020000D6 RID: 214
			// (Invoke) Token: 0x06000443 RID: 1091
			[MonoUnmanagedFunctionPointer(CallingConvention.StdCall)]
			public delegate IntPtr CreateObjectHandler(IntPtr objectHandler, uint netObjectTypeName, uint sessionRefId, uint objectRefId, IntPtr objectDescriptorHandle, byte objectRole);

			// Token: 0x020000D7 RID: 215
			// (Invoke) Token: 0x06000447 RID: 1095
			[MonoUnmanagedFunctionPointer(CallingConvention.StdCall)]
			public delegate void ReleaseObjectHandler(IntPtr handle);

			// Token: 0x020000D8 RID: 216
			// (Invoke) Token: 0x0600044B RID: 1099
			[MonoUnmanagedFunctionPointer(CallingConvention.StdCall)]
			public delegate void SetObject(IntPtr objectHandlerHandle, IntPtr objectHandle);

			// Token: 0x020000D9 RID: 217
			// (Invoke) Token: 0x0600044F RID: 1103
			[MonoUnmanagedFunctionPointer(CallingConvention.StdCall)]
			public delegate void GetDefaultReplicationMode(IntPtr objectHandlerHandle, ref uint mode, ref uint sessionPeerId);

			// Token: 0x020000DA RID: 218
			// (Invoke) Token: 0x06000453 RID: 1107
			[MonoUnmanagedFunctionPointer(CallingConvention.StdCall)]
			public delegate void GetDefaultMigrationMode(IntPtr objectHandlerHandle, ref uint giveMasterRoleMode, ref uint acquireMasterRoleMode, ref uint replaceGoneMasterMode, ref uint resolveMasterConflictMode);

			// Token: 0x020000DB RID: 219
			// (Invoke) Token: 0x06000457 RID: 1111
			[MonoUnmanagedFunctionPointer(CallingConvention.StdCall)]
			public delegate int OnWriteMementoCallback(IntPtr objectHandlerHandle, IntPtr objectMementoHandle);

			// Token: 0x020000DC RID: 220
			// (Invoke) Token: 0x0600045B RID: 1115
			[MonoUnmanagedFunctionPointer(CallingConvention.StdCall)]
			public delegate int OnReadMementoCallback(IntPtr objectHandlerHandle, IntPtr objectMementoHandle, uint sessionTime);

			// Token: 0x020000DD RID: 221
			// (Invoke) Token: 0x0600045F RID: 1119
			[MonoUnmanagedFunctionPointer(CallingConvention.StdCall)]
			public delegate int OnReplicaMessageCallback(IntPtr objectHandlerHandle, uint senderPeerId, IntPtr objectMessageHandle, uint sessionTime);

			// Token: 0x020000DE RID: 222
			// (Invoke) Token: 0x06000463 RID: 1123
			[MonoUnmanagedFunctionPointer(CallingConvention.StdCall)]
			public delegate int OnMasterBroadcastCallback(IntPtr objectHandlerHandle, uint senderPeerId, IntPtr objectMessageHandle, uint sessionTime);

			// Token: 0x020000DF RID: 223
			// (Invoke) Token: 0x06000467 RID: 1127
			[MonoUnmanagedFunctionPointer(CallingConvention.StdCall)]
			public delegate int OnMasterUnicastCallback(IntPtr objectHandlerHandle, uint senderPeerId, IntPtr objectMessageHandle, uint sessionTime);

			// Token: 0x020000E0 RID: 224
			// (Invoke) Token: 0x0600046B RID: 1131
			[MonoUnmanagedFunctionPointer(CallingConvention.StdCall)]
			public delegate int OnReplicaResolvedCallback(IntPtr objectHandlerHandle, uint peerId);

			// Token: 0x020000E1 RID: 225
			// (Invoke) Token: 0x0600046F RID: 1135
			[MonoUnmanagedFunctionPointer(CallingConvention.StdCall)]
			public delegate int OnMasterRoleAcquiredCallback(IntPtr objectHandlerHandle, uint previousPeerId);

			// Token: 0x020000E2 RID: 226
			// (Invoke) Token: 0x06000473 RID: 1139
			[MonoUnmanagedFunctionPointer(CallingConvention.StdCall)]
			public delegate int OnMementoTooBigCallback(IntPtr objectHandlerHandle, IntPtr objectMementoHandle);
		}
	}
}
