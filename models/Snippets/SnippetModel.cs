using System.ComponentModel.DataAnnotations;

namespace CRUDWithFluxor.models.Snippets
{
    public class SnippetModel
    {
        public int snippet_Id { get; set; } = 0;

        [Required(ErrorMessage = "Title is required")]
        public string Title { get; set; } = string.Empty;

        [Required(ErrorMessage = "Language is required")]
        public string Language { get; set; } = "";

        [Required(ErrorMessage = "Category is required")]
        public string Category { get; set; } = "";

        public string Tags { get; set; } = string.Empty;

        [Required(ErrorMessage = "Description is required")]
        public string Description { get; set; } = string.Empty;

        [Required(ErrorMessage = "Code is required")]
        public string Code { get; set; } = string.Empty;
    }

    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; } = "";
    }

    public class Language
    {
        public int Id { get; set; }
        public string Name { get; set; } = "";
    }

}
