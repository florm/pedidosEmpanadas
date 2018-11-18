using App.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace App
{
    public class PedidoTemplateSelector : DataTemplateSelector
    {
        public DataTemplate PedidoAbiertoTemplate { get; set; }
        public DataTemplate PedidoCerradoTemplate { get; set; }
        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            return ((PedidosVm)item).Estado.Equals(1) ? PedidoAbiertoTemplate : PedidoCerradoTemplate;
        }
    }
}
