using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace AuxiliaryFeature.Utility
{
    public class WindowsServiceManager : IDisposable
    {

        public WindowsServiceManager()
        {
            _service = new ServiceController();
        }

        ServiceController _service = null;
        string _currentServiceName = string.Empty;
        string _currentMachineName = string.Empty;

        public bool StartWindowsService(string name, string machineName, bool restartService, params string[] args)
        {
            Initialize(name, machineName);
            _service.ServiceName = name;
            _service.MachineName = machineName;
            if (restartService)
                return Restart(args);
            else
                return Start(args);
        }

        void Initialize(string serviceName, string machineName)
        {
            if (_currentMachineName != machineName)
            {
                Dispose();
                _service = new ServiceController();
                return;
            }

            if (_currentServiceName != serviceName)
            {
                Dispose();
                _service = new ServiceController();
                return;
            }
        }

        bool Restart(params string[] args)
        {
            if (_service.Status == ServiceControllerStatus.Running)
            {
                if (_service.CanStop)
                {
                    _service.Stop();
                    _service.Refresh();
                    return Start(args);
                }
                return false;
            }

            if (_service.Status == ServiceControllerStatus.Stopped)
                return Start(args);
            else
                return false;
        }

        bool Start(params string[] args)
        {
            if (_service.Status == ServiceControllerStatus.StartPending)
            {
                return true;
            }

            if (_service.Status == ServiceControllerStatus.Stopped)
            {
                if (args == null)
                    _service.Start();
                else
                    _service.Start(args);
                _service.Refresh();
                return true;
            }

            if (_service.Status == ServiceControllerStatus.StopPending)
            {
                int counter = 0;
                while (_service.Status == ServiceControllerStatus.StopPending && counter < 10)
                {
                    System.Threading.Thread.Sleep(500);
                    counter++;
                }
                _service.Refresh();
                if (_service.Status == ServiceControllerStatus.Stopped)
                {
                    if (args == null)
                        _service.Start();
                    else
                        _service.Start(args);
                    _service.Refresh();
                    return true;
                }
                return false;
            }
            return _service.Status == ServiceControllerStatus.Running;
        }

        //?????
        public bool StopWindowsService(string serviceName, string machineName)
        {
            Initialize(serviceName, machineName);//?????
            _service.ServiceName = serviceName;
            _service.MachineName = machineName;
            return Stop();
        }

        bool Stop()
        {
            if (_service.Status == ServiceControllerStatus.StopPending)
            {
                return true;
            }

            if (_service.Status == ServiceControllerStatus.Running)
            {
                if (_service.CanStop)
                {
                    _service.Stop();
                    _service.Refresh();
                    return true;
                }
                return false;
            }

            if (_service.Status == ServiceControllerStatus.StartPending)
            {
                int counter = 0;
                while (_service.Status == ServiceControllerStatus.StartPending && counter < 10)
                {
                    System.Threading.Thread.Sleep(500);
                    counter++;
                }
                _service.Refresh();
                if (_service.Status == ServiceControllerStatus.Running)
                {
                    if (_service.CanStop)
                    {
                        _service.Stop();
                        _service.Refresh();
                        return true;
                    }
                }
                return false;
            }
            return _service.Status == ServiceControllerStatus.Stopped;
        }

        public bool IsWindowsServiceInstalled(string serviceName, string machineName)
        {
            return ServiceController.GetServices(machineName).FirstOrDefault(s => s.ServiceName.ToUpper() == serviceName.ToUpper()) != null;

        }

        //http://msdn.microsoft.com/en-us/library/ms244737.aspx
        public void Dispose()
        {
            _service.Close();
            _service.Dispose();
            _service = null;
            GC.SuppressFinalize(this);
        }
    }
}
