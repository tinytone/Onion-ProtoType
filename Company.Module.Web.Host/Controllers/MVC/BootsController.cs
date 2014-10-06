using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.Mvc;

using Company.Module.Domain;
using Company.Module.Domain.Boots;

namespace Company.Module.Web.Host.Controllers.MVC
{
    public class BootsController : Controller
    {
        //// ----------------------------------------------------------------------------------------------------------
		 
        // GET: Boots
        public ActionResult Index()
        {
            return View();
        }

        //// ----------------------------------------------------------------------------------------------------------

        // GET: /Boots/GetProductDetails?style=7177382
        public ContentResult GetProductDetails(int style)
        {
            var boots = GetBoots();
            
            var boot = boots.SingleOrDefault(b => b.Style == style);
            
            var bootDetails = boot == null ? "Boot Details not found!" : boot.ToHtmlString();

            return Content(bootDetails);
        }

        //// ----------------------------------------------------------------------------------------------------------
		 
        // GET: /Boots/GetColourOptions?style=7177382
        public ContentResult GetColourOptions(int style)
        {
            var boots = GetBoots();

            var boot = boots.SingleOrDefault(b => b.Style == style);

            if (boot == null)
                return Content("<option>No Colours available</option>");

            return Content(boot.GetHtmlColours());
        }
        
        //// ----------------------------------------------------------------------------------------------------------

        // GET: /Boots/GetSizeOptions?style=7177382&colour=black
        public ContentResult GetSizeOptions(int style, string colour)
        {
            var boots = GetBoots();

            var boot = boots.SingleOrDefault(b => b.Style == style);

            if (boot == null)
                return Content("<option>No sizes available</option>");

            return Content(boot.GetHtmlSizes(colour));
        }


        //// ----------------------------------------------------------------------------------------------------------
		 
        private IEnumerable<Boot> GetBoots()
        {
            // TODO: Move this to a repository! :P
            var boots = new List<Boot>
                            {
                                new Boot
                                    {
                                        Style = 7177382,
                                        Name = "Caterpillar Tradesman Work Boot",
                                        Sku = 7177382,
                                        Height = "6 inches",
                                        Lining = "Leather",
                                        BootColours = new List<BootColour> 
                                        { 
                                            new BootColour { Code = "hy", Colour = "Honey" },
                                            new BootColour { Code = "sd", Colour = "Peanut" }
                                        },
                                        BootSizes = new List<BootSize>
                                        {
                                            new BootSize { Code = "9d", Size = "9 D", ColourCode = "hy" },
                                            new BootSize { Code = "9ee", Size = "9 EE", ColourCode = "hy" },
                                            new BootSize { Code = "95d", Size = "9&frac12; D", ColourCode = "hy" },
                                            new BootSize { Code = "95ee", Size = "9&frac12; EE", ColourCode = "hy" },
                                            new BootSize { Code = "95eee", Size = "9&frac12; EEE", ColourCode = "hy" },
                                            new BootSize { Code = "10ee", Size = "10 EE", ColourCode = "hy" },
                                            new BootSize { Code = "10eee", Size = "10 EEE", ColourCode = "hy" },
                                            new BootSize { Code = "13e", Size = "13 E", ColourCode = "hy" },
                                            new BootSize { Code = "9d", Size = "9 D", ColourCode = "sd" },
                                            new BootSize { Code = "9ee", Size = "9 EE", ColourCode = "sd" },
                                            new BootSize { Code = "10eee", Size = "10 EEE", ColourCode = "sd" },
                                        },
                                        Price = 87.00M,
                                        Features =
                                            "<abbr>Full-grain</abbr> <abbr>oil-tanned</abbr> leather. Nylon mesh lining. Ortholite sock liner. EVA midsole. T84V Rubberlon outsole."
                                    },
                                new Boot
                                    {
                                        Style = 7269643,
                                        Name = "Caterpillar Logger Boot",
                                        Sku = 7269643,
                                        Height = "8 inches",
                                        Lining = "<abbr>Cambrelle</abbr>",
                                        BootColours = new List<BootColour> 
                                        { 
                                            new BootColour { Code = "bk", Colour = "Black" }
                                        },
                                        BootSizes = new List<BootSize>
                                        {
                                            new BootSize { Code = "9d", Size = "9 D", ColourCode = "bk" },
                                            new BootSize { Code = "9ee", Size = "9 EE", ColourCode = "bk" },
                                            new BootSize { Code = "95d", Size = "9&frac12; D", ColourCode = "bk" },
                                            new BootSize { Code = "95ee", Size = "9&frac12; EE", ColourCode = "bk" },
                                            new BootSize { Code = "95eee", Size = "9&frac12; EEE", ColourCode = "bk" }
                                        },                                        
                                        Price = 157.99M,
                                        Features =
                                            "<abbr>Full-grain</abbr> leather. <abbr>Cambrelle</abbr>&reg; lining. Steel safety toe. Electrical hazard protection. Poliyou&reg; cushion insole. Rubber lug outsole."
                                    },
                                new Boot
                                    {
                                        Style = 7141832,
                                        Name = "Chippewa 17-inch Engineer Boot",
                                        Sku = 7141832,
                                        Height = "17 inches",
                                        Lining = "Leather",
                                        BootColours = new List<BootColour> 
                                        { 
                                            new BootColour { Code = "bk", Colour = "Black Oil-tanned" },
                                            new BootColour { Code = "br", Colour = "Black Polishable" }
                                        },
                                        BootSizes = new List<BootSize>
                                        {
                                            new BootSize { Code = "9d", Size = "9 D", ColourCode = "bk" },
                                            new BootSize { Code = "9ee", Size = "9 EE", ColourCode = "bk" },
                                            new BootSize { Code = "10ee", Size = "10 EE", ColourCode = "bk" },
                                            new BootSize { Code = "10eee", Size = "10 EEE", ColourCode = "bk" },
                                            new BootSize { Code = "13e", Size = "13 E", ColourCode = "bk" },
                                            new BootSize { Code = "9d", Size = "9 D", ColourCode = "br" },
                                            new BootSize { Code = "9ee", Size = "9 EE", ColourCode = "br" },
                                            new BootSize { Code = "10eee", Size = "10 EEE", ColourCode = "br" },
                                        },
                                        Price = 187.00M,
                                        Features =
                                            "<abbr>Oil-tanned</abbr> or polishable <abbr>full-grain</abbr> leather uppers. <abbr>Vibram</abbr> sole. <abbr>Goodyear welt</abbr>. Padded insole. Steel safety toe."
                                    },
                                new Boot
                                    {
                                        Style = 7173656,
                                        Name = "Chippewa 11-inch Engineer Boot",
                                        Sku = 7173656,
                                        Height = "11 inches",
                                        Lining = "None",
                                        BootColours = new List<BootColour> 
                                        { 
                                            new BootColour { Code = "bk", Colour = "Black" },
                                            new BootColour { Code = "br", Colour = "Crazy Horse" }
                                        },
                                        BootSizes = new List<BootSize>
                                        {
                                            new BootSize { Code = "9d", Size = "9 D", ColourCode = "bk" },
                                            new BootSize { Code = "9ee", Size = "9 EE", ColourCode = "bk" },
                                            new BootSize { Code = "95d", Size = "9&frac12; D", ColourCode = "bk" },
                                            new BootSize { Code = "10ee", Size = "10 EE", ColourCode = "bk" },
                                            new BootSize { Code = "10eee", Size = "10 EEE", ColourCode = "bk" },
                                            new BootSize { Code = "13e", Size = "13 E", ColourCode = "bk" },
                                            new BootSize { Code = "9d", Size = "9 D", ColourCode = "br" },
                                            new BootSize { Code = "9ee", Size = "9 EE", ColourCode = "br" },
                                            new BootSize { Code = "10eee", Size = "10 EEE", ColourCode = "br" },
                                        },
                                        Price = 167.00M,
                                        Features =
                                            "<abbr>Oil-tanned</abbr> <abbr>full-grain</abbr> leather uppers. <abbr>Vibram</abbr> sole. <abbr>Goodyear welt</abbr>. Padded insole. Steel safety toe. Texon&reg; insole."
                                    },
                                new Boot
                                    {
                                        Style = 7141922,
                                        Name = "Chippewa Harness Boot",
                                        Sku = 7141922,
                                        Height = "13 inches",
                                        Lining = "Leather",
                                        BootColours = new List<BootColour> 
                                        { 
                                            new BootColour { Code = "bk", Colour = "Black" },
                                            new BootColour { Code = "br", Colour = "Crazy Horse" }
                                        },
                                        BootSizes = new List<BootSize>
                                        {
                                            new BootSize { Code = "9d", Size = "9 D", ColourCode = "bk" },
                                            new BootSize { Code = "95d", Size = "9&frac12; D", ColourCode = "bk" },
                                            new BootSize { Code = "13e", Size = "13 E", ColourCode = "bk" },
                                            new BootSize { Code = "9d", Size = "9 D", ColourCode = "br" },
                                            new BootSize { Code = "9ee", Size = "9 EE", ColourCode = "br" },
                                            new BootSize { Code = "10eee", Size = "10 EEE", ColourCode = "br" },
                                        },
                                        Price = 199.99M,
                                        Features = "<abbr>Full-grain</abbr> leather uppers. Leather lining. <abbr>Vibram</abbr> sole. <abbr>Goodyear welt</abbr>."
                                    }
                            };
            return boots;
        }

        //// ----------------------------------------------------------------------------------------------------------
    }
}