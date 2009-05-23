using System;
using System.Collections.Generic;
using System.Text;
using AppFrame.Common;
using System.Collections;
using System.Windows.Forms;
using CoralPOS.Interfaces.Model;

namespace CoralPOS.Interfaces.Presenter.Masters
{
    public class UniversalMasterSaveEventArgs : BaseEventArgs
    {
        public Form ParentForm { get; set; }
        public object CreatedData { get; set; }
        public string Name { get; set; }
        public long Id { get; set; }
        public MasterType MasterType { get; set; }
    }
}