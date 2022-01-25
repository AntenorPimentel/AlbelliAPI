using System.ComponentModel;

namespace AlbelliAPI.Business.Models
{
    public class Enums
    {
        public enum ProductTypes
        {
            [Description("19mm")]
            PhotoBook = 1,
            [Description("10mm")]
            Calendar = 2,
            [Description("16mm")]
            Canvas = 3,
            [Description("4.7mm")]
            SetOfGreetingCards = 4,
            [Description("94mm")]
            Mug = 5
        }
    }
}