using Microsoft.AspNetCore.Mvc;

namespace Football_Insight.Components
{
    public class FooterComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            return await Task.FromResult<IViewComponentResult>(View());
        }
    }
}
