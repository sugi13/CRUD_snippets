using CRUDWithFluxor.models.Snippets;
using Fluxor;
using System;
using System.Linq;
using System.Reflection;

namespace CRUDWithFluxor.Features.Snippets
{
    public static class SnippetReducer
    {
        // get
        [ReducerMethod]
        public static SnippetState LoadProducts(
       SnippetState state,
       ProductLoadedAction action)
        {
            return new SnippetState(action.Products);
        }
        // add
        [ReducerMethod]
        public static SnippetState AddProduct(
            SnippetState state,
            AddProductAction action)
        {
            var products = new List<SnippetModel>(state.Products);

            products.Add(action.Product);

            return new SnippetState(products);
        }
        // update
        [ReducerMethod]
        public static SnippetState UpdateProduct(
            SnippetState state,
            UpdateProductAction action)
        {
            var products = new List<SnippetModel>(state.Products);

            var index = products.FindIndex(
                x => x.snippet_Id == action.Product.snippet_Id);

            if (index >= 0)
            {
                products[index] = action.Product;
            }

            return new SnippetState(products);
        }
        // delete - for state management, we will remove the product from the list and return a new state
        [ReducerMethod]
        public static SnippetState DeleteProduct(
            SnippetState state,
            DeleteProductAction action)
        {
            var products = new List<SnippetModel>(state.Products);

            products.RemoveAll(
                x => x.snippet_Id == action.id);

            return new SnippetState(products);
        }

    }
}