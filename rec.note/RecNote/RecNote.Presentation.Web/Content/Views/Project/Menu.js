if (project == null) { var project = {}; }

project.currentID = function()
{
    return $('input[name="CurrentProject.ID"]').val()
}

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
    if( !Util.isEmail( param["User.Email"] ) )
    {
        isValid = false;
        Util.showError(id + ' input[name="User.Email"]', 'error.emailInvalid');
    }

    if ( Util.isNull( param["User.Name"] ) )
    {
        isValid = false;
        Util.showError(id + ' input[name="User.Name"]', 'error.isNull');
    }

    if ( Util.isNull( param["User.Role"] ) )
    {
        isValid = false;
        Util.showError(id + ' input[name="User.Role"]', 'error.isNull');
    }

    if(isValid)
    {
        param["ProjectID"] = project.currentID();
        Util.post('Project/SaveActor', param, function (){
            Util.clear(id);
            var listActors = '#projectListActors';
            if( $(listActors).length > 0 )
            {
                Util.replaceAction( listActors, 'Project/ListActors', { projectId : project.currentID() } );
            }
        });
    }
}