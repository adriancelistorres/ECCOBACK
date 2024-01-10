namespace EccoBack.Entities
{
    public class Producto
    {
//        intModeloEquipoID int no	4	10   	0    	no(n/a)   (n/a)	NULL
//strModeloEquipoDesc varchar no	60	     	     	yes no  yes Modern_Spanish_CI_AS
//strModeloEquipoEstado char no	2	     	     	yes no  yes Modern_Spanish_CI_AS
//strModeloEquipoUsuCre varchar no	50	     	     	yes no  yes Modern_Spanish_CI_AS
//dteModeloEquipoFeCre datetime    no	8	     	     	yes(n/a)   (n/a)	NULL
//strModeloEquipoUsuModi  varchar no	50	     	     	yes no  yes Modern_Spanish_CI_AS
//dteModeloEquipoFeModi datetime    no	8	     	     	yes(n/a)   (n/a)	NULL
//strModeloEquipoUsuAnul  varchar no	50	     	     	yes no  yes Modern_Spanish_CI_AS
//dteModeloEquipoFeAnul datetime    no	8	     	     	yes(n/a)   (n/a)	NULL

        public int? intModeloEquipoID { get; set; }
        public string? strModeloEquipoDesc { get; set; }
        public string? strModeloEquipoEstado { get; set; }
        public string? strModeloEquipoUsuCre { get; set; }
        public string? dteModeloEquipoFeCre { get; set; }
        public string? strModeloEquipoUsuModi { get; set; }
        public string? dteModeloEquipoFeModi { get; set; }
        public string? strModeloEquipoUsuAnul { get; set; }
        public string? dteModeloEquipoFeAnul { get; set; }




    }
}