using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Fluxor;

namespace CRUDWithFluxor.Features.Users
{
    public static class UserReducer
    {
        // for fetching users
        [ReducerMethod]
        public static UserState onFetchUsers(UserState state, UserActions.FetchUserAction action) =>
            state with
            {
                IsLoading = true,
                ErrorMessage = null
            };
        [ReducerMethod]
        // Fetching users success
        public static UserState onFetchUsersSuccess(UserState state, UserActions.FetchUsersSuccessAction action) =>
            state with
            {
                Users = action.Users,
                IsLoading = false,
                ErrorMessage = null
            };

        [ReducerMethod]
        // Adding new user
        public static UserState onCreateUser(UserState state, UserActions.CreateUserSuccessAction action) =>
            state with
            {
                // 7.
                Users = state.Users.Append(action.CreateUser).ToList(),
                ErrorMessage = null
            };
        // reducer updates the Users state with new data //
        // Deleting user
        [ReducerMethod]
        public static UserState onDeleteUser(UserState state, UserActions.DeleteUserSuccessAction action) =>
            state with
            {
                Users = state.Users.Where(u => u.Id != action.Id).ToList(),
                ErrorMessage = null
            };
    }
}