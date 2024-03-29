﻿using System;
using System.Collections.Generic;
using Basket.Models;
using Basket.Models.Repository;
using System.Linq;

namespace Basket.Pages
{
    public partial class Listing : System.Web.UI.Page
    {
        private Repository repository = new Repository();
        // private int pageSize = 4;

        protected int CurrentPage
        {
            get
            {
                int page;
                page = int.TryParse(Request.QueryString["page"], out page) ? page : 1;
                return page > MaxPage ? MaxPage : page;
            }
        }

        // Новое свойство, возвращающее наибольший номер допустимой страницы
        protected int MaxPage
        {
            get
            {
                return 0;
                // return (int)Math.Ceiling((decimal)repository.Games.Count() / pageSize);
            }
        }

        // protected IEnumerable<Game> GetGames()
        // {
            // return repository.Games
            //     .OrderBy(g => g.GameId)
            //     .Skip((CurrentPage - 1) * pageSize)
            //     .Take(pageSize);
        // }

        protected void Page_Load(object sender, EventArgs e)
        {

        }
    }
}