if (project == null) { var project = {}; }

project.new = function()
{
    $('#main #center').append($('<section>'+(new Date()).toLocaleTimeString()+'</section>'));
};