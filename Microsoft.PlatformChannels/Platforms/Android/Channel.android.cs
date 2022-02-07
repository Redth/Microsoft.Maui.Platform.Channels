
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
				return new Java.Lang.String((string)obj);
			else if (objType == typeof(int))
				return new Java.Lang.Integer((int)obj);
			else if (objType == typeof(double))
				return new Java.Lang.Double((double)obj);
			else if (objType == typeof(float))
				return new Java.Lang.Float((float)obj);
			else if (objType == typeof(bool))
				return new Java.Lang.Boolean((bool)obj);
			else if (objType == typeof(long))
				return new Java.Lang.Long((long)obj);
			else if (objType == typeof(short))
#pragma warning disable CS0618 // Type or member is obsolete
                return new Java.Lang.Short((short)obj);
#pragma warning restore CS0618 // Type or member is obsolete

            throw new NotSupportedException();
		}

		internal object ToDotNetObject(PlatformObject obj)
		{
			if (obj is Java.Lang.String jstr)
				return jstr.ToString();
			else if (obj is Java.Lang.Double jdbl)
				return jdbl.DoubleValue();
			else if (obj is Java.Lang.Integer jint)
				return jint.IntValue();
			else if (obj is Java.Lang.Float jflt)
				return jflt.FloatValue();
			else if (obj is Java.Lang.Boolean jbl)
				return jbl.BooleanValue();
			else if (obj is Java.Lang.Long jlng)
				return jlng.LongValue();
			else if (obj is Java.Lang.Short jsht)
				return jsht.ShortValue();
			else if (obj is Java.Lang.Number jnum)
				return jnum.DoubleValue();
			
			return obj;
		}
    }
}