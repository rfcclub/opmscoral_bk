﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using AppFrame.Base;
using AppFrame.DataLayer;
using AppFrame.Invocation;
using AppFrame.Validation;
using AppFrame.WPF;
using Caliburn.Micro;

namespace AppFrame.Extensions
{
    public static class PosViewModelExtensions
    {
        public static bool HasError(this PosViewModel viewModel,object target)
        {
            DefaultValidator validator = new DefaultValidator();
            var error = validator.Validate(target).FirstOrDefault();
            return error != null ? true : false;     
        }

        /// <summary>
        /// Return true if does not have view or does not using PosDataErrorProvider
        /// </summary>
        /// <param name="viewModel"></param>
        /// <returns></returns>
        public static bool HasError(this PosViewModel viewModel)
        {
            var view = viewModel.GetView(null) as DependencyObject;
            if(view == null) return false;
            DependencyObject vp = LogicalTreeHelper.FindLogicalNode(view, "PosDataErrorProvider");
            PosDataErrorProvider pvp = vp as PosDataErrorProvider;
            if (pvp == null) return false;
            return !pvp.Validate();
        }
        
        public static bool Validate(this PosViewModel viewModel)
        {
            var view = viewModel.GetView(null) as DependencyObject;
            if (view == null) return true;
            DependencyObject vp = LogicalTreeHelper.FindLogicalNode(view, "PosDataErrorProvider");
            PosDataErrorProvider pvp = vp as PosDataErrorProvider;
            if (pvp == null) return true;
            return pvp.Validate();
        }
        public static bool HasError(this PosViewModel viewModel,string pdpName)
        {
            var view = viewModel.GetView(viewModel) as DependencyObject;
            if (view == null) return true;
            DependencyObject vp = LogicalTreeHelper.FindLogicalNode(view, pdpName);
            PosDataErrorProvider pvp = vp as PosDataErrorProvider;
            if (pvp == null) return true;
            return pvp.Validate();
        }


        public static IEnumerable<IError> GetErrors<T>(this PosViewModel viewModel,T target)
        {
            //DefaultValidator validator = new DefaultValidator();
            return GlobalValidator.Instance.Validate(target);
        }

        public static IEnumerable<IError> GetErrors(this PosViewModel viewModel)
        {
            return GlobalValidator.Instance.Validate(viewModel);
        }

        public static void CatchExecute(this PosViewModel viewModel,Func<object> theDelegate)
        {
            try
            {
                theDelegate.Invoke();
            }
            catch (Exception exception)
            {
                Execute.OnUIThread(() => ProcessException(exception)); 
            }
        }

        private static void ProcessException(Exception exception)
        {
            MessageBox.Show(exception.Message);
        }

        public static void ExecuteAsync(this PosViewModel viewModel, Func<object> theDelegate,int loadScreenType = 0,bool catchException = false)
        {
            BackgroundTask task = new BackgroundTask(theDelegate);
            viewModel.StartWaitingScreen(loadScreenType);
            task.Completed += (sender, e) => viewModel.StopWaitingScreen(loadScreenType);
            task.Start(theDelegate);
        }

        public static void ExecuteAsync(this PosViewModel viewModel, Func<object> theDelegate, Func<object> theResult,int loadScreenType = 0, bool catchException = false)
        {
            BackgroundTask task = new BackgroundTask(theDelegate);
            viewModel.StartWaitingScreen(loadScreenType);
            task.Completed += (sender, e) => 
            { viewModel.StopWaitingScreen(loadScreenType);
              theResult();
            };
            task.Start(theDelegate);
        }
    }
}
