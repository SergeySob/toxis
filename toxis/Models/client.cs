using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace toxis.Models
{
    public class client
    {
        [Display(Name = "Введите Фамилию")]
        [Required(ErrorMessage = "Обязательное поле")]
        public string secondName { get; set; }

        [Display(Name = "Введите имя")]
        [Required(ErrorMessage = "Обязательное поле")]
        public string firstName { get; set; }

        [Display(Name = "Введите отчество")]
        [Required(ErrorMessage = "Обязательное поле")]
        public string surname { get; set; }

        [Display(Name = "Введите адрес электронной почты")]
        [Required(ErrorMessage = "Обязательное поле")]
        public string email { get; set; }

        [Display(Name = "Выберите зону билета")]
        [Required(ErrorMessage = "Обязательное поле")]
        public string SelectedZone { get; set; }

        public List<SelectListItem> Zones { get; set; } = new List<SelectListItem>
        {
            new SelectListItem { Value = "Танцпол", Text = "Танцпол" },
            new SelectListItem { Value = "Нижний ярус", Text = "Нижний ярус" },
            new SelectListItem { Value = "Средний ярус", Text = "Средний ярус" },
            new SelectListItem { Value = "Балконы", Text = "Балконы" }
        };
    }
}
