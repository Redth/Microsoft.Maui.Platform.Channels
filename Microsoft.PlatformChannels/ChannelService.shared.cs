
#if IOS || MACCATALYST
using System.Runtime.InteropServices;
using Foundation;
#elif ANDROID
using Android.Runtime;
#endif

using PlatformChannelService = Microsoft.PlatformChannels.Platform.ChannelService;
using IPlatformChannelProvider = Microsoft.PlatformChannels.Platform.IChannelProvider;

namespace Microsoft.PlatformChannels;

public partial class ChannelService : IChannelService
{
	public const string DEFAULT_INSTANCE_ID = "DEFAULT";

	ChannelProvider channelProvider;

	public ChannelService(ChannelServiceConfiguration configuration = null)
	{
		Configuration = configuration ?? new ChannelServiceConfiguration();

		channelProvider = new ChannelProvider();

		PlatformProvider = channelProvider;
		ManagedProvider = channelProvider;

		Initialize();
	}

	public IPlatformChannelProvider PlatformProvider
	{
		get => PlatformChannelService.Provider;
		private set => PlatformChannelService.Provider = value;
	}

	public IManagedChannelProvider ManagedProvider { get; private set; }

	public ChannelServiceConfiguration Configuration { get; }

	public Channel GetOrCreateChannel(string channelId, string instanceId)
		=> ManagedProvider.GetManagedInstance(channelId, instanceId ?? DEFAULT_INSTANCE_ID);

	public void DisposeChannel(string channelId, string instanceId)
		=> PlatformProvider.DisposeInstance(channelId, instanceId ?? DEFAULT_INSTANCE_ID);


#if IOS || MACCATALYST
	const string LIBOBJC_DYLIB = "/usr/lib/libobjc.dylib";
	[DllImport(LIBOBJC_DYLIB, EntryPoint = "objc_msgSend")]
	internal extern static void void_objc_msgSend(IntPtr receiver, IntPtr selector);

	internal void Initialize()
	{
		var c = new ObjCRuntime.Class(Configuration.InitClassName);
		var s = new ObjCRuntime.Selector(Configuration.InitMethodName);
		void_objc_msgSend(c.Handle, s.Handle);
	}
#elif ANDROID
	internal void Initialize()
	{
		if (!string.IsNullOrEmpty(Configuration.InitPackageName) &&
			!string.IsNullOrEmpty(Configuration.InitMethodName))
		{
			// Call the java side init which gives the java library a chance to register for its own handlers on the platform side
			var javaStartClassPath =
				string.Join(".", Configuration.InitPackageName, Configuration.InitClassName);
			javaStartClassPath = javaStartClassPath.Replace('.', '/');
			var startClass = JNIEnv.FindClass(javaStartClassPath);
			var method = JNIEnv.GetStaticMethodID(
				startClass,
				Configuration.InitMethodName,
				"()V");
			JNIEnv.CallStaticVoidMethod(startClass, method);
		}
	}
#endif

}