<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Listing.aspx.cs" Inherits="Basket.Pages.Listing "%>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>GameStore</title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <%
                foreach(Basket.Models.Game game in GetGames())
                {
                    Response.Write(String.Format(@"
                        <div class='item'>
                            <h3>{0}</h3>
                            {1}
                            <h4>{2:c}</h4>
<button name='add' type='submit' value='{3}'>
                                Добавить в корзину
                            </button>
                        </div>",
                        game.Name, game.Description, game.Price, game.GameId));
                }
            %>
        </div>
    </form>
    <div>
        <%
            for (int i = 1; i <= MaxPage; i++)
            {
                Response.Write(
                    String.Format("<a href='/Pages/Listing.aspx?page={0}' {1}>{2}</a>",
                        i, i == CurrentPage ? "class='selected'" : "", i));
            }
        %>
    </div>
</body>
</html>
