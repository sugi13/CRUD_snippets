using CRUDWithFluxor.models.Cars;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRUDWithFluxor.Features.Users
{
    public class UserActions
    {
        // read

        public record FetchUserAction();

        public record FetchUsersSuccessAction(List<CarDetailModel> Users);
        
        public record FetchUsersFailureAction(string ErrorMessage);

        // create
        public record CreateUserAction(CarDetailModel CreateUser);
        
        public record CreateUserSuccessAction(CarDetailModel CreateUser);

        public record CreateUserFailureAction(string ErrorMessage);
        
        // Delete  
        public record DeleteUserSuccessAction(int Id);

        public record DeleteUserFailureAction(string ErrorMessage);
    }
}