using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using AppFrame.Base;
using AppFrame.CustomAttributes;
using AppFrame.Invocation;
using AppFrame.Utils;
using AppFrame.Validation;
using AppFrame.Extensions;
using AppFrame.WPF.Screens;
using Caliburn.Micro;
using CoralPOS.Models;
using POSServer.BusinessLogic.Common;
using POSServer.BusinessLogic.Implement;
using POSServer.Utils;
using POSServer.ViewModels.Dialogs;
using POSServer.ViewModels.Menu.Stock;


namespace POSServer.ViewModels.Stock.StockIn
{

    [AttachMenuAndMainScreen(typeof(IStockInMenuViewModel), typeof(IStockMainViewModel))]
    public class StockInViewModel : PosViewModel, IStockInViewModel
    {

        private IShellViewModel _startViewModel;
        public StockInViewModel()
        {
            _startViewModel = ShellViewModel.Current;
        }

        #region Fields

        private string _wholeSalePrice;
        public string WholeSalePrice
        {
            get
            {
                return _wholeSalePrice;
            }
            set
            {
                _wholeSalePrice = value;
                NotifyOfPropertyChange(() => WholeSalePrice);
            }
        }

        private string _price;
        public string Price
        {
            get
            {
                return _price;
            }
            set
            {
                _price = value;
                NotifyOfPropertyChange(() => Price);
            }
        }

        private string _inputPrice;
        public string InputPrice
        {
            get
            {
                return _inputPrice;
            }
            set
            {
                _inputPrice = value;
                NotifyOfPropertyChange(() => InputPrice);
            }
        }

        private string _textBox4;
        public string textBox4
        {
            get
            {
                return _textBox4;
            }
            set
            {
                _textBox4 = value;
                NotifyOfPropertyChange(() => textBox4);
            }
        }

        private CoralPOS.Models.ProductMaster _productMaster;
        public CoralPOS.Models.ProductMaster ProductMaster
        {
            get
            {
                return _productMaster;
            }
            set
            {
                _productMaster = value;
                NotifyOfPropertyChange(() => ProductMaster);
                NotifyOfPropertyChange(() => CanOpenProperty);
            }
        }

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

        private CoralPOS.Models.StockIn _stockIn;
        public CoralPOS.Models.StockIn StockIn
        {
            get
            {
                return _stockIn;
            }
            set
            {
                _stockIn = value;
                NotifyOfPropertyChange(() => StockIn);
            }
        }

        public string _productNameText;
        public string ProductNameText
        {
            get { return _productNameText; }
            set { _productNameText = value; NotifyOfPropertyChange(() => ProductNameText); }
        }

        public IProductMasterLogic ProductMasterLogic { get; set; }
        public IMainPriceLogic MainPriceLogic { get; set; }
        #endregion

        #region List use to fetch object for view

        private IList _productMasterDetailList;
        public IList ProductMasterList
        {
            get
            {
                return _productMasterDetailList;
            }
            set
            {
                _productMasterDetailList = value;
                NotifyOfPropertyChange(() => ProductMasterList);
            }
        }
        #endregion

        #region List which just using in Data Grid

        private IList _stockInDetailList;
        public IList StockInDetailList
        {
            get
            {
                return _stockInDetailList;
            }
            set
            {
                _stockInDetailList = value;
                NotifyOfPropertyChange(() => StockInDetailList);
            }
        }

        private IList _selectedStockInDetails;
        public IList SelectedStockInDetails
        {
            get
            {
                return _selectedStockInDetails;
            }
            set
            {
                _selectedStockInDetails = value;
                NotifyOfPropertyChange(() => SelectedStockInDetails);
            }
        }
        #endregion

        #region Methods

        public void Help()
        {

        }

        public void Recreate()
        {

        }

        public void ReloadProductMaster(string text)
        {
            ExecuteHelper.OnBackgroundThread(() => LoadProductMaster(text), CompletedLoadProductMaster);
        }

        private object LoadProductMaster(string text)
        {
            if (string.IsNullOrEmpty(text)) text = "";
            IoC.Get<ICircularLoadViewModel>().StartLoading();
            IList list = ProductMasterLogic.LoadAllProductMasterWithType(text.Trim());
            ProductMasterList = list;
            return null;
        }
        void CompletedLoadProductMaster(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            IoC.Get<ICircularLoadViewModel>().StopLoading();
        }

        /*private bool _canSave;
        public bool CanSave
        {
            get
            {
                //StockIn.StockInDetails = ObjectConverter.ConvertTo<StockInDetail>(StockInDetailList);
                if(ObjectUtility.IsNullOrEmpty(StockIn.StockInDetails)) return false;
                return _canSave;
                //return !this.HasError();
            }
            set
            {
                _canSave = value;
                NotifyOfPropertyChange(()=>CanSave);
            }
        }*/
                
        public void Save()
        {

            StockIn.StockInDetails = ObjectConverter.ConvertTo<StockInDetail>(StockInDetailList);
            if (!this.Validate()) return;
            IEnumerable<IError> errors = this.GetErrors(StockIn);
            if (errors.Count() > 0)
            {
                var test = IoC.Get<IErrorDialogViewModel>();
                test.ErrorResult = errors.ToList();
                _startViewModel.ShowDialog(test);
            }
            else
            {
                Flow.Session.Put(FlowConstants.SAVE_STOCK_IN, StockIn);
                GoToNextNode();
            }
        }



        public void Stop()
        {
            Flow.End();
        }

        public void CreateNewProductMaster()
        {

        }

        public void EditProductMaster()
        {

        }

        public void PutPrice()
        {
            var list = SelectedStockInDetails;
            var stockInList = StockInDetailList;
            foreach (StockInDetail detail in list)
            {
                foreach (StockInDetail stockInDetail in stockInList)
                {
                    if(stockInDetail.Product.ProductId.Equals(detail.Product.ProductId))
                    {
                        stockInDetail.MainPrice.WholeSalePrice = int.Parse(WholeSalePrice);
                        stockInDetail.MainPrice.Price = int.Parse(Price);
                        break;
                    }
                }
                
            }
            IList newlist  = new ArrayList(stockInList);
            StockInDetailList = newlist;
            
        }

        protected override void OnInitialize()
        {
            var list = Flow.Session.Get(FlowConstants.PRODUCT_NAMES_LIST);
            ProductMasterList = list as IList;
            _stockInDetailList = new ArrayList();
            _selectedStockInDetails = new ArrayList();
            SelectedStockInDetails = _selectedStockInDetails;
            StockIn = Flow.Session.Get(FlowConstants.SAVE_STOCK_IN) as CoralPOS.Models.StockIn;
            if (StockIn == null)
            {
                //CoralPOS.Models.StockIn stockIn = DataErrorInfoFactory.Create<CoralPOS.Models.StockIn>();
                var stockIn = new CoralPOS.Models.StockIn();
                stockIn.StockInType = 0;
                stockIn.ConfirmFlg = 0;
                stockIn.Description = Description;
                stockIn.CreateDate = DateTime.Now;
                stockIn.UpdateDate = DateTime.Now;
                stockIn.StockInDate = DateTime.Now;
                stockIn.CreateId = "admin";
                stockIn.UpdateId = "admin";
                stockIn.DelFlg = 0;
                stockIn.ExclusiveKey = 0;
                stockIn.StockInDetails = ObjectConverter.ConvertTo<StockInDetail>(StockInDetailList);
                StockIn = stockIn;
            }
            else
            {
                StockInDetailList = ObjectConverter.ConvertFrom(StockIn.StockInDetails);
            }
        }

        public bool CanOpenProperty
        {
            get { return ProductMaster != null; }
        }

        public void OpenProperty()
        {
            var screen = IoC.Get<IProductPropertiesViewModel>("IProductPropertiesViewModel");
            screen.ProductName = ProductMaster.ProductName;
            screen.Setup();
            screen.ConfirmEvent += screen_ConfirmEvent;
            _startViewModel.ShowDialog(screen);
        }



        void screen_ConfirmEvent(object sender, ProductEventArgs e)
        {
            CreateProductIdForInput(e.ProductColorList, e.ProductSizeList);
        }

        private void CreateProductIdForInput(IList colorList, IList sizeList)
        {

            var productMaster = ProductMaster;
            string price = string.IsNullOrEmpty(Price) ? "0" : Price;
            string wholesaleprice = string.IsNullOrEmpty(WholeSalePrice) ? "0" : WholeSalePrice;
            MainPricePK findPk = new MainPricePK { DepartmentId = 0, ProductMasterId = productMaster.ProductMasterId };
            MainPrice newPrice = null;
            newPrice = MainPriceLogic.FindById(findPk);
            if (newPrice == null)
            {
                newPrice = new MainPrice();
                newPrice.Price = Int64.Parse(price);
                newPrice.WholeSalePrice = Int64.Parse(wholesaleprice);

                MainPricePK newPricePK = new MainPricePK
                {
                    DepartmentId = 0,
                    ProductMasterId = productMaster.ProductMasterId
                };
                newPrice.MainPricePK = newPricePK;
            }
            else // price has been existed
            {
                Price = newPrice.Price.ToString();
                WholeSalePrice = newPrice.WholeSalePrice.ToString();
            }

            IList<Product> products = StockInHelper.CreateProduct(productMaster, colorList, sizeList);
            foreach (Product newProduct in products)
            {
                bool isFound = CheckProductInStockInDetailList(newProduct);
                if (isFound) continue;

                string inputPrice = string.IsNullOrEmpty(InputPrice) ? "0" : InputPrice;
                //StockInDetail newDetail = DataErrorInfoFactory.Create<StockInDetail>();
                //StockInDetail newDetail = DataErrorInfoFactory.Create<StockInDetail>();
                StockInDetail newDetail = new StockInDetail();
                newDetail.Product = newProduct;
                newDetail.ProductMaster = productMaster;
                newDetail.CreateDate = DateTime.Now;
                newDetail.UpdateDate = DateTime.Now;
                newDetail.CreateId = "admin";
                newDetail.UpdateId = "admin";
                newDetail.Quantity = 0;
                newDetail.Price = Int64.Parse(inputPrice);
                StockInDetailPK detailPK = new StockInDetailPK
                                               {
                                                   ProductId = newProduct.ProductId
                                               };

                newDetail.StockInDetailPK = detailPK;
                //newDetail = DataErrorInfoFactory.CreateProxyFor(newDetail);


                //MainPrice newPrice = DataErrorInfoFactory.Create<MainPrice>();


                //newPrice = DataErrorInfoFactory.CreateProxyFor(newPrice);
                newDetail.MainPrice = newPrice;

                IList list = new ArrayList(_stockInDetailList);
                list.Add(newDetail);
                StockInDetailList = list;

            }
            if (StockIn.StockInDetails == null)
            {
                StockIn.StockInDetails = ObjectConverter.ConvertTo<StockInDetail>(StockInDetailList);
                NotifyOfPropertyChange(() => StockIn);
            }
        }

        private bool CheckProductInStockInDetailList(Product product)
        {
            foreach (StockInDetail inDetail in _stockInDetailList)
            {
                if (inDetail.Product.ProductId.Equals(product.ProductId)) return true;
            }
            return false;
        }
    }
        #endregion

}