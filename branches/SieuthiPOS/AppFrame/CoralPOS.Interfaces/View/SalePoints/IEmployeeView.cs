using System;
using AppFrame.Common;
using CoralPOS.Interfaces.Presenter.SalePoints;

namespace CoralPOS.Interfaces.View.SalePoints
{
    public interface IEmployeeView : IView<EmployeeEventArgs>
    {
        #region main controller and event
        IEmployeeController EmployeeController { get; set; }
        event EventHandler<EmployeeEventArgs> SaveEmployeeEvent;
        event EventHandler<EmployeeEventArgs> ResetEmployeeEvent;
        event EventHandler<EmployeeEventArgs> CloseEmployeeFormEvent;
        event EventHandler<EmployeeEventArgs> HelpEvent;
        #endregion

        #region sub controller and event
        ISalePointController SalePointController { set; }
        #endregion
    }
}