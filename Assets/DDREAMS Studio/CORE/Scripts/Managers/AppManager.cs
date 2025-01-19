using UnityEngine;
using UnityEditor;

namespace DDREAMS.CORE
{
    public class AppManager : MonoBehaviour
    {
        [SerializeField]
        [Tooltip("Check to hide the cursor in Runtime mode. By default the cursor is visible.")]
        private bool _IsCursorVisible = true;


        private void Start()
        {
            Cursor.visible = _IsCursorVisible;
        }

        /// <summary>
        /// Quits the application, both in Edit as in Runtime mode.
        /// </summary>
        public static void QuitApplication()
        {
#if UNITY_EDITOR
            EditorApplication.ExitPlaymode();
#else
            Application.Quit();
#endif
        }
    }
}
