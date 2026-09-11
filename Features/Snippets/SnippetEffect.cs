using CRUDWithFluxor.models.Snippets;
using CRUDWithFluxor.Services;
using Fluxor;
using Serilog;

namespace CRUDWithFluxor.Features.Snippets;

public class SnippetEffect
{
    private const string storageKey = "products";

    private readonly LocalStorageService _localStorage;

    private readonly ILogger<SnippetEffect> _logger; // it tells logging system that, logs comes from the SnippetEffect class

    public SnippetEffect(LocalStorageService localStorage, ILogger<SnippetEffect> logger)
    {
        _localStorage = localStorage;
        _logger = logger;
    }

    // LOAD PRODUCTS

    [EffectMethod]
    public async Task LoadProducts(
        LoadProductAction action,
        IDispatcher dispatcher)
    {

       try
        {
            var products =
           await _localStorage.GetItemAsync<List<SnippetModel>>(
               storageKey);

            products ??= new List<SnippetModel>();

            dispatcher.Dispatch(
                new ProductLoadedAction(products));

            // log info
           _logger.LogInformation("Snippets loaded from local storage. Count: {Count}", products.Count);

            // using serilog
            // Log.Information("Snippets loaded from local storage. Count: {Count}", products.Count);

        }
        catch(Exception ex)
        {
            // using Ilogger
            _logger.LogError(ex, "Failed to load snippets from local storage.");
            // using SeriLog
            // Log.Error(ex, "Failed to load snippets from local storage.");
        }
    }

    // ADD PRODUCT
    [EffectMethod]
    public async Task AddProduct(
        AddProductAction action,
        IDispatcher dispatcher)
    {

        try
        {
            // Get existing products
            var products =
                await _localStorage.GetItemAsync<List<SnippetModel>>(
                    storageKey);

            products ??= new List<SnippetModel>();

            // Add the new product
            products.Add(action.Product);

            // Save the complete list
            await _localStorage.SetItemAsync(
                storageKey,
                products);

            // logging info
            _logger.LogInformation("Snippet added to local storage. Snippet ID: {SnippetId}", action.Product.snippet_Id);

            // using SeriLog
            // Log.Information("Snippet added to Local Storage.");
        }
        catch (Exception ex)
        {
            _logger.LogError("Failed to add snippet to local storage. Error: {ErrorMessage}", ex.Message);

           // Log.Error("Failed to add Snippet to local storage. Error: {ErrorMessage}", ex) ;
        }
    }

    [EffectMethod]
    public async Task UpdateProduct(
        UpdateProductAction action,
        IDispatcher dispatcher)
    {
        try
        {
            // Get existing products
            var products =
                await _localStorage.GetItemAsync<List<SnippetModel>>(
                    storageKey);
            products ??= new List<SnippetModel>();

            var existingSnippet = products.FirstOrDefault(x => x.snippet_Id == action.Product.snippet_Id);
            if (existingSnippet == null)
                return;

            // Update the existing item
            existingSnippet.Title = action.Product.Title;
            existingSnippet.Language = action.Product.Language;
            existingSnippet.Category = action.Product.Category;
            existingSnippet.Tags = action.Product.Tags;
            existingSnippet.Code = action.Product.Code;

            await _localStorage.SetItemAsync(storageKey, products);


            dispatcher.Dispatch(new ProductUpdatedAction(existingSnippet));
        }
        catch (Exception ex)
        {
            Console.WriteLine($"{ex.Message}", ex);
        }
    }

    [EffectMethod]
    public async Task HandleDeleteSnippet(
        DeleteProductAction action,
        IDispatcher dispatcher)
    {
        // 1. Get existing snippets from LocalStorage
        var snippets = await _localStorage.GetItemAsync<List<SnippetModel>>(storageKey)
                       ?? new List<SnippetModel>();

        // 2. Find the snippet
        var snippet = snippets.FirstOrDefault(x => x.snippet_Id == action.id);

        if (snippet == null)
            return;

        // 3. Remove it
        snippets.Remove(snippet);

        // 4. Save updated list back to LocalStorage
        await _localStorage.SetItemAsync(storageKey, snippets);

        // 5. Tell Fluxor that deletion succeeded
        dispatcher.Dispatch(new ProductDeletedAction(action.id));

        Console.WriteLine("Delete Effect Called");
    }
}