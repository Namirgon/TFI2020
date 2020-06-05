﻿using EcommerceHelper.BLL;
using EcommerceHelper.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EcommerceHelper.Presentacion.Views.Private
{
    public partial class AltaEmpleado : System.Web.UI.Page
    {
        private SexoBLL unManagerSexo = new SexoBLL();
        public List<SexoEntidad> unSexo = new List<SexoEntidad>();


        private TipoDomicilioBLL unManagerTipoDomicilio = new TipoDomicilioBLL();
        public List<TipoDireccionEntidad> unTipoDireccion = new List<TipoDireccionEntidad>();


        private TipoTelefonoBLL unManagerTipoTelefono = new TipoTelefonoBLL();
        public List<TipoTelefonoEntidad> unTipoTelefono = new List<TipoTelefonoEntidad>();

        public UsuarioEntidad unUsuario = new UsuarioEntidad();

        public DireccionEntidad UnaDireccion = new DireccionEntidad();
        private DireccionBLL UnManagerDireccion = new DireccionBLL();
        private UsuarioBLL unManagerUsuario = new UsuarioBLL();

        //private UsuarioBLL unManagerUsuario = new UsuarioBLL();
        public List<ProvinciaEntidad> unasProvincias = new List<ProvinciaEntidad>();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                cargarSexo();
                CargarTipoTelefono();
                cargarProvincias();
                cargarLocalidades();
                cargarTipodeDireccion();
            }
        }

            public void cargarProvincias(int? elIndice = null)
            {
                ddProvincia.DataSource = null;
                unasProvincias = unManagerUsuario.SelectALLProvincias();
                ddProvincia.DataSource = unasProvincias;
                ddProvincia.DataValueField = "IdProvincia";
                ddProvincia.DataTextField = "Descripcion";
                ddProvincia.DataBind();
                if (elIndice != null)
                    ddProvincia.SelectedIndex = (int)elIndice;
            }

            public void cargarLocalidades()
            {
                ddLocalidad.DataSource = null;
                ddLocalidad.DataSource = unasProvincias.Find(X => X.IdProvincia == (Int32.Parse(ddProvincia.SelectedValue))).misLocalidades;
                ddLocalidad.DataValueField = "IdLocalidad";
                ddLocalidad.DataTextField = "Descripcion";
                ddLocalidad.DataBind();
            }

            protected void ddProvincia_SelectedIndexChanged(object sender, EventArgs e)
            {

                int aux = Int32.Parse(ddProvincia.SelectedValue);
                aux--;
                cargarProvincias(aux);
                cargarLocalidades();


            }

            public void cargarSexo()
            {
                ddSexo.DataSource = null;
                unSexo = unManagerSexo.SelectALLSexos();
                ddSexo.DataSource = unSexo;
                ddSexo.DataValueField = "IdSexo";
                ddSexo.DataTextField = "Descripcion";
                ddSexo.DataBind();

            }

            public void cargarTipodeDireccion()
            {
                DDLTipodeDireccion.DataSource = null;
                unTipoDireccion = unManagerTipoDomicilio.SelectALLTipoDomicilio();
                DDLTipodeDireccion.DataSource = unTipoDireccion;
                DDLTipodeDireccion.DataValueField = "IdTipoDireccion";
                DDLTipodeDireccion.DataTextField = "Descripcion";
                DDLTipodeDireccion.DataBind();

            }

            public void CargarTipoTelefono()
            {
                ddTipoTelefono.DataSource = null;
                unTipoTelefono = unManagerTipoTelefono.SelectALLTipoTelefono();
                ddTipoTelefono.DataSource = unTipoTelefono;
                ddTipoTelefono.DataValueField = "IdTipoTelefono";
                ddTipoTelefono.DataTextField = "Descripcion";
                ddTipoTelefono.DataBind();

            }

        protected void BtnContinuar_Click(object sender, EventArgs e)
        {
            var NroUsuario = 0;
            if (Page.IsValid)

            {
                unUsuario.IdUsuarioTipo = 4; // Usuario Cliente 
                unUsuario.Email = txtusuario.Text;
                unUsuario.Password = txtcontrasena.Text;
                unUsuario.Nombre = txtNombre.Text;
                unUsuario.Apellido = txtApellido.Text;
                unUsuario.MiSexo = new SexoEntidad();
                unUsuario.MiSexo.IdSexo = Int32.Parse(ddSexo.SelectedValue);
                unUsuario.NumeroDocumento = Int32.Parse(txtDNI.Text);
                unUsuario.MiTelefono = new TipoTelefonoEntidad();
                unUsuario.MiTelefono.IdTipoTelefono = Int32.Parse(ddTipoTelefono.SelectedValue);
                unUsuario.NumeroTelefono = Int32.Parse(txtTelefono.Text); //Int32.Parse(txtTelefono.Text);   


                NroUsuario = unManagerUsuario.RegistrarUsuario(unUsuario);

                //Direccion
                UnaDireccion.Calle = txtCalle.Text;
                UnaDireccion.Numero = Int32.Parse(txtNumero.Text);
                UnaDireccion.Piso = txtPiso.Text;
                UnaDireccion.Departamento = txtDepartamento.Text;
                UnaDireccion.MiProvincia = new ProvinciaEntidad();
                UnaDireccion.MiProvincia.IdProvincia = Int32.Parse(ddProvincia.SelectedValue);
                UnaDireccion.MiLocalidad = new LocalidadEntidad();
                UnaDireccion.MiLocalidad.IdLocalidad = Int32.Parse(ddLocalidad.SelectedValue);
                UnaDireccion.MiTipoDireccion = new TipoDireccionEntidad();
                UnaDireccion.MiTipoDireccion.IdTipoDireccion = Int32.Parse(DDLTipodeDireccion.SelectedValue);

                unManagerUsuario.InsertDireccionDeFacturacion(UnaDireccion, unUsuario);

                limpiarCampos();
            }
            else
            {

                Exception ex;
            }
        }
        public void limpiarCampos()
        {
            txtNombre.Text = string.Empty;
            txtApellido.Text = string.Empty;
            txtDNI.Text = string.Empty;
            txtcontrasena.Text = string.Empty;
            ddLocalidad.SelectedIndex = 0;
            ddProvincia.SelectedIndex = 0;
            ddSexo.SelectedIndex = 0;
            ddTipoTelefono.SelectedIndex = 0;
            txtTelefono.Text = string.Empty;
            txtCalle.Text = string.Empty;
            txtNumero.Text = string.Empty;
            txtPiso.Text = string.Empty;
            txtDepartamento.Text = string.Empty;
            txtusuario.Text = string.Empty;
            txtrepetircontrasena.Text = string.Empty;
        }


    }


}
