using System;
namespace CommonLib
{
    public abstract class Handler
    {
        protected virtual void HandleUnKownPackage(object data, UInt32 id)
        {
            Logger.Info($"UnKown  Package ......");
        }
    }
}
