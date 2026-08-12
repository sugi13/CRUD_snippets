using System;
using System.Linq;
using CRUDWithFluxor.models.Snippets;




namespace CRUDWithFluxor.Features.Snippets
{
    // load data from localStorage
    public record LoadProductAction;

    public record ProductLoadedAction(List<SnippetModel> Products);

    public record SaveProductsAction(List<SnippetModel> Products);
    //----------------------------------------------------------------------------

    // create 
    public record AddProductAction(SnippetModel Product);

    // update
    public record UpdateProductAction(SnippetModel Product);

    public record ProductUpdatedAction(SnippetModel Product);


    // delete
    public record DeleteProductAction(int id);

    public record ProductDeletedAction(int id);
}
