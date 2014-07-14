app.controller("TeamsController", function ($scope, filterFilter, $window, $modal, teamsService) {

    var pathname = window.location.pathname;
    var controller = pathname.split("/")[1];
    var action = pathname.split("/")[2];
    var params = pathname.split("/")[3];

    $scope.listOfSelectedUsers = [];

    teamsService.getAllTeams().success(function (teams) {
        $scope.teams = teams;
    }).error();

    teamsService.getAllUsers().success(function (users) {
        $scope.users = users;

    }).error();

    $scope.save = function () {

        var newTeam = {
            //Id:0,
            Name: $scope.teamName,
            UserIds: $scope.listOfSelectedUsers
        };


        teamsService.createNewTeam(newTeam).success(function (response) {

            toastr.success(newTeam.Name + ' has been created successfully');
            $scope.teamForm.$setPristine();
            $scope.teamName = '';
            $scope.listOfSelectedUsers = [];

        }).error();
    }

    $scope.team = {
        text: "",
        name: $scope.name
    };

    /// open the authentication dialog
    $scope.delete = function (team) {
        var format = team.split(',');



        $scope.team = {
            Name: format[0],
            Id: format[1]
        };

        var modalInstance = $modal.open({
            templateUrl: 'myModalContent.html',
            controller: authenticationController,
            resolve: {
                item: function () {
                    return $scope.team;
                }
            }
        });

    };

    teamsService.getTeamToEdit(params).success(function (team) {
        $scope.teamToEdit = team;
    }).error();


    $scope.editTeam = function (teamToEdit) {

        var editedTeam = {
            Id: teamToEdit.Id,
            Name: teamToEdit.Name,
            UserIds: $scope.listOfSelectedUsers
        };

        teamsService.editTeam(editedTeam).success(function (response) {
            if (response == 'true') {
                toastr.success("Your changes have been saved");
            } else {
                toastr.error("An error occurred while trying to save the changes, please contact your systems administrator for help ");
            }

            $scope.teamForm.$setPristine();
            $scope.listOfSelectedUsers = [];

            setTimeout(function () {
                window.location.href = "/Teams/Edit/" + params + "";
            }, 4000);


        }).error();
    }


});

app.controller("TasksController", function ($scope, taskService) {
    $('#EstimatedStartDate').datepicker({
        format:'dd/mm/yyyy'
    });
    $('#EstimatedEndDate').datepicker({
        format: 'dd/mm/yyyy'
    });

    $scope.Task = {};

    taskService.getAllProjects().success(function (projects) {
        $scope.projects = projects;
    }).error();

    $scope.saveTask = function () {

        taskService.saveNewTask($scope.Task).success(function (response) {
            if (response=="true") {
                toastr.success("Task has been created successfully");
            } else {
                toastr.error("An error occurred while saving the task, please contact your system's administrator for help");
            }
        }).error();

        $scope.teamForm.$setPristine();
        $scope.Task = {};
    }

});

var authenticationController = function ($scope, $modalInstance, item, teamsService) {

    // model to hold user password and comment
    $scope.teamToDelete = item;

    $scope.ok = function () {
        teamsService.deleteTeam($scope.teamToDelete).success(function (response) {
            if (response == 'true') {
                toastr.success("Team has been deleted successfully");
            } else {
                toastr.success("Error has occurred while trying to delete the team, please contact the systems administrator for help");
            }

            $modalInstance.close();
        }).error();

        setTimeout(function () {
            window.location.href = "/Teams/Index";
        }, 4000);
    };

    $scope.cancel = function () {
        $modalInstance.dismiss('cancel');
    };
};




