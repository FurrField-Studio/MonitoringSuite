using FurrFieldStudio.MonitoringSuite.SO;

namespace FurrFieldStudio.MonitoringSuite.Editor.Views
{
    public abstract class MonitoringSuiteWindowView
    {
        public bool Initialized;
        
        public abstract void Initialize(MonitoringSessionSO monitoringSession);

        public abstract void Render();
    }
}