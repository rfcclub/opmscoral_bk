﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using AppFrame.Common;
using AppFrame.Model;

namespace AppFrameServer.Services
{
    [ServiceContract(
        Name = "ServerService",Namespace = "http://localhost:8001/",
        SessionMode = SessionMode.Required,
        CallbackContract = typeof(IDepartmentStockOutCallback))]
    public interface IServerService
    {
        [OperationContract()]
        void JoinDistributingGroup(Department department);
        
        [OperationContract(IsOneWay = true)]
        void MakeDepartmentStockOut(Department department, DepartmentStockOut stockOut,DepartmentPrice price);

        [OperationContract(IsOneWay = true)]
        void ExitDistributingGroup(Department department);
    }
}
