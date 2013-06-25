if (project == null) { var project = {}; }

project.currentID = function()
{
    return $('input[name="CurrentProject.ID"]').val()
}

project.new = function()
{
    if(project._current != null) { return; }
    Util.post('Project/New', null, function (html) {
        project._current = $(html);
        $('#main #center').append(project._current);
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
            if ($('#newMember').is(':visible')) { $('#newMember').hide(); }
            Util.post('Project/ListMembers', { projectID: project.currentID() },
               function (html) {
                   $('#memberSection #memberSectionBody').html($(html));
               });
            var listActors = '#projectListActors';
            if( $(listActors).length > 0 )
            {
                Util.replaceAction( listActors, 'Project/ListActors', { projectId : project.currentID() } );
            }
        });
    }
}

project.removeActor = function(container)
{
    var actorID = $('input[name="Actor.ID"]', container).val();

    Util.post('Project/RemoveActor', { projectID : project.currentID(), actorID : actorID },
    function(){
        $(container).remove();
    });
}

project.save = function(container)
{
    var data = Util.getParams(container);
    if( Util.isNull(data['CurrentProject.Name']) ) { Util.showError('input[name="CurrentProject.Name"]', 'error.isNull'); return; }
    Util.post('Project/Save', data, function()
    {
        var listProjects = '#projectList';
        if ( $(listProjects).length > 0 )
        {
            Util.replaceAction( listProjects, 'Project/ListProjects', {} );
            project.cancel(container);
        }
    });
}

project.cancel = function(container)
{
    project._current.remove();
    project._current = null;
}

project.view = function (projectID)
{
    if (project._current != null) {
        $('#projectView .body > section').not('.init').remove();
        $('#projectView .body > section.init').show();
    }

    Util.post('Project/View', { projectID : projectID }, function (html) {
        if (project._current != null) { project._current.remove(); }
        project._current = $(html);
        $('#main #center').append(project._current);
        $('#projectList li').removeClass('selected');
        $('#projectList li[projectID="'+projectID+'"]').addClass('selected');
    });
}

project.newMember = function (projectID) {
    $('#newMember').show();
};

project.viewMembers = function(projectID)
{
    var memberSection = $('#memberSection');
    
    if ($(memberSection).is(':visible'))
        memberSection.hide('slow');
    else {
        Util.post('Project/ListMembers', { projectID: projectID },
            function (html) {
                $('#memberSection #memberSectionBody').html($(html));
            });
        memberSection.show('slow');
    }
}

var audio = {
    current : null,
    new : function(projectID)
    {
        if(audio.current == null)
        {
            Util.post('Audio/New', { projectID : projectID }, 
                function(html){
                    audio.current = $(html);
                    $('body').append(audio.current);
                    $('#Audio_File').fileupload({
                        url: 'file/upload',
                        start: function () { alert('subiendo archivo'); },
                        done: function (e, data) {
                            $('#audioNew label').html('');
                            $('#audioNew #Audio_File').remove();
                            var fileID = $('<input>')
                                .attr('type', 'hidden')
                                .attr('name', 'ProjectID')
                                .addClass('important')
                                .attr('value', data.result.file.id);

                            $('#audioNew label').append(fileID);
                            $('#audioNew label').append($('<span>').text(data.result.file.name));
                        },
                        autoUpload: true
                    });
                });
        }
    },
    cancel: function () {
        audio.current.remove();
        audio.current = null;
    },
    append: function () {
        var fileID = $('#audioNew input[name=ProjectID]').val();
        var projectID = project.currentID();
        var audioName = $('#audioNew #Audio_Name').val();
        if (Util.isNull(fileID)) { alert(i18n.getString('error.fileNotfound')); }
        if (Util.isNull(audioName)) { alert(i18n.getString('error.audioNameNull')); }
        var data = $('#audioNew');
        Util.post('Audio/Append', { projectID: projectID, fileID: fileID, audioName: audioName },
            function (data) {
                if (!Util.isNull(data.ID))
                { alert(i18n.getString('message.audioAdded')); }
                audio.cancel();
            });
    },
    list: function () {
        var projectID = project.currentID();
    },
    remove: function (audioID) {
        Util.post('Audio/Remove', { audioID: audioID }, function () {
            $('.audio[audioID="' + audioID + '"]').parent().remove();
        });
    },
    play: function (audioID) {
        Util.post('Audio/Play', { audioID: audioID }, function (html) {
            $('#audioPlay').remove();
            $('body').append(html);
        });
    }
};




var projectItem = {
    current: null,
    view: function (projectID, type) {
        $('#projectView .body .init').hide();
        Util.post('projectItem/view', { projectID: projectID, type: type },
            function (html) {
                projectItem.current = $(html);
                $('#projectView .body').append(projectItem.current);
                projectItem.current.show();
            });
    },
    edit: function (projectID, itemName, parentName) {

    },
    newComment: function (projectID, type, name) {
        Util.post('projectItem/newComment', { projectID: projectID, type: type, name: name },
             function (html) {
                 $('body').append(html);
                 currentEditor = Util.editor.panelInstance('projectItemCommentData', { hasPanel: true });
             });
    },
    viewComment: function(projectID, type, name, datetime)
    {
        Util.post('projectItem/viewComment', { projectID: projectID, type: type, name: name, timeTicks: datetime },
            function (html)
            {
                $('body').append(html);
            });
    },
    closeComment : function()
    {
        $('#viewComment').remove();
    },
    addComment: function () {
        var form = Util.getParams('#projectItemNewComment');
        var data = {
            projectID : form["Project.ID"],
            name: form["Item.Name"],
            type: form["Type"]
        };
        data.message = nicEditors.findEditor('projectItemCommentData').getContent();
        Util.post('projectItem/addComment', data, function () {
            $('#projectItemNewComment').remove();
        });
    }
};