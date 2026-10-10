using EasyHook;
using System;

namespace SRSE
{
    public class EntryPoint : IEntryPoint
    {
        // Public event for each frame tick
        public static event Action SinsTick;

        public EntryPoint(RemoteHooking.IContext content)
        {
            Sins.Initialize();

            SinsGame.Initialize();
            SinsCamera.Initialize();
        }

        public void Run(RemoteHooking.IContext context)
        {
            int lastFrame = -1;

            while (true)
            {
                int currentFrame = SinsGetters.TotalFrame;

                if (currentFrame != lastFrame)
                {
                    lastFrame = currentFrame;
                    SinsTick?.Invoke();
                }

                System.Threading.Thread.Sleep(1);
            }
        }
    }
}
