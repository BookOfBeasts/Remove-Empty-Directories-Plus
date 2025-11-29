using System;
using System.Xml.Serialization;

using NotBob.Lib;

namespace RED.Config
{
    [Serializable]
    public class ConfigOptions : NBDataIsDirty
    {
        public ConfigOptions()
        {
            Language = "";
            DataIsDirty = false;
        }

        public string Language { get { return _Language; } set { SetField(ref _Language, value); } }
        private string _Language;

        public bool IgnoreHiddenDirectories { get { return _IgnoreHiddenDirectories; } set { SetField(ref _IgnoreHiddenDirectories, value); } }
        private bool _IgnoreHiddenDirectories;

        public bool IgnoreSystemDirectories { get { return _IgnoreSystemDirectories; } set { SetField(ref _IgnoreSystemDirectories, value); } }
        private bool _IgnoreSystemDirectories;

        public bool IgnoreEmptyFiles { get { return _IgnoreEmptyFiles; } set { SetField(ref _IgnoreEmptyFiles, value); } }
        private bool _IgnoreEmptyFiles;

        public bool HideDeletionErrors { get { return _HideDeletionErrors; } set { SetField(ref _HideDeletionErrors, value); } }
        private bool _HideDeletionErrors;

        public bool HideScanErrors { get { return _HideScanErrors; } set { SetField(ref _HideScanErrors, value); } }
        private bool _HideScanErrors;

        public bool HideIgnoredDirectories { get { return _HideIgnoredDirectories; } set { SetField(ref _HideIgnoredDirectories, value); } }
        private bool _HideIgnoredDirectories;

        public bool ClipboardPathDetection { get { return _ClipboardPathDetection; } set { SetField(ref _ClipboardPathDetection, value); } }
        private bool _ClipboardPathDetection;

        public bool FastSearchMode { get { return _FastSearchMode; } set { SetField(ref _FastSearchMode, value); } }
        private bool _FastSearchMode;

        public bool AutoProtectRoot { get { return _AutoProtectRoot; } set { SetField(ref _AutoProtectRoot, value); } }
        private bool _AutoProtectRoot;

        public int PauseBetweenDeletions { get { return _PauseBetween; } set { SetField(ref _PauseBetween, value); } }
        private int _PauseBetween;

        public int MaxDirectoryDepth { get { return _MaxDepth; } set { SetField(ref _MaxDepth, value); } }
        private int _MaxDepth;

        public int InfiniteLoopDetectionCount { get { return _InfiniteLoopDetectionCount; } set { SetField(ref _InfiniteLoopDetectionCount, value); } }
        private int _InfiniteLoopDetectionCount;

        public uint MinDirectoryAgeHours { get { return _MinDirectoyAgeHours; } set { SetField(ref _MinDirectoyAgeHours, value); } }
        private uint _MinDirectoyAgeHours;

        public int DeleteModeInt { get { return _DeleteModeInt; } set { SetField(ref _DeleteModeInt, value); } }
        private int _DeleteModeInt;

        public DeleteModes DeleteMode { get { return (DeleteModes)DeleteModeInt; } }

        public bool SavePrompt { get { return _SavePrompt; } set { SetField(ref _SavePrompt, value); } }
        private bool _SavePrompt;

        public bool RememberWindowDetails { get { return _RememberWindowDetails; } set { SetField(ref _RememberWindowDetails, value); } }
        private bool _RememberWindowDetails;

        public bool RememberLastUsedDirectory { get { return _RememberLastUsedDirectory; } set { SetField(ref _RememberLastUsedDirectory, value); } }
        private bool _RememberLastUsedDirectory;

        public bool RememberDeletionStats { get { return _RememberDeletionStats; } set { SetField(ref _RememberDeletionStats, value); } }
        private bool _RememberDeletionStats;

        public bool NoSTAD { get { return _NoSTAD; } set { SetField(ref _NoSTAD, value); } }
        private bool _NoSTAD;

        [XmlIgnore]
        public override bool DataIsDirty
        {
            get { return base.DataIsDirty; }
            set { base.DataIsDirty = value; }
        }

        public void SetToDefaults()
        {
            AutoProtectRoot = true;
            ClipboardPathDetection = false;
            IgnoreHiddenDirectories = false;
            FastSearchMode = false;
            HideIgnoredDirectories = false;
            HideScanErrors = true;
            HideDeletionErrors = true;
            IgnoreEmptyFiles = true;
            InfiniteLoopDetectionCount = 10;
            IgnoreSystemDirectories = true;
            MaxDirectoryDepth = -1;
            MinDirectoryAgeHours = 0;
            PauseBetweenDeletions = 0;
            DeleteModeInt = (int)DeleteModes.RecycleBin;
            RememberWindowDetails = false;
            RememberLastUsedDirectory = false;
            RememberDeletionStats = false;
            NoSTAD = false;
        }
    }
}