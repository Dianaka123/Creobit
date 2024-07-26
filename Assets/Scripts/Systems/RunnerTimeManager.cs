using Assets.Scripts.Systems.Interfaces;
using System;

namespace Assets.Scripts.Systems
{
    public class RunnerTimeManager : IRunnerTimeManager
    {
        private readonly ITimeService _timeService;

        private DateTime _startTime;
        private DateTime _endTime;

        public RunnerTimeManager(ITimeService timeService)
        {
            _timeService = timeService;
        }

        public void Start()
        {
            _startTime = _timeService.Now;
        }

        public void Stop()
        {
           _endTime = _timeService.Now;
        }

        public TimeSpan Result()
        {
            return _endTime - _startTime;
        }

        public TimeSpan Elapsed()
        {
            return _timeService.Now - _startTime;
        }
    }
}
