
using Foundation;

namespace Microsoft.PlatformChannels
{
	public partial class Channel
	{
		internal PlatformObject ToPlatformObject(object obj)
		{
			if (obj is null)
				return null;

			var objType = obj.GetType();
			
			try {
				if (obj is PlatformObject pobj)
					return pobj;
			} catch { }

			if (objType == typeof(string))
				return new NSString((string)obj);
			else if (objType == typeof(int))
				return new NSNumber((int)obj);
			else if (objType == typeof(double))
				return new NSNumber((double)obj);
			else if (objType == typeof(float))
				return new NSNumber((float)obj);
			else if (objType == typeof(bool))
				return new NSNumber((bool)obj);
			else if (objType == typeof(long))
				return new NSNumber((long)obj);
			else if (objType == typeof(short))
#pragma warning disable CS0618 // Type or member is obsolete
                return new NSNumber((short)obj);
#pragma warning restore CS0618 // Type or member is obsolete

            throw new NotSupportedException();
		}

		internal object ToDotNetObject(PlatformObject obj)
		{
			return obj;
		}
    }
}