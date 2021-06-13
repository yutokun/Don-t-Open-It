using System;
using DontOpenIt.Properties;

namespace DontOpenIt
{
    public enum KillMethod
    {
        CloseMainWindow,
        Kill
    }

    public static class KillMethodExtension
    {
        public static string GetReadableString(this KillMethod killMethod)
        {
            switch (killMethod)
            {
                case KillMethod.CloseMainWindow:
                    return Resources.requestToExit;
                case KillMethod.Kill:
                    return Resources.kill;
                default:
                    throw new ArgumentOutOfRangeException(nameof(killMethod), killMethod, null);
            }
        }
    }
}
