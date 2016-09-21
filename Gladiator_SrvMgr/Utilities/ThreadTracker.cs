using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Gladiator_SrvMgr.Utilities
{
    public sealed class ThreadTracker
    {
        private static ThreadTracker _instance;
        private static object _lockObj = new object();
        private Dictionary<string, int> _jobs = new Dictionary<string, int>();
        private bool _isAppStopped = false;
        public bool IsAppStopped
        {
            set { _isAppStopped = value; }
            get { return _isAppStopped; }
        }
        private ThreadTracker()
        {
            //Private constructor to block initialization
        }

        public static ThreadTracker GetInstance()
        {
            lock (_lockObj)
            {
                if(_instance==null)
                {
                    lock(_lockObj)
                    {
                        _instance = new ThreadTracker();
                    }
                }
                return _instance;
            }
        }

        public Dictionary<string, int> GetRunningJobs()
        {
            lock(_lockObj)
            {
                return _jobs;
            }
        }
    }
}