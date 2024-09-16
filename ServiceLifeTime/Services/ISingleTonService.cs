using System;
using System.Security.Policy;

namespace ServiceLifeTime.Services
{
    public interface ISingleTonService
    {
        string GetGuid();
    }
    public class SingleTonService : ISingleTonService
    {
        private Guid guid;
        public SingleTonService()
        {
            guid = Guid.NewGuid();
        }
        public string GetGuid() => guid.ToString();
    }
}
