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

            foreach (var path in Filter)
            {
                _list.AddRange(new DirectoryInfo(path).GetFiles(_fileName, SearchOption.AllDirectories).Select(f => f.FullName));
            }

            return _list;
        }

        public static IEnumerable<string> Filter
        {
            get { return _filter; }
            set { _filter = value; }
        }
    }
}
