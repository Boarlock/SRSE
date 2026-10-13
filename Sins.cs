using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace SRSE
{
    public static class SinsAddresses
    {
        public const int Game_Time = 0xB0D34C;
        public const int Focused_Time = 0xB0D350;
        public const int Total_Frame = 0x656FB0;
        public const int Play_Frame = 0xA22250;
        public const int Simulation_Frame = 0x9F0488;
        public const int Game_Sim_Frame = 0x9F06C8;

        public const int Set_Pause_Function = 0x000F0BC0; // FUN_008f0bc0 
        public const int Set_Game_Speed_Function = 0x000B1B50; // FUN_008f1b50

        public const int Game_Instance_Pointer = 0xA22108; // Base Pointer 0x01262108
        public const int Game_Pause_Offset = 0x3ED0; // Pause field offset within Iron::Game
        public const int Game_Speed_Offset = 0x3EE8; // Speed field offset within Iron::Game

        public const int Left_Click_Up = 0xAFE158; 
        public const int Right_Click_Up = 0xAFE15C; 

        public const int Main_Menu_Flag = 0x9F04B0;
        public const int Game_Flag = 0x9F7C80;
        public const int Game_Over_Flag = 0x6980DE;
        public const int Window_Width = 0x644A90; // In pixels
        public const int Window_Height = 0x644C98; // In pixels
        public const int Display_Mode = 0x644888; // 0 Fullscreen, 1 BorderedFullscreen, 2 Windowed

        public const int Cursor_X_Coord = 0xAFE148; // In pixels
        public const int Cursor_Y_Coord = 0xAFE14C; // In pixels, 0 starts at top and moves down

        public const int Camera_Object_Pointer = 0x9F0D70;
        public const int Camera_Object_Offset = 0x2AC; 
        public const int Camera_Timer_Offset = 0x15C;
        public const int Camera_Move_Horizontal_Offset = 0x160;
        public const int Camera_Move_Vertical_Offset = 0x164;
        public const int Camera_Zoom_Velocity_Offset = 0x174;
        public const int Camera_Zoom_Offset = 0x17C;
        public const int Camera_Pitch_Offset = 0x190;
        public const int Camera_Rotation_Offset = 0x194;
    }

    // PTRTOSTRUCTURE WITH TYPEOF(FLOAT) MUST BE USED FOR THIS .NET FRAMEWORK, PTRTOSTRUCTURE<FLOAT> DOES NOT WORK!!
    public static class SinsGetters
    {
        public static float GameTime => (float)Marshal.PtrToStructure(Sins.Address(SinsAddresses.Game_Time), typeof(float));
        public static float FocusedTime => (float)Marshal.PtrToStructure(Sins.Address(SinsAddresses.Focused_Time), typeof(float));
        public static int TotalFrame => Marshal.ReadInt32(Sins.Address(SinsAddresses.Total_Frame));
        public static int PlayFrame => Marshal.ReadInt32(Sins.Address(SinsAddresses.Play_Frame));
        public static int SimulationFrame => Marshal.ReadInt32(Sins.Address(SinsAddresses.Simulation_Frame));
        public static int GameSimulationFrame => Marshal.ReadInt32(Sins.Address(SinsAddresses.Game_Sim_Frame));

        public static bool IsLeftClickDown => Marshal.ReadInt32(Sins.Address(SinsAddresses.Left_Click_Up)) == 0;
        public static bool IsRightClickDown => Marshal.ReadInt32(Sins.Address(SinsAddresses.Right_Click_Up)) == 0;

        public static bool IsAtMainMenu => Marshal.ReadInt32(Sins.Address(SinsAddresses.Main_Menu_Flag)) == 0;
        public static bool IsCurPlaying => Marshal.ReadByte(Sins.Address(SinsAddresses.Game_Flag)) == 0;
        public static bool IsGameOver => Marshal.ReadByte(Sins.Address(SinsAddresses.Game_Over_Flag)) == 1;

        public static int WindowWidth => Marshal.ReadInt32(Sins.Address(SinsAddresses.Window_Width));
        public static int WindowHeight => Marshal.ReadInt32(Sins.Address(SinsAddresses.Window_Height));
        public static int DisplayMode => Marshal.ReadInt32(Sins.Address(SinsAddresses.Display_Mode));

        public static int CursorX => Marshal.ReadInt32(Sins.Address(SinsAddresses.Cursor_X_Coord));
        public static int CursorY => Marshal.ReadInt32(Sins.Address(SinsAddresses.Cursor_Y_Coord));
    }

    public static class SinsGame
    {
        static SinsGame()
        {
            EntryPoint.SinsTick += Initialize;
        }

        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        private delegate void SetPauseStateDelegate(byte bPause, byte bShowNotifications);
        private static SetPauseStateDelegate setPauseState;

        // Confirm the actual calling convention before relying on this delegate.
        [UnmanagedFunctionPointer(CallingConvention.FastCall)]
        private delegate void SetGameSpeedDelegate(byte bSpeed);
        private static SetGameSpeedDelegate setGameSpeed;

        private static IntPtr GameInstanceAddress = IntPtr.Zero;
        private static bool initialized = false; // For first initialization

        private static IntPtr GameSpeedAddress = IntPtr.Zero;
        private static IntPtr GamePauseAddress = IntPtr.Zero;
        
        public static void Initialize()
        {
            int instanceAdress = Marshal.ReadInt32(Sins.Address(SinsAddresses.Game_Instance_Pointer));

            // Set pause and set game speed functions have no dependency

            if (setGameSpeed == null)
            {
                IntPtr speedFunctionAddress = Sins.Address(SinsAddresses.Set_Game_Speed_Function);

                if (speedFunctionAddress != IntPtr.Zero)
                    setGameSpeed = (SetGameSpeedDelegate)Marshal.GetDelegateForFunctionPointer(
                        speedFunctionAddress, typeof(SetGameSpeedDelegate));
            }

            if (setPauseState == null)
            {
                IntPtr pauseFunctionAddress = Sins.Address(SinsAddresses.Set_Pause_Function);

                if (pauseFunctionAddress != IntPtr.Zero)
                    setPauseState = (SetPauseStateDelegate)Marshal.GetDelegateForFunctionPointer(
                        pauseFunctionAddress, typeof(SetPauseStateDelegate));
            }

            if (GameInstanceAddress == IntPtr.Zero ||
                GameInstanceAddress.ToInt32() != instanceAdress ||
                !initialized)
            {
                if (instanceAdress != 0)
                {
                    GameInstanceAddress = new IntPtr(instanceAdress);
                    initialized = true;
                }
                else
                {
                    GameInstanceAddress = IntPtr.Zero;
                    GameSpeedAddress = IntPtr.Zero;
                    GamePauseAddress = IntPtr.Zero;
                    return;
                }
            }
            else
            {
                return;
            }

            // Pause and Game speed fields are independent of each other but depend on game instance

            IntPtr gameSpeedAddress = new IntPtr(GameInstanceAddress.ToInt32() + SinsAddresses.Game_Speed_Offset);
            IntPtr gamePauseAddress = new IntPtr(GameInstanceAddress.ToInt32() + SinsAddresses.Game_Pause_Offset);

            if (GameSpeedAddress == IntPtr.Zero ||
                GameSpeedAddress != gameSpeedAddress)
            {
                if (gameSpeedAddress != IntPtr.Zero)
                {
                    GameSpeedAddress = gameSpeedAddress;
                }
                else
                {
                    GameSpeedAddress = IntPtr.Zero;
                }
            }

            if (GamePauseAddress == IntPtr.Zero ||
                GamePauseAddress != gamePauseAddress)
            {
                if (gamePauseAddress != IntPtr.Zero)
                {
                    GamePauseAddress = gamePauseAddress;
                }
                else
                {
                    GamePauseAddress = IntPtr.Zero;
                }
            }

        }

        public static int Pause
        {
            get
            {
                if (GamePauseAddress == IntPtr.Zero)
                    return -5;

                return Marshal.ReadInt32(GamePauseAddress);
            }
        }

        public static bool SetPause(int pause, int showNotification)
        {
            if (pause != 1 &&
                pause != 0)
                return false;

            int currentPause = Pause;
            if (currentPause == pause)
                return false;

            if (showNotification == 0 &&
                GamePauseAddress != IntPtr.Zero)
            {
                Marshal.WriteInt32(GamePauseAddress, pause);
                return true;
            }

            if (setPauseState == null)
                return false;

            setPauseState((byte)(pause), (byte)(showNotification));

            return true;
        }

        public static int GameSpeed
        {
            get
            {
                if (GameSpeedAddress == IntPtr.Zero)
                    return -5;

                return Marshal.ReadInt32(GameSpeedAddress);
            }
        }

        public static bool SetSpeed(int speed, int showNotification)
        {
            if (speed != 1 &&
                speed != 2 &&
                speed != 4 &&
                speed != 6 &&
                speed != 8)
                return false;

            int currentSpeed = GameSpeed;
            if (currentSpeed == speed)
                return false;

            if (showNotification == 0 &&
                GameSpeedAddress != IntPtr.Zero)
            {
                Marshal.WriteInt32(GameSpeedAddress, speed);
                return true;
            }

            if (setGameSpeed == null)
                return false;

            setGameSpeed((byte)(speed));

            return true;
        }
    }

    public static class SinsCamera
    {
        static SinsCamera()
        {
            EntryPoint.SinsTick += Initialize;
        }

        private static IntPtr CameraObjectParent = IntPtr.Zero;
        private static bool initialized; // For first initialization

        private static IntPtr CameraObject = IntPtr.Zero;
        private static IntPtr TimerAddress = IntPtr.Zero;
        private static IntPtr HorizontalMotionAddress = IntPtr.Zero;
        private static IntPtr VerticalMotionAddress = IntPtr.Zero;
        private static IntPtr ZoomVelocityAddress = IntPtr.Zero;
        private static IntPtr ZoomAddress = IntPtr.Zero;
        private static IntPtr PitchAddress = IntPtr.Zero;
        private static IntPtr RotationAddress = IntPtr.Zero;

        private static IntPtr SelectedObject = IntPtr.Zero;

        public static void Initialize()
        {
            int cameraObjectParent = Marshal.ReadInt32(Sins.Address(SinsAddresses.Camera_Object_Pointer));

            if (CameraObjectParent == IntPtr.Zero ||
                CameraObjectParent.ToInt32() != cameraObjectParent ||
                !initialized)
            {
                if (cameraObjectParent != 0)
                {
                    CameraObjectParent = new IntPtr(cameraObjectParent);
                    CameraObject = new IntPtr(cameraObjectParent + SinsAddresses.Camera_Object_Offset);
                    initialized = true;
                }
                else
                {
                    CameraObjectParent = IntPtr.Zero;
                    CameraObject = IntPtr.Zero;
                    TimerAddress = IntPtr.Zero;
                    HorizontalMotionAddress = IntPtr.Zero;
                    VerticalMotionAddress = IntPtr.Zero;
                    ZoomVelocityAddress = IntPtr.Zero;
                    ZoomAddress = IntPtr.Zero;
                    PitchAddress = IntPtr.Zero;
                    RotationAddress = IntPtr.Zero;

                    SelectedObject = IntPtr.Zero;
                }
            }
            else
            {
                return;
            }

            // If Parent is reset everything else needs to be reset as well
            int cameraObjectAddress = CameraObject.ToInt32();

            TimerAddress = new IntPtr(cameraObjectAddress + SinsAddresses.Camera_Timer_Offset);
            HorizontalMotionAddress = new IntPtr(cameraObjectAddress + SinsAddresses.Camera_Move_Horizontal_Offset);
            VerticalMotionAddress = new IntPtr(cameraObjectAddress + SinsAddresses.Camera_Move_Vertical_Offset);
            ZoomVelocityAddress = new IntPtr(cameraObjectAddress + SinsAddresses.Camera_Zoom_Velocity_Offset);
            ZoomAddress = new IntPtr(cameraObjectAddress + SinsAddresses.Camera_Zoom_Offset);
            PitchAddress = new IntPtr(cameraObjectAddress + SinsAddresses.Camera_Pitch_Offset);
            RotationAddress = new IntPtr(cameraObjectAddress + SinsAddresses.Camera_Rotation_Offset);

            // Read pointer at camera + 0x6BC
            IntPtr candidate = new IntPtr(Marshal.ReadInt32(CameraObject, 0x6BC));

            // Read pointer at candidate + 0x08
            IntPtr hoverState = new IntPtr(Marshal.ReadInt32(candidate, 0x08));

            // Calculate hover string address: hoverState + 0x1070
            SelectedObject = new IntPtr(hoverState.ToInt32() + 0x1070);
        }

        /*public static float Timer
        {
            get
            {
                IntPtr address = new IntPtr(CameraObject.ToInt32() + 0x15C);
                return (float)Marshal.PtrToStructure(address, typeof(float));
            }
        }*/

        public static float HorizontalMotion
        {
            get
            {
                if (CameraObject == IntPtr.Zero)
                    return -5f; // Normally at 0

                return (float)Marshal.PtrToStructure(HorizontalMotionAddress, typeof(float));
            }
            set
            {
                if (CameraObject == IntPtr.Zero)
                    return;

                Marshal.StructureToPtr(value, HorizontalMotionAddress, false);
            }
        }

        public static float VerticalMotion
        {
            get
            {
                if (CameraObject == IntPtr.Zero)
                    return -5f; // Normally at 0

                return (float)Marshal.PtrToStructure(VerticalMotionAddress, typeof(float));
            }
            set
            {
                if (CameraObject == IntPtr.Zero)
                    return;

                Marshal.StructureToPtr(value, VerticalMotionAddress, false);
            }
        }

        public static float ZoomVelocity
        {
            get
            {
                if (CameraObject == IntPtr.Zero)
                    return -5f; // Normally at 0

                return (float)Marshal.PtrToStructure(ZoomVelocityAddress, typeof(float));
            }
            set
            {
                if (CameraObject == IntPtr.Zero)
                    return;

                Marshal.StructureToPtr(value, ZoomVelocityAddress, false);
            }
        }

        public static float Pitch
        {
            get
            {
                if (CameraObject == IntPtr.Zero)
                    return 0f; // Zero is valid for pitch but very unlikely

                return (float)Marshal.PtrToStructure(PitchAddress, typeof(float));
            }
            set
            {
                if (CameraObject == IntPtr.Zero)
                    return;

                value = MathExtensions.Clamp(value, -1.55334f, 1.55334f);

                Marshal.StructureToPtr(value, PitchAddress, false);
            }
        }

        public static float Rotation
        {
            get
            {
                if (CameraObject == IntPtr.Zero)
                    return 0f; // Zero is valid for rotation but very unlikely

                return (float)Marshal.PtrToStructure(RotationAddress, typeof(float));
            }
            set
            {
                if (CameraObject == IntPtr.Zero)
                    return;

                Marshal.StructureToPtr(value, RotationAddress, false);
            }
        }

        public static float Zoom
        {
            get
            {
                if (CameraObject == IntPtr.Zero)
                    return -5f;

                return (float)Marshal.PtrToStructure(ZoomAddress, typeof(float));
            }
            set
            {
                if (CameraObject == IntPtr.Zero)
                    return;

                value = MathExtensions.Clamp(value, 20f, 11222389f);

                Marshal.StructureToPtr(value, ZoomAddress, false);
            }
        }
    }

    public static class Sins
    {
        public static bool initialized = false;

        public static IntPtr BaseAddress { get; private set; }

        public static void Initialize()
        {
            BaseAddress = Process.GetCurrentProcess().MainModule.BaseAddress;

            if (BaseAddress != IntPtr.Zero)
                initialized = true;
        }

        public static IntPtr Address(int rva)
        {
            return new IntPtr(BaseAddress.ToInt32() + rva);
        }
    }
}
