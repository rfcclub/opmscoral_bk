﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AppFrame.Base;
using AppFrame.CustomAttributes;
using Caliburn.Micro;


namespace POSClient.ViewModels
{
    public interface IShellViewModel
    {
        void Open<T>() where T : IScreen;
        void ShowDialog<U>(U screen) where U : IScreen;
        //IServiceLocator ServiceLocator { get; set; }
        IScreen ActiveMenu { get; set; }
        IFlow ActiveFlow { get; set; }

        bool EnterFlow(string flowName);
        bool StartFlow(string flowName);
        bool ResumeFlow(string flowName);
        void LeaveFlow();
        void EnterChildFlow(string childFlowName, IFlow parentFlow);
    }
}