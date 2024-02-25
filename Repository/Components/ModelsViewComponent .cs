using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Project_PhoneStore.Repository
{
	public class ModelsViewComponent : ViewComponent
	{
		private readonly DataContext _dataContext;
		public ModelsViewComponent(DataContext Context)
		{
			_dataContext = Context;
		}
		public async Task<IViewComponentResult> InvokeAsync() => View(await _dataContext.Models.ToListAsync());
	}
}
