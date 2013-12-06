$(document).ready(function()
{
    $('header .close').click(function () {
        Util.post('Home/Close', null, function () { Util.redirect(''); });
    });
})