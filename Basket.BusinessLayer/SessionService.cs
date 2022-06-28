using System;
using System.Collections.Generic;

namespace Basket.BusinessLayer
{
    public class SessionService : ISessionService
    {
        private readonly int MaxSessionDurationInMinutes = 30;

        private static Dictionary<string, DateTime> _sessions = new Dictionary<string, DateTime>();

        public string CreateNewSession()
        {
            var newSession = Guid.NewGuid().ToString();

            _sessions[newSession] = DateTime.Now;

            return newSession;
        }

        public bool CheckExpirationDate(string sessionId)
        {
            if (string.IsNullOrWhiteSpace(sessionId))
            {
                return false;
            }

            return _sessions.ContainsKey(sessionId) && 
                _sessions[sessionId].AddMinutes(MaxSessionDurationInMinutes) > DateTime.Now;
            
        }

    }
}
