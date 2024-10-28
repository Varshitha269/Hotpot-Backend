using HotpotLibrary.DTO;
using HotpotLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotpotLibrary.Interfaces
{
    public interface ICartInterface
    {
        CartDTO GetCartById(int id);

        IEnumerable<CartDTO> GetAllCarts();
        void CreateCart(CartDTO cartDto);
        void UpdateCart(int id, CartDTO cartDto);
        void DeleteCart(int id);
        IEnumerable<CartDTO> GetCartItemsByUserId(int userId);

        public void DeleteCartByUserId(int userId);
    }
}
