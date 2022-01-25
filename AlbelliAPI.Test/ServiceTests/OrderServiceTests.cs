using AlbelliAPI.Business.Models;
using AlbelliAPI.Business.Services;
using AlbelliAPI.Data.DTOs;
using AlbelliAPI.Data.Gateways;
using AlbelliAPI.Data.Models;
using AlbelliAPI.Test.Extensions;
using Moq;
using System;
using Xunit;

namespace AlbelliAPI.Test
{
    public class OrderServiceTests : ServiceTestsBase
    {
        private readonly Mock<IOrderPersistenceGateway> _mockOrderPersistenceGateway;
        private readonly OrderService _sut;

        public OrderServiceTests()
        {
            _mockOrderPersistenceGateway = new Mock<IOrderPersistenceGateway>();
            _sut = new OrderService(_mockOrderPersistenceGateway.Object, _mapper);
        }

        [Fact]
        public async void When_GetOrderDetails_HasValidOrderID_Then_Returns_OrderDetails()
        {
            _mockOrderPersistenceGateway.Setup(g => g.GetOrderDetails(It.IsAny<int>()))
                .ReturnsAsync(new OrderDetailsPersistence().Build());

            var result = await _sut.GetOrderDetails(orderID: 1);

            _mockOrderPersistenceGateway.Verify(s => s.GetOrderDetails(It.IsAny<int>()), Times.Once());
        }

        [Fact]
        public async void When_GetOrderDetails_HasInvalidOrderID_Then_ThrowArgumentException()
        {
            _mockOrderPersistenceGateway.Setup(g => g.GetOrderDetails(It.IsAny<int>()))
                .ReturnsAsync(new OrderDetailsPersistence().Build());

            var ex = await Assert.ThrowsAsync<ArgumentException>(() => _sut.GetOrderDetails(orderID: 0));

            Assert.Equal("OrderId is invalid", ex.Message);
            _mockOrderPersistenceGateway.Verify(s => s.GetOrderDetails(It.IsAny<int>()), Times.Never());
        }

        [Fact]
        public async void When_GetOrderDetails_DoesNotContainsRecords_Then_DoesNotReturns_OrderDetails()
        {
            _mockOrderPersistenceGateway.Setup(g => g.GetOrderDetails(It.IsAny<int>()))
                .ReturnsAsync(new OrderDetailsPersistence().Build().WithNull());

            var result = await _sut.GetOrderDetails(orderID: 1);

            Assert.Empty(result.Products);
            Assert.Equal(0, result.RequiredBinWidth);
        }

        [Fact]
        public async void When_GetOrderDetails_With_PhotoBooks_Then_CalculateRequiredBinWidth()
        {
            var expected = 1425;
            _mockOrderPersistenceGateway.Setup(g => g.GetOrderDetails(It.IsAny<int>()))
                .ReturnsAsync(new OrderDetailsPersistence().Build().WithProducts(Enums.ProductTypes.PhotoBook, 75));

            var result = await _sut.GetOrderDetails(orderID: 1);

            Assert.Equal(expected, result.RequiredBinWidth);
        }

        [Fact]
        public async void When_GetOrderDetails_With_Calendars_Then_CalculateRequiredBinWidth()
        {
            var productyType = Enums.ProductTypes.Calendar;
            var quantity = 20;
            var expected = 200;
            _mockOrderPersistenceGateway.Setup(g => g.GetOrderDetails(It.IsAny<int>()))
                .ReturnsAsync(new OrderDetailsPersistence().Build().WithProducts(productyType, quantity));

            var result = await _sut.GetOrderDetails(orderID: 1);

            Assert.Equal(expected, result.RequiredBinWidth);
        }

        [Fact]
        public async void When_GetOrderDetails_With_Canvas_Then_CalculateRequiredBinWidth()
        {
            var productyType = Enums.ProductTypes.Canvas;
            var quantity = 2;
            var expected = 32;
            _mockOrderPersistenceGateway.Setup(g => g.GetOrderDetails(It.IsAny<int>()))
                .ReturnsAsync(new OrderDetailsPersistence().Build().WithProducts(productyType, quantity));

            var result = await _sut.GetOrderDetails(orderID: 1);

            Assert.Equal(expected, result.RequiredBinWidth);
        }

        [Fact]
        public async void When_GetOrderDetails_With_SetOfGreetingCards_Then_CalculateRequiredBinWidth()
        {
            var productyType = Enums.ProductTypes.SetOfGreetingCards;
            var quantity = 5;
            var expected = 23.5;
            _mockOrderPersistenceGateway.Setup(g => g.GetOrderDetails(It.IsAny<int>()))
                .ReturnsAsync(new OrderDetailsPersistence().Build().WithProducts(productyType, quantity));

            var result = await _sut.GetOrderDetails(orderID: 1);

            Assert.Equal(expected, result.RequiredBinWidth);
        }

        [Fact]
        public async void When_GetOrderDetails_With_Mug_AsQuantityMultipleOfFour_Then_CalculateRequiredBinWidth()
        {
            var productyType = Enums.ProductTypes.Mug;
            var quantity = 12;
            var expected = 282;
            _mockOrderPersistenceGateway.Setup(g => g.GetOrderDetails(It.IsAny<int>()))
                .ReturnsAsync(new OrderDetailsPersistence().Build().WithProducts(productyType, quantity));

            var result = await _sut.GetOrderDetails(orderID: 1);

            Assert.Equal(expected, result.RequiredBinWidth);
        }

        [Fact]
        public async void When_GetOrderDetails_With_Mug_NotAsQuantityMultipleOfFour_Then_CalculateRequiredBinWidth()
        {
            var productyType = Enums.ProductTypes.Mug;
            var quantity = 17;
            var expected = 470;
            _mockOrderPersistenceGateway.Setup(g => g.GetOrderDetails(It.IsAny<int>()))
                .ReturnsAsync(new OrderDetailsPersistence().Build().WithProducts(productyType, quantity));

            var result = await _sut.GetOrderDetails(orderID: 1);

            Assert.Equal(expected, result.RequiredBinWidth);
        }

        [Fact]
        public async void When_GetOrderDetails_With_AllProducts_WhichContainsOneMug_Then_CalculateRequiredBinWidth()
        {
            var quantityOfMug = 1;
            var expected = 728.1;
            _mockOrderPersistenceGateway.Setup(g => g.GetOrderDetails(It.IsAny<int>()))
                .ReturnsAsync(new OrderDetailsPersistence().Build().WithAllProducts(quantityOfMug));

            var result = await _sut.GetOrderDetails(orderID: 1);

            Assert.Equal(expected, result.RequiredBinWidth);
        }

        [Fact]
        public async void When_GetOrderDetails_With_AllProducts_WhichContainsFourMugs_Then_CalculateRequiredBinWidth()
        {
            var quantityOfMug = 4;
            var expected = 728.1;
            _mockOrderPersistenceGateway.Setup(g => g.GetOrderDetails(It.IsAny<int>()))
                .ReturnsAsync(new OrderDetailsPersistence().Build().WithAllProducts(quantityOfMug));

            var result = await _sut.GetOrderDetails(orderID: 1);

            Assert.Equal(expected, result.RequiredBinWidth);
        }

        [Fact]
        public async void When_GetOrderDetails_With_AllProducts_WhichContainsFiveMugs_Then_CalculateRequiredBinWidth()
        {
            var quantityOfMug = 5;
            var expected = 822.1;
            _mockOrderPersistenceGateway.Setup(g => g.GetOrderDetails(It.IsAny<int>()))
                .ReturnsAsync(new OrderDetailsPersistence().Build().WithAllProducts(quantityOfMug));

            var result = await _sut.GetOrderDetails(orderID: 1);

            Assert.Equal(expected, result.RequiredBinWidth);
        }

        [Fact]
        public async void Try_SubmitOrder_Valid_Order_Then_SaveOrder()
        {
            var request = new OrderPlaced().Build();
            
            await _sut.SubmitOrder(request);

            _mockOrderPersistenceGateway.Verify(s => s.SubmitOrder(It.IsAny<OrderDetailsPersistence>()), Times.Once());
        }

        [Fact]
        public async void Try_SubmitOrder_Order_WithoutOrderId_Then_ThrowArgumentException()
        {
            var request = new OrderPlaced().Build().WithInvalidOrderId();

            var ex = await Assert.ThrowsAsync<ArgumentException>(() => _sut.SubmitOrder(request));

            Assert.Equal("OrderID is required", ex.Message);
            _mockOrderPersistenceGateway.Verify(s => s.SubmitOrder(It.IsAny<OrderDetailsPersistence>()), Times.Never());
        }

        [Fact]
        public async void Try_SubmitOrder_Order_WithoutProductType_Then_ThrowArgumentException()
        {
            var request = new OrderPlaced().Build().WithoutProductType();

            var ex = await Assert.ThrowsAsync<ArgumentException>(() => _sut.SubmitOrder(request));

            Assert.Equal("ProductType is required", ex.Message);
            _mockOrderPersistenceGateway.Verify(s => s.SubmitOrder(It.IsAny<OrderDetailsPersistence>()), Times.Never());
        }

        [Fact]
        public async void Try_SubmitOrder_Order_WithInvalidProductType_Then_ThrowArgumentException()
        {
            var request = new OrderPlaced().Build().WithInvalidProductType();

            var ex = await Assert.ThrowsAsync<ArgumentException>(() => _sut.SubmitOrder(request));

            Assert.Equal("ProductType is invalid", ex.Message);
            _mockOrderPersistenceGateway.Verify(s => s.SubmitOrder(It.IsAny<OrderDetailsPersistence>()), Times.Never());
        }

        [Fact]
        public async void Try_SubmitOrder_Order_WithoutQuantity_Then_ThrowArgumentException()
        {
            var request = new OrderPlaced().Build().WithoutQuantity();

            var ex = await Assert.ThrowsAsync<ArgumentException>(() => _sut.SubmitOrder(request));

            Assert.Equal("Quantity is required", ex.Message);
            _mockOrderPersistenceGateway.Verify(s => s.SubmitOrder(It.IsAny<OrderDetailsPersistence>()), Times.Never());
        }
    }
}