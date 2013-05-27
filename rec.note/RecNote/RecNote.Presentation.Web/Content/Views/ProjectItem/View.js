var ProjectItem = {
    current: null,
    view : function(projectID, itemName, parentName)
    {
        $('#projectView .body .option').hide();
        Util.post('projectItem', { projectID: projectID, itemName: itemName, parentName: parentName },
            function (html) {
                ProjectItem.current = $(html);
                ProjectItem.current.show();
            });
    },
    edit : function(projectID, itemName, parentName)
    {
        
    }
};