using SmartStore.BusinessLogic.DBModel;
using SmartStore.BusinessLogic.DBModel.Seed;
using SmartStore.Domain.Entities.Products;
using SmartStore.Domain.Entities.Responses;
using SmartStore.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI;

namespace SmartStore.BusinessLogic.Core
{
    public class AdminAPI
    {
        public AdminAPI() { }
        public ResponseNewProduct CreateNewProduct(ProductData products)
        {
            if (products == null)
            {
                string Error = "Error to add product";
                return new ResponseNewProduct { Status = false, Message = Error };
            }


            var product = new DBProduct()
            {
                ProductName = products.ProductName,
                ProductPicture = products.ProductPicture,
                ProductCategory = products.ProductCategory, // Используем цену с учетом скидки
                ProductDescription = products.ProductDescription,
                ProductPrice = products.ProductPrice,
                ID = products.ID,
                isAvailable = products.isAvailable,


            };

            using (var db = new ProductContext())
            {
                db.Products.Add(product);
                db.SaveChanges();
            }

            return new ResponseNewProduct { Status = true };
        }
        public async Task<ResponseSupport> DeleteUserAccountAction(int userId)
        {
            try
            {
                using (var db = new UserContext())
                {

                    var userToDelete = await db.Users.FindAsync(userId);
                    if (userToDelete.Level == Domain.Enums.URole.Admin)
                    {
                        return new ResponseSupport
                        {
                            Status = false,
                            ResponseSupportMessage = "Error"
                        };
                    }
                    if (userToDelete != null)
                    {
                        db.Users.Remove(userToDelete);
                        await db.SaveChangesAsync();
                        return new ResponseSupport
                        {
                            Status = true
                        };
                    }
                    else
                    {
                        return new ResponseSupport
                        {
                            Status = false,
                            ResponseSupportMessage = "User not found"
                        };
                    }
                }
            }
            catch (Exception ex)
            {
                // Обработка ошибок, если необходимо
                // Здесь можно добавить логику для записи информации об ошибке
                return new ResponseSupport
                {
                    Status = false,
                    ResponseSupportMessage = ex.Message // Используем сообщение об ошибке как статусное сообщение
                };
            }
        }


        public async Task<ResponseSupport> ChangeUserRoleAction(int userId, string newRole)
        {
            try
            {
                using (var db = new UserContext())
                {
                    var user = await db.Users.FindAsync(userId); // Находим пользователя по его идентификатору

                    if (user != null)
                    {
                        URole NewRole = (URole)Enum.Parse(typeof(URole), newRole);
                        if (user.Level == URole.Admin)
                        {
                            return new ResponseSupport
                            { Status = false, ResponseSupportMessage = "Admin role cannot be changed for other admins." };
                        }
                        if (NewRole == URole.Admin)
                        {
                            user.Level = URole.Admin;
                        }
                        if (NewRole == URole.Moder)
                        {
                            user.Level = URole.Moder;
                        }
                        if (NewRole == URole.User)
                        {
                            user.Level = URole.User;
                        }
                        if (NewRole == URole.None)
                        {
                            user.Level = URole.None;
                        }

                        // Сохраняем изменения в базе данных
                        await db.SaveChangesAsync();

                        // Возвращаем успешный результат
                        return new ResponseSupport
                        {
                            Status = true,
                            ResponseSupportMessage = "User role successfully changed."
                        };
                    }
                    else
                    {
                        // Если пользователь не найден, возвращаем сообщение об ошибке
                        return new ResponseSupport
                        {
                            Status = false,
                            ResponseSupportMessage = "User not found."
                        };
                    }
                }
            }
            catch (Exception ex)
            {
                // Обработка ошибок, если необходимо
                // Здесь можно добавить логику для записи информации об ошибке
                return new ResponseSupport
                {
                    Status = false,
                    ResponseSupportMessage = "An error occurred while changing user role: " + ex.Message
                };
            }
        }

        public async Task<ResponseSupport> GetAdminPanelUsersAction(int currentUserId)
        {
            try
            {
                using (var db = new UserContext())
                {
                    var totalUsers = await db.Users.Where(u => u.ID != currentUserId).ToListAsync(); // Фильтруем пользователей, исключая текущего пользователя

                    if (totalUsers != null)
                    {
                        return new ResponseSupport // Создаем объект ResponseSupport
                        {
                            Status = true,
                            TotalUsers = totalUsers, // Передаем список пользователей
                        };
                    }
                    else
                    {
                        return new ResponseSupport // Создаем объект ResponseSupport
                        {
                            Status = true,
                            ResponseSupportMessage = "You registerew now"
                        };
                    }
                }
            }
            catch (Exception ex)
            {
                // Обработка ошибок, если необходимо
                // Здесь можно добавить логику для записи информации об ошибке
                return new ResponseSupport // Создаем объект ResponseSupport
                {
                    Status = false,
                    ResponseSupportMessage = ex.Message // Используем сообщение об ошибке как статусное сообщение
                };
            }
        }
    }
}
