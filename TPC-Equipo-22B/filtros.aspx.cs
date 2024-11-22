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
        public List<Marca> ListaMarca { get; set; }
        public Categoria categoria { get; set; }

        ArticuloNegocio negocio = new ArticuloNegocio();
        CategoriaNegocio cnegocio = new CategoriaNegocio();
        MarcaNegocio mnegocio = new MarcaNegocio();
        protected void Page_Load(object sender, EventArgs e)
        {
            ListaArticulo = negocio.listarConSP();
            Session.Add("listaArticulos", ListaArticulo);
            ListaCategoria = cnegocio.listarConSP();
            ListaMarca = mnegocio.listar();
            Session.Add("listaCategoria", ListaCategoria);

            if (!IsPostBack)
            {
                ddlCategoria.DataSource = ListaCategoria;
                ddlCategoria.DataTextField = "Nombre";
                ddlCategoria.DataValueField = "Id";
                ddlCategoria.DataBind();
                ddlCategoria.Items.Insert(0, new ListItem("Todas", ""));
                ddlCategoria.SelectedIndex = 0;

                ddlMarca.DataSource = ListaMarca;
                ddlMarca.DataTextField = "Nombre";
                ddlMarca.DataValueField = "Codigo";
                ddlMarca.DataBind();
                ddlMarca.Items.Insert(0, new ListItem("Todas", ""));
                ddlMarca.SelectedIndex = 0;

                
                string categoriaIdParam = Request.QueryString["categoriaId"];
                if (!string.IsNullOrEmpty(categoriaIdParam) && int.TryParse(categoriaIdParam, out int categoriaId))
                {
                    
                    FiltrarPorCategoria(categoriaId);
                }
            }
        }

        private void FiltrarPorCategoria(int categoriaId)
        {
            ddlCategoria.SelectedValue = categoriaId.ToString(); 
            ListaArticulo = negocio.listarConSP(-1, categoriaId);                                                             
        }



        protected void filtroRapido_TextChanged(object sender, EventArgs e)
        {
            List<Articulo> lista = (List<Articulo>)Session["listaArticulos"];
            List<Articulo> listaFiltrada = lista.FindAll(x => x.Nombre.ToUpper().Contains(filtroRapido.Text.ToUpper()));
            ListaArticulo = listaFiltrada;
        }

        protected void FiltrarProductos(object sender, EventArgs e)
        {
            int marcaSeleccionada;
            int categoriaSeleccionada;

            if (ddlMarca.SelectedIndex == 0)
            {
                marcaSeleccionada = -1;
            }
            else
            {
                marcaSeleccionada = ddlMarca.SelectedIndex;
            }

            if (ddlCategoria.SelectedIndex == 0)
            {
                categoriaSeleccionada = -1;
            }
            else
            {
                categoriaSeleccionada = ddlCategoria.SelectedIndex;
            }

            if (marcaSeleccionada == -1 && categoriaSeleccionada == -1)
            {
                ListaArticulo = negocio.listarConSP();
            }
            else
            {
                ListaArticulo = negocio.listarConSP(marcaSeleccionada, categoriaSeleccionada);
            }
        }

    }
}