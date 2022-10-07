using SAExpiations.Models;

namespace SAExpiations.ViewModels
{
    public class LocalServiceVM
    {
        public List<int> Years { set; get; }
        public List<LocalAreaVM> LocalArea { get; set; }
        public int SelectedYear { get; set; }
        
    }
}
