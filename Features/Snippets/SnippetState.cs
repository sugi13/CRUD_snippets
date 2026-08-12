using CRUDWithFluxor.models.Cars;
using CRUDWithFluxor.models.Snippets;
using Fluxor;
using System;



namespace CRUDWithFluxor.Features.Snippets
{
    [FeatureState]
    public class SnippetState
    {
       public List<SnippetModel> Products { get; set; }


        public SnippetState()
        {
            Products = new List<SnippetModel>();
        }

        public SnippetState(List<SnippetModel> products)
        {
            Products = products;
        }
    }
}
