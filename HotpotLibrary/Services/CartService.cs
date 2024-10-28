using HotpotLibrary.DTO;
using HotpotLibrary.Interfaces;
using log4net;
using System;
using System.Collections.Generic;

namespace HotpotLibrary.Services
{
    public class CartService
    {
        private readonly ICartInterface _cartRepository;
        private static readonly ILog _logger = LogManager.GetLogger(typeof(CartService));

        public CartService(ICartInterface cartRepository)
        {
            _cartRepository = cartRepository;
        }

        public CartDTO GetCartById(int id)
        {
            try
            {
                _logger.Info($"Fetching cart with ID {id}.");
                return _cartRepository.GetCartById(id);
            }
            catch (Exception ex)
            {
                _logger.Error($"An error occurred while fetching cart with ID {id}.", ex);
                throw; // Re-throw the exception after logging
            }
        }

        public IEnumerable<CartDTO> GetAllCarts()
        {
            try
            {
                _logger.Info("Fetching all carts.");
                return _cartRepository.GetAllCarts();
            }
            catch (Exception ex)
            {
                _logger.Error("An error occurred while fetching all carts.", ex);
                throw;
            }
        }

        public void CreateCart(CartDTO cartDto)
        {
            if (cartDto == null)
            {
                throw new ArgumentNullException(nameof(cartDto), "CartDTO cannot be null.");
            }

            try
            {
                _logger.Info($"Creating a cart with ID {cartDto.CartID}.");
                _cartRepository.CreateCart(cartDto);
            }
            catch (Exception ex)
            {
                _logger.Error($"An error occurred while creating cart with ID {cartDto.CartID}.", ex);
                throw;
            }
        }

        public void UpdateCart(int id, CartDTO cartDto)
        {
            if (cartDto == null)
            {
                throw new ArgumentNullException(nameof(cartDto), "CartDTO cannot be null.");
            }

            try
            {
                _logger.Info($"Updating cart with ID {id}.");
                _cartRepository.UpdateCart(id, cartDto);
            }
            catch (Exception ex)
            {
                _logger.Error($"An error occurred while updating cart with ID {id}.", ex);
                throw;
            }
        }

        public void DeleteCart(int id)
        {
            try
            {
                _logger.Info($"Deleting cart with ID {id}.");
                _cartRepository.DeleteCart(id);
            }
            catch (Exception ex)
            {
                _logger.Error($"An error occurred while deleting cart with ID {id}.", ex);
                throw;
            }
        }
    }
}
