using HotpotLibrary.Data;
using HotpotLibrary.DTO;
using HotpotLibrary.Interfaces;
using HotpotLibrary.Models;
using System.Collections.Generic;
using System.Linq;

namespace HotpotLibrary.Repository
{
    public class CartRepository : ICartInterface
    {
        private readonly AppDbContext _context;

        public CartRepository(AppDbContext context)
        {
            _context = context;
        }
        public IEnumerable<CartDTO> GetCartItemsByUserId(int userId)
        {
            return _context.Carts
                .Where(c => c.UserID == userId)
                .Select(c => new CartDTO
                {
                    CartID = c.CartID,
                    UserID = c.UserID,
                    MenuItemID = c.MenuItemID,
                    Quantity = c.Quantity,
                    Price = c.Price,
                    CreatedDate = c.CreatedDate
                })
                .ToList();
        }

        public CartDTO GetCartById(int id)
        {
            var cart = _context.Carts.Find(id);
            if (cart == null) return null;

            return new CartDTO
            {
                CartID = cart.CartID,
                UserID = cart.UserID,
                MenuItemID = cart.MenuItemID,
                Quantity = cart.Quantity,
                Price = cart.Price,
                CreatedDate = (DateTime)cart.CreatedDate
            };
        }

        public IEnumerable<CartDTO> GetAllCarts()
        {
            return _context.Carts.Select(cart => new CartDTO
            {
                CartID = cart.CartID,
                UserID = cart.UserID,
                MenuItemID = cart.MenuItemID,
                Quantity = cart.Quantity,
                Price = cart.Price,
                CreatedDate = (DateTime)cart.CreatedDate
            }).ToList();
        }

        public void CreateCart(CartDTO cartDto)
        {
            var cart = new Cart
            {
                CartID = cartDto.CartID,
                UserID = cartDto.UserID,
                MenuItemID = cartDto.MenuItemID,
                Quantity = cartDto.Quantity,
                Price = cartDto.Price,
                CreatedDate = cartDto.CreatedDate
            };
            _context.Carts.Add(cart);
            _context.SaveChanges();
        }

        public void UpdateCart(int id, CartDTO cartDto)
        {
            var cart = _context.Carts.Find(id);
            if (cart != null)
            {
                cart.UserID = cartDto.UserID;
                cart.MenuItemID = cartDto.MenuItemID;
                cart.Quantity = cartDto.Quantity;
                cart.Price = cartDto.Price;
                cart.CreatedDate = cartDto.CreatedDate;

                _context.Carts.Update(cart);
                _context.SaveChanges();
            }
        }

        public void DeleteCart(int id)
        {
            var cart = _context.Carts.Find(id);
            if (cart != null)
            {
                _context.Carts.Remove(cart);
                _context.SaveChanges();
            }
        }


        public void DeleteCartByUserId(int userId)
        {
            // Retrieve all cart items for the specified user
            var userCarts = _context.Carts.Where(c => c.UserID == userId).ToList();

            // Check if there are any cart items for the user
            if (userCarts.Any())
            {
                // Remove all cart items for the user
                _context.Carts.RemoveRange(userCarts);
                _context.SaveChanges(); // Save changes to the database
            }
            else
            {
                throw new InvalidOperationException("No cart items found for the specified user.");
            }
        }

    }
}
