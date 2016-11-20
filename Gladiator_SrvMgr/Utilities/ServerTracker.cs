using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Gladiator_SrvMgr.Utilities
{
    public sealed class ServerTracker
    {
        private static ServerTracker _serverTracker;
        private static object _lockObj = new object();

        public List<GSM.Common.DTOs.ServerDTO> CurrentServerLists
        {
            get;
            set;
        }

        private ServerTracker()
        {
            CurrentServerLists = new List<GSM.Common.DTOs.ServerDTO>();
            //Comstructor logic
        }

        public static ServerTracker GetInstance
        {
            get
            {
                lock(_lockObj)
                {
                    if(_serverTracker==null)
                    {
                        lock(_lockObj)
                        {
                            _serverTracker = new ServerTracker();
                        }
                    }
                }
                return _serverTracker;
            }
        }

    }
}