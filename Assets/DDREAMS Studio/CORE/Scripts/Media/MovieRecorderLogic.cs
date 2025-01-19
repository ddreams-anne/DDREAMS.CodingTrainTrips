#if UNITY_EDITOR

using System.IO;
using UnityEditor.Recorder;
using UnityEditor.Recorder.Encoder;
using UnityEditor.Recorder.Input;

namespace UnityEngine.Recorder.Examples
{
    /// <summary>
    /// Enter Play Mode to start the recording.
    /// 
    /// By default the recording automatically stops when Play Mode is stopped or when the component is disabled.
    /// If the _UseTime property is set to true, the recording will stop after the number of seconds from the _MovieLength property.
    /// 
    /// The recording output is saved in the [Project Folder]/Recordings folder.
    /// </summary>
    public class MovieRecorderLogic : MonoBehaviour
    {
        [Header("Movie Recorder Settings")]
        [SerializeField]
        [Tooltip("The name of the movie file.")]
        private string _MovieName = "Movie Name";

        [SerializeField]
        [Tooltip("The width of the movie (in pixels).")]
        private int _MovieWidth = 1920;

        [SerializeField]
        [Tooltip("The height of the movie (in pixels).")]
        private int _MovieHeight = 1080;

        [SerializeField]
        [Tooltip("The frame rate of the movie (frames per second).")]
        private float _FrameRate = 60.0f;

        [SerializeField]
        [Tooltip("The codec used to encode the movie.")]
        private CoreEncoderSettings.OutputCodec _OutputCodec = CoreEncoderSettings.OutputCodec.MP4;

        [SerializeField]
        [Tooltip("The quality of the movie encoding.")]
        private CoreEncoderSettings.VideoEncodingQuality _EncodingQuality = CoreEncoderSettings.VideoEncodingQuality.High;

        [Header("Audio Settings")]
        [SerializeField]
        [Tooltip("Record audio with the movie?")]
        private bool _RecordAudio = false;

        [Header("Time Settings")]
        [SerializeField]
        [Tooltip("Use a specific time for the length of the movie?")]
        private bool _UseTime = false;

        [SerializeField]
        [Tooltip("The start time of the movie (in seconds)")]
        private float _StartTime = 0.0f;

        [SerializeField]
        [Tooltip("The end time of the movie (in seconds)")]
        private float _EndTime = 10.0f;


        private string _fileExtension = string.Empty;
        private string _recordingFolder = "Recordings";
        private RecorderController _recorderController;
        internal MovieRecorderSettings _mediaSettings = null;


        private void OnEnable()
        {
            RecorderControllerSettings controllerSettings = ScriptableObject.CreateInstance<RecorderControllerSettings>();
            DirectoryInfo mediaOutputFolder = new(Path.Combine(Application.dataPath, "..", _recordingFolder));

            _recorderController = new RecorderController(controllerSettings);

            _mediaSettings = ScriptableObject.CreateInstance<MovieRecorderSettings>();
            _mediaSettings.name = _MovieName;
            _mediaSettings.Enabled = true;

            _mediaSettings.EncoderSettings = new CoreEncoderSettings
            {
                EncodingQuality = _EncodingQuality,
                Codec = _OutputCodec
            };

            _mediaSettings.CaptureAlpha = true;
            _mediaSettings.CaptureAudio = _RecordAudio;

            _mediaSettings.ImageInputSettings = new GameViewInputSettings
            {
                OutputWidth = _MovieWidth,
                OutputHeight = _MovieHeight
            };

            _mediaSettings.OutputFile = $"{mediaOutputFolder.FullName}/{_MovieName}";

            controllerSettings.AddRecorderSettings(_mediaSettings);
            controllerSettings.SetRecordModeToManual();
            controllerSettings.FrameRate = _FrameRate;

            if (_UseTime) controllerSettings.SetRecordModeToTimeInterval(_StartTime, _EndTime);

            RecorderOptions.VerboseMode = false;

            _recorderController.PrepareRecording();
            _recorderController.StartRecording();

            Debug.Log($"Started recording '{OutputFile.FullName}'.");
        }

        private void OnDisable()
        {
            _recorderController.StopRecording();
        }

        private void LateUpdate()
        {
            if (_recorderController.IsRecording()) return;

            Debug.Log($"Recording of '{OutputFile.FullName}' stopped.");
            DDREAMS.CORE.AppManager.QuitApplication();
        }


        private FileInfo OutputFile
        {
            get
            {
                _fileExtension = _OutputCodec switch
                {
                    CoreEncoderSettings.OutputCodec.MP4 => "mp4",
                    CoreEncoderSettings.OutputCodec.WEBM => "webm",
                    _ => "mp4",
                };

                string fileName = $"{_mediaSettings.OutputFile}.{_fileExtension}";

                return new FileInfo(fileName);
            }
        }
    }
}

#endif
