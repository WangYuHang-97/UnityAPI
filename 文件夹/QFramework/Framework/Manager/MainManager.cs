using UnityEngine;

namespace QFramework
{
    public enum EnviromentMode
    {
        Developing,
        Test,
        Production,
    }

    public abstract class MainManager : MonoBehaviour
    {
        public EnviromentMode Mode;

        private static EnviromentMode mSharedMode;
        private static bool mModeSetted = false;

        void Start()
        {
            if (!mModeSetted)
            {
                mSharedMode = Mode;
                mModeSetted = true;
            }

            switch (mSharedMode)
            {
                case EnviromentMode.Developing:
                    LaunchInDevelopingMode();
                    break;
                case EnviromentMode.Test:
                    LaunchInTestMode();
                    break;
                case EnviromentMode.Production:
                    LaunchInProductionMode();
                    break;
            }
        }

        protected abstract void LaunchInDevelopingMode();
        protected abstract void LaunchInTestMode();
        protected abstract void LaunchInProductionMode();
    }
}