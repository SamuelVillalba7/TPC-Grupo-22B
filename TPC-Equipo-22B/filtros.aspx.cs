using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using dominio;
using negocio;

namespace TPC_Equipo_22B
{
    public partial class filtros : System.Web.UI.Page
    {
        public List<Articulo> ListaArticulo { get; set; }
        public List<Categoria> ListaCategoria { get; set; }
        public Categoria categoria { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
            ArticuloNegocio negocio = new ArticuloNegocio();
            CategoriaNegocio cnegocio = new CategoriaNegocio();

            if (!IsPostBack)
            {
                ListaArticulo = negocio.listarConSP();
            }
            else
            {
                ListaArticulo = negocio.listaFiltrandoCategoria();
            }

            
            Session.Add("listaArticulos", ListaArticulo);
            ListaCategoria = cnegocio.listarConSP();
            Session.Add("listaCategoria", ListaCategoria);

            CblCategorias.DataSource = ListaCategoria;
            CblCategorias.DataTextField = "Nombre";
            CblCategorias.DataValueField = "Id";

            CblCategorias.DataBind();
           

        }

        protected void filtroRapido_TextChanged(object sender, EventArgs e)
        {
            List<Articulo> lista = (List <Articulo>) Session["listaArticulos"];
            List<Articulo> listaFiltrada = lista.FindAll(x => x.Nombre.ToUpper().Contains(filtroRapido.Text.ToUpper()));
            ListaArticulo = listaFiltrada;
        }

        protected void btnFiltrar_Click(object sender, EventArgs e)
        {
            //Revisar por qué no me está tildando las categorias de la colección que están tildadas
            
            foreach (ListItem lista in CblCategorias.Items)
            {
                //if (lista.Selected)
                //{
                    //Categoria cat = new Categoria();
                    //cat.Id = int.Parse(lista.Value);
                    //cat.Nombre = lista.Text;
                    //cat.filtro = true;

                    //CategoriaNegocio negocio = new CategoriaNegocio();
                    //negocio.modificarFiltro(cat);

                //}
                //else
                //{
                Categoria cat = new Categoria();
                cat.Id = int.Parse(lista.Value);
                cat.Nombre = lista.Text;
                cat.filtro = false;

                CategoriaNegocio negocio = new CategoriaNegocio();
                negocio.modificarFiltro(cat);
                //}
            }
        }

        protected void CblCategorias_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        //protected void chkCategorias_ChekedChanged(object sender, EventArgs e)
        //{
        //    ArticuloNegocio negocio = new ArticuloNegocio();
        //    CategoriaNegocio cnegocio = new CategoriaNegocio();
        //    cnegocio.modificarFiltro(categoria);
        //    ListaArticulo = negocio.listaFiltrandoCategoria();

        //}
    }
}