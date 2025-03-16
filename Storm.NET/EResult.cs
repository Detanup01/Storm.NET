using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace StormDotNet
{
	// Token: 0x020000A7 RID: 167
	public sealed class EResult
	{
		// Token: 0x06000343 RID: 835 RVA: 0x00009B70 File Offset: 0x00007D70
		public static EResult Create(EResult.ECode code, string msg = "")
		{
			string text = "";
			int num = 0;
			return new EResult(code, msg, text, num);
		}

		// Token: 0x06000344 RID: 836 RVA: 0x00009B8E File Offset: 0x00007D8E
		internal EResult(EResult.ECode code, string msg, string file, int line)
		{
			this.errorCode = code;
		}

		// Token: 0x06000345 RID: 837 RVA: 0x00009BA0 File Offset: 0x00007DA0
		internal static EResult GetLastResult()
		{
			EResult eresult;
			using (StormEngine.SetProfilePoint(null))
			{
				int num = 0;
				uint num2 = 0U;
				if (EResult.feedStringBuffer == IntPtr.Zero)
				{
					EResult.feedStringBuffer = Marshal.AllocHGlobal(1024);
					EResult.feedStringLength = 1024;
				}
				if (EResult.debugFileBuffer == IntPtr.Zero)
				{
					EResult.debugFileBuffer = Marshal.AllocHGlobal(512);
					EResult.debugFileLength = 512;
				}
				int num3 = EResult.feedStringLength;
				int num4 = EResult.debugFileLength;
				if (EResult.Native.GetLastResult(ref num, EResult.feedStringBuffer, ref num3, EResult.debugFileBuffer, ref num4, ref num2) == 0)
				{
					Marshal.FreeHGlobal(EResult.feedStringBuffer);
					Marshal.FreeHGlobal(EResult.debugFileBuffer);
					num3 = (EResult.feedStringLength = num3 + 1);
					num4 = (EResult.debugFileLength = num4 + 1);
					EResult.feedStringBuffer = Marshal.AllocHGlobal(EResult.feedStringLength);
					EResult.debugFileBuffer = Marshal.AllocHGlobal(EResult.debugFileLength);
					EResult.Native.GetLastResult(ref num, EResult.feedStringBuffer, ref num3, EResult.debugFileBuffer, ref num4, ref num2);
				}
				eresult = new EResult((EResult.ECode)num, Marshal.PtrToStringAnsi(EResult.feedStringBuffer, num3), Marshal.PtrToStringAnsi(EResult.debugFileBuffer, num4), (int)num2);
			}
			return eresult;
		}

		// Token: 0x06000346 RID: 838 RVA: 0x00009CDC File Offset: 0x00007EDC
		public EResult.ECode GetErrorCode()
		{
			return this.errorCode;
		}

		// Token: 0x06000347 RID: 839 RVA: 0x00009CE4 File Offset: 0x00007EE4
		public string GetErrorCodeName()
		{
			string text;
			if (EResult.CODE_MAPPING.ContainsKey(this.errorCode))
			{
				text = EResult.CODE_MAPPING[this.errorCode].Item2;
			}
			else
			{
				text = "EResult::GetErrorName() - Unknown Error";
			}
			return text;
		}

		// Token: 0x06000348 RID: 840 RVA: 0x00009D24 File Offset: 0x00007F24
		public string GetErrorGroupName()
		{
			string text;
			if (EResult.CODE_MAPPING.ContainsKey(this.errorCode))
			{
				text = EResult.CODE_MAPPING[this.errorCode].Item1.ToString();
			}
			else
			{
				text = "Unknown Error group";
			}
			return text;
		}

		// Token: 0x06000349 RID: 841 RVA: 0x00009D70 File Offset: 0x00007F70
		public string GetErrorFeedString()
		{
			return "No eresult string available.";
		}

		// Token: 0x0600034A RID: 842 RVA: 0x00009D77 File Offset: 0x00007F77
		public string GetLogStr()
		{
			return string.Format("{0}:{1} {2}", this.GetErrorGroupName(), this.GetErrorCodeName(), this.GetErrorFeedString());
		}

		// Token: 0x0400013E RID: 318
		[ThreadStatic]
		private static IntPtr feedStringBuffer = IntPtr.Zero;

		// Token: 0x0400013F RID: 319
		[ThreadStatic]
		private static int feedStringLength = 0;

		// Token: 0x04000140 RID: 320
		[ThreadStatic]
		private static IntPtr debugFileBuffer = IntPtr.Zero;

		// Token: 0x04000141 RID: 321
		[ThreadStatic]
		private static int debugFileLength = 0;

		// Token: 0x04000142 RID: 322
		private EResult.ECode errorCode;

		// Token: 0x04000143 RID: 323
		private static readonly Dictionary<EResult.ECode, Tuple<EResult.EGroup, string>> CODE_MAPPING = new Dictionary<EResult.ECode, Tuple<EResult.EGroup, string>>
		{
			{
				EResult.ECode.NotAssigned,
				new Tuple<EResult.EGroup, string>(EResult.EGroup.General, "NotAssigned")
			},
			{
				EResult.ECode.OK,
				new Tuple<EResult.EGroup, string>(EResult.EGroup.General, "Ok")
			},
			{
				EResult.ECode.OK_Async,
				new Tuple<EResult.EGroup, string>(EResult.EGroup.General, "Ok_Async, operation is asynchronous, will finish later")
			},
			{
				EResult.ECode.Failed,
				new Tuple<EResult.EGroup, string>(EResult.EGroup.General, "Failed")
			},
			{
				EResult.ECode.NotImplemented,
				new Tuple<EResult.EGroup, string>(EResult.EGroup.General, "Not implemented")
			},
			{
				EResult.ECode.NotSupported,
				new Tuple<EResult.EGroup, string>(EResult.EGroup.General, "Not supported")
			},
			{
				EResult.ECode.FeatureNotActivated,
				new Tuple<EResult.EGroup, string>(EResult.EGroup.General, "Feature not activated")
			},
			{
				EResult.ECode.HandlerRefIdRemoved,
				new Tuple<EResult.EGroup, string>(EResult.EGroup.General, "Handler was already removed")
			},
			{
				EResult.ECode.HandlerRefIdInvalid,
				new Tuple<EResult.EGroup, string>(EResult.EGroup.General, "Invalid handler reference id")
			},
			{
				EResult.ECode.InitStateUnexpected,
				new Tuple<EResult.EGroup, string>(EResult.EGroup.General, "Unexpected Actor's InitState")
			},
			{
				EResult.ECode.ResultEventNotAvailable,
				new Tuple<EResult.EGroup, string>(EResult.EGroup.General, "Result event not available")
			},
			{
				EResult.ECode.PeerMustBeHost,
				new Tuple<EResult.EGroup, string>(EResult.EGroup.General, "Failed, because peer is not a host")
			},
			{
				EResult.ECode.CanNotBeDoneToYourself,
				new Tuple<EResult.EGroup, string>(EResult.EGroup.General, "Local peer has the same peer GUID as the session host")
			},
			{
				EResult.ECode.ProxyNotFound,
				new Tuple<EResult.EGroup, string>(EResult.EGroup.General, "Failed, because proxy was not found")
			},
			{
				EResult.ECode.ProxyNotReady,
				new Tuple<EResult.EGroup, string>(EResult.EGroup.General, "Failed, because proxy was not ready")
			},
			{
				EResult.ECode.WrongObjectType,
				new Tuple<EResult.EGroup, string>(EResult.EGroup.General, "Failed, because of unregistered object type")
			},
			{
				EResult.ECode.WrongObjectTypeId,
				new Tuple<EResult.EGroup, string>(EResult.EGroup.General, "Failed, because of unregistered object type id")
			},
			{
				EResult.ECode.WrongObjectGroupType,
				new Tuple<EResult.EGroup, string>(EResult.EGroup.General, "Failed, because of unregistered object group type")
			},
			{
				EResult.ECode.WrongProtocolEntryId,
				new Tuple<EResult.EGroup, string>(EResult.EGroup.General, "Failed, because of unregistered protocol entry id")
			},
			{
				EResult.ECode.InvalidGUID,
				new Tuple<EResult.EGroup, string>(EResult.EGroup.General, "Failed, because of invalid GUID")
			},
			{
				EResult.ECode.Timeout,
				new Tuple<EResult.EGroup, string>(EResult.EGroup.General, "Failed, because of timeout")
			},
			{
				EResult.ECode.VersionMismatch,
				new Tuple<EResult.EGroup, string>(EResult.EGroup.General, "Failed, because of version mismatch")
			},
			{
				EResult.ECode.InvalidParameter,
				new Tuple<EResult.EGroup, string>(EResult.EGroup.General, "API called with invalid parameter")
			},
			{
				EResult.ECode.ModuleDisabled,
				new Tuple<EResult.EGroup, string>(EResult.EGroup.General, "API called while module is disabled")
			},
			{
				EResult.ECode.ModuleNotCreated,
				new Tuple<EResult.EGroup, string>(EResult.EGroup.General, "API called while module is not created")
			},
			{
				EResult.ECode.ModuleNotInitialized,
				new Tuple<EResult.EGroup, string>(EResult.EGroup.General, "API called while module is not initialized")
			},
			{
				EResult.ECode.ModuleNotStarted,
				new Tuple<EResult.EGroup, string>(EResult.EGroup.General, "API called while module is not started")
			},
			{
				EResult.ECode.ModuleNotInAppropriateState,
				new Tuple<EResult.EGroup, string>(EResult.EGroup.General, "API called while module is not in appropriate state")
			},
			{
				EResult.ECode.ApiRequestCancelled,
				new Tuple<EResult.EGroup, string>(EResult.EGroup.General, "API request was Canceled")
			},
			{
				EResult.ECode.NotificationHandlerAlreadyRegistered,
				new Tuple<EResult.EGroup, string>(EResult.EGroup.General, "Notification handler is already registered")
			},
			{
				EResult.ECode.NotificationHandlerNotRegistered,
				new Tuple<EResult.EGroup, string>(EResult.EGroup.General, "Notification handler is not registered")
			},
			{
				EResult.ECode.IdPoolExhausted,
				new Tuple<EResult.EGroup, string>(EResult.EGroup.General, "Id pool exhausted")
			},
			{
				EResult.ECode.StreamReadFailed,
				new Tuple<EResult.EGroup, string>(EResult.EGroup.General, "Failure during stream read")
			},
			{
				EResult.ECode.StreamWriteFailed,
				new Tuple<EResult.EGroup, string>(EResult.EGroup.General, "Failure during stream write")
			},
			{
				EResult.ECode.DataContainerCreateFailed,
				new Tuple<EResult.EGroup, string>(EResult.EGroup.General, "Failure during datacontainer create")
			},
			{
				EResult.ECode.TransmissionStreamReadError,
				new Tuple<EResult.EGroup, string>(EResult.EGroup.Transmission, "Stream read error")
			},
			{
				EResult.ECode.TransmissionInvalidConnectionState,
				new Tuple<EResult.EGroup, string>(EResult.EGroup.Transmission, "Invalid connection state")
			},
			{
				EResult.ECode.TransmissionInvalidPrototypeType,
				new Tuple<EResult.EGroup, string>(EResult.EGroup.Transmission, "Invalid prototype type")
			},
			{
				EResult.ECode.TransmissionMissingTransmissionTable,
				new Tuple<EResult.EGroup, string>(EResult.EGroup.Transmission, "Missing transmission table")
			},
			{
				EResult.ECode.TransmissionChecksumMismatch,
				new Tuple<EResult.EGroup, string>(EResult.EGroup.Transmission, "Checksum mismatch")
			},
			{
				EResult.ECode.TransmissionMessageNotSent,
				new Tuple<EResult.EGroup, string>(EResult.EGroup.Transmission, "Message was not sent to the peer")
			},
			{
				EResult.ECode.TransmissionMessageSentButNotConfirmed,
				new Tuple<EResult.EGroup, string>(EResult.EGroup.Transmission, "Message was sent but its delivery wasn't confirmed")
			},
			{
				EResult.ECode.TransmissionMessageSentButNotDelivered,
				new Tuple<EResult.EGroup, string>(EResult.EGroup.Transmission, "Message was sent but not delivered, transmission failed")
			},
			{
				EResult.ECode.SocketBindFailed,
				new Tuple<EResult.EGroup, string>(EResult.EGroup.Socket, "Bind failure")
			},
			{
				EResult.ECode.SocketOpenFailed,
				new Tuple<EResult.EGroup, string>(EResult.EGroup.Socket, "Open failure")
			},
			{
				EResult.ECode.SocketSetBlockingFailed,
				new Tuple<EResult.EGroup, string>(EResult.EGroup.Socket, "Set blocking failure")
			},
			{
				EResult.ECode.SocketAlreadyCreated,
				new Tuple<EResult.EGroup, string>(EResult.EGroup.Socket, "Socket already created")
			},
			{
				EResult.ECode.SocketReuseAddressFailed,
				new Tuple<EResult.EGroup, string>(EResult.EGroup.Socket, "Reuse address failure")
			},
			{
				EResult.ECode.SocketSetBroadcastFailed,
				new Tuple<EResult.EGroup, string>(EResult.EGroup.Socket, "Set broadcast failure")
			},
			{
				EResult.ECode.SocketClosed,
				new Tuple<EResult.EGroup, string>(EResult.EGroup.Socket, "Socket is closed")
			},
			{
				EResult.ECode.SocketNotAvailable,
				new Tuple<EResult.EGroup, string>(EResult.EGroup.Socket, "Socket not available")
			},
			{
				EResult.ECode.SocketRebindUpnpMappingConflict,
				new Tuple<EResult.EGroup, string>(EResult.EGroup.Socket, "Socket rebind to another port, because of an Upnp mapping conflict")
			},
			{
				EResult.ECode.SocketRebindNATTypeIncoherent,
				new Tuple<EResult.EGroup, string>(EResult.EGroup.Socket, "Socket rebind to another port, because of NAT types are incoherent")
			},
			{
				EResult.ECode.LanModuleMatchmakingSocketSetupFailed,
				new Tuple<EResult.EGroup, string>(EResult.EGroup.LanModule, "Matchmaking socket setup failed")
			},
			{
				EResult.ECode.LanModuleDiscoverySocketSetupFailed,
				new Tuple<EResult.EGroup, string>(EResult.EGroup.LanModule, "Discovery socket setup failed")
			},
			{
				EResult.ECode.NetEngineNotCreated,
				new Tuple<EResult.EGroup, string>(EResult.EGroup.NetEngine, "Engine not created")
			},
			{
				EResult.ECode.NetEngineAlreadyStarted,
				new Tuple<EResult.EGroup, string>(EResult.EGroup.NetEngine, "Already started")
			},
			{
				EResult.ECode.NetEngineNotShutdown,
				new Tuple<EResult.EGroup, string>(EResult.EGroup.NetEngine, "Engine not shutdown")
			},
			{
				EResult.ECode.NetEngineNotStarted,
				new Tuple<EResult.EGroup, string>(EResult.EGroup.NetEngine, "Engine not started")
			},
			{
				EResult.ECode.NetEngineProtocolValidationFailed,
				new Tuple<EResult.EGroup, string>(EResult.EGroup.NetEngine, "Protocol validation failure")
			},
			{
				EResult.ECode.NetEngineNoShutdownDuringUpdate,
				new Tuple<EResult.EGroup, string>(EResult.EGroup.NetEngine, "No shutdown during update")
			},
			{
				EResult.ECode.NetEngineModuleNotAvailable,
				new Tuple<EResult.EGroup, string>(EResult.EGroup.NetEngine, "Module not available")
			},
			{
				EResult.ECode.NetEngineOnlyAvailableInSession,
				new Tuple<EResult.EGroup, string>(EResult.EGroup.NetEngine, "Module only available in a session")
			},
			{
				EResult.ECode.PeerChannelTargetPeerInvalid,
				new Tuple<EResult.EGroup, string>(EResult.EGroup.PeerChannel, "Target peer is invalid")
			},
			{
				EResult.ECode.PeerChannelTargetPeerIsGone,
				new Tuple<EResult.EGroup, string>(EResult.EGroup.PeerChannel, "Target peer is not present or gone; message will be locally dropped")
			},
			{
				EResult.ECode.PeerChannelUnicastToSelf,
				new Tuple<EResult.EGroup, string>(EResult.EGroup.PeerChannel, "Can not unicast to local peer/instance")
			},
			{
				EResult.ECode.PeerChannelTransmissionFull,
				new Tuple<EResult.EGroup, string>(EResult.EGroup.PeerChannel, "Unreliable message was dropped because transmission window is full")
			},
			{
				EResult.ECode.ObjectRefIdInvalid,
				new Tuple<EResult.EGroup, string>(EResult.EGroup.Object, "Invalid object reference id")
			},
			{
				EResult.ECode.ObjectRouteIsNotAvailable,
				new Tuple<EResult.EGroup, string>(EResult.EGroup.Object, "Route is not available for specific peer")
			},
			{
				EResult.ECode.ObjectRouteAlreadyExists,
				new Tuple<EResult.EGroup, string>(EResult.EGroup.Object, "Object route already exists")
			},
			{
				EResult.ECode.ObjectConnectionNotPublished,
				new Tuple<EResult.EGroup, string>(EResult.EGroup.Object, "Connection to peer is not published")
			},
			{
				EResult.ECode.ObjectMessageNotSentNoValidRouteFound,
				new Tuple<EResult.EGroup, string>(EResult.EGroup.Object, "Message was not sent, no valid route found to target peer")
			},
			{
				EResult.ECode.ObjectMessageResolvedReplicaNotReady,
				new Tuple<EResult.EGroup, string>(EResult.EGroup.Object, "Message was not sent, resolved replica not ready")
			},
			{
				EResult.ECode.ObjectWrongNetId,
				new Tuple<EResult.EGroup, string>(EResult.EGroup.Object, "Unexpected ObjectNetId used")
			},
			{
				EResult.ECode.ObjectMessageTransmissionFull,
				new Tuple<EResult.EGroup, string>(EResult.EGroup.Object, "Unreliable message was dropped because transmission window is full")
			},
			{
				EResult.ECode.ObjectObserverAlreadyRemoved,
				new Tuple<EResult.EGroup, string>(EResult.EGroup.ObjectObserver, "ObjectObserver was already removed")
			},
			{
				EResult.ECode.SessionRefIdInvalid,
				new Tuple<EResult.EGroup, string>(EResult.EGroup.Session, "Invalid session reference id")
			},
			{
				EResult.ECode.SessionHostMigrationDisabled,
				new Tuple<EResult.EGroup, string>(EResult.EGroup.Session, "Host migration disabled")
			},
			{
				EResult.ECode.SessionHostMigrationInvalidTopology,
				new Tuple<EResult.EGroup, string>(EResult.EGroup.Session, "Invalid topology for host migration")
			},
			{
				EResult.ECode.SessionKicked,
				new Tuple<EResult.EGroup, string>(EResult.EGroup.Session, "Session peer kicked")
			},
			{
				EResult.ECode.SessionDissolved,
				new Tuple<EResult.EGroup, string>(EResult.EGroup.Session, "Session dissolved")
			},
			{
				EResult.ECode.SessionRefused,
				new Tuple<EResult.EGroup, string>(EResult.EGroup.Session, "Session request refused")
			},
			{
				EResult.ECode.SessionHostGone,
				new Tuple<EResult.EGroup, string>(EResult.EGroup.Session, "Session host gone")
			},
			{
				EResult.ECode.SessionPeerGone,
				new Tuple<EResult.EGroup, string>(EResult.EGroup.Session, "Session peer gone")
			},
			{
				EResult.ECode.SessionInvalid,
				new Tuple<EResult.EGroup, string>(EResult.EGroup.Session, "Session invalid")
			},
			{
				EResult.ECode.SessionRefIdPoolExhausted,
				new Tuple<EResult.EGroup, string>(EResult.EGroup.Session, "SessionRefId pool exhausted")
			},
			{
				EResult.ECode.SessionPeerNotFound,
				new Tuple<EResult.EGroup, string>(EResult.EGroup.Session, "Session peer not found")
			},
			{
				EResult.ECode.SessionMaxSession,
				new Tuple<EResult.EGroup, string>(EResult.EGroup.Session, "Session max reached")
			},
			{
				EResult.ECode.SessionVersionMismatch,
				new Tuple<EResult.EGroup, string>(EResult.EGroup.Session, "Session version mismatch")
			},
			{
				EResult.ECode.SessionExists,
				new Tuple<EResult.EGroup, string>(EResult.EGroup.Session, "Session exists")
			},
			{
				EResult.ECode.SessionLeaving,
				new Tuple<EResult.EGroup, string>(EResult.EGroup.Session, "Leaving session")
			},
			{
				EResult.ECode.SessionHostMigrationRunning,
				new Tuple<EResult.EGroup, string>(EResult.EGroup.Session, "Host migration is running")
			},
			{
				EResult.ECode.SessionRefIdError,
				new Tuple<EResult.EGroup, string>(EResult.EGroup.Session, "Session ref id error")
			},
			{
				EResult.ECode.GroupRefIdInvalid,
				new Tuple<EResult.EGroup, string>(EResult.EGroup.Group, "Invalid group reference id")
			},
			{
				EResult.ECode.GroupRefIdPoolExhausted,
				new Tuple<EResult.EGroup, string>(EResult.EGroup.Group, "GroupRefId pool exhausted")
			},
			{
				EResult.ECode.GroupRequestDuplicated,
				new Tuple<EResult.EGroup, string>(EResult.EGroup.Group, "Group request is a duplicate")
			},
			{
				EResult.ECode.GroupPeerAlreadyExists,
				new Tuple<EResult.EGroup, string>(EResult.EGroup.Group, "Group peer already exists")
			},
			{
				EResult.ECode.GroupVersionMismatch,
				new Tuple<EResult.EGroup, string>(EResult.EGroup.Group, "Group version mismatch")
			},
			{
				EResult.ECode.GroupPeerIdPoolExhausted,
				new Tuple<EResult.EGroup, string>(EResult.EGroup.Group, "Group PeerId pool exhausted")
			},
			{
				EResult.ECode.GroupPeerIdInvalid,
				new Tuple<EResult.EGroup, string>(EResult.EGroup.Group, "Group PeerId invalid")
			},
			{
				EResult.ECode.GroupRequestNotFound,
				new Tuple<EResult.EGroup, string>(EResult.EGroup.Group, "Group request not found")
			},
			{
				EResult.ECode.GroupPeerDescriptorInvalid,
				new Tuple<EResult.EGroup, string>(EResult.EGroup.Group, "Group PeerDescriptor invalid")
			},
			{
				EResult.ECode.GroupDeleted,
				new Tuple<EResult.EGroup, string>(EResult.EGroup.Group, "Group deleted")
			},
			{
				EResult.ECode.GroupMaxPeerReached,
				new Tuple<EResult.EGroup, string>(EResult.EGroup.Group, "Max group peer reached")
			},
			{
				EResult.ECode.GroupDescriptorInvalid,
				new Tuple<EResult.EGroup, string>(EResult.EGroup.Group, "Group descriptor invalid")
			},
			{
				EResult.ECode.GroupRefIdError,
				new Tuple<EResult.EGroup, string>(EResult.EGroup.Group, "Group ref id error")
			},
			{
				EResult.ECode.GroupAddPeerRefused,
				new Tuple<EResult.EGroup, string>(EResult.EGroup.Group, "Group add peer refused")
			},
			{
				EResult.ECode.AudioHardwareError,
				new Tuple<EResult.EGroup, string>(EResult.EGroup.Voice, "Audio Hardware Error")
			},
			{
				EResult.ECode.NoValidLocalUser,
				new Tuple<EResult.EGroup, string>(EResult.EGroup.Voice, "No valid local user account selected or sign-in")
			},
			{
				EResult.ECode.Link_Fail,
				new Tuple<EResult.EGroup, string>(EResult.EGroup.Link, "Link failed")
			},
			{
				EResult.ECode.Link_ResolveFail,
				new Tuple<EResult.EGroup, string>(EResult.EGroup.Link, "Link resolve failed")
			},
			{
				EResult.ECode.Link_ResolveTimeout,
				new Tuple<EResult.EGroup, string>(EResult.EGroup.Link, "Link resolve timeout")
			},
			{
				EResult.ECode.Link_ReachFail,
				new Tuple<EResult.EGroup, string>(EResult.EGroup.Link, "Link reach failed")
			},
			{
				EResult.ECode.Link_ReachRefused,
				new Tuple<EResult.EGroup, string>(EResult.EGroup.Link, "Link reach refused")
			},
			{
				EResult.ECode.Link_Drop,
				new Tuple<EResult.EGroup, string>(EResult.EGroup.Link, "Link dropped")
			},
			{
				EResult.ECode.Link_Disconnected,
				new Tuple<EResult.EGroup, string>(EResult.EGroup.Link, "Link disconnected")
			},
			{
				EResult.ECode.Connection_Fail,
				new Tuple<EResult.EGroup, string>(EResult.EGroup.Transport, "Connection failed")
			},
			{
				EResult.ECode.Connection_RequestFail,
				new Tuple<EResult.EGroup, string>(EResult.EGroup.Transport, "Connection request failed")
			},
			{
				EResult.ECode.Connection_ReachFail,
				new Tuple<EResult.EGroup, string>(EResult.EGroup.Transport, "Connection reach failed")
			},
			{
				EResult.ECode.Connection_CipherFail,
				new Tuple<EResult.EGroup, string>(EResult.EGroup.Transport, "Connection cipher failed")
			},
			{
				EResult.ECode.Connection_AuthenticationFail,
				new Tuple<EResult.EGroup, string>(EResult.EGroup.Transport, "Connection authentication refused")
			},
			{
				EResult.ECode.Connection_ReachRefused,
				new Tuple<EResult.EGroup, string>(EResult.EGroup.Transport, "Connection reach refused")
			},
			{
				EResult.ECode.Connection_Drop,
				new Tuple<EResult.EGroup, string>(EResult.EGroup.Transport, "Connection dropped")
			},
			{
				EResult.ECode.Connection_Disconnection,
				new Tuple<EResult.EGroup, string>(EResult.EGroup.Transport, "Connection disconnection")
			},
			{
				EResult.ECode.Connection_Timeout,
				new Tuple<EResult.EGroup, string>(EResult.EGroup.Transport, "Connection timeout")
			},
			{
				EResult.ECode.TransportColorPoolExhausted,
				new Tuple<EResult.EGroup, string>(EResult.EGroup.Transport, "TransportColor pool exhausted")
			},
			{
				EResult.ECode.PunchClientNotificationHandlerAlreadyRegistered,
				new Tuple<EResult.EGroup, string>(EResult.EGroup.Punch, "Punch client: Notification handler is already registered")
			},
			{
				EResult.ECode.PunchClientNotificationHandlerNotRegistered,
				new Tuple<EResult.EGroup, string>(EResult.EGroup.Punch, "Punch client: Notification handler is not registered")
			},
			{
				EResult.ECode.PunchClientMpePacketHandlerAlreadyRegistered,
				new Tuple<EResult.EGroup, string>(EResult.EGroup.Punch, "Punch client: MPE packet handler is already registered")
			},
			{
				EResult.ECode.PunchClientMpePacketHandlerNotRegistered,
				new Tuple<EResult.EGroup, string>(EResult.EGroup.Punch, "Punch client: MPE packet handler is not registered")
			},
			{
				EResult.ECode.PunchTraversalMpeAddressAlreadyRegistered,
				new Tuple<EResult.EGroup, string>(EResult.EGroup.Punch, "Punch traversal: MPE address is already registered")
			},
			{
				EResult.ECode.PunchTraversalMpeAddressNotRegistered,
				new Tuple<EResult.EGroup, string>(EResult.EGroup.Punch, "Punch traversal: MPE address is not registered")
			},
			{
				EResult.ECode.PunchTraversalAlreadyUsingServer,
				new Tuple<EResult.EGroup, string>(EResult.EGroup.Punch, "Punch traversal is already using the server")
			},
			{
				EResult.ECode.PunchTraversalNoServerInUse,
				new Tuple<EResult.EGroup, string>(EResult.EGroup.Punch, "Punch traversal is not using any server")
			},
			{
				EResult.ECode.PunchTraversalConnectionLost,
				new Tuple<EResult.EGroup, string>(EResult.EGroup.Punch, "Punch Traversal server connection lost")
			},
			{
				EResult.ECode.PunchTraversalConnectionTimeout,
				new Tuple<EResult.EGroup, string>(EResult.EGroup.Punch, "The connection to the Punch Traversal server timed out")
			},
			{
				EResult.ECode.PunchTraversalUrlNotResolved,
				new Tuple<EResult.EGroup, string>(EResult.EGroup.Punch, "Punch Traversal URL server not resolved")
			},
			{
				EResult.ECode.PunchServerDetect1Unreachable,
				new Tuple<EResult.EGroup, string>(EResult.EGroup.Punch, "Punch server Detect01 unreachable")
			},
			{
				EResult.ECode.PunchServerDetect2Unreachable,
				new Tuple<EResult.EGroup, string>(EResult.EGroup.Punch, "Punch server Detect02 unreachable")
			},
			{
				EResult.ECode.PunchDetectDetectAlreadyInProgress,
				new Tuple<EResult.EGroup, string>(EResult.EGroup.Punch, "Punch Detect already in progress")
			},
			{
				EResult.ECode.PunchDetectMap1Failed,
				new Tuple<EResult.EGroup, string>(EResult.EGroup.Punch, "Punch Detection Map1 failed")
			},
			{
				EResult.ECode.PunchDetectTimedOut,
				new Tuple<EResult.EGroup, string>(EResult.EGroup.Punch, "Punch Detection timed out")
			},
			{
				EResult.ECode.PunchDetectAlreadyEnabled,
				new Tuple<EResult.EGroup, string>(EResult.EGroup.Punch, "Punch Detect already enabled")
			},
			{
				EResult.ECode.PunchUpnpNoDeviceFound,
				new Tuple<EResult.EGroup, string>(EResult.EGroup.Punch, "Punch UPnP, no device found")
			},
			{
				EResult.ECode.PunchUpnpThreadNotStarted,
				new Tuple<EResult.EGroup, string>(EResult.EGroup.Punch, "Punch UPnP, the internal thread is not started")
			},
			{
				EResult.ECode.PunchUpnpCancelInProgress,
				new Tuple<EResult.EGroup, string>(EResult.EGroup.Punch, "A cancel request is in progress")
			},
			{
				EResult.ECode.PunchUpnpCommandAlreadyInProgress,
				new Tuple<EResult.EGroup, string>(EResult.EGroup.Punch, "A command is already in progress")
			},
			{
				EResult.ECode.Audio_DeviceNotFound,
				new Tuple<EResult.EGroup, string>(EResult.EGroup.Audio, "The audio device is not found.")
			},
			{
				EResult.ECode.Audio_DeviceNotConnected,
				new Tuple<EResult.EGroup, string>(EResult.EGroup.Audio, "The audio device is not connected.")
			},
			{
				EResult.ECode.Audio_DeviceAlreadyInUse,
				new Tuple<EResult.EGroup, string>(EResult.EGroup.Audio, "The audio device is already in use.")
			},
			{
				EResult.ECode.Audio_DeviceNotSupported,
				new Tuple<EResult.EGroup, string>(EResult.EGroup.Audio, "The audio device is not supported.")
			},
			{
				EResult.ECode.Audio_NotAuthorized,
				new Tuple<EResult.EGroup, string>(EResult.EGroup.Audio, "The user did not grant recording permission to the application.")
			},
			{
				EResult.ECode.Audio_WaitingForAuthorization,
				new Tuple<EResult.EGroup, string>(EResult.EGroup.Audio, "The application is waiting for authorization. Retry later.")
			},
			{
				EResult.ECode.Video_DeviceNotFound,
				new Tuple<EResult.EGroup, string>(EResult.EGroup.Video, "The video device is not found.")
			},
			{
				EResult.ECode.Video_DeviceNotConnected,
				new Tuple<EResult.EGroup, string>(EResult.EGroup.Video, "The video device is not connected.")
			},
			{
				EResult.ECode.Video_DeviceAlreadyInUse,
				new Tuple<EResult.EGroup, string>(EResult.EGroup.Video, "The video device is already in use.")
			},
			{
				EResult.ECode.Video_DeviceNotSupported,
				new Tuple<EResult.EGroup, string>(EResult.EGroup.Video, "The video device is not supported.")
			},
			{
				EResult.ECode.Video_CodecNotSupported,
				new Tuple<EResult.EGroup, string>(EResult.EGroup.Video, "Codec is not supported by the video chat module.")
			},
			{
				EResult.ECode.Video_PixelFormatNotSupported,
				new Tuple<EResult.EGroup, string>(EResult.EGroup.Video, "Pixel format is not supported by the video chat module.")
			},
			{
				EResult.ECode.Video_ResolutionNotSupported,
				new Tuple<EResult.EGroup, string>(EResult.EGroup.Video, "Resolution is not supported by the video chat module.")
			},
			{
				EResult.ECode.Video_FramerateNotSupported,
				new Tuple<EResult.EGroup, string>(EResult.EGroup.Video, "Framerate is not supported by the video chat module.")
			},
			{
				EResult.ECode.Video_NotAuthorized,
				new Tuple<EResult.EGroup, string>(EResult.EGroup.Video, "The user did not grant capture permission to the application.")
			},
			{
				EResult.ECode.Video_WaitingForAuthorization,
				new Tuple<EResult.EGroup, string>(EResult.EGroup.Video, "The application is waiting for authorization. Retry later.")
			},
			{
				EResult.ECode.Video_EncodeFailed,
				new Tuple<EResult.EGroup, string>(EResult.EGroup.Video, "Encode failed.")
			},
			{
				EResult.ECode.Video_DecodeFailed,
				new Tuple<EResult.EGroup, string>(EResult.EGroup.Video, "Decode failed.")
			},
			{
				EResult.ECode.Video_FrameDropped,
				new Tuple<EResult.EGroup, string>(EResult.EGroup.Video, "Frame was dropped.")
			},
			{
				EResult.ECode.Services_USError,
				new Tuple<EResult.EGroup, string>(EResult.EGroup.Services, "UbiServices error, check system error code.")
			},
			{
				EResult.ECode.Services_SMMError,
				new Tuple<EResult.EGroup, string>(EResult.EGroup.Services, "SMM error, check system error code.")
			},
			{
				EResult.ECode.Services_PunchError,
				new Tuple<EResult.EGroup, string>(EResult.EGroup.Services, "Punch error, check system error code.")
			},
			{
				EResult.ECode.Services_LocalAddressError,
				new Tuple<EResult.EGroup, string>(EResult.EGroup.Services, "Local address error, check system error code.")
			},
			{
				EResult.ECode.Registry_SessionTypeNotFound,
				new Tuple<EResult.EGroup, string>(EResult.EGroup.Registry, "The session type is not found. Cannot register the type or query the CRC.")
			},
			{
				EResult.ECode.Registry_SessionTypeCommitted,
				new Tuple<EResult.EGroup, string>(EResult.EGroup.Registry, "The session type is already committed. Cannot register additional types.")
			},
			{
				EResult.ECode.Registry_SessionTypeNotCommitted,
				new Tuple<EResult.EGroup, string>(EResult.EGroup.Registry, "The session type is not committed. Cannot query the CRC.")
			},
			{
				EResult.ECode.Registry_GroupTypeNotCommitted,
				new Tuple<EResult.EGroup, string>(EResult.EGroup.Registry, "The group type is not committed. Cannot query the CRC.")
			}
		};

		// Token: 0x020000A8 RID: 168
		public enum EGroup
		{
			// Token: 0x04000145 RID: 325
			General,
			// Token: 0x04000146 RID: 326
			Transmission,
			// Token: 0x04000147 RID: 327
			Socket,
			// Token: 0x04000148 RID: 328
			LanModule,
			// Token: 0x04000149 RID: 329
			NetEngine,
			// Token: 0x0400014A RID: 330
			PeerChannel,
			// Token: 0x0400014B RID: 331
			Object,
			// Token: 0x0400014C RID: 332
			ObjectObserver,
			// Token: 0x0400014D RID: 333
			Session,
			// Token: 0x0400014E RID: 334
			Group,
			// Token: 0x0400014F RID: 335
			Voice,
			// Token: 0x04000150 RID: 336
			Link,
			// Token: 0x04000151 RID: 337
			Transport,
			// Token: 0x04000152 RID: 338
			Punch,
			// Token: 0x04000153 RID: 339
			Audio,
			// Token: 0x04000154 RID: 340
			Video,
			// Token: 0x04000155 RID: 341
			Services,
			// Token: 0x04000156 RID: 342
			Registry,
			// Token: 0x04000157 RID: 343
			EndMarker
		}

		// Token: 0x020000A9 RID: 169
		public enum ECode
		{
			// Token: 0x04000159 RID: 345
			NotAssigned,
			// Token: 0x0400015A RID: 346
			OK,
			// Token: 0x0400015B RID: 347
			OK_Async,
			// Token: 0x0400015C RID: 348
			SocketRebindUpnpMappingConflict,
			// Token: 0x0400015D RID: 349
			SocketRebindNATTypeIncoherent,
			// Token: 0x0400015E RID: 350
			Failed = -1000000,
			// Token: 0x0400015F RID: 351
			NotImplemented,
			// Token: 0x04000160 RID: 352
			NotSupported,
			// Token: 0x04000161 RID: 353
			FeatureNotActivated,
			// Token: 0x04000162 RID: 354
			HandlerRefIdInvalid,
			// Token: 0x04000163 RID: 355
			HandlerRefIdRemoved,
			// Token: 0x04000164 RID: 356
			InitStateUnexpected,
			// Token: 0x04000165 RID: 357
			ResultEventNotAvailable,
			// Token: 0x04000166 RID: 358
			PeerMustBeHost,
			// Token: 0x04000167 RID: 359
			CanNotBeDoneToYourself,
			// Token: 0x04000168 RID: 360
			ProxyNotFound,
			// Token: 0x04000169 RID: 361
			ProxyNotReady,
			// Token: 0x0400016A RID: 362
			WrongObjectType,
			// Token: 0x0400016B RID: 363
			WrongObjectTypeId,
			// Token: 0x0400016C RID: 364
			WrongObjectGroupType,
			// Token: 0x0400016D RID: 365
			WrongProtocolEntryId,
			// Token: 0x0400016E RID: 366
			InvalidGUID,
			// Token: 0x0400016F RID: 367
			Timeout,
			// Token: 0x04000170 RID: 368
			VersionMismatch,
			// Token: 0x04000171 RID: 369
			InvalidParameter,
			// Token: 0x04000172 RID: 370
			ModuleDisabled,
			// Token: 0x04000173 RID: 371
			ModuleNotCreated,
			// Token: 0x04000174 RID: 372
			ModuleNotInitialized,
			// Token: 0x04000175 RID: 373
			ModuleNotStarted,
			// Token: 0x04000176 RID: 374
			ModuleNotInAppropriateState,
			// Token: 0x04000177 RID: 375
			ApiRequestCancelled,
			// Token: 0x04000178 RID: 376
			NotificationHandlerAlreadyRegistered,
			// Token: 0x04000179 RID: 377
			NotificationHandlerNotRegistered,
			// Token: 0x0400017A RID: 378
			IdPoolExhausted,
			// Token: 0x0400017B RID: 379
			StreamReadFailed,
			// Token: 0x0400017C RID: 380
			StreamWriteFailed,
			// Token: 0x0400017D RID: 381
			DataContainerCreateFailed,
			// Token: 0x0400017E RID: 382
			TransmissionStreamReadError,
			// Token: 0x0400017F RID: 383
			TransmissionInvalidConnectionState,
			// Token: 0x04000180 RID: 384
			TransmissionInvalidPrototypeType,
			// Token: 0x04000181 RID: 385
			TransmissionMissingTransmissionTable,
			// Token: 0x04000182 RID: 386
			TransmissionChecksumMismatch,
			// Token: 0x04000183 RID: 387
			TransmissionMessageNotSent,
			// Token: 0x04000184 RID: 388
			TransmissionMessageSentButNotConfirmed,
			// Token: 0x04000185 RID: 389
			TransmissionMessageSentButNotDelivered,
			// Token: 0x04000186 RID: 390
			SocketBindFailed,
			// Token: 0x04000187 RID: 391
			SocketOpenFailed,
			// Token: 0x04000188 RID: 392
			SocketSetBlockingFailed,
			// Token: 0x04000189 RID: 393
			SocketAlreadyCreated,
			// Token: 0x0400018A RID: 394
			SocketReuseAddressFailed,
			// Token: 0x0400018B RID: 395
			SocketSetBroadcastFailed,
			// Token: 0x0400018C RID: 396
			SocketClosed,
			// Token: 0x0400018D RID: 397
			SocketNotAvailable,
			// Token: 0x0400018E RID: 398
			LanModuleMatchmakingSocketSetupFailed,
			// Token: 0x0400018F RID: 399
			LanModuleDiscoverySocketSetupFailed,
			// Token: 0x04000190 RID: 400
			NetEngineNotCreated,
			// Token: 0x04000191 RID: 401
			NetEngineAlreadyStarted,
			// Token: 0x04000192 RID: 402
			NetEngineNotShutdown,
			// Token: 0x04000193 RID: 403
			NetEngineNotStarted,
			// Token: 0x04000194 RID: 404
			NetEngineProtocolValidationFailed,
			// Token: 0x04000195 RID: 405
			NetEngineNoShutdownDuringUpdate,
			// Token: 0x04000196 RID: 406
			NetEngineModuleNotAvailable,
			// Token: 0x04000197 RID: 407
			NetEngineOnlyAvailableInSession,
			// Token: 0x04000198 RID: 408
			PeerChannelTargetPeerInvalid,
			// Token: 0x04000199 RID: 409
			PeerChannelTargetPeerIsGone,
			// Token: 0x0400019A RID: 410
			PeerChannelUnicastToSelf,
			// Token: 0x0400019B RID: 411
			PeerChannelTransmissionFull,
			// Token: 0x0400019C RID: 412
			ObjectRefIdInvalid,
			// Token: 0x0400019D RID: 413
			ObjectRouteIsNotAvailable,
			// Token: 0x0400019E RID: 414
			ObjectRouteAlreadyExists,
			// Token: 0x0400019F RID: 415
			ObjectConnectionNotPublished,
			// Token: 0x040001A0 RID: 416
			ObjectMessageNotSentNoValidRouteFound,
			// Token: 0x040001A1 RID: 417
			ObjectMessageResolvedReplicaNotReady,
			// Token: 0x040001A2 RID: 418
			ObjectWrongNetId,
			// Token: 0x040001A3 RID: 419
			ObjectMessageTransmissionFull,
			// Token: 0x040001A4 RID: 420
			ObjectObserverAlreadyRemoved,
			// Token: 0x040001A5 RID: 421
			SessionRefIdInvalid,
			// Token: 0x040001A6 RID: 422
			SessionHostMigrationDisabled,
			// Token: 0x040001A7 RID: 423
			SessionHostMigrationInvalidTopology,
			// Token: 0x040001A8 RID: 424
			SessionKicked,
			// Token: 0x040001A9 RID: 425
			SessionDissolved,
			// Token: 0x040001AA RID: 426
			SessionRefused,
			// Token: 0x040001AB RID: 427
			SessionHostGone,
			// Token: 0x040001AC RID: 428
			SessionPeerGone,
			// Token: 0x040001AD RID: 429
			SessionInvalid,
			// Token: 0x040001AE RID: 430
			SessionRefIdPoolExhausted,
			// Token: 0x040001AF RID: 431
			SessionPeerNotFound,
			// Token: 0x040001B0 RID: 432
			SessionMaxSession,
			// Token: 0x040001B1 RID: 433
			SessionVersionMismatch,
			// Token: 0x040001B2 RID: 434
			SessionExists,
			// Token: 0x040001B3 RID: 435
			SessionLeaving,
			// Token: 0x040001B4 RID: 436
			SessionHostMigrationRunning,
			// Token: 0x040001B5 RID: 437
			SessionRefIdError,
			// Token: 0x040001B6 RID: 438
			GroupRefIdInvalid,
			// Token: 0x040001B7 RID: 439
			GroupRefIdPoolExhausted,
			// Token: 0x040001B8 RID: 440
			GroupRequestDuplicated,
			// Token: 0x040001B9 RID: 441
			GroupPeerAlreadyExists,
			// Token: 0x040001BA RID: 442
			GroupVersionMismatch,
			// Token: 0x040001BB RID: 443
			GroupPeerIdPoolExhausted,
			// Token: 0x040001BC RID: 444
			GroupPeerIdInvalid,
			// Token: 0x040001BD RID: 445
			GroupRequestNotFound,
			// Token: 0x040001BE RID: 446
			GroupPeerDescriptorInvalid,
			// Token: 0x040001BF RID: 447
			GroupDeleted,
			// Token: 0x040001C0 RID: 448
			GroupMaxPeerReached,
			// Token: 0x040001C1 RID: 449
			GroupDescriptorInvalid,
			// Token: 0x040001C2 RID: 450
			GroupRefIdError,
			// Token: 0x040001C3 RID: 451
			GroupAddPeerRefused,
			// Token: 0x040001C4 RID: 452
			AudioHardwareError,
			// Token: 0x040001C5 RID: 453
			NoValidLocalUser,
			// Token: 0x040001C6 RID: 454
			Link_Fail,
			// Token: 0x040001C7 RID: 455
			Link_ResolveFail,
			// Token: 0x040001C8 RID: 456
			Link_ResolveTimeout,
			// Token: 0x040001C9 RID: 457
			Link_ReachFail,
			// Token: 0x040001CA RID: 458
			Link_ReachRefused,
			// Token: 0x040001CB RID: 459
			Link_Drop,
			// Token: 0x040001CC RID: 460
			Link_Disconnected,
			// Token: 0x040001CD RID: 461
			Connection_Fail,
			// Token: 0x040001CE RID: 462
			Connection_RequestFail,
			// Token: 0x040001CF RID: 463
			Connection_ReachFail,
			// Token: 0x040001D0 RID: 464
			Connection_CipherFail,
			// Token: 0x040001D1 RID: 465
			Connection_AuthenticationFail,
			// Token: 0x040001D2 RID: 466
			Connection_ReachRefused,
			// Token: 0x040001D3 RID: 467
			Connection_Drop,
			// Token: 0x040001D4 RID: 468
			Connection_Disconnection,
			// Token: 0x040001D5 RID: 469
			Connection_Timeout,
			// Token: 0x040001D6 RID: 470
			TransportColorPoolExhausted,
			// Token: 0x040001D7 RID: 471
			PunchClientNotificationHandlerAlreadyRegistered,
			// Token: 0x040001D8 RID: 472
			PunchClientNotificationHandlerNotRegistered,
			// Token: 0x040001D9 RID: 473
			PunchClientMpePacketHandlerAlreadyRegistered,
			// Token: 0x040001DA RID: 474
			PunchClientMpePacketHandlerNotRegistered,
			// Token: 0x040001DB RID: 475
			PunchTraversalMpeAddressAlreadyRegistered,
			// Token: 0x040001DC RID: 476
			PunchTraversalMpeAddressNotRegistered,
			// Token: 0x040001DD RID: 477
			PunchTraversalAlreadyUsingServer,
			// Token: 0x040001DE RID: 478
			PunchTraversalNoServerInUse,
			// Token: 0x040001DF RID: 479
			PunchTraversalConnectionLost,
			// Token: 0x040001E0 RID: 480
			PunchTraversalConnectionTimeout,
			// Token: 0x040001E1 RID: 481
			PunchTraversalUrlNotResolved,
			// Token: 0x040001E2 RID: 482
			PunchServerDetect1Unreachable,
			// Token: 0x040001E3 RID: 483
			PunchServerDetect2Unreachable,
			// Token: 0x040001E4 RID: 484
			PunchDetectDetectAlreadyInProgress,
			// Token: 0x040001E5 RID: 485
			PunchDetectMap1Failed,
			// Token: 0x040001E6 RID: 486
			PunchDetectTimedOut,
			// Token: 0x040001E7 RID: 487
			PunchDetectAlreadyEnabled,
			// Token: 0x040001E8 RID: 488
			PunchUpnpNoDeviceFound,
			// Token: 0x040001E9 RID: 489
			PunchUpnpThreadNotStarted,
			// Token: 0x040001EA RID: 490
			PunchUpnpCancelInProgress,
			// Token: 0x040001EB RID: 491
			PunchUpnpCommandAlreadyInProgress,
			// Token: 0x040001EC RID: 492
			Audio_DeviceNotFound,
			// Token: 0x040001ED RID: 493
			Audio_DeviceNotConnected,
			// Token: 0x040001EE RID: 494
			Audio_DeviceAlreadyInUse,
			// Token: 0x040001EF RID: 495
			Audio_DeviceNotSupported,
			// Token: 0x040001F0 RID: 496
			Audio_NotAuthorized,
			// Token: 0x040001F1 RID: 497
			Audio_WaitingForAuthorization,
			// Token: 0x040001F2 RID: 498
			Video_DeviceNotFound,
			// Token: 0x040001F3 RID: 499
			Video_DeviceNotConnected,
			// Token: 0x040001F4 RID: 500
			Video_DeviceAlreadyInUse,
			// Token: 0x040001F5 RID: 501
			Video_DeviceNotSupported,
			// Token: 0x040001F6 RID: 502
			Video_CodecNotSupported,
			// Token: 0x040001F7 RID: 503
			Video_PixelFormatNotSupported,
			// Token: 0x040001F8 RID: 504
			Video_ResolutionNotSupported,
			// Token: 0x040001F9 RID: 505
			Video_FramerateNotSupported,
			// Token: 0x040001FA RID: 506
			Video_NotAuthorized,
			// Token: 0x040001FB RID: 507
			Video_WaitingForAuthorization,
			// Token: 0x040001FC RID: 508
			Video_EncodeFailed,
			// Token: 0x040001FD RID: 509
			Video_DecodeFailed,
			// Token: 0x040001FE RID: 510
			Video_FrameDropped,
			// Token: 0x040001FF RID: 511
			Services_USError,
			// Token: 0x04000200 RID: 512
			Services_SMMError,
			// Token: 0x04000201 RID: 513
			Services_PunchError,
			// Token: 0x04000202 RID: 514
			Services_LocalAddressError,
			// Token: 0x04000203 RID: 515
			Registry_SessionTypeNotFound,
			// Token: 0x04000204 RID: 516
			Registry_SessionTypeCommitted,
			// Token: 0x04000205 RID: 517
			Registry_SessionTypeNotCommitted,
			// Token: 0x04000206 RID: 518
			Registry_GroupTypeNotCommitted,
			// Token: 0x04000207 RID: 519
			EndMarker
		}

		// Token: 0x020000AA RID: 170
		private static class Native
		{
			// Token: 0x0600034C RID: 844
			[DllImport("Storm", CharSet = CharSet.Ansi)]
			public static extern byte GetLastResult(ref int errorCode, IntPtr feedString, ref int feedStringLength, IntPtr debugFile, ref int debugFileLength, ref uint debugLine);
		}
	}
}
