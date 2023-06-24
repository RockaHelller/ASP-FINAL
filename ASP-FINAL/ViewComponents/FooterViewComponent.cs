﻿using ASP_FINAL.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ASP_FINAL.ViewComponents
{
    public class FooterViewComponent : ViewComponent
    {
        private readonly ILayoutService _layoutService;
        public FooterViewComponent(ILayoutService layoutService)
        {
            _layoutService = layoutService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var datas = await _layoutService.GetAllDatas();
            return await Task.FromResult(View(datas));
        }
    }
}
