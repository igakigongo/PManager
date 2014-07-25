app.controller("TeamsController", function ($scope, filterFilter, $window, $modal, teamsService, projectService, taskService) {

    var pathname = window.location.pathname;
    var controller = pathname.split("/")[1];
    var action = pathname.split("/")[2];
    var params = pathname.split("/")[3];

    $scope.listOfSelectedUsers = [];
    $scope.projectSelected = false;

    $scope.loadProjectTasks = function (id) {

        if (id!='') {
            taskService.getProjectTasks(id).success(function (tasks) {
                $scope.projectSelected = true;
                $scope.tasks = tasks;
            }).error();
        } else {
            $scope.projectSelected = false;
            angular.element('#task').empty().html('<option value="">-- Select Task --</option>');
        }
    }

    projectService.getAllProjects().success(function (projects) {
        $scope.projects = projects;
    }).error();

    teamsService.getAllTeams().success(function (teams) {
        $scope.teams = teams;
    }).error();

    teamsService.getAllUsers().success(function (users) {
        $scope.users = users;

    }).error();

    $scope.save = function () {

        var newTeam = {
            TaskId:$scope.selectedTask,
            Name: $scope.teamName,
            UserIds: $scope.listOfSelectedUsers
        };
        
        teamsService.createNewTeam(newTeam).success(function (response) {

            toastr.success(newTeam.Name + ' has been created successfully');
            $scope.teamForm.$setPristine();
            $scope.teamName = '';
            $scope.listOfSelectedUsers = [];

            setTimeout(function () {
                window.location.href = "/Teams/Index";
            }, 4000);

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

app.controller("TasksController", function ($scope, taskService, projectService) {
    $('#EstimatedStartDate').datepicker({
        format: 'dd/mm/yyyy'
    });
    $('#EstimatedEndDate').datepicker({
        format: 'dd/mm/yyyy'
    });

    $scope.Task = {};

    projectService.getAllProjects().success(function (projects) {
        $scope.projects = projects;
    }).error();

    $scope.saveTask = function () {

        taskService.saveNewTask($scope.Task).success(function (response) {
            if (response == "true") {
                toastr.success("Task has been created successfully");
            } else {
                toastr.error("An error occurred while saving the task, please contact your system's administrator for help");
            }
        }).error();

        $scope.teamForm.$setPristine();
        $scope.Task = {};
    }

});

app.controller('resourceController', function ($scope, $modal) {

    $scope.assignResourceToProject = function (item) {
        var format = item.split(',');

        var resource = {
            Id: format[0].trim(),
            Name: format[1].trim(),
            Model: format[2] == null ? "" : format[2].trim()
        };

        var modalInstance = $modal.open({
            templateUrl: 'dialogModal.html',
            controller: 'dialogController',
            resolve: {
                item: function () {
                    return resource;
                }
            }
        });
    };
});


var dialogController = function ($scope, $modalInstance, item, resourceService, projectService) {

    var pathname = window.location.pathname;
    var controller = pathname.split("/")[1];
    var action = pathname.split("/")[2];
    var params = pathname.split("/")[3];

    console.log(action);

    $scope.Project = {}

    projectService.getAllProjects().success(function (projects) {
        $scope.projects = projects;
    }).error();

    $scope.displayName = item.Model + ", " + item.Name;

    $scope.ok = function () {
        $scope.resourceToAssign = {
            resourceId: item.Id,
            ProjectId: $scope.Project.ProjectId
        };

        if (action == 'Laptops') {
            resourceService.AssignLaptopToProject($scope.resourceToAssign).success(function (response) {

                $modalInstance.close();

                if (JSON.stringify(response.Status) == "true") {
                    toastr.success(response.Text);
                    setTimeout(function () {
                        window.location.href = "/ProjectResource/Laptops";
                    }, 4000);
                } else {
                    toastr.error(response.Text);
                }

            }).error();

            return;
        }

        if (action == 'Vehicles') {
            resourceService.AssignVehicleToProject($scope.resourceToAssign).success(function (response) {

                $modalInstance.close();

                if (JSON.stringify(response.Status) == "true") {
                    toastr.success(response.Text);
                    setTimeout(function () {
                        window.location.href = "/ProjectResource/Vehicles";
                    }, 4000);
                } else {
                    toastr.error(response.Text);
                }

            }).error();

            return;
        }
    };

    $scope.cancel = function () {
        $modalInstance.dismiss('cancel');
    };
};




