using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TIS.Core.Entities.Mariage
{
    public class TableMariage
    {
        #region Proprietes
        public int? Id { get; set; }
        public string? Nom { get; set; }
        public string? Couleur { get; set; }
        public string? PositionTop { get; set; }
        public string? PositionLeft { get; set; }

        #endregion
    }
}
