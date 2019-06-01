using System;
using System.Text;
using DG.Tweening;

public class Assert {

    	public static bool Inv(bool condition, params object[] args)
	{
		if (!condition)
		{
			var sb = new StringBuilder("Assertion: ");

			if (args.Length > 0)
			{
				sb.Append(args[0]);
				for (var i = 1; i < args.Length; ++i)
				{
					sb.Append(" ");
					sb.Append(args[i] != null ? args[i].ToString() : "NULL");
				}
			}

			throw new Exception(sb.ToString());
		}
		return condition;
	}

}
