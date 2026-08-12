using CRUDWithFluxor.Services;
using Fluxor;

namespace CRUDWithFluxor.Features.Users
{
    public class UsersEffect
    {
        private readonly UserService _userService;

        public UsersEffect(UserService userService)
        {
            _userService = userService;
        }

        // Fetch Users
        [EffectMethod]
        public async Task FetchUsers(UserActions.FetchUserAction action, IDispatcher dispatcher)
        {
            try
            {
                var users = await _userService.GetUsers(); // this line calls the service.

                dispatcher.Dispatch(
                    new UserActions.FetchUsersSuccessAction(users) // now it dispatch the successaction with data
                );
            }
            catch (Exception ex)
            {
                dispatcher.Dispatch(
                    new UserActions.FetchUsersFailureAction(ex.Message)
                );
            }
        }
        

        // 2. Create User
        [EffectMethod]
        public async Task AddUser(UserActions.CreateUserSuccessAction action, IDispatcher dispatcher)
        {
            try
            {
                // 3.
                var createdUser = await _userService.CreateUser(action.CreateUser);

                if (createdUser != null)
                {
                    dispatcher.Dispatch(
                        new UserActions.CreateUserSuccessAction(createdUser)
                    // after getting the response from service, effect dispatch the success action with the created user data to reducer
                    );
                }
            }
            catch (Exception ex)
            {
                dispatcher.Dispatch(
                    new UserActions.CreateUserFailureAction(ex.Message)
                );
            }
        }

        // Delete User
        [EffectMethod]
        public async Task DeleteUser(UserActions.DeleteUserSuccessAction action, IDispatcher dispatcher)
        {
            try
            {
                await _userService.DeleteUser(action.Id);

                dispatcher.Dispatch(
                    new UserActions.DeleteUserSuccessAction(action.Id)
                );
            }
            catch (Exception ex)
            {
                dispatcher.Dispatch(
                    new UserActions.DeleteUserFailureAction(ex.Message)
                );
            }
        }
    }
}