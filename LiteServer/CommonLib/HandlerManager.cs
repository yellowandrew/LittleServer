using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection;

namespace CommonLib
{
    public class HandlerManager
    {
        
       public Dictionary<UInt32, Tuple<MethodInfo, Handler>> actions
            = new Dictionary<UInt32, Tuple<MethodInfo, Handler>>();
        public HandlerManager(Assembly asm = null)
        {

            if (asm == null)
            {
                SetHandler(Assembly.GetEntryAssembly());
            }
            else {
                SetHandler(asm);
            }
            
            
        }

        void SetHandler(Assembly asm) {
            var handleClasses = asm.GetTypes().Where(x => x.IsSubclassOf(typeof(Handler)));
            foreach (var cs in handleClasses)
            {
                var handler = Activator.CreateInstance(cs) as Handler;

                foreach (var meth in handler.GetType().GetMethods())
                {
                    var attrib = meth.GetCustomAttribute<PackageHandleAttribute>();
                    if (attrib == null) continue;
                    actions.Add(attrib.id, new Tuple<MethodInfo, Handler>(meth, handler));
                }
            }
            foreach (var item in handleClasses)
            {
                Logger.Info($"Package Handler Class->{item.Name}");
            }
        }

        public void Handle(object connection, IParser parser, byte[] buffer, EndPoint endpoint=null) {
            var package = parser.ReadPackageFromBuffer(buffer);
            if (actions.TryGetValue(package.id, out var item))
            {
                var (m, h) = item;
                m.Invoke(h, new object[] { connection, package, parser,endpoint });
            }

        }
        
    }
}
