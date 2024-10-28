namespace CrudUsuario.Models
{
    public class ProdutoModel
    {
        public int Id {  get; set; }
        public string nome { get; set; }

        public string status { get; set; } 

        public string descricao { get; set; } 

        public decimal preco { get; set; }  

        //public int id_Categoria { get; set; }  

        //public List<CategoriaModel> categorias { get; set; } = new List<CategoriaModel>();
    }
}
