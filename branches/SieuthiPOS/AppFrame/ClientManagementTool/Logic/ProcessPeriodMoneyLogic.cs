using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AppFrame;
using AppFrame.Utility;
using ClientManagementTool.View.Management;
using CoralPOS.Interfaces.Logic;

namespace ClientManagementTool.Logic
{
    public class ProcessPeriodMoneyLogic : IProcessPeriodMoneyLogic
    {
        private IProcessPeriodMoneyView processPeriodMoneyView;
        public IProcessPeriodMoneyView ProcessPeriodMoneyView
        {
            get { return processPeriodMoneyView; }
            set 
            { 
                processPeriodMoneyView = value;
                processPeriodMoneyView.LoadProcessPeriodMoneyEvent += new EventHandler<ProcessPeriodMoneyEventArgs>(processPeriodMoneyView_LoadProcessPeriodMoneyEvent);
            }
        }

        void processPeriodMoneyView_LoadProcessPeriodMoneyEvent(object sender, ProcessPeriodMoneyEventArgs e)
        {
            DateTime toDay = DateUtility.DateOnly(DateTime.Now);
            ObjectCriteria objectCriteria = new ObjectCriteria();
            objectCriteria.AddEqCriteria("EmployeeMoneyPK.WorkingDay", toDay);
            IList list  = EmployeeMoneyLogic.FindAll(objectCriteria);
            e.PeriodMoneyList = list;
        }

        public IEmployeeMoneyLogic EmployeeMoneyLogic
        {
            get; set;
        }
    }
}
