using Flooring_Ordering_System;
using FOS.Models;
using FOS.Models.Interfaces;
using FOS.Models.Responses;
using FOS.Models.Responses.OrderResponses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FOS.BLL
{
    public class OrderManager
    {
        private IOrderRepository _orderRepository;
        private IProductRepository _productRepository;
        private ITaxRepository _taxRepository;
        public OrderManager(IOrderRepository orderRepository, IProductRepository productRepository, ITaxRepository taxRepository)
        {
            _orderRepository = orderRepository;
            _productRepository = productRepository;
            _taxRepository = taxRepository;
        }

        public DisplayOrdersResponse DisplayOrders(DateTime orderDate)
        {
            DisplayOrdersResponse response = new DisplayOrdersResponse();
            response.Order = _orderRepository.DisplayOrder(orderDate);
            if (response.Order.Count == 0)
            {
                response.Success = false;
                response.Message = "No Orders are available";
            }
            else
            {
                response.Success = true;
            }

            return response;
        }
        public DisplayOrdersByOrderNumberResponse DisplayOrdersByOrderNumber(DateTime orderDate, int orderNumber)
        {
            DisplayOrdersByOrderNumberResponse response = new DisplayOrdersByOrderNumberResponse();
            response.Order = _orderRepository.DisplayOrderByOrderNumber(orderDate, orderNumber);
            if (response.Order == null)
            {
                response.Success = false;
                response.Message = "No Orders are available";
            }
            else
            {
                response.Success = true;
            }
            return response;
        }
        public AddOrderResponse AddOrder(DateTime orderDate, string custName, string state, string prodType, string area)
        {
            AddOrderResponse response = new AddOrderResponse();

            response.Order = _orderRepository.AddOrder( orderDate, custName, state, prodType, area);

            if (response.Order.CustomerName == "")
            {
                response.Success = false;
                response.Message = $"Customers name cannot be blank.";
            }


            if (response.Order == null)
            {
                response.Success = false;
                response.Message = $"Can not add order";
            }
            else
            {
                response.Success = true;
            }
            return response;
        }
        public EditOrderResponse EditOrder(DateTime orderDate, string orderNumber)
        {
            EditOrderResponse response = new EditOrderResponse();
            response.Order = _orderRepository.EditOrder(orderDate,orderNumber);
            if (response.Order == null)
            {
                response.Success = false;
                response.Message = $"Cannot edit this order";
            }
            else
            {
                response.Success = true;
            }
            return response;
        }
        public RemoveOrderResponse RemoveOrder(DateTime date, int orderNumber)
        {
            RemoveOrderResponse response = new RemoveOrderResponse();
            response.Order = _orderRepository.RemoveOrder(date, orderNumber);
            if (response.Order == null)
            {
                response.Success = true;
                
            }
            else
            {
                response.Success = false;
                response.Message = $"Error this order was not deleted.";
            }
            return response;

        }

        public LoadProductsResponse LoadProducts()
        {
            LoadProductsResponse response = new LoadProductsResponse();
            response.Product = _productRepository.LoadProduct();
            if (response.Product == null)
            {
                response.Success = false;
                response.Message = "Product not available";
            }
            else
            {
                response.Success = true;
            }
            return response;
        }

        public LoadTaxesResponse LoadTaxes()
        {
            LoadTaxesResponse response = new LoadTaxesResponse();
            response.Tax = _taxRepository.LoadTax();
            if (response.Tax == null)
            {
                response.Success = false;
                response.Message = "Taxes not available";
            }
            else
            {
                response.Success = true;
            }
            return response;
        }

        public Order Calculate(Order order)
        {
            ProductRepository products = new ProductRepository();
            TaxRepository taxes = new TaxRepository();

            foreach (var product in products.LoadProduct())
            {
                if (order.ProductType.ToLower() == product.ProductType.ToLower())
                {
                   order.LaborCostPerSquareFoot = product.LaborCostPerSquareFoot;
                   order.CostPerSquareFoot = product.CostPerSquareFoot;
                }
            }

            foreach (var _tax in taxes.LoadTax())
            {
                if (order.State.ToUpper() == _tax.StateName.ToUpper() || order.State.ToUpper() == _tax.StateAbbreviation.ToUpper())
                {
                    order.TaxRate  = _tax.TaxRate;
                }
            }

            decimal area = order.Area;
            decimal materialCost = area * order.CostPerSquareFoot;
            decimal laborCost = area * order.LaborCostPerSquareFoot;
            decimal tax = ((materialCost + laborCost)*(order.TaxRate/100));
            decimal total = (materialCost + laborCost + tax);

            order.Area = area;
            order.MaterialCost = materialCost;
            order.LaborCost = laborCost;
            order.Tax = tax;
            order.Total = total;

            return order;
        }

        public EditOrderResponse ReplaceEdit(DateTime orderDate, int orderNumber, Order myOrder)
        {
            EditOrderResponse response = new EditOrderResponse();
            response.Order = _orderRepository.ReplaceOrderEdit(orderDate, orderNumber, myOrder);
            if (response.Order == null)
            {
                response.Success = false;
                response.Message = $"Cannot edit this order";
            }
            else
            {
                response.Success = true;
            }
            return response;
        }
    }
}
 