using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CRUDWithFluxor.models.Cars;
using Fluxor;

namespace CRUDWithFluxor.Features.Users
{
    public record UserState
    {
        public List<CarDetailModel> Users { get; init; }
        
        public bool IsLoading { get; init; }
        
        public string? ErrorMessage { get; init; }
    }
    
    public class UserFeature : Feature<UserState>
    {
        public override string GetName() => "Users List";
        
        protected override UserState GetInitialState() =>
            new UserState
            {
                Users = new List<CarDetailModel>(),
                IsLoading = false,
                ErrorMessage = null
            };
    }
}