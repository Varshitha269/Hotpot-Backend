using NUnit.Framework;
using HotpotLibrary.DTO;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using HotpotLibrary.Interfaces;
using HotpotLibrary.Services;
using HotpotLibrary.Repository;
using HotpotLibrary.Data;
using Microsoft.EntityFrameworkCore;
using HotpotLibrary.Models;


namespace Hotpot_nunitTesting
{
    [TestFixture]

    public class Tests
    {
        private AppDbContext _appDbContext;
        private IUserRepository _userRepository;
        private IRestaurantRepository _restaurantRepository;
        private IMenuRepository _menuRepository;
        private IMenuItemRepository _menuItemRepository;
        private IOrderInterface _orderRepository;
        private IFeedbackRatingInterface _feedbackRatingRepository;
        private IOrderDetailInterface _orderDetailsRepository;



        private Mock<IUserRepository> _mockUserRepository;
        private Mock<IRestaurantRepository> _mockRestaurantRepository;
        private Mock<IMenuRepository> _mockMenuRepository;
        private Mock<IMenuItemRepository> _mockMenuItemRepository;
        private Mock<IOrderInterface> _mockOrderRepository;
        private Mock<IOrderDetailInterface> _mockOrderDetailsRepository;
        private Mock<IFeedbackRatingInterface> _mockFeedbackRatingRepository;
        [SetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseSqlServer("Server=EDITH;Database=HotpotFinal;Trusted_Connection=True;TrustServerCertificate=True")
                .Options;
            _appDbContext = new AppDbContext(options);
            _userRepository = new UserRepository(_appDbContext);
            _restaurantRepository= new RestaurantRepository(_appDbContext);
            _menuRepository = new MenuRepository(_appDbContext);
            _menuItemRepository = new MenuItemRepository(_appDbContext);
            _orderRepository = new OrderRepository(_appDbContext);
            _feedbackRatingRepository=new FeedbackRatingRepository(_appDbContext);
            _orderDetailsRepository=new OrderDetailsRepository(_appDbContext);




            _mockUserRepository = new Mock<IUserRepository>();
            _mockRestaurantRepository = new Mock<IRestaurantRepository>();
            _mockMenuRepository = new Mock<IMenuRepository>();
            _mockMenuItemRepository = new Mock<IMenuItemRepository>();
            _mockOrderRepository = new Mock<IOrderInterface>();
            _mockOrderDetailsRepository = new Mock<IOrderDetailInterface>();
            _mockFeedbackRatingRepository = new Mock<IFeedbackRatingInterface>();
        }
        #region unittesting
        //[Test]
        //public void TestGetAllUsers()
        //{
        //    IEnumerable<UserDTO> result = _userRepository.GetAllUsers();
        //    Assert.IsNotNull(result);
        //    Assert.IsTrue(result.Any(), "There should be at least one restaurant in the result.");
        //}
        //[Test]
        //public void TestGetAllRestaurants()
        //{
        //    // Act
        //    var restaurants = _restaurantRepository.GetAllRestaurants();

        //    // Assert
        //    Assert.IsNotNull(restaurants, "The result should not be null.");
        //    Assert.IsTrue(restaurants.Any(), "There should be at least one restaurant in the result.");
        //}

        //[Test]
        //public void TestGetAllMenus()
        //{
        //    // Log information
        //    Console.WriteLine("Retrieving all menus from the repository.");

        //    // Act
        //    var menus = _menuRepository.GetAllMenus();

        //    // Assert
        //    Assert.IsNotNull(menus, "The result should not be null.");
        //    Assert.IsTrue(menus.Any(), "There should be at least one menu in the result.");
        //    Console.WriteLine($"Retrieved {menus.Count()} menus successfully.");
        //}
        //[Test]
        //public void TestGetAllMenuItems()
        //{
        //    // Act
        //    var menuItems = _menuItemRepository.GetAllMenuItems();

        //    // Assert
        //    Assert.IsNotNull(menuItems, "The result should not be null.");
        //    Assert.IsTrue(menuItems.Any(), "There should be at least one menu item in the result.");
        //}

        //[Test]
        //public void TestGetAllOrders()
        //{
        //    // Act
        //    var orders = _orderRepository.GetAllOrders();

        //    // Assert
        //    Assert.IsNotNull(orders, "The result should not be null.");
        //    Assert.IsTrue(orders.Any(), "There should be at least one order in the result.");
        //}

        //[Test]
        //public void TestGetAllOrderDetails()
        //{
        //    // Act
        //    var orderDetails = _orderDetailsRepository.GetAllOrderDetails();

        //    // Assert
        //    Assert.IsNotNull(orderDetails, "The result should not be null.");
        //    Assert.IsTrue(orderDetails.Any(), "There should be at least one order detail in the result.");
        //}

        //[Test]
        //public void TestGetAllFeedbackRatings()
        //{
        //    // Act
        //    var feedbackRatings = _feedbackRatingRepository.GetAllFeedbackRatings();

        //    // Assert
        //    Assert.IsNotNull(feedbackRatings, "The result should not be null.");
        //    Assert.IsTrue(feedbackRatings.Any(), "There should be at least one feedback rating in the result.");
        //}
        #endregion


        #region moqTesting
        [Test]
        public void TestGetAllUsers1()
        {
            // Arrange
            var mockUsers = new List<UserDTO> { new UserDTO { Username = "TestUser" } };
            _mockUserRepository.Setup(repo => repo.GetAllUsers()).Returns(mockUsers);

            // Act
            var result = _mockUserRepository.Object.GetAllUsers();

            // Assert
            Assert.IsNotNull(result);
            Assert.IsTrue(result.Any(), "There should be at least one user in the result.");
        }

        [Test]
        public void TestGetAllRestaurants1()
        {
            // Arrange
            var mockRestaurants = new List<RestaurantDTO> { new RestaurantDTO { Name = "TestRestaurant" } };
            _mockRestaurantRepository.Setup(repo => repo.GetAllRestaurants()).Returns(mockRestaurants);

            // Act
            var restaurants = _mockRestaurantRepository.Object.GetAllRestaurants();

            // Assert
            Assert.IsNotNull(restaurants, "The result should not be null.");
            Assert.IsTrue(restaurants.Any(), "There should be at least one restaurant in the result.");
        }

        [Test]
        public void TestGetAllMenus1()
        {
            // Arrange
            var mockMenus = new List<MenuDTO> { new MenuDTO { MenuName = "TestMenu" } };
            _mockMenuRepository.Setup(repo => repo.GetAllMenus()).Returns(mockMenus);

            // Act
            var menus = _mockMenuRepository.Object.GetAllMenus();

            // Assert
            Assert.IsNotNull(menus, "The result should not be null.");
            Assert.IsTrue(menus.Any(), "There should be at least one menu in the result.");
        }

        [Test]
        public void TestGetAllMenuItems1()
        {
            // Arrange
            var mockMenuItems = new List<MenuItemDTO> { new MenuItemDTO { ItemName = "TestMenuItem" } };
            _mockMenuItemRepository.Setup(repo => repo.GetAllMenuItems()).Returns(mockMenuItems);

            // Act
            var menuItems = _mockMenuItemRepository.Object.GetAllMenuItems();

            // Assert
            Assert.IsNotNull(menuItems, "The result should not be null.");
            Assert.IsTrue(menuItems.Any(), "There should be at least one menu item in the result.");
        }

        [Test]
        public void TestGetAllOrders1()
        {
            // Arrange
            var mockOrders = new List<OrderDTO> { new OrderDTO { OrderID = 1 } };
            _mockOrderRepository.Setup(repo => repo.GetAllOrders()).Returns(mockOrders);

            // Act
            var orders = _mockOrderRepository.Object.GetAllOrders();

            // Assert
            Assert.IsNotNull(orders, "The result should not be null.");
            Assert.IsTrue(orders.Any(), "There should be at least one order in the result.");
        }

        [Test]
        public void TestGetAllOrderDetails1()
        {
            // Arrange
            var mockOrderDetails = new List<OrderDetailDTO> { new OrderDetailDTO { OrderID = 1, MenuItemID = 1 } };
            _mockOrderDetailsRepository.Setup(repo => repo.GetAllOrderDetails()).Returns(mockOrderDetails);

            // Act
            var orderDetails = _mockOrderDetailsRepository.Object.GetAllOrderDetails();

            // Assert
            Assert.IsNotNull(orderDetails, "The result should not be null.");
            Assert.IsTrue(orderDetails.Any(), "There should be at least one order detail in the result.");
        }

        [Test]
        public void TestGetAllFeedbackRatings1()
        {
            // Arrange
            var mockFeedbackRatings = new List<FeedbackRatingDTO> { new FeedbackRatingDTO { Rating = 5 } };
            _mockFeedbackRatingRepository.Setup(repo => repo.GetAllFeedbackRatings()).Returns(mockFeedbackRatings);

            // Act
            var feedbackRatings = _mockFeedbackRatingRepository.Object.GetAllFeedbackRatings();

            // Assert
            Assert.IsNotNull(feedbackRatings, "The result should not be null.");
            Assert.IsTrue(feedbackRatings.Any(), "There should be at least one feedback rating in the result.");
        }
        #endregion




        #region TestUser
        //[Test]
        //public void TestGetAllUsers()
        //{
        //    IEnumerable<UserDTO> result = _userRepository.GetAllUsers();
        //    Assert.IsNotNull(result);
        //    Assert.AreEqual(28, result.Count());
        //}

        //[TestCase(1)]
        //public void TestGetUserById(int id)
        //{
        //    var expectedUser = new UserDTO()
        //    {
        //        UserID = 1,
        //        Username = "Ravi Kumar",
        //        Email= "ravi.kumar@gmail.com",
        //        Password= "pass123",
        //        PhNo= "9876543210",
        //        AddressLine= "123 Street A",
        //        City= "Chennai",
        //        State= "Tamil Nadu",
        //        PostalCode= "600001",
        //        Country= "India",
        //        Role= "user",
        //        CreatedDate= DateTime.Parse("2024-09-10 10:33:32.403"),
        //        IsActive=true
        //    };
        //    var result = _userRepository.GetUserById(id);

        //    Assert.AreEqual(expectedUser.UserID, result.UserID);
        //    Assert.AreEqual(expectedUser.Username, result.Username);
        //    Assert.AreEqual(expectedUser.Email, result.Email);
        //    Assert.AreEqual(expectedUser.Password, result.Password);
        //    Assert.AreEqual(expectedUser.PhNo, result.PhNo);
        //    Assert.AreEqual(expectedUser.AddressLine, result.AddressLine);
        //    Assert.AreEqual(expectedUser.City, result.City);
        //    Assert.AreEqual(expectedUser.State, result.State);
        //    Assert.AreEqual(expectedUser.PostalCode, result.PostalCode);
        //    Assert.AreEqual(expectedUser.Country, result.Country);
        //    Assert.AreEqual(expectedUser.Role, result.Role);
        //    Assert.AreEqual(expectedUser.CreatedDate, result.CreatedDate);
        //    Assert.AreEqual(expectedUser.IsActive, result.IsActive);
        //}

        //[Test]
        //public void TestAddUser()
        //{

        //    var newUser = new UserDTO()
        //    {
        //        UserID = 0,
        //        Username = "John Doe4",
        //        Email = "john4.doe@example.com",
        //        Password = "securepassword",
        //        PhNo = "1234567890",
        //        AddressLine = "456 Avenue B",
        //        City = "New York",
        //        State = "NY",
        //        PostalCode = "10001",
        //        Country = "USA",
        //        Role = "user",
        //        CreatedDate = DateTime.Now, 
        //        IsActive = true
        //    };

        //    // Act
        //    _userRepository.CreateUser(newUser);


        //    var addedUser = _userRepository.GetUserByEmail(newUser.Email);

        //    // Assert

        //    Assert.AreEqual(newUser.Username, addedUser.Username);
        //    Assert.AreEqual(newUser.Email, addedUser.Email);
        //    Assert.AreEqual(newUser.Password, addedUser.Password);
        //    Assert.AreEqual(newUser.PhNo, addedUser.PhNo);
        //    Assert.AreEqual(newUser.AddressLine, addedUser.AddressLine);
        //    Assert.AreEqual(newUser.City, addedUser.City);
        //    Assert.AreEqual(newUser.State, addedUser.State);
        //    Assert.AreEqual(newUser.PostalCode, addedUser.PostalCode);
        //    Assert.AreEqual(newUser.Country, addedUser.Country);
        //    Assert.AreEqual(newUser.Role, addedUser.Role);
        //    Assert.AreEqual(newUser.CreatedDate.ToShortDateString(), addedUser.CreatedDate.ToShortDateString());
        //    Assert.AreEqual(newUser.IsActive, addedUser.IsActive);
        //}


        //[Test]
        //public void TestToUpdateUser()
        //{
        //    var updatedUser = new UserDTO()
        //    {
        //        UserID = 32, 
        //        Username = "Jane Smith5", 
        //        Email = "jane5.smith@example.com",
        //        Password = "newpassword",
        //        PhNo = "1234567890",
        //        AddressLine = "123 New Address",
        //        City = "San Francisco",
        //        State = "CA",
        //        PostalCode = "94101",
        //        Country = "USA",
        //        Role = "admin",
        //        CreatedDate = DateTime.Now,
        //        IsActive = true 
        //    };


        //    _userRepository.UpdateUser(updatedUser.UserID, updatedUser);


        //    var result = _userRepository.GetUserById(updatedUser.UserID);

        //    Assert.IsNotNull(result, "The updated user should not be null.");
        //    Assert.AreEqual(updatedUser.Username, result.Username, "Username was not updated correctly.");
        //    Assert.AreEqual(updatedUser.Email, result.Email, "Email was not updated correctly.");
        //    Assert.AreEqual(updatedUser.Password, result.Password, "Password was not updated correctly.");
        //    Assert.AreEqual(updatedUser.PhNo, result.PhNo, "Phone number was not updated correctly.");
        //    Assert.AreEqual(updatedUser.AddressLine, result.AddressLine, "AddressLine was not updated correctly.");
        //    Assert.AreEqual(updatedUser.City, result.City, "City was not updated correctly.");
        //    Assert.AreEqual(updatedUser.State, result.State, "State was not updated correctly.");
        //    Assert.AreEqual(updatedUser.PostalCode, result.PostalCode, "PostalCode was not updated correctly.");
        //    Assert.AreEqual(updatedUser.Country, result.Country, "Country was not updated correctly.");
        //    Assert.AreEqual(updatedUser.Role, result.Role, "Role was not updated correctly.");
        //    Assert.AreEqual(updatedUser.CreatedDate.ToShortDateString(), result.CreatedDate.ToShortDateString(), "CreatedDate should not be updated.");
        //    Assert.AreEqual(updatedUser.IsActive, result.IsActive, "IsActive status was not updated correctly.");
        //}

        //[TestCase(31)]
        //public void TestToDeleteUser(int id)
        //{
        //    _userRepository.DeleteUser(id);
        //    var deletedtask = _userRepository.GetUserById(id);
        //    Assert.IsNull(deletedtask);


        //} 
        #endregion

        #region Test Restaruant
        //[Test]
        //public void TestAddRestaurant()
        //{
        //    // Arrange
        //    var newRestaurant = new RestaurantDTO
        //    {
        //        RestaurantID = 0, // ID will be generated by the database
        //        Name = "Test Restaurant",
        //        Description = "This is a test restaurant.",
        //        PhNo = "1234567890",
        //        Email = "testrestaurant@example.com",
        //        OperatingHours = "10:00 AM - 9:00 PM",
        //        AddressLine = "123 Test Street",
        //        City = "Test City",
        //        State = "Test State",
        //        PostalCode = "12345",
        //        Country = "Test Country",
        //        CreatedDate = DateTime.Now,
        //        IsActive = true
        //    };

        //    // Act
        //    _restaurantRepository.CreateRestaurant(newRestaurant);

        //    // Assert
        //    var addedRestaurant = _restaurantRepository.GetRestaurantByEmail(newRestaurant.Email);
        //    Assert.IsNotNull(addedRestaurant, "The restaurant should have been added.");
        //    Assert.AreEqual(newRestaurant.Name, addedRestaurant.Name, "Restaurant name does not match.");
        //    Assert.AreEqual(newRestaurant.Email, addedRestaurant.Email, "Restaurant email does not match.");
        //}

        //[Test]
        //public void TestDeleteRestaurant()
        //{
        //    // Arrange
        //    int restaurantId = 24; // Assuming this restaurant exists in the database

        //    // Act
        //    _restaurantRepository.DeleteRestaurant(restaurantId);

        //    // Assert
        //    var deletedRestaurant = _restaurantRepository.GetRestaurantById(restaurantId);
        //    Assert.IsNull(deletedRestaurant, "The restaurant should have been deleted.");
        //}

        //[Test]
        //public void TestUpdateRestaurant()
        //{
        //    // Arrange
        //    var updatedRestaurant = new RestaurantDTO
        //    {
        //        RestaurantID = 25, // Assuming this restaurant exists in the database
        //        Name = "Updated Restaurant",
        //        Description = "Updated description.",
        //        PhNo = "0987654321",
        //        Email = "updatedrestaurant@example.com",
        //        OperatingHours = "9:00 AM - 11:00 PM",
        //        AddressLine = "456 Updated Street",
        //        City = "Updated City",
        //        State = "Updated State",
        //        PostalCode = "67890",
        //        Country = "Updated Country",
        //        CreatedDate = DateTime.Now,
        //        IsActive = true
        //    };

        //    // Act
        //    _restaurantRepository.UpdateRestaurant(updatedRestaurant.RestaurantID, updatedRestaurant);

        //    // Assert
        //    var result = _restaurantRepository.GetRestaurantById(updatedRestaurant.RestaurantID);
        //    Assert.IsNotNull(result, "The restaurant should exist.");
        //    Assert.AreEqual(updatedRestaurant.Name, result.Name, "Restaurant name was not updated.");
        //    Assert.AreEqual(updatedRestaurant.Email, result.Email, "Restaurant email was not updated.");
        //    Assert.AreEqual(updatedRestaurant.Description, result.Description, "Restaurant description was not updated.");
        //}

        //[Test]
        //public void TestGetAllRestaurants()
        //{
        //    // Act
        //    var restaurants = _restaurantRepository.GetAllRestaurants();

        //    // Assert
        //    Assert.IsNotNull(restaurants, "The result should not be null.");
        //    Assert.IsTrue(restaurants.Any(), "There should be at least one restaurant in the result.");
        //}

        //[TestCase(1)] // Use an ID that exists in your test data
        //public void TestGetRestaurantById(int restaurantId)
        //{
        //    // Act
        //    var result = _restaurantRepository.GetRestaurantById(restaurantId);

        //    // Assert
        //    Assert.IsNotNull(result, "The restaurant should exist.");
        //    Assert.AreEqual(restaurantId, result.RestaurantID, "Restaurant ID does not match.");
        //} 
        #endregion

        #region Test Menu

        //[Test]
        //public void TestAddMenu()
        //{
        //    // Arrange
        //    var newMenu = new MenuDTO
        //    {
        //        MenuID = 0, // ID will be generated by the database
        //        RestaurantID = 10, // Assuming this restaurant exists
        //        MenuName = "Test Menu",
        //        Description = "This is a test menu.",
        //        CreatedDate = DateTime.Now,
        //        IsActive = true
        //    };


        //    // Act
        //    _menuRepository.CreateMenu(newMenu);

        //    // Assert
        //    var addedMenu = _menuRepository.GetAllMenus().Last();
        //    Assert.IsNotNull(addedMenu, "The menu should have been added.");
        //    Assert.AreEqual(newMenu.MenuName, addedMenu.MenuName, "Menu name does not match.");
        //    Console.WriteLine("Menu successfully added and verified.");
        //}

        //[Test]
        //public void TestDeleteMenu()
        //{
        //    // Arrange
        //    int menuId = 22; // Assuming this menu exists in the database



        //    // Act
        //    _menuRepository.DeleteMenu(menuId);

        //    // Assert
        //    var deletedMenu = _menuRepository.GetMenuById(menuId);
        //    Assert.IsNull(deletedMenu);

        //}

        //[Test]
        //public void TestUpdateMenu()
        //{
        //    // Arrange
        //    var updatedMenu = new MenuDTO
        //    {
        //        MenuID = 21, // Assuming this menu exists in the database
        //        RestaurantID = 10,
        //        MenuName = "Updated Menu",
        //        Description = "Updated description.",
        //        CreatedDate = DateTime.Now,
        //        IsActive = true
        //    };

        //    // Log information
        //    Console.WriteLine($"Updating menu with ID: {updatedMenu.MenuID}");

        //    // Act
        //    _menuRepository.UpdateMenu(updatedMenu.MenuID, updatedMenu);

        //    // Assert
        //    var result = _menuRepository.GetMenuById(updatedMenu.MenuID);
        //    Assert.IsNotNull(result, "The menu should exist.");
        //    Assert.AreEqual(updatedMenu.MenuName, result.MenuName, "Menu name was not updated.");
        //    Assert.AreEqual(updatedMenu.Description, result.Description, "Menu description was not updated.");
        //    Console.WriteLine("Menu successfully updated and verified.");
        //}

        //[Test]
        //public void TestGetAllMenus()
        //{
        //    // Log information
        //    Console.WriteLine("Retrieving all menus from the repository.");

        //    // Act
        //    var menus = _menuRepository.GetAllMenus();

        //    // Assert
        //    Assert.IsNotNull(menus, "The result should not be null.");
        //    Assert.IsTrue(menus.Any(), "There should be at least one menu in the result.");
        //    Console.WriteLine($"Retrieved {menus.Count()} menus successfully.");
        //}

        //[TestCase(1)] // Use an ID that exists in your test data
        //public void TestGetMenuById(int menuId)
        //{
        //    // Log information
        //    Console.WriteLine($"Retrieving menu with ID: {menuId}");

        //    // Act
        //    var result = _menuRepository.GetMenuById(menuId);

        //    // Assert
        //    Assert.IsNotNull(result, "The menu should exist.");
        //    Assert.AreEqual(menuId, result.MenuID, "Menu ID does not match.");
        //    Console.WriteLine($"Menu with ID: {menuId} successfully retrieved.");
        //}

        #endregion

        #region Test MenuItem

        //[Test]
        //public void TestAddMenuItem()
        //{
        //    // Arrange
        //    var newMenuItem = new MenuItemDTO
        //    {
        //        MenuItemID = 0, // ID will be generated by the database
        //        MenuID = 1, // Assuming this menu exists
        //        ItemName = "Test Menu Item",
        //        Description = "This is a test menu item.",
        //        Category = "Appetizer",
        //        Price = 10.99M,
        //        SpecialDietaryInfo = "Vegan",
        //        TasteInfo = "Spicy",
        //        NutritionalInfo = "200 Calories",
        //        AvailabilityTime = "10:00 AM - 9:00 PM",
        //        IsInStock = true,
        //        ImageUrl = "http://example.com/image.jpg",
        //        CreatedDate = DateTime.Now,
        //        IsAvailable = true,
        //        Discounts = 0
        //    };

        //    // Act
        //    _menuItemRepository.CreateMenuItem(newMenuItem);

        //    // Assert
        //    var addedMenuItem = _menuItemRepository.GetAllMenuItems().Last();
        //    Assert.IsNotNull(addedMenuItem, "The menu item should have been added.");
        //    Assert.AreEqual(newMenuItem.ItemName, addedMenuItem.ItemName, "Menu item name does not match.");
        //    Assert.AreEqual(newMenuItem.Price, addedMenuItem.Price, "Menu item price does not match.");
        //}

        //[Test]
        //public void TestDeleteMenuItem()
        //{
        //    // Arrange
        //    int menuItemId = 16; // Assuming this menu item exists in the database

        //    // Act
        //    _menuItemRepository.DeleteMenuItem(menuItemId);

        //    // Assert
        //    var deletedMenuItem = _menuItemRepository.GetMenuItemById(menuItemId);
        //    Assert.IsNull(deletedMenuItem, "The menu item should have been deleted.");
        //}

        //[Test]
        //public void TestUpdateMenuItem()
        //{
        //    // Arrange
        //    var updatedMenuItem = new MenuItemDTO
        //    {
        //        MenuItemID = 15, // Assuming this menu item exists in the database
        //        MenuID = 1,
        //        ItemName = "Updated Menu Item",
        //        Description = "Updated description.",
        //        Category = "Main Course",
        //        Price = 15.99M,
        //        SpecialDietaryInfo = "Veg",
        //        TasteInfo = "Sweet",
        //        NutritionalInfo = "350 Calories",
        //        AvailabilityTime = "11:00 AM - 10:00 PM",
        //        IsInStock = true,
        //        ImageUrl = "http://example.com/updated_image.jpg",
        //        CreatedDate = DateTime.Now,
        //        IsAvailable = true,
        //        Discounts = 5
        //    };

        //    // Act
        //    _menuItemRepository.UpdateMenuItem(updatedMenuItem.MenuItemID, updatedMenuItem);

        //    // Assert
        //    var result = _menuItemRepository.GetMenuItemById(updatedMenuItem.MenuItemID);
        //    Assert.IsNotNull(result, "The menu item should exist.");
        //    Assert.AreEqual(updatedMenuItem.ItemName, result.ItemName, "Menu item name was not updated.");
        //    Assert.AreEqual(updatedMenuItem.Price, result.Price, "Menu item price was not updated.");
        //}

        //[Test]
        //public void TestGetAllMenuItems()
        //{
        //    // Act
        //    var menuItems = _menuItemRepository.GetAllMenuItems();

        //    // Assert
        //    Assert.IsNotNull(menuItems, "The result should not be null.");
        //    Assert.IsTrue(menuItems.Any(), "There should be at least one menu item in the result.");
        //}

        //[TestCase(1)] // Use an ID that exists in your test data
        //public void TestGetMenuItemById(int menuItemId)
        //{
        //    // Act
        //    var result = _menuItemRepository.GetMenuItemById(menuItemId);

        //    // Assert
        //    Assert.IsNotNull(result, "The menu item should exist.");
        //    Assert.AreEqual(menuItemId, result.MenuItemID, "Menu item ID does not match.");
        //}

        #endregion


        #region Test Orders

        //[Test]
        //public void TestAddOrder()
        //{
        //    // Arrange
        //    var newOrder = new OrderDTO
        //    {
        //        UserID = 1,
        //        RestaurantID = 2,
        //        OrderDate = DateTime.Now,
        //        TotalAmount = 250.75m,
        //        OrderStatus = "Pending",
        //        PaymentStatus = "Unpaid",
        //        DeliveryAddress = "123 Test Street",
        //        DeliveryDate = DateTime.Now.AddDays(2)
        //    };

        //    // Act
        //    _orderRepository.CreateOrder(newOrder);

        //    // Assert
        //    var addedOrder = _orderRepository.GetAllOrders().Last();
        //    Assert.IsNotNull(addedOrder, "The order should have been added.");
        //    Assert.AreEqual(newOrder.UserID, addedOrder.UserID, "UserID does not match.");
        //    Assert.AreEqual(newOrder.RestaurantID, addedOrder.RestaurantID, "RestaurantID does not match.");
        //    Assert.AreEqual(newOrder.TotalAmount, addedOrder.TotalAmount, "TotalAmount does not match.");
        //}

        //[Test]
        //public void TestDeleteOrder()
        //{
        //    // Arrange
        //    int orderId =15; // Assuming this order exists in the database

        //    // Act
        //    _orderRepository.DeleteOrder(orderId);

        //    // Assert
        //    var deletedOrder = _orderRepository.GetOrderById(orderId);
        //    Assert.IsNull(deletedOrder, "The order should have been deleted.");
        //}

        //[Test]
        //public void TestUpdateOrder()
        //{
        //    // Arrange
        //    var updatedOrder = new OrderDTO
        //    {
        //        UserID = 1,
        //        RestaurantID = 2,
        //        OrderDate = DateTime.Now.AddDays(-1),
        //        TotalAmount = 300.50m,
        //        OrderStatus = "Completed",
        //        PaymentStatus = "Paid",
        //        DeliveryAddress = "456 Updated Street",
        //        DeliveryDate = DateTime.Now
        //    };

        //    int orderId = 10; // Assuming this order exists in the database

        //    // Act
        //    _orderRepository.UpdateOrder(orderId, updatedOrder);

        //    // Assert
        //    var result = _orderRepository.GetOrderById(orderId);
        //    Assert.IsNotNull(result, "The order should exist.");
        //    Assert.AreEqual(updatedOrder.OrderStatus, result.OrderStatus, "OrderStatus was not updated.");
        //    Assert.AreEqual(updatedOrder.PaymentStatus, result.PaymentStatus, "PaymentStatus was not updated.");
        //    Assert.AreEqual(updatedOrder.TotalAmount, result.TotalAmount, "TotalAmount was not updated.");
        //}

        //[Test]
        //public void TestGetAllOrders()
        //{
        //    // Act
        //    var orders = _orderRepository.GetAllOrders();

        //    // Assert
        //    Assert.IsNotNull(orders, "The result should not be null.");
        //    Assert.IsTrue(orders.Any(), "There should be at least one order in the result.");
        //}

        //[TestCase(1)] // Use an ID that exists in your test data
        //public void TestGetOrderById(int orderId)
        //{
        //    // Act
        //    var result = _orderRepository.GetOrderById(orderId);

        //    // Assert
        //    Assert.IsNotNull(result, "The order should exist.");
        //    Assert.AreEqual(orderId, result.UserID, "Order ID does not match.");
        //}

        #endregion

        #region Test OrderDetails

        //[Test]
        //public void TestAddOrderDetails()
        //{
        //    // Arrange
        //    var newOrderDetail = new OrderDetailDTO
        //    {
        //        OrderID = 1, // Assuming this order exists
        //        MenuItemID = 5, // Assuming this menu item exists
        //        Quantity = 2,
        //        Price = 15.99m,
        //        Subtotal = 31.98m
        //    };

        //    // Act
        //    _orderDetailsRepository.CreateOrderDetails(newOrderDetail);

        //    // Assert
        //    var addedOrderDetail = _orderDetailsRepository.GetOrderDetailsById(newOrderDetail.OrderID);
        //    Assert.IsNotNull(addedOrderDetail, "The order detail should have been added.");
        //    Assert.AreEqual(newOrderDetail.MenuItemID, addedOrderDetail.MenuItemID, "Menu item ID does not match.");
        //    Assert.AreEqual(newOrderDetail.Quantity, addedOrderDetail.Quantity, "Quantity does not match.");
        //    Assert.AreEqual(newOrderDetail.Subtotal, addedOrderDetail.Subtotal, "Subtotal does not match.");
        //}

        //[Test]
        //public void TestDeleteOrderDetails()
        //{
        //    // Arrange
        //    int orderDetailId = 10; // Assuming this order detail exists in the database

        //    // Act
        //    _orderDetailsRepository.DeleteOrderDetails(orderDetailId);

        //    // Assert
        //    var deletedOrderDetail = _orderDetailsRepository.GetOrderDetailsById(orderDetailId);
        //    Assert.IsNull(deletedOrderDetail, "The order detail should have been deleted.");
        //}

        //[Test]
        //public void TestUpdateOrderDetails()
        //{
        //    // Arrange
        //    var updatedOrderDetail = new OrderDetailDTO
        //    {
        //        OrderID = 12, // Assuming this order detail exists
        //        MenuItemID = 7,
        //        Quantity = 3,
        //        Price = 20.99m,
        //        Subtotal = 62.97m
        //    };

        //    // Act
        //    _orderDetailsRepository.UpdateOrderDetails(updatedOrderDetail.OrderID, updatedOrderDetail);

        //    // Assert
        //    var result = _orderDetailsRepository.GetOrderDetailsById(updatedOrderDetail.OrderID);
        //    Assert.IsNotNull(result, "The order detail should exist.");
        //    Assert.AreEqual(updatedOrderDetail.MenuItemID, result.MenuItemID, "Menu item ID was not updated.");
        //    Assert.AreEqual(updatedOrderDetail.Quantity, result.Quantity, "Quantity was not updated.");
        //    Assert.AreEqual(updatedOrderDetail.Subtotal, result.Subtotal, "Subtotal was not updated.");
        //}

        //[Test]
        //public void TestGetAllOrderDetails()
        //{
        //    // Act
        //    var orderDetails = _orderDetailsRepository.GetAllOrderDetails();

        //    // Assert
        //    Assert.IsNotNull(orderDetails, "The result should not be null.");
        //    Assert.IsTrue(orderDetails.Any(), "There should be at least one order detail in the result.");
        //}

        //[TestCase(1)] // Use an ID that exists in your test data
        //public void TestGetOrderDetailsById(int orderDetailId)
        //{
        //    // Act
        //    var result = _orderDetailsRepository.GetOrderDetailsById(orderDetailId);

        //    // Assert
        //    Assert.IsNotNull(result, "The order detail should exist.");
        //    Assert.AreEqual(orderDetailId, result.OrderID, "Order detail ID does not match.");
        //}

        #endregion

        #region Test FeedbackRating

        //[Test]
        //public void TestAddFeedbackRating()
        //{
        //    // Arrange
        //    var newFeedbackRating = new FeedbackRatingDTO
        //    {
        //        FeedbackRatingID = 0, // ID will be generated by the database
        //        UserID = 1,
        //        RestaurantID = 1,
        //        Message = "Great food!",
        //        Rating = 5,
        //        CreatedDate = DateTime.Now
        //    };

        //    // Act
        //    _feedbackRatingRepository.CreateFeedbackRating(newFeedbackRating);

        //    // Assert
        //    var addedFeedbackRating = _feedbackRatingRepository.GetFeedbackRatingById(newFeedbackRating.FeedbackRatingID);
        //    Assert.IsNotNull(addedFeedbackRating, "The feedback rating should have been added.");
        //    Assert.AreEqual(newFeedbackRating.UserID, addedFeedbackRating.UserID, "User ID does not match.");
        //    Assert.AreEqual(newFeedbackRating.RestaurantID, addedFeedbackRating.RestaurantID, "Restaurant ID does not match.");
        //    Assert.AreEqual(newFeedbackRating.Message, addedFeedbackRating.Message, "Message does not match.");
        //    Assert.AreEqual(newFeedbackRating.Rating, addedFeedbackRating.Rating, "Rating does not match.");
        //}

        //[Test]
        //public void TestDeleteFeedbackRating()
        //{
        //    // Arrange
        //    int feedbackRatingId = 1; // Assuming this feedback rating exists in the database

        //    // Act
        //    _feedbackRatingRepository.DeleteFeedbackRating(feedbackRatingId);

        //    // Assert
        //    var deletedFeedbackRating = _feedbackRatingRepository.GetFeedbackRatingById(feedbackRatingId);
        //    Assert.IsNull(deletedFeedbackRating, "The feedback rating should have been deleted.");
        //}

        //[Test]
        //public void TestUpdateFeedbackRating()
        //{
        //    // Arrange
        //    var updatedFeedbackRating = new FeedbackRatingDTO
        //    {
        //        FeedbackRatingID = 1, // Assuming this feedback rating exists in the database
        //        UserID = 2,
        //        RestaurantID = 2,
        //        Message = "Updated feedback.",
        //        Rating = 4,
        //        CreatedDate = DateTime.Now
        //    };

        //    // Act
        //    _feedbackRatingRepository.UpdateFeedbackRating(updatedFeedbackRating.FeedbackRatingID, updatedFeedbackRating);

        //    // Assert
        //    var result = _feedbackRatingRepository.GetFeedbackRatingById(updatedFeedbackRating.FeedbackRatingID);
        //    Assert.IsNotNull(result, "The feedback rating should exist.");
        //    Assert.AreEqual(updatedFeedbackRating.UserID, result.UserID, "User ID was not updated.");
        //    Assert.AreEqual(updatedFeedbackRating.RestaurantID, result.RestaurantID, "Restaurant ID was not updated.");
        //    Assert.AreEqual(updatedFeedbackRating.Message, result.Message, "Message was not updated.");
        //    Assert.AreEqual(updatedFeedbackRating.Rating, result.Rating, "Rating was not updated.");
        //}

        //[Test]
        //public void TestGetAllFeedbackRatings()
        //{
        //    // Act
        //    var feedbackRatings = _feedbackRatingRepository.GetAllFeedbackRatings();

        //    // Assert
        //    Assert.IsNotNull(feedbackRatings, "The result should not be null.");
        //    Assert.IsTrue(feedbackRatings.Any(), "There should be at least one feedback rating in the result.");
        //}

        //[TestCase(1)] // Use an ID that exists in your test data
        //public void TestGetFeedbackRatingById(int feedbackRatingId)
        //{
        //    // Act
        //    var result = _feedbackRatingRepository.GetFeedbackRatingById(feedbackRatingId);

        //    // Assert
        //    Assert.IsNotNull(result, "The feedback rating should exist.");
        //    Assert.AreEqual(feedbackRatingId, result.FeedbackRatingID, "FeedbackRatingID does not match.");
        //}

        #endregion


        [TearDown]
        public void TearDown()
        {
            _appDbContext.Dispose();
        }



    }
       
    
}
