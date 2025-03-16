using System;
using System.Runtime.InteropServices;
using StormDotNet.Extensions;

namespace StormDotNet.Echo
{
	// Token: 0x020000AD RID: 173
	public sealed class SEngineStartUpParams
	{
		// Token: 0x06000352 RID: 850 RVA: 0x0000AD99 File Offset: 0x00008F99
		internal static int GetNativeDataSize()
		{
			return Marshal.SizeOf(typeof(SEngineStartUpParams.Native));
		}

		// Token: 0x1700003F RID: 63
		// (get) Token: 0x06000353 RID: 851 RVA: 0x0000ADAA File Offset: 0x00008FAA
		// (set) Token: 0x06000354 RID: 852 RVA: 0x0000ADB8 File Offset: 0x00008FB8
		public unsafe ushort MainSocketPort
		{
			get
			{
				return *this.startupParams.mainSocketPort;
			}
			set
			{
				*this.startupParams.mainSocketPort = value;
			}
		}

		// Token: 0x17000040 RID: 64
		// (get) Token: 0x06000355 RID: 853 RVA: 0x0000ADC7 File Offset: 0x00008FC7
		// (set) Token: 0x06000356 RID: 854 RVA: 0x0000ADD5 File Offset: 0x00008FD5
		public unsafe UIntPtr MainSocketPortRetryCount
		{
			get
			{
				return *this.startupParams.mainSocketPortRetryCount;
			}
			set
			{
				*this.startupParams.mainSocketPortRetryCount = value;
			}
		}

		// Token: 0x17000041 RID: 65
		// (set) Token: 0x06000357 RID: 855 RVA: 0x0000ADE4 File Offset: 0x00008FE4
		public unsafe ushort PublicPort
		{
			set
			{
				*this.startupParams.publicPort = value;
			}
		}

		// Token: 0x17000042 RID: 66
		// (get) Token: 0x06000358 RID: 856 RVA: 0x0000ADF4 File Offset: 0x00008FF4
		public string PublicAddress
		{
			get
			{
				string text = new string(this.startupParams.publicAddress, 0, 64);
				int num = text.IndexOf('\0', 0, 64);
				return text.Substring(0, num);
			}
		}

		// Token: 0x17000043 RID: 67
		// (set) Token: 0x06000359 RID: 857 RVA: 0x0000AE26 File Offset: 0x00009026
		public unsafe ushort LanMatchmakingDiscoveryPort
		{
			set
			{
				*this.startupParams.lanMatchmakingDiscoveryPort = value;
			}
		}

		// Token: 0x17000044 RID: 68
		// (set) Token: 0x0600035A RID: 858 RVA: 0x0000AE35 File Offset: 0x00009035
		public bool TimeoutEnabled
		{
			set
			{
				MarshalEx.WriteBoolean(this.startupParams.timeoutEnabled, value, this.startupParams.sizeOfBool);
			}
		}

		// Token: 0x0600035B RID: 859 RVA: 0x0000AE54 File Offset: 0x00009054
		internal SEngineStartUpParams(IntPtr handle, IntPtr startupParams)
		{
			if (handle == IntPtr.Zero)
			{
				throw new ArgumentNullException("handle");
			}
			if (startupParams == IntPtr.Zero)
			{
				throw new ArgumentNullException("startupParams");
			}
			this.handle = handle;
			this.startupParams = (SEngineStartUpParams.Native)Marshal.PtrToStructure(startupParams, typeof(SEngineStartUpParams.Native));
		}

		// Token: 0x0400020F RID: 527
		private IntPtr handle;

		// Token: 0x04000210 RID: 528
		private SEngineStartUpParams.Native startupParams;

		// Token: 0x020000AE RID: 174
		[StructLayout(0, Pack = 1)]
		private struct Native
		{
			// Token: 0x04000211 RID: 529
			public int sizeOfBool;

			// Token: 0x04000212 RID: 530
			public unsafe ushort* mainSocketPort;

			// Token: 0x04000213 RID: 531
			public unsafe UIntPtr* mainSocketPortRetryCount;

			// Token: 0x04000214 RID: 532
			public unsafe sbyte* localAddress;

			// Token: 0x04000215 RID: 533
			public unsafe UIntPtr* mainSocketConcurrentThreads;

			// Token: 0x04000216 RID: 534
			public unsafe ushort* publicPort;

			// Token: 0x04000217 RID: 535
			public unsafe sbyte* publicAddress;

			// Token: 0x04000218 RID: 536
			public unsafe ushort* queryPort;

			// Token: 0x04000219 RID: 537
			public unsafe sbyte* queryAddress;

			// Token: 0x0400021A RID: 538
			public IntPtr networkEnabled;

			// Token: 0x0400021B RID: 539
			public IntPtr lanEnabled;

			// Token: 0x0400021C RID: 540
			public IntPtr natEnabled;

			// Token: 0x0400021D RID: 541
			public unsafe byte* peerGUID;

			// Token: 0x0400021E RID: 542
			public unsafe UIntPtr* peerGUIDSize;

			// Token: 0x0400021F RID: 543
			public unsafe uint* sessionJoinTimeout;

			// Token: 0x04000220 RID: 544
			public unsafe uint* sessionRejoinDelay;

			// Token: 0x04000221 RID: 545
			public unsafe uint* sessionRejoinTimeout;

			// Token: 0x04000222 RID: 546
			public unsafe uint* sessionHostMigrationTimeout;

			// Token: 0x04000223 RID: 547
			public unsafe uint* groupAddPeerRequestTimeout;

			// Token: 0x04000224 RID: 548
			public IntPtr lanMatchmakingReuseAddress;

			// Token: 0x04000225 RID: 549
			public unsafe ushort* lanMatchmakingPort;

			// Token: 0x04000226 RID: 550
			public IntPtr lanMatchmakingDiscoveryOnLoopbackEnable;

			// Token: 0x04000227 RID: 551
			public unsafe ushort* lanMatchmakingDiscoveryPort;

			// Token: 0x04000228 RID: 552
			public unsafe sbyte* localAddressForLANMatchmaking;

			// Token: 0x04000229 RID: 553
			public unsafe sbyte* lanMatchmakingDiscoveryAddress;

			// Token: 0x0400022A RID: 554
			public unsafe sbyte* routerAddress;

			// Token: 0x0400022B RID: 555
			public unsafe uint* bandwidthLimitInitialConnection;

			// Token: 0x0400022C RID: 556
			public unsafe float* streamPackerPeerChannelPacketLimit;

			// Token: 0x0400022D RID: 557
			public unsafe uint* streamPackerTimeSlice;

			// Token: 0x0400022E RID: 558
			public IntPtr streamPackerTimeSliceEnable;

			// Token: 0x0400022F RID: 559
			public unsafe UIntPtr* updateConcurrency;

			// Token: 0x04000230 RID: 560
			public IntPtr timeoutEnabled;

			// Token: 0x04000231 RID: 561
			public IntPtr voiceChatEnabled;

			// Token: 0x04000232 RID: 562
			public IntPtr voiceChatRingBufferOutputEnabled;

			// Token: 0x04000233 RID: 563
			public unsafe uint* voiceMegaPacketPeriod;

			// Token: 0x04000234 RID: 564
			public unsafe uint* voicePacketTimeToLive;

			// Token: 0x04000235 RID: 565
			public unsafe uint* voiceXBoxChatSessionAffinityMask;

			// Token: 0x04000236 RID: 566
			public unsafe int* voiceThreadPriority;

			// Token: 0x04000237 RID: 567
			public unsafe int* voiceCaptureThreadPriority;

			// Token: 0x04000238 RID: 568
			public unsafe int* voicePlaybackThreadPriority;

			// Token: 0x04000239 RID: 569
			public IntPtr voiceSphinxModelPath;

			// Token: 0x0400023A RID: 570
			public unsafe int* voiceSphinxThreadPriority;

			// Token: 0x0400023B RID: 571
			public IntPtr signingCertificateNameOrPath;

			// Token: 0x0400023C RID: 572
			public IntPtr signingCertificatePassword;

			// Token: 0x0400023D RID: 573
			public IntPtr signingPublicKeyBase64Encoded;

			// Token: 0x0400023E RID: 574
			public IntPtr processKeyAgreementInPPLXTask;

			// Token: 0x0400023F RID: 575
			public unsafe uint* simulationCameraStatePeriodInMs;

			// Token: 0x04000240 RID: 576
			public unsafe uint* simulationCameraReplicaOperationPeriodInMs;

			// Token: 0x04000241 RID: 577
			public IntPtr emulatorEnable;

			// Token: 0x04000242 RID: 578
			public unsafe uint* emulatorBufferSize;

			// Token: 0x04000243 RID: 579
			public unsafe uint* pingTimeOut;

			// Token: 0x04000244 RID: 580
			public unsafe uint* pingRetryInterval;

			// Token: 0x04000245 RID: 581
			public unsafe uint* objectServiceMainUpdateRuntimeSlice;

			// Token: 0x04000246 RID: 582
			public unsafe uint* objectServiceMainUpdateMinPeriod;

			// Token: 0x04000247 RID: 583
			public unsafe uint* objectServiceMementoSampleMinPeriod;

			// Token: 0x04000248 RID: 584
			public unsafe uint* objectMessageTransmissionThreshold;

			// Token: 0x04000249 RID: 585
			public IntPtr dropObjectUnreliableMessagesUponTransmissonThreshold;

			// Token: 0x0400024A RID: 586
			public unsafe uint* maxMementoSizeInBytes;

			// Token: 0x0400024B RID: 587
			public IntPtr objectReplicaSynchronizedBeforeBecomingMaster;

			// Token: 0x0400024C RID: 588
			public unsafe ushort* objectNetIdPoolSize;

			// Token: 0x0400024D RID: 589
			public unsafe uint* peerMessageTransmissionThreshold;

			// Token: 0x0400024E RID: 590
			public IntPtr dropPeerChannelUnreliableMessagesUponTransmissonThreshold;

			// Token: 0x0400024F RID: 591
			public unsafe uint* connectionRequestingTimeout;

			// Token: 0x04000250 RID: 592
			public unsafe uint* connectionRequestingTimeout_SDA;

			// Token: 0x04000251 RID: 593
			public unsafe uint* connectionInactiveTimeout;

			// Token: 0x04000252 RID: 594
			public unsafe uint* connectionDropTimeout;

			// Token: 0x04000253 RID: 595
			public unsafe uint* connectionInactivePeriod;

			// Token: 0x04000254 RID: 596
			public unsafe uint* connectionHeartbeatPeriod;

			// Token: 0x04000255 RID: 597
			public unsafe uint* connectionReachAttempt;

			// Token: 0x04000256 RID: 598
			public unsafe uint* linkPendingTimeout;

			// Token: 0x04000257 RID: 599
			public unsafe uint* linkResolvingTimeout;

			// Token: 0x04000258 RID: 600
			public unsafe uint* linkResolving_SDA_Timeout;

			// Token: 0x04000259 RID: 601
			public unsafe uint* linkInactiveTimeout;

			// Token: 0x0400025A RID: 602
			public unsafe uint* linkDropTimeout;

			// Token: 0x0400025B RID: 603
			public unsafe uint* linkInactivePeriod;

			// Token: 0x0400025C RID: 604
			public unsafe uint* linkHeartbeatPeriod;

			// Token: 0x0400025D RID: 605
			public unsafe uint* linkReachAttempt;

			// Token: 0x0400025E RID: 606
			public unsafe uint* linkRelayTimeoutCoeff;

			// Token: 0x0400025F RID: 607
			public unsafe uint* relayPokingPeriodInMs;

			// Token: 0x04000260 RID: 608
			public IntPtr autoCommit;

			// Token: 0x04000261 RID: 609
			public unsafe uint* rxRuntimeSliceInMs;
		}
	}
}
