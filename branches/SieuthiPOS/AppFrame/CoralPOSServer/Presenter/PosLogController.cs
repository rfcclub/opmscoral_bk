using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using AppFrame;
using AppFrame.Common;
using CoralPOS.Interfaces.Logic;
using CoralPOS.Interfaces.Model;
using CoralPOS.Interfaces.Presenter;
using CoralPOS.Interfaces.Presenter.GoodsIO;
using CoralPOS.Interfaces.Presenter.SalePoints;
using AppFrame.Utility;
using CoralPOS.Interfaces.View;
using CoralPOS.Interfaces.View.GoodsIO;
using NHibernate.Criterion;

namespace CoralPOSServer.Presenter
{
    public class PosLogController : IPosLogController
    {
        #region View use in IGoodsIOSearchController

        private IPosLogView _posLogView;
        public IPosLogView PosLogView
        { 
            get
            {
                return _posLogView;
            }
            set
            {
                _posLogView = value;
                _posLogView.GetPosLogEvent += new System.EventHandler<PosLogEventArgs>(posLogView_GetPosLogEvent);
                _posLogView.SearchPosLogEvent += new System.EventHandler<PosLogEventArgs>(posLogView_SearchPosLogEvent);
            }
        }

        private void posLogView_SearchPosLogEvent(object sender, PosLogEventArgs e)
        {
            var criteria = new ObjectCriteria();
            if(!string.IsNullOrEmpty(e.Username))
            {
                criteria.AddLikeCriteria("PosUser", e.Username + "%");    
            }
            if(!string.IsNullOrEmpty(e.Action))
            {
                criteria.AddLikeCriteria("PosAction", e.Action + "%");    
            }
            criteria.AddGreaterOrEqualsCriteria("Date", DateUtility.ZeroTime(e.LogDateFrom));
            criteria.AddLesserOrEqualsCriteria("Date", DateUtility.MaxTime(e.LogDateTo));
            criteria.AddOrder("Date", false);
            IList list = PosLogLogic.FindAll(criteria);
            e.PosLogList = list;
        }

        #endregion

        public void posLogView_GetPosLogEvent(object sender, PosLogEventArgs e)
        {
            if (e.PosLogId > 0)
            {
                e.PosLog = PosLogLogic.FindById(e.PosLogId);
            }
        }

        #region Implementation of IBaseController<PosLogEventArgs>

        public PosLogEventArgs ResultEventArgs
        {
            get; set;
        }

        #endregion

        public IPosLogLogic PosLogLogic { get; set; }
    }
}
