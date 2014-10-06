using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Company.Module.Domain.Boots
{
    public class Boot
    {
        //// ----------------------------------------------------------------------------------------------------------

        public int Style { get; set; }

        //// ----------------------------------------------------------------------------------------------------------

        public string Name { get; set; }

        //// ----------------------------------------------------------------------------------------------------------

        public int Sku { get; set; }

        //// ----------------------------------------------------------------------------------------------------------

        public string Height { get; set; }

        //// ----------------------------------------------------------------------------------------------------------

        public string Lining { get; set; }

        //// ----------------------------------------------------------------------------------------------------------

        public IEnumerable<BootColour> BootColours { get; set; }

        //// ----------------------------------------------------------------------------------------------------------

        public IEnumerable<BootSize> BootSizes { get; set; }

        //// ----------------------------------------------------------------------------------------------------------

        public decimal Price { get; set; }

        //// ----------------------------------------------------------------------------------------------------------

        public string Features { get; set; }

        //// ----------------------------------------------------------------------------------------------------------

        public string GetHtmlColours()
        {
            var sb = new StringBuilder();
            sb.AppendLine("<option value=''>&mdash; choose colour &mdash;</option>");

            foreach (var bootColour in this.BootColours) 
                sb.AppendFormat("<option value='{0}'>{1}</option>{2}", bootColour.Code, bootColour.Colour, Environment.NewLine);

            return sb.ToString();
        }

        //// ----------------------------------------------------------------------------------------------------------

        public string GetHtmlSizes(string colourCode)
        {
            var sb = new StringBuilder();
            sb.AppendLine("<option value=''>&mdash; choose size &mdash;</option>");

            foreach (var bootSize in this.BootSizes.Where(bs => bs.ColourCode == colourCode))
                sb.AppendFormat("<option value='{0}'>{1}</option>{2}", bootSize.Code, bootSize.Size, Environment.NewLine);

            return sb.ToString();
        }
        
        //// ----------------------------------------------------------------------------------------------------------

        public string ToHtmlString()
        {
            return String.Format(
@"<div>
  <label>Item name:</label> {0}
</div>
<div>
  <label>SKU:</label> {1}
</div>
<div>
  <label>Height:</label> {2}
</div>
<div>
  <label>Colours:</label> {3}
</div>
<div>
  <label>Lining:</label> {4}
</div>
<div>
  <label>Today's price:</label> {5}
</div>
<div>
  <label>Features:</label> {6}
</div>
<div align=""center"">
    <img id=""itemPhoto"" src=""/Content/Images/boots/{1}.png""/>
</div>", 
       this.Name, 
       this.Sku,
       this.Height,
       String.Join(", ", this.BootColours),
       this.Lining,
       this.Price,
       this.Features);
        }


        //// ----------------------------------------------------------------------------------------------------------
    }
}
