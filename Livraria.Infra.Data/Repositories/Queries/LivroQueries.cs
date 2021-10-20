using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Livraria.Infra.Data.Repositories.Queries
{
    public static class LivroQueries
    {
        public static string Inserir = @"Insert into Livro(Nome, Autor, Edicao, Isbn, Imagem) Values (@Nome, @Autor, @Edicao, @Isbn, @Imagem);
                            Select SCOPE_IDENTITY();";
        public static string Atualizar = "Update Livro Set Nome=@Nome, Autor=@Autor, Edicao=@Edicao, Isbn=@Isbn, Imagem=@Imagem Where Id=@Id";

        public static string Excluir = "Delete from Livro Where Id=@Id";

        public static string Listar = @"Select 
                                Id as Id,
                                Nome as Nome,
                                Autor as Autor, 
                                Edicao as Edicao,
                                Isbn as Isbn, 
                                Imagem as Imagem
                            From Livro
                            Order by Nome";

        public static string Obter = @"Select
                                Id as Id,
                                Nome as Nome,
                                Autor as Autor, 
                                Edicao as Edicao,
                                Isbn as Isbn, 
                                Imagem as Imagem
                            From Livro
                            Where Id=@id";

        public static string CheckId = "Select Id from Livro Where Id=@Id";
    }
}
