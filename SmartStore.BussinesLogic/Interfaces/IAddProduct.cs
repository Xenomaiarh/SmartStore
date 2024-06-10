using SmartStore.Domain.Entities.Products;
using SmartStore.Domain.Entities.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartStore.BusinessLogic.Interfaces
{
    public interface IAddProduct
    {
        Task<ResponseSupport> ChangeUserRole(int userId, string newRole);
        ResponseNewProduct CreateNewProduct(ProductData productData);
        Task<ResponseSupport> DeleteUserAction(int userId);
        Task<ResponseSupport> GetAdminPanelUsers(int currentUserId);
    }
}
