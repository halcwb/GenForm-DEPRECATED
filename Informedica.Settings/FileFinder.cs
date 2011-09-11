using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;

namespace Informedica.Settings
{
    public class FileFinder
    {
        private static FileFinder _instance;
        private static IEnumerable<string> _filter = new List<string>(); 
        private static readonly object LockThis = new object();

        private string _fileName;
        private List<string> _list;

        private static FileFinder Instance
        {
            get
            {
                if (_instance == null)
                    lock (LockThis)
                    {
                        if (_instance == null)
                        {
                            var newInstance = new FileFinder();
                            Thread.MemoryBarrier();
                            _instance = newInstance;
                        }
                    }
                return _instance;
            }
        }

        public static IEnumerable<String> FindPath(string fileName)
        {
            return Instance.FindPath_(fileName);
        }

        private IEnumerable<string> FindPath_(string fileName)
        {
            _fileName = fileName;
            _list = new List<string>();

            foreach (var drive in DriveInfo.GetDrives())
            {
                if (!drive.IsReady) continue;

                foreach (var root in TryGetDirectories(drive.RootDirectory))
                {
                    FindPathInDir(root);
                }
            }
            return _list;
        }

        public static IEnumerable<string> Filter
        {
            get { return _filter; }
            set { _filter = value; }
        }

        private static bool CanSearchDirectory(DirectoryInfo dir)
        {
            if (dir.Attributes == FileAttributes.Hidden || dir.Attributes == FileAttributes.System || dir.Attributes == FileAttributes.Offline) return false;
            return true;
        }

        private void FindPathInDir(DirectoryInfo dir)
        {
            if (!CanSearchDirectory(dir)) return;

            if (TryGetDirectories(dir).Count() == 0)
            {
                AddFoundToList(dir);
                return;
            }
            
            AddFoundToList(dir);
            foreach (var child in TryGetDirectories(dir))
            {
                FindPathInDir(child);
            }
        }

        private void AddFoundToList(DirectoryInfo dir)
        {
            _list.AddRange(TryGetFiles(dir).Where(file => file.Name == _fileName).Select(file => file.FullName));
        }

        private static IEnumerable<DirectoryInfo> TryGetDirectories(DirectoryInfo dir)
        {
            try
            {
                if (_filter.Count() == 0) return dir.GetDirectories();

                var list = new List<DirectoryInfo>();
                foreach (var item in _filter)
                {
                    string filter = item.Substring(0, item.LastIndexOf("\\"));
                    list.AddRange(dir.GetDirectories(filter));
                }
                _filter = new List<string>();
                return list;
            }
            catch (Exception)
            {
                return new List<DirectoryInfo>();
            }
        }

        private static IEnumerable<FileInfo> TryGetFiles(DirectoryInfo dir)
        {
            try
            {
                return dir.GetFiles();
            }
            catch (Exception)
            {
                return new List<FileInfo>();
            }
        }
    }
}
