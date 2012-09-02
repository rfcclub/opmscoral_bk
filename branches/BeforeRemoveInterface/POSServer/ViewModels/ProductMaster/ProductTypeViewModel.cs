﻿			 

			 

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Forms;
using AppFrame.Base;
using AppFrame.Utils;
using Caliburn.Micro;


using AppFrame.CustomAttributes;
using AppFrame.CustomAttributes;
using CoralPOS.Models;
using POSServer.BusinessLogic.Common;
using POSServer.BusinessLogic.Implement;
using MessageBox = System.Windows.MessageBox;


namespace POSServer.ViewModels.ProductMaster
{
	//[PerRequest(typeof(IProductTypeViewModel))]
	public class ProductTypeViewModel : PosViewModel,IProductTypeViewModel
	{
	    private IList _originalList;
	    private bool hasCreatedAction = false;
	    private bool hasDeletedAction = false;
		private IShellViewModel _startViewModel;
		public ProductTypeViewModel()
		{
			_startViewModel = ShellViewModel.Current;
			//ProductTypeLogic = IoC.Get<IProductTypeLogic>("IProductTypeLogic");
		}
		
		#region Fields
				
		private string _description;
		public string Description
		{
			get
			{
				return _description;
			}
			set
			{
				_description = value;
				NotifyOfPropertyChange(() => Description);
			}
		}
				
		private string _typeName;
		public string TypeName
		{
			get
			{
				return _typeName;
			}
			set
			{
				_typeName = value;
				NotifyOfPropertyChange(() => TypeName);
			}
		}
				
		private string _typeId;
		public string TypeId
		{
			get
			{
				return _typeId;
			}
			set
			{
				_typeId = value;
				NotifyOfPropertyChange(() => TypeId);
			}
		}

		public IProductTypeLogic ProductTypeLogic
		{
			get; set;
		}

		private ProductType _selectedProductType;
		public ProductType SelectedProductType
		{
			get { return _selectedProductType; }
			set
			{
				_selectedProductType = value;
				NotifyOfPropertyChange(() => SelectedProductType);
			}
		}

		#endregion
		
		#region List use to fetch object for view
				#endregion
		
		#region List which just using in Data Grid
				
		private IList _productTypeList;
		public IList ProductTypeList
		{
			get
			{
				return _productTypeList;
			}
			set
			{
				_productTypeList = value;
				NotifyOfPropertyChange(() => ProductTypeList);
			}
		}
				#endregion
		
		#region Methods
				
		public void Help()
		{
			
		}
				
		public void Delete()
		{
			IList list = new ArrayList(_productTypeList);
			ObjectUtility.RemoveFromList(list, SelectedProductType, "TypeName");
			ProductTypeList = list;
		    hasDeletedAction = true;
		}
				
		public void Edit()
		{
			
		}
				
		public void Stop()
		{
			Flow.End(); 
		}
				
		public void Create()
		{
			IList list = new ArrayList(_productTypeList);
			list.Add(new ProductType
			{
				TypeName = "NONAME"
			});
            ProductTypeList = list;
		    hasCreatedAction = true;
		}

		public void Save()
		{
            if (hasCreatedAction || hasDeletedAction)
            {
                MessageBoxResult result = MessageBox.Show("Danh sách được thêm hoặc bớt. Xin xác nhận muốn lưu thay đổi","Cảnh báo", MessageBoxButton.YesNo);
                if (result == MessageBoxResult.No) return;
            }
		    ProductTypeLogic.Update(ProductTypeList);
		    ProductTypeLogic.Sync(ObjectConverter.ConvertTo<ProductType>(ProductTypeList),Flow.Session);
			SetBackToParentFlow(FlowConstants.PRODUCT_TYPE_LIST, ProductTypeList);
			GoToNextNode();
		}

		protected override void OnInitialize()
		{
			ProductTypeLogic.LoadDefinition(Flow.Session);
			IList list = null;
			list = Flow.Session.Get(FlowConstants.PRODUCT_TYPE_LIST) as IList;
			if (list == null) list = new ArrayList();
            _originalList = list;
			ProductTypeList = list;
		}
		
		#endregion
		
		
		
	}
}