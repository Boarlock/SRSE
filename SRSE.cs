using System;

namespace SRSE
{
    public class SRSE
    {
        // SRSE State

        // Needed check to derive all other functions
        public static bool IsInitialized
        {
            get { return Sins.initialized; }
        }

        public struct Coordinate
        {
            public int X;
            public int Y;
        }

        public static Coordinate GetWindowSize()
        {
            Coordinate window = new Coordinate
            {
                X = SinsGetters.WindowWidth,
                Y = SinsGetters.WindowHeight
            };

            return window;
        }

        // PAUSE CONTROLS

        // Inverts pause state no matter current state
        public static void TogglePause()
        {
            int invert = (SinsGame.Pause == 0) ? 1 : 0;

            SinsGame.SetPause(invert, 0);
        }

        public static void Resume()
        {
            SinsGame.SetPause(0, 0);
        }

        public static void Pause()
        {
            SinsGame.SetPause(1, 0);
        }

        // GAME SPEED CONTROLS

        private static readonly int[] SupportedSpeeds = { 1, 2, 4, 6, 8 };
        // Game Speed enum for convenience
        public enum GameSpeed
        {
            Normal = 1,
            Fast = 2,
            Super = 4,
            Ultra = 6,
            Max = 8,
            Unknown

        }

        public static GameSpeed GetSpeed()
        {
            int currentSpeed = SinsGame.GameSpeed;

            for (int i = 0; i < SupportedSpeeds.Length; i++)
                if (currentSpeed == SupportedSpeeds[i])
                    return (GameSpeed)currentSpeed;

            return GameSpeed.Unknown;
        }

        public static void SetSpeed(GameSpeed speed)
        {
            if (speed == GameSpeed.Unknown)
                return;

            SinsGame.SetSpeed((int)speed, 0);
        }

        public static void SpeedUp()
        {
            int currentSpeed = SinsGame.GameSpeed;

            for (int i = 0; i < SupportedSpeeds.Length - 1; i++)
            {
                if (SupportedSpeeds[i] == currentSpeed)
                {
                    SinsGame.SetSpeed(SupportedSpeeds[i + 1], 0);
                    return;
                }
            }
        }

        public static void SlowDown()
        {
            int currentSpeed = SinsGame.GameSpeed;

            for (int i = 1; i < SupportedSpeeds.Length; i++)
            {
                if (SupportedSpeeds[i] == currentSpeed)
                {
                    SinsGame.SetSpeed(SupportedSpeeds[i - 1], 0);
                    return;
                }
            }
        }

        public static void ResetSpeed()
        {
            SinsGame.SetSpeed(1, 0);
        }

        // GAME STATE CONTROLS

        // Game State enum for convenience
        public enum GameState
        {
            MainMenu,
            Playing,
            GameOver,
            Unknown
        }

        public static GameState CurrentGameState()
        {
            if (SinsGetters.IsAtMainMenu)
            {
                return GameState.MainMenu;
            }

            else if (SinsGetters.IsCurPlaying)
            {
                return GameState.Playing;
            }

            else if (SinsGetters.IsGameOver)
            {
                return GameState.GameOver;
            }

            return GameState.Unknown;
        }

        // INPUT CONTROLS

        public enum MouseButton
        {
            LeftMouseButton,
            RightMouseButton
        }

        public static bool IsMouseButtonDown(MouseButton button)
        {
            switch (button)
            {
                case MouseButton.LeftMouseButton:
                    return SinsGetters.IsLeftClickDown;

                case MouseButton.RightMouseButton:
                    return SinsGetters.IsRightClickDown;

                default:
                    return false;
            }
        }

        public static Coordinate CursorPosition(bool invertYAxis) // Game records Y 0 at the top
        {
            Coordinate cursorPos = new Coordinate
            {
                X = SinsGetters.CursorX
            };

            int Y = SinsGetters.CursorY;

            if (!invertYAxis)
                cursorPos.Y = Y;
            else
                cursorPos.Y = SinsGetters.WindowHeight - Y;

            return cursorPos;
        }

        // CAMERA CONTROLS
        private const double FullRotation = 2 * Math.PI;
        private const float FullPitch = 1.55334f;
        public static void RotateCamera(float degrees, bool counterClockwise = false)
        {
            if (float.IsNaN(degrees) || float.IsInfinity(degrees))
                return;

            float delta = degrees % 360f;

            if (counterClockwise)
                delta = -delta;

            SinsCamera.Rotation += delta / 360f * (float)FullRotation;
        }

        public static void AngleCamera(float angle)
        {
            if (angle > 90f ||
                angle < -90f)
                return;

            SinsCamera.Pitch = (angle / 90f) * FullPitch;
        }
    }
}
