if (project == null) { var project = {}; }

project.new = function()
{
    if(project._new != null) { return; }
    Util.post('Project/New', null, function (html) {
        project._new = $(html);
        $('#main #center').append(project._new);
    });
};

project.saveNewActor = function()
{
    var id = '#projectNewActor';
    var param = Util.getParams(id);
    var isValid = true;
    if( !Util.isEmail( param.Email ) )
    {
        isValid = false;
        Util.showError(id + ' input[name="Email"]', 'error.emailInvalid');
    }

    if ( Util.isNull( param.Name ) )
    {
        isValid = false;
        Util.showError(id + ' input[name="Name"]', 'error.isNull');
    }

    if ( Util.isNull( param.Role ) )
    {
        isValid = false;
        Util.showError(id + ' input[name="Role"]', 'error.isNull');
    }

    if(isValid)
    {
        Util.post('Project/SaveActor', param, function (){
            Util.clear(id);
        });
    }
}