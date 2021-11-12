namespace ProAgil.Domain
{
    public class RedeSocial
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string URL { get; set; }

        //Aqui colocamos a convenção do nome EventoId.. para que o entity framework entenda que esta é uma chave estrangeira
        //por que se nao tiver assim ele ira criar outro campo para relacionar a evento.. como FK.
        //Entao fica o nome da entidade ou nome da tabela(que sera criada) .. mais o Id.
        public int? EventoId { get; set; }

        public Evento Evento { get; }

        //chave estrangeira
        public int? PalestranteId { get; set; }

        public Palestrante Palestrante { get; }

    }
}